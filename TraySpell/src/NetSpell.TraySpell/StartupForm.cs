// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using NetSpell.SpellChecker;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StartupForm : System.Windows.Forms.Form
	{
		private ArrayList clipboardHistory = new ArrayList();
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MenuItem menuAbout;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuHistory;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuOptions;
		private System.Windows.Forms.MenuItem menuSpellCheck;
		
		private IntPtr NextClipboard;
		private Int16 AtomID;

		private NetSpell.SpellChecker.Spelling spelling;
		private System.Windows.Forms.NotifyIcon trayIcon;
		
		private System.Windows.Forms.ContextMenu trayMenu;
		private NetSpell.TraySpell.HotkeyForm hotKeyForm = new HotkeyForm();

		public StartupForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new StartupForm());
		}

		private void menuAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		}

		private void menuBuffer_Click(object sender, System.EventArgs e)
		{
			try
			{

				MenuItem menu = (MenuItem)sender;
				string tempData = (string)clipboardHistory[menu.Index];
				Clipboard.SetDataObject(tempData, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void menuExit_Click(object sender, System.EventArgs e)
		{
			try
			{
				trayIcon.Visible = false;

				// clean up api calls
				Win32.ChangeClipboardChain(this.Handle, this.NextClipboard);
				Win32.UnregisterHotKey(this.Handle, (Int32)AtomID);
				Win32.GlobalDeleteAtom(AtomID);			
			}
			finally
			{
				Application.Exit();
			}

		}

		private void menuOptions_Click(object sender, System.EventArgs e)
		{
			try
			{
				hotKeyForm.ShowDialog(this);
				if (hotKeyForm.DialogResult == DialogResult.OK) this.SetHotkey();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void menuSpellCheck_Click(object sender, System.EventArgs e)
		{
			try
			{
				IDataObject iData = Clipboard.GetDataObject();
				if(iData.GetDataPresent(DataFormats.Text)) 
				{
					this.spelling.Text = (string)iData.GetData(DataFormats.Text); 
					this.spelling.SpellCheck();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void spelling_EndOfText(object sender, System.EventArgs args)
		{
			try
			{
				Clipboard.SetDataObject(this.spelling.Text, true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void SetHotkey()
		{
			try
			{

				int modifiers = 0;

				if(hotKeyForm.ControlModifier) modifiers |= Win32.MOD_CONTROL;
				if(hotKeyForm.ShiftModifier) modifiers |= Win32.MOD_SHIFT;
				if(hotKeyForm.AltModifier) modifiers |= Win32.MOD_ALT;
			
				int result = Win32.RegisterHotKey(this.Handle, (Int32)AtomID, 
					modifiers, (Int32)hotKeyForm.HotKey);
				if(result == 0) throw new Win32Exception();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void StartupForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.Hide();
				this.NextClipboard = Win32.SetClipboardViewer(this.Handle);
				AtomID = Win32.GlobalAddAtom(DateTime.Now.ToString());
				if(AtomID == 0) throw new Win32Exception();
				this.SetHotkey();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void trayIcon_DoubleClick(object sender, System.EventArgs e)
		{
			menuSpellCheck_Click(sender, e);
		}

		private void trayMenu_Popup(object sender, System.EventArgs e)
		{
			try
			{
				menuHistory.Enabled = false;
				menuHistory.MenuItems.Clear();
				for (int i = 0; i < clipboardHistory.Count; i++)
				{
					string tempText = (string)clipboardHistory[i]; 
					if (tempText.Length > 30) 
					{
						int pos = tempText.LastIndexOf(" ", 30);
						if (pos < 1) pos = 26;
						tempText = tempText.Substring(0, pos) + " ...";
					}
					menuHistory.MenuItems.Add(tempText, new System.EventHandler(this.menuBuffer_Click));
				}
				menuHistory.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				this.trayIcon.Dispose(); 
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			try
			{
				switch (m.Msg)
				{
					case Win32.WM_CHANGECBCHAIN :
						Debug.WriteLine("Another clipboard viewer closed");
					
						// update clipboard chain
						if (this.NextClipboard == m.WParam) this.NextClipboard = m.LParam;
						else Win32.SendMessage(this.NextClipboard, m.Msg, m.WParam, m.LParam);

						break;
					case Win32.WM_HOTKEY :
						Debug.WriteLine("Hotkey Pressed");

						if (AtomID == (Int16)m.WParam)
						{
							this.menuSpellCheck_Click(this, new EventArgs());
						}
						break;
					case Win32.WM_DRAWCLIPBOARD :
					
						// add data object to history
						IDataObject iData = Clipboard.GetDataObject();
						if(iData.GetDataPresent(DataFormats.Text)) 
						{
							string tempData = (String)iData.GetData(DataFormats.Text);
							string lastData = "";

							Debug.WriteLine(string.Format("Clipboard Changed: {0}", tempData));
						
							if(clipboardHistory.Count > 0) 
							{
								lastData = (string)clipboardHistory[0];
							}

							if(tempData != lastData) 
							{
								clipboardHistory.Insert(0, tempData);
							}

							if(clipboardHistory.Count > 10)
							{
								clipboardHistory.RemoveRange(10, clipboardHistory.Count - 10);
							}
						}
					
						//sending message to next clipboard viewer
						Win32.SendMessage(this.NextClipboard, m.Msg, m.WParam, m.LParam);

						break;
					default :
						base.WndProc(ref m);
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartupForm));
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.trayMenu = new System.Windows.Forms.ContextMenu();
			this.menuSpellCheck = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuHistory = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuOptions = new System.Windows.Forms.MenuItem();
			this.menuAbout = new System.Windows.Forms.MenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			// 
			// trayMenu
			// 
			this.trayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuSpellCheck,
																					 this.menuItem2,
																					 this.menuHistory,
																					 this.menuItem3,
																					 this.menuExit,
																					 this.menuOptions,
																					 this.menuAbout});
			this.trayMenu.Popup += new System.EventHandler(this.trayMenu_Popup);
			// 
			// menuSpellCheck
			// 
			this.menuSpellCheck.Index = 0;
			this.menuSpellCheck.Text = "Spell Check Clipboard";
			this.menuSpellCheck.Click += new System.EventHandler(this.menuSpellCheck_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// menuHistory
			// 
			this.menuHistory.Index = 2;
			this.menuHistory.Text = "Clipboard History";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.Text = "-";
			// 
			// menuExit
			// 
			this.menuExit.Index = 4;
			this.menuExit.Text = "Exit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuOptions
			// 
			this.menuOptions.Index = 5;
			this.menuOptions.Text = "Options ...";
			this.menuOptions.Click += new System.EventHandler(this.menuOptions_Click);
			// 
			// menuAbout
			// 
			this.menuAbout.Index = 6;
			this.menuAbout.Text = "About ...";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenu = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Spell Check Clipboard";
			this.trayIcon.Visible = true;
			this.trayIcon.DoubleClick += new System.EventHandler(this.trayIcon_DoubleClick);
			// 
			// spelling
			// 
			this.spelling.IgnoreAllCapsWords = ((bool)(configurationAppSettings.GetValue("spelling.IgnoreAllCapsWords", typeof(bool))));
			this.spelling.IgnoreWordsWithDigits = ((bool)(configurationAppSettings.GetValue("spelling.IgnoreWordsWithDigits", typeof(bool))));
			this.spelling.MainDictionary = ((string)(configurationAppSettings.GetValue("spelling.MainDictionary", typeof(string))));
			this.spelling.MaxSuggestions = ((int)(configurationAppSettings.GetValue("spelling.MaxSuggestions", typeof(int))));
			this.spelling.UserDictionary = ((string)(configurationAppSettings.GetValue("spelling.UserDictionary", typeof(string))));
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			// 
			// StartupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(178, 78);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StartupForm";
			this.ShowInTaskbar = false;
			this.Text = "Tray Spell";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.StartupForm_Load);

		}
		#endregion

	}

}
