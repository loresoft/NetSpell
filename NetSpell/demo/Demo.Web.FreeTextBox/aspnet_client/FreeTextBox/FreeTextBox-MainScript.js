//** FreeTextBox Main Script ********************/
//   by John Dyer
//   http://www.freetextbox.com/
//***********************************************/

/** START:BROWSER DETECTION ********************/
_d=document;
_nv=navigator.appVersion.toLowerCase();
_f=false;_t=true;
ie4=(!_d.getElementById&&_d.all)?_t:_f;
ie5=(_nv.indexOf("msie 5.0")!=-1)?_t:_f;
ie55=(_nv.indexOf("msie 5.5")!=-1)?_t:_f;
ie6=(_nv.indexOf("msie 6.0")!=-1)?_t:_f;
isIE=(ie5||ie55||ie6)?_t:_f;
/** END:BROWSER DETECTION ********************/

/** START:MAIN FREETEXTBOX FUNCTIONS ********************/
function FTB_InitializeAll() {
	for (var i=0; i<FTB_StartUpArray.length; i++)
	FTB_Initialize(FTB_StartUpArray[i]);
}

function FTB_Initialize(ftbName) {
	
	startMode = eval(ftbName + "_StartMode");
	readOnly = eval(ftbName + "_ReadOnly");
	designModeCss = eval(ftbName + "_DesignModeCss");
	htmlModeCss = eval(ftbName + "_HtmlModeCss");

	hiddenHtml = FTB_GetHiddenField(ftbName);
	editor = FTB_GetIFrame(ftbName);
	
	if (readOnly) {
		editor.document.designMode = 'Off';
	} else {
		editor.document.designMode = 'On';
	}
	
	editor.document.open();
	editor.document.write(hiddenHtml.value);
	editor.document.close();
		
	if (isIE) {
		if (htmlModeCss != "" || designModeCss != "" ) {
			editor.document.createStyleSheet(designModeCss);
			editor.document.createStyleSheet(htmlModeCss);
			editor.document.styleSheets[1].disabled = true;
		}
	} else {
		// turn off <span style="font-weight:bold">, use <b>
		editor.document.execCommand("useCSS", false, true); 
	}
	
	if (readOnly) {
		editor.document.contentEditable = 'False';
	} else {
		editor.document.contentEditable = 'True';
	}
	
	editor.document.body.style.border = '0';
	
	if (isIE) {
		editor.document.onkeydown = function() {
			return FTB_Event(ftbName);	
		};
		editor.document.onkeypress = function() {
			return FTB_Event(ftbName);	
		};	
		editor.document.onclick = function() {
			return FTB_Event(ftbName);	
		};	
		editor.document.onmousedown = function() {
			return FTB_Event(ftbName);	
		};		
	} else {
		editor.addEventListener("keydown", function() {
			return FTB_Event(ftbName);	
		}, true);	
		editor.addEventListener("keypress", function() {
			return FTB_Event(ftbName);	
		}, true);
		editor.addEventListener("click", function() {
			return FTB_Event(ftbName);	
		}, true);			
		editor.addEventListener("mousedown", function() {
			return FTB_Event(ftbName);	
		}, true);		
	}
	
	
	if (startMode != "DesignMode" && FTB_HideToolbar(ftbName)) {
		toolbar = FTB_GetToolbar(ftbName);
		if (toolbar != null) toolbar.style.display = 'none';
	}
}

function FTB_GetFtbName(ftb) {
	ftbName = ftb.name;
	underscore = ftbName.lastIndexOf("_");
	return ftbName.substring(0,underscore); 
}

