/***********************************************************
 * launches the spell checker
 ***********************************************************/
var pubForm
var pubBody
var pubURL
   
function checkSpelling(objForm, strBody, strURL) {
	
	if (navigator.appVersion.indexOf("MSIE 3")==-1) {
		pubForm = objForm;
		pubBody = strBody;
		pubURL = strURL;
		
		for (i = 0; i < pubForm.length; i++) {
			if (pubForm.elements[i].name == pubBody) {
				var strTextArea = pubForm.elements[i].value;
			}
		}
		
		var newWindow = window.open("","newWindow","height=320,width=400");
		
		newWindow.document.open()
		newWindow.document.writeln("<HTML>")
		newWindow.document.writeln("<HEAD>")
		newWindow.document.writeln("	<TITLE>Speller</TITLE>")
		newWindow.document.writeln("</HEAD>")
		newWindow.document.writeln("<BODY bgcolor=\"White\">")
		newWindow.document.writeln("<font face=\"Arial Black\" size=\"+1\">Loading Spell Checker . . .</font>")
		newWindow.document.writeln("<form action=\"" + pubURL + "\" method=\"POST\" name=\"Spell\">")
		newWindow.document.writeln("	<input type=\"Hidden\" name=\"CurrentText\" value=\"\">")
		newWindow.document.writeln("	<input type=\"Hidden\" name=\"SpellCheck\" value=\"true\">")
		newWindow.document.writeln("</form>")
		newWindow.document.writeln("</BODY>")
		newWindow.document.writeln("</HTML>")
		newWindow.document.close()
		
		newWindow.document.Spell.CurrentText.value=strTextArea;
		newWindow.document.Spell.submit();
		
		}
	else {
		alert("Spell Checker is not compatible with this browser.  Please upgrade to Netscape 3+ or IE 4.")
		}
}

/***********************************************************
 * Called from spellchecker to update form text
 ***********************************************************/
function updateForm(strReturnText) 
{
	for (i = 0; i < pubForm.length; i++) {
		if (pubForm.elements[i].name == pubBody) {
			pubForm.elements[i].value = strReturnText;
		}
	}
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
