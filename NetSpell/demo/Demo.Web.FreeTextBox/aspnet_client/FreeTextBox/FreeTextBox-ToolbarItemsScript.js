//** FreeTextBox Builtin ToolbarItems Script ***/
//   by John Dyer
//   http://www.freetextbox.com/
//**********************************************/

function FTB_Bold(ftbName) { 
	FTB_Format(ftbName,'bold'); 
}
function FTB_BulletedList(ftbName) { 
	FTB_Format(ftbName,'insertunorderedlist'); 
}
function FTB_Copy(ftbName) { 
	try {
		FTB_Format(ftbName,'copy'); 
	} catch (e) {
		alert('Your security settings to not allow you to use this command.  Please visit http://www.mozilla.org/editor/midasdemo/securityprefs.html for more information.');
	}

}
function FTB_CreateLink(ftbName) { 
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	editor.focus();
	if (isIE) {
		editor.document.execCommand('createlink','1',null);
	} else {
		var url = prompt('Enter a URL:', 'http://');
		if ((url != null) && (url != ''))  editor.document.execCommand('createlink',false,url);
	}
}
function FTB_Cut(ftbName) { 
	try {
		FTB_Format(ftbName,'cut'); 
	} catch (e) {
		alert('Your security settings to not allow you to use this command.  Please visit http://www.mozilla.org/editor/midasdemo/securityprefs.html for more information.');
	}

}
function FTB_Delete(ftbName) { 
	editor = FTB_GetIFrame(ftbName);
	if (confirm('Do you want to delete all the HTML and text presently in the editor?')) {	
		editor.document.body.innerHTML = '';
		if (isIE) {			
			editor.document.body.innerText = '';
		}
	}
	editor.focus();
}
function FTB_Indent(ftbName) { 
	FTB_Format(ftbName,'indent'); 
}
function FTB_InsertDate(ftbName) { 
	var d = new Date();
	FTB_InsertText(ftbName,d.toLocaleDateString());
}
function FTB_InsertImage(ftbName) { 
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	editor.focus();
    editor.document.execCommand('insertimage',1,'');
}
function FTB_InsertRule(ftbName) { 
	FTB_Format(ftbName,'inserthorizontalrule');
}
function FTB_InsertTime(ftbName) { 
	var d = new Date();
	FTB_InsertText(ftbName,d.toLocaleTimeString());
}
function FTB_Italic(ftbName) { 
	FTB_Format(ftbName,'italic'); 
}
function FTB_JustifyRight(ftbName) { 
	FTB_Format(ftbName,'justifyright'); 
}
function FTB_JustifyCenter(ftbName) { 
	FTB_Format(ftbName,'justifycenter'); 
}
function FTB_JustifyFull(ftbName) { 
	FTB_Format(ftbName,'justifyfull'); 
}
function FTB_JustifyLeft(ftbName) { 
	FTB_Format(ftbName,'justifyleft'); 
}
function FTB_NumberedList(ftbName) { 
	FTB_Format(ftbName,'insertorderedlist'); 
}
function FTB_Outdent(ftbName) { 
	FTB_Format(ftbName,'outdent'); 
}
function FTB_Paste(ftbName) { 
	try {
		FTB_Format(ftbName,'paste'); 
	} catch (e) {
		alert('Your security settings to not allow you to use this command.  Please visit http://www.mozilla.org/editor/midasdemo/securityprefs.html for more information.');
	}
}
function FTB_Print(ftbName) { 
	if (isIE) {
		FTB_Format(ftbName,'print'); 
	} else {
		editor = FTB_GetIFrame(ftbName);
		editor.print();
	}
}
function FTB_Redo(ftbName) { 
	FTB_Format(ftbName,'undo'); 
}
function FTB_RemoveFormat(ftbName) { 
	FTB_Format(ftbName,'removeformat'); 
}
function FTB_Save(ftbName) { 
	FTB_CopyHtmlToHidden(ftbName); 
	__doPostBack(ftbName,'Save');
}
function FTB_StrikeThrough(ftbName) { 
	FTB_Format(ftbName,'strikethrough'); 
}
function FTB_SubScript(ftbName) { 
	FTB_Format(ftbName,'subscript'); 
}
function FTB_SuperScript(ftbName) { 
	FTB_Format(ftbName,'superscript'); 
}
function FTB_Underline(ftbName) { 
	FTB_Format(ftbName,'underline'); 
}
function FTB_Undo(ftbName) { 
	FTB_Format(ftbName,'undo'); 
}
function FTB_Unlink(ftbName) { 
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	editor.focus();
    editor.document.execCommand('unlink',false,null);
}
function FTB_SetFontBackColor(ftbName,name,value) {
	editor = FTB_GetIFrame(ftbName);
	
	if (FTB_IsHtmlMode(ftbName)) return;
	editor.focus();
	editor.document.execCommand('backcolor','',value);
}
function FTB_SetFontFace(ftbName,name,value) {
	editor = FTB_GetIFrame(ftbName);
	
	if (FTB_IsHtmlMode(ftbName)) return;
	editor.focus();
	editor.document.execCommand('fontname','',value);
}
function FTB_SetFontForeColor(ftbName,name,value) {
	editor = FTB_GetIFrame(ftbName);
	
	if (FTB_IsHtmlMode(ftbName)) return;
	editor.focus();
	editor.document.execCommand('forecolor','',value);
}
function FTB_SetFontSize(ftbName,name,value) {
	editor = FTB_GetIFrame(ftbName);
	
	if (FTB_IsHtmlMode(ftbName)) return;
	editor.focus();
	editor.document.execCommand('fontsize','',value);
}
function FTB_InsertHtmlMenu(ftbName,name,value) {
	FTB_InsertText(ftbName,value);
}
function FTB_SetParagraph(ftbName,name,value) {
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	if (value == '<body>') {
		editor.document.execCommand('formatBlock','','Normal');
		editor.document.execCommand('removeFormat');
		return;
	}
	editor.document.execCommand('formatBlock','',value);
}
function FTB_SymbolsMenu(ftbName,name,value) {
	FTB_InsertText(ftbName,value);
}
function FTB_SetStyle(ftbName,name,value) { 
	editor = FTB_GetIFrame(ftbName);
	cssClass = value;

	
	if (cssClass == "[Remove Style]") { 
		if (editor.document.selection.type == "Control") { 
			var oControlRange = editor.document.selection.createRange(); 
			var oControlItem = oControlRange.item(0); 
			var oTextRange = editor.document.body.createTextRange(); 
			oTextRange.moveToElementText(oControlItem); 
			oTextRange.select(); 
			var sHTML = oTextRange.htmlText; 
			sHTML = sHTML.replace(/<SPAN[^>]*>([\s\S]*?)<\/SPAN>/ig, "<FONT face=ftb_removestyle>$1</FONT>"); 
			oTextRange.pasteHTML(sHTML); 
		} else { 
			var oRange = editor.document.selection.createRange(); 
			oRange.execCommand("FontName", false, "ftb_removestyle"); 
		} 
		FTB_RemoveStyle(editor.document.body); 
		FTB_RemoveStyleClean(editor.document.body); 
	} else { 
		if (editor.document.selection.type == "Control") { 
			var oControlRange = editor.document.selection.createRange(); 
			var oControlItem = oControlRange.item(0); 
			var oTextRange = editor.document.body.createTextRange(); 
			oTextRange.moveToElementText(oControlItem); oTextRange.select(); 
			var sHTML = oTextRange.htmlText; 
			sHTML = sHTML.replace(/<SPAN[^>]*>([\s\S]*?)<\/SPAN>/ig, "$1"); 
			oTextRange.pasteHTML("<FONT face=ftb_span>" + sHTML + "</FONT>"); 
		} else { 
			var oRange = editor.document.selection.createRange(); 
			var sBookmark = oRange.getBookmark(); 
			var sHTML = oRange.htmlText; 
			sHTML = sHTML.replace(/class=\w*/ig,""); 
			oRange.pasteHTML(sHTML); oRange.moveToBookmark(sBookmark); 
			oRange.execCommand("FontName", false, "ftb_span"); 
		} 
		FTB_FontsToSpans(editor.document, editor.document.body, cssClass); 
		FTB_JoinSpans(editor.document.body, null); 
		FTB_RemoveEmptySpans(editor.document.body); 
	} 
} 
function FTB_RemoveStyle(oElement) { 
	for (var i=0;i<oElement.childNodes.length;i++) { 
		FTB_RemoveStyle(oElement.childNodes[i]); 
	} 
	if(oElement.tagName=="SPAN") { 
		if(oElement.innerHTML.indexOf("ftb_removestyle")!=-1) { 
			oElement.removeNode(false); 
		} 
	} 
} 
function FTB_RemoveStyleClean(oElement) { 
	for(var i=0;i<oElement.childNodes.length;i++) { 
		FTB_RemoveStyleClean(oElement.childNodes[i]); 
	} 
	if (oElement.tagName=="FONT") { 
		if (oElement.face=="ftb_removestyle") { 
			oElement.removeNode(false); 
		} 
	} 
} 
function FTB_FontsToSpans(oDocument, oElement, sClass) { 
	for(var i=0;i<oElement.childNodes.length;i++) { 
		FTB_FontsToSpans(oDocument, oElement.childNodes[i], sClass); 
	} 
	if (oElement.tagName=="FONT") { 
		if(oElement.face=="ftb_span") { 
			sPreserve=oElement.innerHTML; 
			oSpan=oDocument.createElement("SPAN"); 
			oElement.replaceNode(oSpan); 
			oSpan.innerHTML=sPreserve; 
			oSpan.className=sClass; 
		} else { 
			var sStyle = ""; 
			if (oElement.face.length) { 
				sStyle += "font-family: " + oElement.face + ";"; 
			} 
			if (oElement.size.length) { 
				var sSize = oElement.size; 
				if (sSize=="1") sSize = "xx-small"; 
				if (sSize=="2") sSize = "x-small"; 
				if (sSize=="3") sSize = "small"; 
				if (sSize=="4") sSize = "medium"; 
				if (sSize=="5") sSize = "large"; 
				if (sSize=="6") sSize = "x-large"; 
				if (sSize=="7") sSize = "xx-large"; 
				if (sSize.substring(0, 1)=="-") sSize = "smaller"; 
				if (sSize.substring(0, 1)=="+") sSize = "larger"; 
				sStyle += "font-size: " + sSize + ";"; 
			} 
			if (oElement.color.length) { 
				sStyle += "color: " + oElement.color + ";"; 
			} 
			if (sStyle.length) { 
				sPreserve=oElement.innerHTML; 
				oSpan=oDocument.createElement("SPAN"); 
				oElement.replaceNode(oSpan); 
				oSpan.innerHTML=sPreserve; 
				oSpan.style.cssText=sStyle; 
			} 
		} 
	} 
} 
function FTB_JoinSpans(oElement, oParent) { 
	for(var i=0;i<oElement.childNodes.length;i++) { 
		var oChild = oElement.childNodes[i]; 
		oElement = FTB_JoinSpans(oChild, oElement); 
	} 
	if (oElement.tagName=="SPAN" && oParent != null && oParent.tagName =="SPAN") { 
		if (oElement.innerText == oParent.innerText) { 
			sElementClass=oElement.className; 
			sParentClass=oParent.className; 
			if(sParentClass.length && !sElementClass.length) { 
				oElement.setAttribute("class", sParentClass); 
			} 
			var parentAttributes = oParent.style.cssText.split("; "); 
			var elementAttributes = oElement.style.cssText.split("; "); 
			for (var i=0;i<parentAttributes.length;i++) { 
				var parentPairs = parentAttributes[i].split(":"); 
				var sPKey = parentPairs[0]; 
				var sPValue = parentPairs[1]; 
				var bKeyExists = false; 
				for (var k=0;k<elementAttributes.length;k++) { 
					var elementPairs = elementAttributes[k].split(":"); 
					var sEKey = elementPairs[0]; 
					var sEValue = elementPairs[1]; 
					if (sEKey == sPKey) { 
						bKeyExists = true; break; 
					} 
				} 
				if (!bKeyExists) { 
					oElement.style.cssText = oElement.style.cssText + ";" + sPKey + ":" + sPValue; 
				} 
			} 
			oParent.removeNode(false); 
			return oElement; 
		} 
	} 
	return oParent; 
} 
function FTB_RemoveEmptySpans(oElement) { 
	for(var i=0;i<oElement.childNodes.length;i++) { 
		FTB_RemoveEmptySpans(oElement.childNodes[i]); 
	} 
	if (oElement.tagName=="SPAN" && oElement.className.length==0 && oElement.style.cssText=="") { 
		oElement.removeNode(false); 
	} 
}