function FTB_ChangeMode(ftb,goToHtmlMode) {
	editor = ftb;
		
	ftbName = FTB_GetFtbName(ftb);
	var toolbar = FTB_GetToolbar(ftbName);
	var hideToolbar = FTB_HideToolbar(ftbName);
	var editorContent;
	
	editor.focus();
	
	if (goToHtmlMode) {
		if (isIE) {			
			if (editor.document.styleSheets.length > 0) {
				editor.document.styleSheets[0].disabled = true;
				editor.document.styleSheets[1].disabled = false;				
			}
			if (FTB_HtmlModeDefaultsToMonoSpaceFont(ftbName) && editor.document.styleSheets.length < 2) {						
				editor.document.body.style.fontFamily = 'Courier New, Courier New';
				editor.document.body.style.fontSize = '10pt';				
			}
						
			editorContent = editor.document.body.innerHTML;			
			//alert(editorContent);
			editor.document.body.innerText = editorContent;
		
		} else {			
			editorContent = document.createTextNode(editor.document.body.innerHTML);
			editor.document.body.innerHTML = "";
			editor.document.body.appendChild(editorContent);			
		}
		
		if (toolbar != null && hideToolbar ) {
			toolbar.style.display = 'none';
		}		
		return true;
	} else {
		// go to Design Mode
		if (isIE) {
			editorContent = editor.document.body.innerText;
			
			if (FTB_HtmlModeDefaultsToMonoSpaceFont(ftbName) && editor.document.styleSheets.length < 2) {					
				editor.document.body.style.fontFamily = '';
				editor.document.body.style.fontSize = '';
			}					
			if (editor.document.styleSheets.length > 0) {
				editor.document.styleSheets[0].disabled = false;
				editor.document.styleSheets[1].disabled = true;
			}
			
			editor.document.body.innerHTML = editorContent;
		} else {
						
			editorContent = editor.document.body.ownerDocument.createRange();
			editorContent.selectNodeContents(editor.document.body);
			editor.document.body.innerHTML = editorContent.toString();
		}

		if (toolbar != null && hideToolbar ) {
			toolbar.style.display = 'inline';
		}
		
		editor.focus(); 
		return true;
	}
}

function FTB_CopyHtmlToHidden(ftbName) {
	hiddenHtml = FTB_GetHiddenField(ftbName);
	editor = FTB_GetIFrame(ftbName);
	
	if (isIE) {
		if (FTB_IsHtmlMode(ftbName)) {
			hiddenHtml.value = editor.document.body.innerText;  
		} else {
			hiddenHtml.value = editor.document.body.innerHTML;  
		}		
	} else {
		if (FTB_IsHtmlMode(ftbName)) {
			editorContent = editor.document.body.ownerDocument.createRange();
			editorContent.selectNodeContents(editor.document.body);
			hiddenHtml.value = editorContent.toString();
		} else {
			hiddenHtml.value = editor.document.body.innerHTML;  
		}	
	}
	if (hiddenHtml.value == '<P>&nbsp;</P>' || hiddenHtml.value == '<br>') {
		hiddenHtml.value = '';
	}
}

function FTB_Format(ftbName,commandName) {
	editor = FTB_GetIFrame(ftbName);

	if (FTB_IsHtmlMode(ftbName)) return;
	editor.focus();
	editor.document.execCommand(commandName,'',null);
}

function FTB_SurroundText(ftbName,start,end) {
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	
	if (isIE) {
		var sel = editor.document.selection.createRange();
		html = start + sel.htmlText + end;
		sel.pasteHTML(html);		
	} else {
        selection = editor.window.getSelection();
        editor.focus();
        if (selection) {
            range = selection.getRangeAt(0);
        } else {
            range = editor.document.createRange();
        } 
        
        FTB_InsertText(ftbName, start + selection + end);
	}	

}

