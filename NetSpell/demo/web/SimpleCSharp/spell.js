/***********************************************************
 * launches the spell checker
 ***********************************************************/
var pubBody = "";
var pubURL = "";

function checkSpelling(strBody, strURL) {

	pubBody = strBody;
	pubURL = strURL;

	var strTextArea = "";
	var oElement = document.getElementById(pubBody);
	if (oElement) strTextArea = oElement.value;

	var newWindow = window.open("","newWindow","height=320,width=400,scrollbars=1,resizable=1,toolbars=1");

	newWindow.document.open()
	newWindow.document.writeln("<HTML>")
	newWindow.document.writeln("<HEAD>")
	newWindow.document.writeln("	<TITLE>Spell Checker</TITLE>")
	newWindow.document.writeln("</HEAD>")
	newWindow.document.writeln("<BODY bgcolor=\"White\">")
	newWindow.document.writeln("<font face=\"Arial Black\" size=\"+1\">Loading Spell Checker . . .</font>")
	newWindow.document.writeln("<form action=\"\" method=\"POST\" name=\"Spell\">")
	newWindow.document.writeln("	<input type=\"Hidden\" name=\"CurrentText\" value=\"\">")
	newWindow.document.writeln("	<input type=\"Hidden\" name=\"SpellCheck\" value=\"true\">")
	newWindow.document.writeln("</form>")
	newWindow.document.writeln("</BODY>")
	newWindow.document.writeln("</HTML>")
	newWindow.document.close()

	newWindow.document.Spell.action=pubURL
	newWindow.document.Spell.CurrentText.value=strTextArea;
	newWindow.document.Spell.submit();


}

/***********************************************************
 * Called from spellchecker to update form text
 ***********************************************************/
function updateForm(strReturnText)
{
	var oElement = document.getElementById(pubBody);
	if (oElement) oElement.value = strReturnText;
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
	if (top.opener) self.close();
}
