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

//html tags to check
var tagGroup = new Array("INPUT", "TEXTAREA", "DIV", "SPAN", "IFRAME");
var iTagGoupIndex = 0;
var iElementIndex = -1;
var parentWindow;

function initialize()
{
    try
    {
        iTagGoupIndex = parseInt(document.getElementById("TagGroupIndex").value);
        iElementIndex = parseInt(document.getElementById("ElementIndex").value);
    }
    catch(e)
    {
        iTagGoupIndex = 0;
        iElementIndex = -1;
    }

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

    var oDocument = parentWindow.document;

    // check if there is an element id to spell check
    if (parentWindow.checkElementID && parentWindow.checkElementID.length > 0)
    {
    	var newText = getElementText(oDocument.getElementById(parentWindow.checkElementID));
		if (newText.length > 0 && iTagGoupIndex == 0)
		{
			updateSettings(newText, 0, -1, -1, "start");
			document.getElementById("StatusText").innerText = "Checking " + parentWindow.checkElementID;
			return true;
		}
		return false;
    }

    //loop through all tag groups on parent document starting with last tag index
    for (iTagGoupIndex; iTagGoupIndex < tagGroup.length; iTagGoupIndex++)
    {
        var sTagName = tagGroup[iTagGoupIndex];
        var oElements = oDocument.getElementsByTagName(sTagName);
        
        //loop through all elements starting with last element index +1
        for(++iElementIndex; iElementIndex < oElements.length; iElementIndex++)
        {            
            var newText = getElementText(oElements[iElementIndex]);
            if (newText.length > 0)
            {
                updateSettings(newText, 0, iTagGoupIndex, iElementIndex, "start");
                document.getElementById("StatusText").innerText = "Checking " + oElements[iElementIndex].name;
                return true;
            }
        }
        iElementIndex = -1; //reset
    }
    return false;
}

function getElementText(oElement)
{
    var sTagName = oElement.tagName;
    var newText = "";
    
    //look for input or textarea elements
    if ((sTagName == "INPUT" && oElement.type == "text") || sTagName == "TEXTAREA")
        newText = oElement.value;
    else if (oElement.isContentEditable && (sTagName == "DIV" || sTagName == "SPAN"))
        newText = oElement.innerHTML;
    else if (oElement.isContentEditable && sTagName == "IFRAME")
        newText = oElement.document.body.innerHTML;
    
    return newText;
}

function updateSettings(currentText, wordIndex, tagGroupIndex, elementIndex, mode)
{
    document.getElementById("CurrentText").value = currentText;
    document.getElementById("WordIndex").value = wordIndex;
    document.getElementById("TagGroupIndex").value = tagGroupIndex;
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
	{
		oElement = oDocument.getElementById(parentWindow.checkElementID);
	}
    else 
    {
    	var sTagName = tagGroup[iTagGoupIndex];
        var oElements = oDocument.getElementsByTagName(sTagName);
        oElement = oElements[iElementIndex];
	}
    
	switch (oElement.tagName)
	{
		case "INPUT" :
        case "TEXTAREA" :
			oElement.value = newText;
			break;
		case "DIV" :
		case "SPAN" :
			oElement.innerHTML = newText;
			break;
		case "IFRAME" :
			oElement.document.body.innerHTML = newText;
			break;
    }
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
