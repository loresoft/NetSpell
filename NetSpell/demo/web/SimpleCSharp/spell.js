var spellURL = "SpellCheck.aspx";

function checkSpelling() 
{
    if (window.showModalDialog) 
        var result = window.showModalDialog(spellURL + "?Modal=true", window, "dialogHeight:320px; dialogWidth:400px; edge:Raised; center:Yes; help:No; resizable:No; status:No; scroll: No");    
    else
        var newWindow = window.open(spellURL, "newWindow", "height=320,width=400,scrollbars=0,resizable=0,toolbars=0");
}


