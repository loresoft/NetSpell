// local path
var sPath = WScript.ScriptFullName;
sPath = sPath.substring(0, sPath.lastIndexOf(WScript.ScriptName)-1);

// shell object
var oShell = WScript.CreateObject("WScript.Shell");
var oFS = WScript.CreateObject("Scripting.FileSystemObject");
			
// setup netspell web demos
var oExec = oShell.Exec('cscript //job:webdir config.wsf /vdir:"NetSpell"');			
while (oExec.Status == 0) { WScript.Sleep(100); }

// create shortcuts
var sTarget = oFS.BuildPath(sPath, "bin/NetSpell.DictionaryBuild.exe");
oExec = oShell.Exec('cscript //job:shortcut config.wsf /name:"Dictionary Build" /path:"NetSpell" /target:"' + sTarget + '" /special:"Programs"');			
while (oExec.Status == 0) { WScript.Sleep(100); }

sTarget = oFS.BuildPath(sPath, "doc/NetSpell.chm");
oExec = oShell.Exec('cscript //job:shortcut config.wsf /name:"NetSpell Documentation" /path:"NetSpell" /target:"' + sTarget + '" /special:"Programs"');			
while (oExec.Status == 0) { WScript.Sleep(100); }

sTarget = "http://localhost/NetSpell/default.htm";
oExec = oShell.Exec('cscript //job:shortcut config.wsf /name:"NetSpell Demo Page" /path:"NetSpell" /target:"' + sTarget + '" /special:"Programs" /web');			
while (oExec.Status == 0) { WScript.Sleep(100); }
