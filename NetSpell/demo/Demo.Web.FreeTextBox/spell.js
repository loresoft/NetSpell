/****************************************************
* Spell Checker Client JavaScript Code
****************************************************/
// spell checker constants
var spellURL = "SpellCheck.aspx";
var showCompleteAlert = true;


var tagGroup = new Array("INPUT", "TEXTAREA", "DIV", "SPAN");
// global elements to check
var checkElements = new Array();

function getText(index)
{
    var oElement = document.getElementById(checkElements[index]);
    var sText = "";

    switch (oElement.tagName)
	{
		case "INPUT" :
        case "TEXTAREA" :
			sText = oElement.value;
			break;
		case "DIV" :
		case "SPAN" :
        case "BODY" :
			sText = oElement.innerHTML;
			break;
        case "IFRAME" :
            var oFrame = eval(oElement.id);
            sText = oFrame.document.body.innerHTML;
    }

    return sText;
}

function setText(index, text)
{
    var oElement = document.getElementById(checkElements[index]);

    switch (oElement.tagName)
	{
		case "INPUT" :
        case "TEXTAREA" :
			oElement.value = text;
			break;
		case "DIV" :
		case "SPAN" :
			oElement.innerHTML = text;
			break;
        case "IFRAME" :
            var oFrame = eval(oElement.id);
            oFrame.document.body.innerHTML = text;
            break;
    }
}

function checkSpelling()
{
    checkElements = new Array();
    //loop through all tag groups
    for (var i = 0; i < tagGroup.length; i++)
    {
        var sTagName = tagGroup[i];
        var oElements = document.getElementsByTagName(sTagName);
        //loop through all elements
        for(var x = 0; x < oElements.length; x++)
        {
            if ((sTagName == "INPUT" && oElements[x].type == "text") || sTagName == "TEXTAREA")
                checkElements[checkElements.length] = oElements[x].id;
            else if ((sTagName == "DIV" || sTagName == "SPAN") && oElements[x].isContentEditable)
                checkElements[checkElements.length] = oElements[x].id;
        }
    }
    openSpellChecker();
}

function checkSpellingById(id)
{
    checkElements = new Array();
    checkElements[checkElements.length] = id;
    openSpellChecker();
}

function checkElementSpelling(oElement)
{
    checkElements = new Array();
    checkElements[checkElements.length] = oElement.id;
    openSpellChecker();
}

function openSpellChecker()
{
    if (window.showModalDialog)
        var result = window.showModalDialog(spellURL + "?Modal=true", window, "dialogHeight:320px; dialogWidth:400px; edge:Raised; center:Yes; help:No; resizable:No; status:No; scroll:No");
    else
        var newWindow = window.open(spellURL, "newWindow", "height=320,width=400,scrollbars=no,resizable=no,toolbars=no,status=no,menubar=no,location=no");
}


/****************************************************
* Spell Checker Suggestion Window JavaScript Code
****************************************************/
var iElementIndex = -1;
var parentWindow;

function initialize()
{
    iElementIndex = parseInt(document.getElementById("ElementIndex").value);

    if (parent.window.dialogArguments)
        parentWindow = parent.window.dialogArguments;
    else if (top.opener)
        parentWindow = top.opener;

    var spellMode = document.getElementById("SpellMode").value;

    switch (spellMode)
    {
        case "start" :
            //do nothing client side
            break;
        case "suggest" :
            //update text from parent document
            updateText();
            //wait for input
            break;
        case "end" :
            //update text from parent document
            updateText();
            //fall through to default
        default :
            //get text block from parent document
            if(loadText())
                document.SpellingForm.submit();
            else
                endCheck()

            break;
    }
}

function loadText()
{
    if (!parentWindow.document)
        return false;

    // check if there is any text to spell check
    for (++iElementIndex; iElementIndex < parentWindow.checkElements.length; iElementIndex++)
    {
        var newText = parentWindow.getText(iElementIndex);
        if (newText.length > 0)
        {
			updateSettings(newText, 0, iElementIndex, "start");
			document.getElementById("StatusText").innerText = "Spell Checking Text ...";
			return true;
        }
    }

    return false;
}

function updateSettings(currentText, wordIndex, elementIndex, mode)
{
    document.getElementById("CurrentText").value = currentText;
    document.getElementById("WordIndex").value = wordIndex;
    document.getElementById("ElementIndex").value = elementIndex;
    document.getElementById("SpellMode").value = mode;
}

function updateText()
{
    if (!parentWindow.document)
        return false;

	var newText = document.getElementById("CurrentText").value;
    parentWindow.setText(iElementIndex, newText);
}

function endCheck()
{
    if (showCompleteAlert)
    	alert("Spell Check Complete");
    closeWindow();
}

function closeWindow()
{
    if (top.opener || parent.window.dialogArguments)
	   self.close();
}

function changeWord(oElement)
{
    var k = oElement.selectedIndex;
    oElement.form.ReplacementWord.value = oElement.options[k].value;
}
