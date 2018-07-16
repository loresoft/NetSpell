using System;
using System.IO;
using System.DirectoryServices;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

namespace InstallerActions
{
	/// <summary>
	/// Summary description for VirtualDirectoryInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class VirtualDirectoryInstaller : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string _iisserver = "localhost";
		private const string _apppath = "/LM/W3SVC/1/Root/";
		private string _dirpath = null;
		private string _vdirname = null;
		private bool _accessexecute = false;
		private bool _accessnoremoteexecute = false;
		private bool _accessnoremoteread = false;
		private bool _accessnoremotescript = false;
		private bool _accessnoremotewrite = false;
		private bool _accessread = true;
		private bool _accesssource = false;
		private bool _accessscript = true;
		private bool _accessssl = false;
		private bool _accessssl128 = false;
		private bool _accesssslmapcert = false;
		private bool _accesssslnegotiatecert = false;
		private bool _accesssslrequirecert = false;
		private bool _accesswrite = false;
		private bool _anonymouspasswordsync = true;
		private bool _appallowclientdebug = false;
		private bool _appallowdebugging = false;
		private bool _aspallowsessionstate = true;
		private bool _aspbufferingon = true;
		private bool _aspenableapplicationrestart = true;
		private bool _aspenableasphtmlfallback = false;
		private bool _aspenablechunkedencoding = true;
		private bool _asperrorstontlog = false;
		private bool _aspenableparentpaths = true;
		private bool _aspenabletypelibcache = true;
		private bool _aspexceptioncatchenable = true;
		private bool _asplogerrorrequests = true;
		private bool _aspscripterrorsenttobrowser = true;
		private bool _aspthreadgateenabled = false;
		private bool _asptrackthreadingmodel = false;
		private bool _authanonymous = true;
		private bool _authbasic = false;
		private bool _authntlm = false;
		private bool _authpersistsinglerequest = false;
		private bool _authpersistsinglerequestifproxy = true;
		private bool _authpersistsinglerequestalwaysifproxy = false;
		private bool _cachecontrolnocache = false;
		private bool _cacheisapi = true;
		private bool _contentindexed = true;
		private bool _cpuappenabled = true;
		private bool _cpucgienabled = true;
		private bool _createcgiwithnewconsole = false;
		private bool _createprocessasuser = true;
		private bool _dirbrowseshowdate = true;
		private bool _dirbrowseshowextension = true;
		private bool _dirbrowseshowlongdate = true;
		private bool _dirbrowseshowsize = true;
		private bool _dirbrowseshowtime = true;
		private bool _dontlog = false;
		private bool _enabledefaultdoc = true;
		private bool _enabledirbrowsing = false;
		private bool _enabledocfooter = false;
		private bool _enablereversedns = false;
		private bool _ssiexecdisable = false;
		private bool _uncauthenticationpassthrough = false;
		private string _aspscripterrormessage = "An error occurred on the server when processing the URL.  Please contact the system administrator.";
		private string _defaultdoc = "Default.htm,Default.asp,index.htm,iisstart.asp,Default.aspx";