function FTB_InsertText(ftbName,insertion) {
	if (FTB_IsHtmlMode(ftbName)) return;
	editor = FTB_GetIFrame(ftbName);
	editor.focus();
	if (isIE) {
		sel = editor.document.selection.createRange();
		sel.pasteHTML(insertion);
	} else {
        editor.focus();
        selection = editor.window.getSelection();
		if (selection) {
			range = selection.getRangeAt(0);
		} else {
			range = editor.document.createRange();
		} 

        var fragment = editor.document.createDocumentFragment();
        var div = editor.document.createElement("div");
        div.innerHTML = insertion;

        while (div.firstChild) {
            fragment.appendChild(div.firstChild);
        }

        selection.removeAllRanges();
        range.deleteContents();

        var node = range.startContainer;
        var pos = range.startOffset;

        switch (node.nodeType) {
            case 3:
                if (fragment.nodeType == 3) {
                    node.insertData(pos, fragment.data);
                    range.setEnd(node, pos + fragment.length);
                    range.setStart(node, pos + fragment.length);
                } else {
                    node = node.splitText(pos);
                    node.parentNode.insertBefore(fragment, node);
                    range.setEnd(node, pos + fragment.length);
                    range.setStart(node, pos + fragment.length);
                }
                break;

            case 1:
                node = node.childNodes[pos];
                node.parentNode.insertBefore(fragment, node);
                range.setEnd(node, pos + fragment.length);
                range.setStart(node, pos + fragment.length);
                break;
        }
        selection.addRange(range);	
	}
}
function FTB_CheckTag(item,tagName) {
	if (item.tagName.search(tagName)!=-1) {
		return item;
	}
	if (item.tagName=='BODY') {
		return false;
	}
	item=item.parentElement;
	return FTB_CheckTag(item,tagName);
}
/** END:MAIN FREETEXTBOX FUNCTIONS ********************/

/** START:PROPERTIES ********************/
function FTB_IsHtmlMode(ftbName) {
	return (eval(ftbName + "_HtmlMode"));
}

function FTB_TabMode(ftbName) {
	return (eval(ftbName + "_TabMode"));
}

function FTB_BreakMode(ftbName) {
	return (eval(ftbName + "_BreakMode"));
}

function FTB_HtmlModeDefaultsToMonoSpaceFont(ftbName) {
	return (eval(ftbName + "_HtmlModeDefaultsToMonoSpaceFont"));
}

function FTB_HideToolbar(ftbName) {
	return (eval(ftbName + "_HideToolbar"));
}

function FTB_UpdateToolbar(ftbName) {
	return (eval(ftbName + "_UpdateToolbar"));
}

function FTB_GetHiddenField(ftbName) {
	return document.getElementById(ftbName);
}

function FTB_GetIFrame(ftbName) {
	if (isIE) {
		return eval(ftbName + "_Editor");
		//return document.getElementById(ftbName + "_Editor");
	} else {
		return document.getElementById(ftbName + "_Editor").contentWindow;
	}
}

function FTB_GetToolbar(ftbName) {
	return document.getElementById(ftbName + "_Toolbar");
}
function FTB_GetToolbarArray(ftbName) {
	return eval(ftbName + "_ToolbarItems");
}

function FTB_GetCssID(ftbName) {
	cssID = ftbName;
	while (cssID.substring(0,1) == '_') {
		cssID = cssID.substring(1);
	}
	return cssID;
}
/** END:PROPERTIES ********************/

/** START: BUTTONS **********************/

