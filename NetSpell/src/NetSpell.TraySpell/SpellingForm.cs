// Copyright (C) 2003  Paul Welter
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using NetSpell.SpellChecker;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for SpellingForm.
	/// </summary>
	public class SpellingForm : System.Windows.Forms.Form
	{

		private NetSpell.TraySpell.AboutForm _aboutForm = new AboutForm();
		private NetSpell.TraySpell.OptionForm _optionForm = new OptionForm();

		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Button CancelBtn;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button IgnoreAllButton;
		private System.Windows.Forms.Button IgnoreButton;
		private System.Windows.Forms.MenuItem menuAbout;
		private System.Windows.Forms.MenuItem menuExit;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuOptions;
		private System.Windows.Forms.MenuItem menuSpellCheck;
		private System.Windows.Forms.Button OptionsButton;
		private System.Windows.Forms.Button ReplaceAllButton;
		private System.Windows.Forms.Button ReplaceButton;
		private System.Windows.Forms.Label ReplaceLabel;
		private System.Windows.Forms.TextBox ReplacementWord;
		private System.Windows.Forms.StatusBar spellStatus;
		private System.Windows.Forms.StatusBarPanel statusPaneCount;
		private System.Windows.Forms.StatusBarPanel statusPaneIndex;
		private System.Windows.Forms.StatusBarPanel statusPaneWord;
		private System.Windows.Forms.Button SuggestButton;
		private System.Windows.Forms.ListBox SuggestionList;
		private System.Windows.Forms.Label SuggestionsLabel;
		private System.Windows.Forms.RichTextBox TextBeingChecked;
		private System.Windows.Forms.Label TextLabel;
		private System.Windows.Forms.NotifyIcon trayIcon;
		private System.Windows.Forms.ContextMenu trayMenu;

		internal static Settings settings;
		internal static Spelling spell;
		private int _AtomID = 0;

		public SpellingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.LoadSettings();

		}

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			_aboutForm.ShowDialog(this);
		}

		private void CancelBtn_Click(object sender, System.EventArgs e)
		{
			//this.HideForm();
		}

		private void HideForm()
		{
			this.Visible = false;
		}
		
		private void IgnoreAllButton_Click(object sender, System.EventArgs e)
		{
			spell.IgnoreAllWord();
			spell.SpellCheck();
		}

		private void IgnoreButton_Click(object sender, System.EventArgs e)
		{
			spell.IgnoreWord();
			spell.SpellCheck();
		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new SpellingForm());
		}

		private void menuAbout_Click(object sender, System.EventArgs e)
		{
			this._aboutForm.ShowDialog(this);
		}

		private void menuExit_Click(object sender, System.EventArgs e)
		{
			this.SaveSettings();
			Application.Exit();
		}

		private void menuOptions_Click(object sender, System.EventArgs e)
		{
			this._optionForm.ShowDialog(this);
		}

		private void menuSpellCheck_Click(object sender, System.EventArgs e)
		{
			this.startSpelling();
		}

		private void OptionsButton_Click(object sender, System.EventArgs e)
		{
			this._optionForm.ShowDialog(this);
		}

		private void ReplaceAllButton_Click(object sender, System.EventArgs e)
		{
			spell.ReplaceAllWord(this.ReplacementWord.Text);
			this.TextBeingChecked.Text = spell.Text;
			spell.SpellCheck();
		}

		private void ReplaceButton_Click(object sender, System.EventArgs e)
		{
			spell.ReplaceWord(this.ReplacementWord.Text);
			this.TextBeingChecked.Text = spell.Text;
			spell.SpellCheck();
		}

		private void ShowForm()
		{
			this.Visible = true;
			this.WindowState = FormWindowState.Normal;
			this.ShowInTaskbar = true;
		}

		private void SpellingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.HideForm();
		}

		private void SpellingForm_Load(object sender, System.EventArgs e)
		{
			this.AttachEvents();
			this.HideForm();
		}

		private void startSpelling()
		{
			IDataObject iData = Clipboard.GetDataObject();
			if(iData.GetDataPresent(DataFormats.Text)) 
			{
				string tempText = (String)iData.GetData(DataFormats.Text); 
				spell.Text = tempText.Replace("\r", "");
			}

			this.TextBeingChecked.Text = spell.Text;
			this.statusPaneWord.Text = "";
			this.statusPaneCount.Text = "Word: 0 of 0";
			this.statusPaneIndex.Text = "Index: 0";

			this.ShowForm();

			if(this.TextBeingChecked.Text.Length == 0) 
			{
				this.SuggestButton.Enabled = true;
				this.ReplacementWord.Focus();
			}
			else 
			{
				this.SuggestButton.Enabled = false;
				spell.SpellCheck();
			}

		}

		private void SuggestButton_Click(object sender, System.EventArgs e)
		{
			spell.Text = this.ReplacementWord.Text;
			this.TextBeingChecked.Text = this.ReplacementWord.Text;
			spell.SpellCheck();
		}

		private void SuggestionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.SuggestionList.SelectedIndex >= 0)
				this.ReplacementWord.Text = this.SuggestionList.SelectedItem.ToString();
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

		/// <summary>
		///     Handles Hotkey messages
		/// </summary>
		protected override void WndProc( ref Message m )
		{	
			switch(m.Msg)	
			{	
				case (int)Win32.Msgs.WM_HOTKEY:		
					this.startSpelling();
					break;	
			} 	
			base.WndProc(ref m );
		}

		internal void LoadSettings()
		{
			if (File.Exists("Settings.xml")) 
			{
				XmlSerializer serializer = new XmlSerializer(typeof(Settings));
				FileStream fs = new FileStream("Settings.xml", FileMode.Open);
				settings = (Settings) serializer.Deserialize(fs);
			}
			else 
			{
				settings = new Settings();
			}

			spell = new Spelling(settings.Dictionaries);
			spell.IgnoreAllCapsWords = settings.IgnoreAllCapsWords;
			spell.IgnoreWordsWithDigits = settings.IgnoreWordsWithDigits;

			_AtomID = Win32.Kernel32.GlobalAddAtom(DateTime.Now.ToString());
			Win32.User32.RegisterHotKey(this.Handle, _AtomID, 
				settings.HotKeyModifier, settings.HotKey);
		}

		internal void SaveSettings()
		{
			FileStream fs = new FileStream("Settings.xml", FileMode.Create, FileAccess.Write); 
			XmlWriter xw = new XmlTextWriter(fs, Encoding.UTF8);

			XmlSerializer serializer = new XmlSerializer(typeof(Settings));
			serializer.Serialize(xw, settings);

			xw.Close();
			fs.Close();

			Win32.User32.UnregisterHotKey(this.Handle, _AtomID);
			Win32.Kernel32.GlobalDeleteAtom(_AtomID);
			
		}