		public VirtualDirectoryInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();
		}
		// Required
		/// <summary>The file system path</summary>
		public string dirpath
		{
			get { return _dirpath; }
			set { _dirpath = value; }
		}

		/// <summary>Name of the IIS Virtual Directory</summary>
		public string vdirname
		{
			get { return _vdirname; }
			set { _vdirname = value; }
		}

		// Optional
		/// <summary>The IIS server.  Defaults to localhost.</summary>
		public string iisserver
		{
			get { return _iisserver; }
			set { _iisserver = value; }
		}

		
		public bool accessexecute
		{
			get { return _accessexecute; }
			set { _accessexecute = value; }
		}
	
		
		public bool accessnoremoteexecute
		{
			get { return _accessnoremoteexecute; }
			set { _accessnoremoteexecute = value; }
		}

		
		public bool accessnoremoteread
		{
			get { return _accessnoremoteread; }
			set { _accessnoremoteread = value; }
		}

		
		public bool accessnoremotescript
		{
			get { return _accessnoremotescript; }
			set { _accessnoremotescript = value; }
		}

		
		public bool accessnoremotewrite
		{
			get { return _accessnoremotewrite; }
			set { _accessnoremotewrite = value; }
		}

		
		public bool accessread
		{
			get { return _accessread; }
			set { _accessread = value; }
		}

		
		public bool accesssource
		{
			get { return _accesssource; }
			set { _accesssource = value; }
		}

		
		public bool accessscript
		{
			get { return _accessscript; }
			set { _accessscript = value; }
		}

		
		public bool accessssl
		{
			get { return _accessssl; }
			set { _accessssl = value; }
		}

		
		public bool accessssl128
		{
			get { return _accessssl128; }
			set { _accessssl128 = value; }
		}

		
		public bool accesssslmapcert
		{
			get { return _accesssslmapcert; }
			set { _accesssslmapcert = value; }
		}

		
		public bool accesssslnegotiatecert
		{
			get { return _accesssslnegotiatecert; }
			set { _accesssslnegotiatecert = value; }
		}

		
		public bool accesssslrequirecert
		{
			get { return _accesssslrequirecert; }
			set { _accesssslrequirecert = value; }
		}

		
		public bool accesswrite
		{
			get { return _accesswrite; }
			set { _accesswrite = value; }
		}

		
		public bool anonymouspasswordsync
		{
			get { return _anonymouspasswordsync; }
			set { _anonymouspasswordsync = value; }
		}
      
		
		public bool appallowclientdebug
		{
			get { return _appallowclientdebug; }
			set { _appallowclientdebug = value; }
		}

		
		public bool appallowdebugging
		{
			get { return _appallowdebugging; }
			set { _appallowdebugging = value; }
		}
      
		
		public bool aspallowsessionstate
		{
			get { return _aspallowsessionstate; }
			set { _aspallowsessionstate = value; }
		}

		
		public bool aspbufferingon
		{
			get { return _aspbufferingon; }
			set { _aspbufferingon = value; }
		}
      
		
		public bool aspenableapplicationrestart
		{
			get { return _aspenableapplicationrestart; }
			set { _aspenableapplicationrestart = value; }
		}

		
		public bool aspenableasphtmlfallback
		{
			get { return _aspenableasphtmlfallback; }
			set { _aspenableasphtmlfallback = value; }
		}
      
		
		public bool aspenablechunkedencoding
		{
			get { return _aspenablechunkedencoding; }
			set { _aspenablechunkedencoding = value; }
		}

		
		public bool asperrorstontlog
		{
			get { return _asperrorstontlog; }
			set { _asperrorstontlog = value; }
		}

		
		public bool aspenableparentpaths
		{
			get { return _aspenableparentpaths; }
			set { _aspenableparentpaths = value; }
		}
      
		
		public bool aspenabletypelibcache
		{
			get { return _aspenabletypelibcache; }
			set { _aspenabletypelibcache = value; }
		}

		
		public bool aspexceptioncatchenable
		{
			get { return _aspexceptioncatchenable; }
			set { _aspexceptioncatchenable = value; }
		}
      
		
		public bool asplogerrorrequests
		{
			get { return _asplogerrorrequests; }
			set { _asplogerrorrequests = value; }
		}

		
		public bool aspscripterrorsenttobrowser
		{
			get { return _aspscripterrorsenttobrowser; }
			set { _aspscripterrorsenttobrowser = value; }
		}
      
		
		public bool aspthreadgateenabled
		{
			get { return _aspthreadgateenabled; }
			set { _aspthreadgateenabled = value; }
		}
      
		
		public bool asptrackthreadingmodel
		{
			get { return _asptrackthreadingmodel; }
			set { _asptrackthreadingmodel = value; }
		}

		
		public bool authanonymous
		{
			get { return _authanonymous; }
			set { _authanonymous = value; }
		}

		
		public bool authbasic
		{
			get { return _authbasic; }
			set { _authbasic = value; }
		}
      
		
		public bool authntlm
		{
			get { return _authntlm; }
			set { _authntlm = value; }
		}

		
		public bool authpersistsinglerequest
		{
			get { return _authpersistsinglerequest; }
			set { _authpersistsinglerequest = value; }
		}

		
		public bool authpersistsinglerequestifproxy
		{
			get { return _authpersistsinglerequestifproxy; }
			set { _authpersistsinglerequestifproxy = value; }
		}

		
		public bool authpersistsinglerequestalwaysifproxy
		{
			get { return _authpersistsinglerequestalwaysifproxy; }
			set { _authpersistsinglerequestalwaysifproxy = value; }
		}
            
		
		public bool cachecontrolnocache
		{
			get { return _cachecontrolnocache; }
			set { _cachecontrolnocache = value; }
		}

		
		public bool cacheisapi
		{
			get { return _cacheisapi; }
			set { _cacheisapi = value; }
		}

		
		public bool contentindexed
		{
			get { return _contentindexed; }
			set { _contentindexed = value; }
		}
      
		
		public bool cpuappenabled
		{
			get { return _cpuappenabled; }
			set { _cpuappenabled = value; }
		}
      
		
		public bool cpucgienabled
		{
			get { return _cpucgienabled; }
			set { _cpucgienabled = value; }
		}
      
		
		public bool createcgiwithnewconsole
		{
			get { return _createcgiwithnewconsole; }
			set { _createcgiwithnewconsole = value; }
		}
      
		
		public bool createprocessasuser
		{
			get { return _createprocessasuser; }
			set { _createprocessasuser = value; }
		}
      
		
		public bool dirbrowseshowdate
		{
			get { return _dirbrowseshowdate; }
			set { _dirbrowseshowdate = value; }
		}
      
		
		public bool dirbrowseshowextension
		{
			get { return _dirbrowseshowextension; }
			set { _dirbrowseshowextension = value; }
		}

		
		public bool dirbrowseshowlongdate
		{
			get { return _dirbrowseshowlongdate; }
			set { _dirbrowseshowlongdate = value; }
		}
      
		
		public bool dirbrowseshowsize
		{
			get { return _dirbrowseshowsize; }
			set { _dirbrowseshowsize = value; }
		}
 
		
		public bool dirbrowseshowtime
		{
			get { return _dirbrowseshowtime; }
			set { _dirbrowseshowtime = value; }
		}

		
		public bool dontlog
		{
			get { return _dontlog; }
			set { _dontlog = value; }
		}

		
		public bool enabledefaultdoc
		{
			get { return _enabledefaultdoc; }
			set { _enabledefaultdoc = value; }
		}

		
		public bool enabledirbrowsing
		{
			get { return _enabledirbrowsing; }
			set { _enabledirbrowsing = value; }
		}

		
		public bool enabledocfooter
		{
			get { return _enabledocfooter; }
			set { _enabledocfooter = value; }
		}

		
		public bool enablereversedns
		{
			get { return _enablereversedns; }
			set { _enablereversedns = value; }
		}

		
		public bool ssiexecdisable
		{
			get { return _ssiexecdisable; }
			set { _ssiexecdisable = value; }
		}

		
		public bool uncauthenticationpassthrough
		{
			get { return _uncauthenticationpassthrough; }
			set { _uncauthenticationpassthrough = value; }
		}

		
		public string aspscripterrormessage
		{
			get { return _aspscripterrormessage; }
			set { _aspscripterrormessage = value; }
		}

		
		public string defaultdoc
		{
			get { return _defaultdoc; }
			set { _defaultdoc = value; }
		}

		private string iispath 
		{
			get { return "IIS://" + iisserver + "/W3SVC/1/Root" ;}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	
		public override void Install(IDictionary stateSaver)
		{
			base.Install (stateSaver);
			this.dirpath = base.Context.Parameters["DIRPATH"];
			this.vdirname = base.Context.Parameters["VDIRNAME"];

			this.CreateVirtualDirectory();
			
		}
	
		public override void Rollback(IDictionary savedState)
		{
			this.vdirname = base.Context.Parameters["VDIRNAME"];
			this.DeleteVirtualDirectory();
			base.Rollback (savedState);
		}
	
		public override void Uninstall(IDictionary savedState)
		{
			this.vdirname = base.Context.Parameters["VDIRNAME"];
			this.DeleteVirtualDirectory();
			base.Uninstall (savedState);
		}

		public void CreateVirtualDirectory()
		{
			try
			{
				DirectoryEntry folderRoot = new DirectoryEntry(iispath);
				folderRoot.RefreshCache();
				DirectoryEntry newVirDir;

				try
				{
					// Try to find the directory
					DirectoryEntry tempVirDir = folderRoot.Children.Find(_vdirname,folderRoot.SchemaClassName);
					newVirDir = tempVirDir;
				}
				catch
				{
					// If the directory doesn't exist create it.
					newVirDir = folderRoot.Children.Add(_vdirname,folderRoot.SchemaClassName);
					newVirDir.CommitChanges();
				}

				string fullPath = Path.GetFullPath(_dirpath);

				// Set Required Properties
				newVirDir.Properties["Path"].Value = fullPath;
				newVirDir.Properties["AppFriendlyName"].Value = _vdirname;
				newVirDir.Properties["AppRoot"].Value = _apppath + _vdirname;

				// Set Optional Properties
				newVirDir.Properties["AccessExecute"][0] = _accessexecute;
				newVirDir.Properties["AccessNoRemoteExecute"][0] = _accessnoremoteexecute;
				newVirDir.Properties["AccessNoRemoteRead"][0] = _accessnoremoteread;
				newVirDir.Properties["AccessNoRemoteScript"][0] = _accessnoremotescript;
				newVirDir.Properties["AccessNoRemoteWrite"][0] = _accessnoremotewrite;
				newVirDir.Properties["AccessRead"][0] = _accessread;
				newVirDir.Properties["AccessSource"][0] = _accesssource;
				newVirDir.Properties["AccessScript"][0] = _accessscript;
				newVirDir.Properties["AccessSSL"][0] = _accessssl;
				newVirDir.Properties["AccessSSL128"][0] = _accessssl128;
				newVirDir.Properties["AccessSSLMapCert"][0] = _accesssslmapcert;
				newVirDir.Properties["AccessSSLNegotiateCert"][0] = _accesssslnegotiatecert;
				newVirDir.Properties["AccessSSLRequireCert"][0] = _accesssslrequirecert;
				newVirDir.Properties["AccessWrite"][0] = _accesswrite;
				newVirDir.Properties["AnonymousPasswordSync"][0] = _anonymouspasswordsync;
				newVirDir.Properties["AppAllowClientDebug"][0] = _appallowclientdebug;
				newVirDir.Properties["AppAllowDebugging"][0] = _appallowdebugging;
				newVirDir.Properties["AspBufferingOn"][0] = _aspbufferingon;
				newVirDir.Properties["AspEnableApplicationRestart"][0] = _aspenableapplicationrestart;
				newVirDir.Properties["AspEnableAspHtmlFallback"][0] = _aspenableasphtmlfallback;
				newVirDir.Properties["AspEnableChunkedEncoding"][0] = _aspenablechunkedencoding;
				newVirDir.Properties["AspErrorsToNTLog"][0] = _asperrorstontlog;
				newVirDir.Properties["AspEnableParentPaths"][0] = _aspenableparentpaths;
				newVirDir.Properties["AspEnableTypelibCache"][0] = _aspenabletypelibcache;
				newVirDir.Properties["AspExceptionCatchEnable"][0] = _aspexceptioncatchenable;
				newVirDir.Properties["AspLogErrorRequests"][0] = _asplogerrorrequests;
				newVirDir.Properties["AspScriptErrorSentToBrowser"][0] = _aspscripterrorsenttobrowser;
				newVirDir.Properties["AspThreadGateEnabled"][0] = _aspthreadgateenabled;
				newVirDir.Properties["AspTrackThreadingModel"][0] = _asptrackthreadingmodel;
				newVirDir.Properties["AuthAnonymous"][0] = _authanonymous;
				newVirDir.Properties["AuthBasic"][0] = _authbasic;
				newVirDir.Properties["AuthNTLM"][0] = _authntlm;
				newVirDir.Properties["AuthPersistSingleRequest"][0] = _authpersistsinglerequest;
				newVirDir.Properties["AuthPersistSingleRequestIfProxy"][0] = _authpersistsinglerequestifproxy;
				newVirDir.Properties["AuthPersistSingleRequestAlwaysIfProxy"][0] = _authpersistsinglerequestalwaysifproxy;
				newVirDir.Properties["CacheControlNoCache"][0] = _cachecontrolnocache;
				newVirDir.Properties["CacheISAPI"][0] = _cacheisapi;
				newVirDir.Properties["ContentIndexed"][0] = _contentindexed;
				newVirDir.Properties["CpuAppEnabled"][0] = _cpuappenabled;
				newVirDir.Properties["CpuCgiEnabled"][0] = _cpucgienabled;
				newVirDir.Properties["CreateCGIWithNewConsole"][0] = _createcgiwithnewconsole;
				newVirDir.Properties["CreateProcessAsUser"][0] = _createprocessasuser;
				newVirDir.Properties["DirBrowseShowDate"][0] = _dirbrowseshowdate;
				newVirDir.Properties["DirBrowseShowExtension"][0] = _dirbrowseshowextension;
				newVirDir.Properties["DirBrowseShowLongDate"][0] = _dirbrowseshowlongdate;
				newVirDir.Properties["DirBrowseShowSize"][0] = _dirbrowseshowsize;
				newVirDir.Properties["DirBrowseShowTime"][0] = _dirbrowseshowtime;
				newVirDir.Properties["DontLog"][0] = _dontlog;
				newVirDir.Properties["EnableDefaultDoc"][0] = _enabledefaultdoc;
				newVirDir.Properties["EnableDirBrowsing"][0] = _enabledirbrowsing;
				newVirDir.Properties["EnableDocFooter"][0] = _enabledocfooter;
				newVirDir.Properties["EnableReverseDns"][0] = _enablereversedns;
				newVirDir.Properties["SSIExecDisable"][0] = _ssiexecdisable;
				newVirDir.Properties["UNCAuthenticationPassthrough"][0] = _uncauthenticationpassthrough;
				newVirDir.Properties["AspScriptErrorMessage"][0] = _aspscripterrormessage;
				newVirDir.Properties["DefaultDoc"][0] = _defaultdoc;


				// Save Changes
				newVirDir.CommitChanges();
				folderRoot.CommitChanges();
				newVirDir.Close();
				folderRoot.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show(string.Format("Error creating virtual directory: {0}", e.Message), "Error", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		public void DeleteVirtualDirectory()
		{
			try
			{
				DirectoryEntry folderRoot = new DirectoryEntry(iispath);
				DirectoryEntries rootEntries = folderRoot.Children;
				folderRoot.RefreshCache();
				DirectoryEntry childVirDir = folderRoot.Children.Find(_vdirname,folderRoot.SchemaClassName);
	      
				rootEntries.Remove(childVirDir);
	  
				childVirDir.Close();
				folderRoot.Close();
			}
			catch (Exception e)
			{
				System.Diagnostics.Trace.Write(e.ToString());
			}
		}
	}
}