function FTB_SetButtonStyle(buttonTD,style,checkstyle) {
	if (buttonTD == null) return;
	if (buttonTD.className != checkstyle)
		buttonTD.className = style;
	
}
function FTB_GetClassSubName(className) {
	underscore = className.indexOf("_");
	if (underscore < 0) return className;
	return className.substring(underscore+1);
}
function FTB_ButtonOver(theTD,ftbName,imageOver,imageDown) {
	FTB_SetButtonStyle(theTD,FTB_GetCssID(ftbName)+'_ButtonOver',null);
	
	//if (eval(ftbName+'_OverImage').src != '')
	//	theTD.style.backgroundImage = "url(" + eval(ftbName+'_OverImage').src + ")";
	
	if(imageOver == 1 & theTD.childNodes.length && theTD.childNodes[0].tagName == "IMG"){
		oldSrc = theTD.childNodes[0].src;
		if (oldSrc.indexOf('.over.') == -1) {
			theTD.childNodes[0].src=oldSrc.substring(0, oldSrc.length-4) + ".over.gif";
		}
	}
	
	//FTB_Event(ftbName);
}
function FTB_ButtonOut(theTD,ftbName,imageOver,imageDown) {
	FTB_SetButtonStyle(theTD,FTB_GetCssID(ftbName)+'_ButtonNormal',null);
	document.body.style.cursor = 'default';
	
	theTD.style.backgroundImage='';
	
	if(theTD.childNodes.length && theTD.childNodes[0].tagName == "IMG"){
		oldSrc = theTD.childNodes[0].src;
		if (oldSrc.indexOf('.over.') > 0) {
			theTD.childNodes[0].src=oldSrc.substring(0, oldSrc.length-9) + ".gif";
		}
		if (oldSrc.indexOf('.down.') > 0) {
			theTD.childNodes[0].src=oldSrc.substring(0, oldSrc.length-9) + ".gif";
		}
	}
	
	//FTB_Event(ftbName);
}
function FTB_ButtonDown(theTD,ftbName,imageOver,imageDown) {
	document.body.style.cursor = 'default';
	FTB_SetButtonStyle(theTD,FTB_GetCssID(ftbName)+'_ButtonDown',null);

	//if (eval(ftbName+'_DownImage').src != '') 
	//	theTD.style.backgroundImage = "url(" + eval(ftbName+'_DownImage').src + ")";
	
	if(imageDown == 1 && theTD.children.length && theTD.children[0].tagName == "IMG"){
		oldSrc = theTD.children[0].src;
		if (oldSrc.indexOf('.over.') > 0) {
			theTD.children[0].src=oldSrc.substring(0, oldSrc.length-9) + ".down.gif";
		}
	}
	
	//FTB_Event(ftbName);
}
function FTB_ButtonUp(theTD,ftbName,imageOver,imageDown) {
	document.body.style.cursor = 'auto';
	FTB_SetButtonStyle(theTD,FTB_GetCssID(ftbName)+'_ButtonOver',null);

	//if (eval(ftbName+'_OverImage').src != '')
	//	theTD.style.backgroundImage = "url(" + eval(ftbName+'_OverImage').src + ")";
	
	if(imageOver == 1 && theTD.children.length && theTD.children[0].tagName == "IMG"){
		oldSrc = theTD.children[0].src;
		if (oldSrc.indexOf('.over.') == -1) {
			theTD.children[0].src=oldSrc.substring(0, oldSrc.length-4) + ".over.gif";
		}
	}
	
	//FTB_Event(ftbName);
}
/** END:BUTTONS ********************/


/** START:TABS ********************/
function FTB_SetActiveTab(theTD,ftbName) {
	parentTR = theTD.parentElement;
	parentTR = document.getElementById(ftbName + "_TabRow");

	selectedTab = 1;
	totalButtons = parentTR.cells.length-1;
	for (var i=1;i< totalButtons;i++) {
		parentTR.cells[i].className = FTB_GetCssID(ftbName) + "_TabOffRight";
		if (theTD == parentTR.cells[i]) { selectedTab = i; }
	}

	if (selectedTab==1) {
		parentTR.cells[0].className = FTB_GetCssID(ftbName) + "_StartTabOn";
	} else {
		parentTR.cells[0].className = FTB_GetCssID(ftbName) + "_StartTabOff";
		parentTR.cells[selectedTab-1].className = FTB_GetCssID(ftbName) + "_TabOffLeft";
	}

	theTD.className = FTB_GetCssID(ftbName) + "_TabOn";
}
function FTB_TabOver() {
	document.body.style.cursor='default';
}
function FTB_TabOut() {
	document.body.style.cursor='auto';
}
/** END:TABS ********************/

