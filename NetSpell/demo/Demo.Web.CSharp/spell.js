/****************************************************
* Spell Checker Client JavaScript Code
****************************************************/
var spellURL = "SpellCheck.aspx";
var checkElementID = "";

function checkSpelling()
{
    checkElementID = "";
    openSpellChecker();
}


function checkSpellingById(id)
{
    checkElementID = id;
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
var iFormIndex = 0;
var iElementIndex = -1;
var parentWindow;

function initialize()
{

    var spellMode = document.getElementById("SpellMode").value;

    try
    {
        iFormIndex = parseInt(document.getElementById("FormIndex").value);
        iElementIndex = parseInt(document.getElementById("ElementIndex").value);
    }
    catch(e)
    {
        iFormIndex = 0;
        iElementIndex = -1;
    }

    if (parent.window.dialogArguments)
        parentWindow = parent.window.dialogArguments;
    else if (top.opener)
        parentWindow = top.opener;

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

    var oDocument = parentWindow.document;

    // check if there is an element id to spell check
    if (parentWindow.checkElementID && parentWindow.checkElementID.length > 0)
    {
    	var newText = oDocument.getElementById(parentWindow.checkElementID).value
		if (newText.length > 0 && iFormIndex == 0)
		{
			updateSettings(newText, 0, -1, -1, "start");
			document.getElementById("StatusText").innerText = "Checking " + parentWindow.checkElementID;
			return true;
		}
		return false;
    }

    //loop through all forms on parent document starting with last form index
    for (iFormIndex; iFormIndex < oDocument.forms.length; iFormIndex++)
    {
        var oForm = oDocument.forms[iFormIndex];
        //loop through all elements starting with last element index +1
        for(++iElementIndex; iElementIndex < oForm.elements.length; iElementIndex++)
        {
            var sTagName = oForm.elements[iElementIndex].tagName;
            var newText = "";

            //look for input or textarea elements
            if ((sTagName == "INPUT" && oForm.elements[iElementIndex].type == "text") || sTagName == "TEXTAREA")
                newText = oForm.elements[iElementIndex].value;

            if (newText.length > 0)
            {
                updateSettings(newText, 0, iFormIndex, iElementIndex, "start");
                document.getElementById("StatusText").innerText = "Checking " + oForm.elements[iElementIndex].name;
                return true;
            }
        }
        iElementIndex = -1; //reset
    }
    return false;
}

function updateSettings(currentText, wordIndex, formIndex, elementIndex, mode)
{
    document.getElementById("CurrentText").value = currentText;
    document.getElementById("WordIndex").value = wordIndex;
    document.getElementById("FormIndex").value = formIndex;
    document.getElementById("ElementIndex").value = elementIndex;
    document.getElementById("SpellMode").value = mode;
}

function updateText()
{
    if (!parentWindow.document)
        return false;

	var oDocument = parentWindow.document;
	var newText = document.getElementById("CurrentText").value;
	var oElement;

	if (parentWindow.checkElementID && parentWindow.checkElementID.length > 0)
		oElement = oDocument.getElementById(parentWindow.checkElementID);
    else
		oElement = oDocument.forms[iFormIndex].elements[iElementIndex];

    oElement.value = newText;
}

function endCheck()
{
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