#region Spelling Events
		private void AttachEvents()
		{
			spell.MisspelledWord += new Spelling.MisspelledWordEventHandler(this.MisspelledWord);
			spell.DoubledWord += new Spelling.DoubledWordEventHandler(this.DoubleWord);
			spell.EndOfText += new Spelling.EndOfTextEventHandler(this.EndOfText);
		}

		private void DoubleWord(object sender, WordEventArgs args)
		{
			
		}

		private void EndOfText(object sender, EventArgs args)
		{
			Clipboard.SetDataObject(spell.Text, true);
			MessageBox.Show(this, "Spell Check Complete. The results have been placed on the Clipboard.", "Tray Spell", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.HideForm();
		}

		private void MisspelledWord(object sender, WordEventArgs args)
		{
			//reset text to black
			this.TextBeingChecked.SelectAll();
			this.TextBeingChecked.SelectionColor = Color.Black;
			//highlight current word
			this.TextBeingChecked.Select(args.TextIndex, args.Word.Length);
			this.TextBeingChecked.SelectionColor = Color.Red;
			//set caret and scroll window
			this.TextBeingChecked.Select(args.TextIndex, 0);
			this.TextBeingChecked.Focus();
			this.TextBeingChecked.ScrollToCaret();
			//update status bar
			this.statusPaneWord.Text = args.Word;
			this.statusPaneCount.Text = "Word: " + args.WordIndex.ToString() + 
				" of " + spell.WordCount.ToString();
			this.statusPaneIndex.Text = "Index: " + args.TextIndex.ToString();
			//generate suggestions
			spell.Suggest();
			//display suggestions
			this.SuggestionList.BeginUpdate();
			this.SuggestionList.SelectedIndex = -1;
			this.SuggestionList.Items.Clear();
			this.SuggestionList.Items.AddRange((string[])spell.Suggestions.ToArray(typeof(string)));
			this.SuggestionList.EndUpdate();
			//reset replacement word
			this.ReplacementWord.Text = string.Empty;
			this.ReplacementWord.Focus();
		}

#endregion

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SpellingForm));
			this.SuggestionList = new System.Windows.Forms.ListBox();
			this.ReplacementWord = new System.Windows.Forms.TextBox();
			this.ReplaceLabel = new System.Windows.Forms.Label();
			this.SuggestionsLabel = new System.Windows.Forms.Label();
			this.IgnoreButton = new System.Windows.Forms.Button();
			this.ReplaceButton = new System.Windows.Forms.Button();
			this.OptionsButton = new System.Windows.Forms.Button();
			this.ReplaceAllButton = new System.Windows.Forms.Button();
			this.IgnoreAllButton = new System.Windows.Forms.Button();
			this.AddButton = new System.Windows.Forms.Button();
			this.SuggestButton = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.TextBeingChecked = new System.Windows.Forms.RichTextBox();
			this.TextLabel = new System.Windows.Forms.Label();
			this.spellStatus = new System.Windows.Forms.StatusBar();
			this.statusPaneWord = new System.Windows.Forms.StatusBarPanel();
			this.statusPaneCount = new System.Windows.Forms.StatusBarPanel();
			this.statusPaneIndex = new System.Windows.Forms.StatusBarPanel();
			this.trayMenu = new System.Windows.Forms.ContextMenu();
			this.menuSpellCheck = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuExit = new System.Windows.Forms.MenuItem();
			this.menuOptions = new System.Windows.Forms.MenuItem();
			this.menuAbout = new System.Windows.Forms.MenuItem();
			this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			((System.ComponentModel.ISupportInitialize)(this.statusPaneWord)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneIndex)).BeginInit();
			this.SuspendLayout();
			// 
			// SuggestionList
			// 
			this.SuggestionList.Location = new System.Drawing.Point(8, 152);
			this.SuggestionList.Name = "SuggestionList";
			this.SuggestionList.Size = new System.Drawing.Size(336, 82);
			this.SuggestionList.TabIndex = 5;
			this.SuggestionList.SelectedIndexChanged += new System.EventHandler(this.SuggestionList_SelectedIndexChanged);
			// 
			// ReplacementWord
			// 
			this.ReplacementWord.Location = new System.Drawing.Point(8, 112);
			this.ReplacementWord.Name = "ReplacementWord";
			this.ReplacementWord.Size = new System.Drawing.Size(336, 20);
			this.ReplacementWord.TabIndex = 3;
			this.ReplacementWord.Text = "";
			// 
			// ReplaceLabel
			// 
			this.ReplaceLabel.AutoSize = true;
			this.ReplaceLabel.Location = new System.Drawing.Point(8, 96);
			this.ReplaceLabel.Name = "ReplaceLabel";
			this.ReplaceLabel.Size = new System.Drawing.Size(72, 13);
			this.ReplaceLabel.TabIndex = 2;
			this.ReplaceLabel.Text = "Replace &with:";
			// 
			// SuggestionsLabel
			// 
			this.SuggestionsLabel.AutoSize = true;
			this.SuggestionsLabel.Location = new System.Drawing.Point(8, 136);
			this.SuggestionsLabel.Name = "SuggestionsLabel";
			this.SuggestionsLabel.Size = new System.Drawing.Size(70, 13);
			this.SuggestionsLabel.TabIndex = 4;
			this.SuggestionsLabel.Text = "S&uggestions:";
			// 
			// IgnoreButton
			// 
			this.IgnoreButton.Location = new System.Drawing.Point(360, 24);
			this.IgnoreButton.Name = "IgnoreButton";
			this.IgnoreButton.TabIndex = 6;
			this.IgnoreButton.Text = "&Ignore";
			this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
			// 
			// ReplaceButton
			// 
			this.ReplaceButton.Location = new System.Drawing.Point(360, 136);
			this.ReplaceButton.Name = "ReplaceButton";
			this.ReplaceButton.TabIndex = 9;
			this.ReplaceButton.Text = "&Replace";
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// OptionsButton
			// 
			this.OptionsButton.Location = new System.Drawing.Point(264, 248);
			this.OptionsButton.Name = "OptionsButton";
			this.OptionsButton.TabIndex = 12;
			this.OptionsButton.Text = "&Options...";
			this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
			// 
			// ReplaceAllButton
			// 
			this.ReplaceAllButton.Location = new System.Drawing.Point(360, 168);
			this.ReplaceAllButton.Name = "ReplaceAllButton";
			this.ReplaceAllButton.TabIndex = 10;
			this.ReplaceAllButton.Text = "Replace A&ll";
			this.ReplaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			// 
			// IgnoreAllButton
			// 
			this.IgnoreAllButton.Location = new System.Drawing.Point(360, 56);
			this.IgnoreAllButton.Name = "IgnoreAllButton";
			this.IgnoreAllButton.TabIndex = 7;
			this.IgnoreAllButton.Text = "I&gnore All";
			this.IgnoreAllButton.Click += new System.EventHandler(this.IgnoreAllButton_Click);
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(360, 88);
			this.AddButton.Name = "AddButton";
			this.AddButton.TabIndex = 8;
			this.AddButton.Text = "&Add";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// SuggestButton
			// 
			this.SuggestButton.Location = new System.Drawing.Point(360, 200);
			this.SuggestButton.Name = "SuggestButton";
			this.SuggestButton.TabIndex = 11;
			this.SuggestButton.Text = "&Suggest";
			this.SuggestButton.Click += new System.EventHandler(this.SuggestButton_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new System.Drawing.Point(360, 248);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.TabIndex = 13;
			this.CancelBtn.Text = "&Cancel";
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// TextBeingChecked
			// 
			this.TextBeingChecked.BackColor = System.Drawing.SystemColors.Window;
			this.TextBeingChecked.Location = new System.Drawing.Point(8, 24);
			this.TextBeingChecked.Name = "TextBeingChecked";
			this.TextBeingChecked.ReadOnly = true;
			this.TextBeingChecked.Size = new System.Drawing.Size(336, 64);
			this.TextBeingChecked.TabIndex = 1;
			this.TextBeingChecked.TabStop = false;
			this.TextBeingChecked.Text = "";
			// 
			// TextLabel
			// 
			this.TextLabel.AutoSize = true;
			this.TextLabel.Location = new System.Drawing.Point(8, 8);
			this.TextLabel.Name = "TextLabel";
			this.TextLabel.Size = new System.Drawing.Size(109, 13);
			this.TextLabel.TabIndex = 0;
			this.TextLabel.Text = "Text Being Checked:";
			// 
			// spellStatus
			// 
			this.spellStatus.Location = new System.Drawing.Point(0, 288);
			this.spellStatus.Name = "spellStatus";
			this.spellStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						   this.statusPaneWord,
																						   this.statusPaneCount,
																						   this.statusPaneIndex});
			this.spellStatus.ShowPanels = true;
			this.spellStatus.Size = new System.Drawing.Size(450, 16);
			this.spellStatus.TabIndex = 14;
			// 
			// statusPaneWord
			// 
			this.statusPaneWord.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPaneWord.Width = 254;
			// 
			// statusPaneCount
			// 
			this.statusPaneCount.Text = "Word: 0 of 0";
			// 
			// statusPaneIndex
			// 
			this.statusPaneIndex.Text = "Index: 0";
			this.statusPaneIndex.Width = 80;
			// 
			// trayMenu
			// 
			this.trayMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuSpellCheck,
																					 this.menuItem2,
																					 this.menuExit,
																					 this.menuOptions,
																					 this.menuAbout});
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
			// menuExit
			// 
			this.menuExit.Index = 2;
			this.menuExit.Text = "Exit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuOptions
			// 
			this.menuOptions.Index = 3;
			this.menuOptions.Text = "Options ...";
			this.menuOptions.Click += new System.EventHandler(this.menuOptions_Click);
			// 
			// menuAbout
			// 
			this.menuAbout.Index = 4;
			this.menuAbout.Text = "About ...";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// trayIcon
			// 
			this.trayIcon.ContextMenu = this.trayMenu;
			this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
			this.trayIcon.Text = "Spell Check Clipboard";
			this.trayIcon.Visible = true;
			// 
			// SpellingForm
			// 
			this.AcceptButton = this.IgnoreButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(450, 304);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.spellStatus,
																		  this.TextLabel,
																		  this.SuggestionsLabel,
																		  this.ReplaceLabel,
																		  this.TextBeingChecked,
																		  this.CancelBtn,
																		  this.SuggestButton,
																		  this.ReplaceAllButton,
																		  this.IgnoreAllButton,
																		  this.OptionsButton,
																		  this.AddButton,
																		  this.ReplaceButton,
																		  this.IgnoreButton,
																		  this.ReplacementWord,
																		  this.SuggestionList});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SpellingForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tray Spell";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SpellingForm_Closing);
			this.Load += new System.EventHandler(this.SpellingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusPaneWord)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneIndex)).EndInit();
			this.ResumeLayout(false);

		}
#endregion

	}
}
