/***********************************************************
 * launches the spell checker
 ***********************************************************/

var spellURL = "SpellCheck.aspx";

var spellElements = new Array();
var spellElementsCount = 0;
var spellCheckCurrentElement = 0;

function resetElements()
{
    spellElements = new Array();
    spellElementsCount = 0;
    checkCurrentElement = 0;
}

function defineForm(oForm)
{
    for(var x = 0; x < oForm.elements.length; x++)
    {
        switch (oForm.elements[x].tagName)
        {
            case "INPUT" :
                if (oForm.elements[x].type == "text")
                    defineElement(oForm.elements[x])
                break;
            case "TEXTAREA" :
                defineElement(oForm.elements[x])
                break;
        }
    }
}

function defineElement(oElement)
{
    spellElements[spellElementsCount++] = oElement;
}

function checkDocument()
{
    resetElements();
    for (var i = 0; i < document.forms.length; i++)
        defineForm(document.forms[i]);
    
    checkSpelling();
}

function checkForm(oForm)
{
    resetElements();
    defineForm(oForm);
    checkSpelling();
}

function checkSpelling() 
{
    if (window.showModalDialog) 
    {
        var result = window.showModalDialog(spellURL, window, "dialogHeight:320px; dialogWidth:400px; edge:Raised; center:Yes; help:No; resizable:Yes; status:No; scroll: No");    
    }
    else
    {
        var newWindow = window.open(spellURL, "newWindow", "height=320,width=400,scrollbars=0,resizable=0,toolbars=0");
    }
}

/***********************************************************
 * Called from spellchecker to update form text
 ***********************************************************/
function getElementText(spellElementIndex)
{
    return spellElements[spellElementIndex].value;
}
function setCurrentElementText(spellElementIndex, spellElementText)
{
    spellElements[spellElementIndex].value = spellElementText;
}





/***********************************************************
* Changes the replace word when user selects word from list
***********************************************************/
function changeWord(oElement)
{
    var k = oElement.selectedIndex;
    oElement.form.ReplacementWord.value = oElement.options[k].value;
}


/***********************************************************
* this calls 'updateForm' from the calling page and passes
* it the current text with replacements at this point
***********************************************************/
function updateCallingPage() {
    var strText = document.SpellingForm.CurrentText.value;
    if (top.opener) top.opener.updateForm(strText);
}

function closeSpellChecker() {
	if (top.opener || parent.window.dialogArguments) self.close();
}


function initializeSpellChecker(spellMode)
{
    var parentWindow;
    if (top.opener)
        parentWindow = top.opener;
    else if(parent.window.dialogArguments)
        parentWindow = parent.window.dialogArguments;
        
        
    switch (spellMode)
    {
        case "load" :
            
            break;
        case "start" :
            this.SpellChecker.SpellCheck();
            break;
        case "checking" :
        
            break;
        case "end" :

            break;
    }

}