function FTB_Event(ftbName) {
	
	editor = FTB_GetIFrame(ftbName);
	htmlMode = FTB_IsHtmlMode(ftbName);
	var _TAB = 9;
	var _ENTER = 13;
	var _QUOTE = 222;
	var _OPENCURLY = '&#8220;';
	var _CLOSECURLY = '&#8221;';
	
	if (isIE) {
	// TAB Functions
		if (editor.event.keyCode == _TAB) {	

			var tabMode = FTB_TabMode(ftbName);

			if (tabMode == "Disabled") {
				editor.event.cancelBubble = true;
				editor.event.returnValue = false;
			}
			if (tabMode == "InsertSpaces") {
				FTB_InsertText(ftbName,"&nbsp;&nbsp;&nbsp;");
				editor.event.cancelBubble = true;
				editor.event.returnValue = false;
			}			
			if (tabMode == "NextControl") {
				// do nothing for TabMode.NextControl
			}	
		}

		// IE defaults to <p>, Mozilla to <br>
		if (editor.event.keyCode == _ENTER) {


			var breakMode = FTB_BreakMode(ftbName);			

			if (breakMode == "LineBreak" || editor.event.ctrlKey || htmlMode) {
				var sel = editor.document.selection;
				if (sel.type == 'Control') {
					return;
				}
				var r = sel.createRange();
				if ((!FTB_CheckTag(r.parentElement(),'LI'))&&(!FTB_CheckTag(r.parentElement(),'H'))) {
					r.pasteHTML('<br>');
					editor.event.cancelBubble = true; 
					editor.event.returnValue = false; 
					r.select();
					r.collapse(false);
					return false;
				}					
			}
		}
	}
	
	// update ToolbarItems only every 50 milliseconds b/c it is expensive
	if (!htmlMode && FTB_UpdateToolbar(ftbName)) {		
		if (editor.timerToolbar) {
			clearTimeout(editor.timerToolbar);
		}
		editor.timerToolbar = setTimeout(function() {
			FTB_SetToolbarItems(ftbName);
			editor.timerToolbar = null;
		}, 50);	
	}
}

function FTB_SetToolbarItems(ftbName) {
	editor = FTB_GetIFrame(ftbName);
	htmlMode = FTB_IsHtmlMode(ftbName);
	toolbarArray = 	FTB_GetToolbarArray(ftbName);
		
	//document.getElementById("Debug").value = "";
	
	if (toolbarArray) {
		for (var i=0; i<toolbarArray.length; i++) {
			toolbarItemID = toolbarArray[i][0];
			toolbarItem = document.getElementById(toolbarItemID);
			commandIdentifier = toolbarArray[i][1];

			state = "";
			try {
				if (toolbarItemID.indexOf("Button") > -1) {
					state = editor.document.queryCommandState(commandIdentifier);

					FTB_SetButtonState(toolbarItemID,ftbName,state);
				} else {
					state = editor.document.queryCommandValue(commandIdentifier);
					
					switch (commandIdentifier) {
						case "backcolor":
							if (isIE) {
								state = FTB_GetHexColor(state);
							} else {
								if (state == "") state = "#FFFFFF";
							}
							break;						
						case "forecolor":
							if (isIE) {
								state = FTB_GetHexColor(state);
							} else {
								if (state == "") state = "#000000";
							}
							break;
						case "formatBlock":
							//document.getElementById("Debug").value += "****: " + state + "\n";
							if (!isIE) {
								if (state == "p" || state == "" || state == "<x>") 
									state = "<body>";
								else 
									state = "<" + state + ">";							
							}
							break;					
					}
						
					//document.getElementById("Debug").value += commandIdentifier + ": " + state + "\n";
					
					FTB_SetDropDownListState(toolbarItemID,state);					
				}
			} catch(e) {
			}
		}
	}
}

function FTB_GetHexColor(intColor) {
	intColor = intColor.toString(16).toUpperCase();
	while (intColor.length < 6) {
		intColor = "0" + intColor;
	}
	return "#" + intColor.substring(4,6) + intColor.substring(2,4) + intColor.substring(0,2);
}

function FTB_SetDropDownListState(ddlName,value) {
	ddl = document.getElementById(ddlName);
	
	if (ddl) {
		for (var i=0; i<ddl.options.length; i++) {
			if (ddl.options[i].text == value || ddl.options[i].value == value) {
				ddl.options.selectedIndex = i;
				return;
			}	
		}
	}
}

function FTB_SetButtonState(buttonName,ftbName,value) {
	buttonTD = document.getElementById(buttonName);
	
	if (buttonTD) {
		if (value) {
			FTB_ButtonOver(buttonTD,ftbName,0,0);
		} else {
			FTB_ButtonOut(buttonTD,ftbName,0,0);
		}
	}
}