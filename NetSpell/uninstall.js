// shell object
var oShell = WScript.CreateObject("WScript.Shell");
var oFS = WScript.CreateObject("Scripting.FileSystemObject");
			
// setup netspell web demos
var oExec = oShell.Exec('cscript //job:webdir config.wsf /vdir:"NetSpell" /delete');			
while (oExec.Status == 0) { WScript.Sleep(100); }

// create shortcuts
var sShellFolder = oShell.SpecialFolders("Programs");					
var sShortcutPath = oFS.BuildPath(sShellFolder, "NetSpell");

if (oFS.FolderExists(sShortcutPath))
	oFS.DeleteFolder(sShortcutPath);
