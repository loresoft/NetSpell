// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using NetSpell.SpellChecker;

namespace NetSpell.Demo.Windows
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TextForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem aboutMenu;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton copyBarButton;
		private System.Windows.Forms.MenuItem copyMenu;
		private System.Windows.Forms.TextBox currentText;
		private System.Windows.Forms.ToolBarButton cutBarButton;
		private System.Windows.Forms.MenuItem cutMenu;

		private bool DocumentChanged = false;
		private System.Windows.Forms.MenuItem editMenu;
		private System.Windows.Forms.ToolBar editToolBar;
		private System.Windows.Forms.MenuItem exitMenu;
		private System.Windows.Forms.MenuItem fileMenu;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.MenuItem fontMenu;
		private System.Windows.Forms.MenuItem formatMenu;
		private System.Windows.Forms.MenuItem helpMenu;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.ToolBarButton newBarButton;
		private System.Windows.Forms.MenuItem newMenu;
		private System.Windows.Forms.ToolBarButton openBarButton;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.MenuItem openMenu;
		private System.Windows.Forms.ToolBarButton pasteBarButton;
		private System.Windows.Forms.MenuItem pasteMenu;
		private System.Windows.Forms.MenuItem saveAsMenu;
		private System.Windows.Forms.ToolBarButton saveBarButton;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private System.Windows.Forms.MenuItem saveMenu;
		private System.Windows.Forms.MenuItem selectAllMenu;
		private System.Windows.Forms.ToolBarButton spellBarButton;
		private NetSpell.SpellChecker.Spelling spelling;
		private System.Windows.Forms.MenuItem spellingMenu;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ToolBarButton toolBarButton11;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton8;
		private System.Windows.Forms.ImageList toolBarImages;
		private System.Windows.Forms.MenuItem toolsMenu;
		private System.Windows.Forms.ToolBarButton undoBarButton;
		private System.Windows.Forms.MenuItem undoMenu;
		private System.Windows.Forms.MenuItem wordWrapMenu;
		
		public TextForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void aboutMenu_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);

		}

		private void copyMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.Copy();
		}

		private void currentText_TextChanged(object sender, System.EventArgs e)
		{
			if (!this.DocumentChanged) 
			{
				this.DocumentChanged = true;
				this.statusBar.Text += "*";
			}
		}

		private void cutMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.Cut();
		}


		private void editToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			switch (e.Button.ToolTipText)
			{
				case "New" :
					this.newMenu_Click(sender, new EventArgs());
					break;
				case "Open" :
					this.openMenu_Click(sender, new EventArgs());
					break;
				case "Save" :
					this.saveMenu_Click(sender, new EventArgs());
					break;
				case "Cut" :
					this.cutMenu_Click(sender, new EventArgs());
					break;
				case "Copy" :
					this.copyMenu_Click(sender, new EventArgs());
					break;
				case "Paste" :
					this.pasteMenu_Click(sender, new EventArgs());
					break;
				case "Undo" :
					this.undoMenu_Click(sender, new EventArgs());
					break;
				case "Spelling" :
					this.spellingMenu_Click(sender, new EventArgs());
					break;
			}
		}

		private void exitMenu_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void fontMenu_Click(object sender, System.EventArgs e)
		{
			this.fontDialog.Font = this.currentText.Font;
			if (this.fontDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.currentText.Font = this.fontDialog.Font;
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TextForm());
		}

		private void newMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.Clear();
		}

		private void openMenu_Click(object sender, System.EventArgs e)
		{
			if (this.openDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo fi = new FileInfo(this.openDialog.FileName);
				StreamReader sr = fi.OpenText();
				this.currentText.Text = sr.ReadToEnd().ToString();
				sr.Close();
				this.DocumentChanged = false;
				this.saveDialog.FileName = this.openDialog.FileName;
				this.statusBar.Text = this.saveDialog.FileName;
			}

		}

		private void pasteMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.Paste();
		}

		private void saveAsMenu_Click(object sender, System.EventArgs e)
		{
			if (this.saveDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileInfo fi = new FileInfo(this.saveDialog.FileName);
				StreamWriter sr = fi.CreateText();
				sr.WriteLine(this.currentText.Text);
				sr.Close();
				this.statusBar.Text = this.saveDialog.FileName;
			}
		}

		private void saveMenu_Click(object sender, System.EventArgs e)
		{
			if (this.saveDialog.FileName.Length == 0 || this.saveDialog.FileName == "untitled") 
			{
				this.saveAsMenu_Click(sender, e);
			}
			else 
			{
				FileInfo fi = new FileInfo(this.saveDialog.FileName);
				StreamWriter sr = fi.CreateText();
				sr.WriteLine(this.currentText.Text);
				sr.Close();
				this.statusBar.Text = this.saveDialog.FileName;
			}
		}

		private void selectAllMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.SelectAll();
		}

#region Spell Checker Events
		private void spelling_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.currentText.Text = this.spelling.Text;
		}

		private void spelling_EndOfText(object sender, System.EventArgs args)
		{
			this.currentText.Text = this.spelling.Text;
		}

		private void spelling_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.currentText.Text = this.spelling.Text;
		}
#endregion //Spell Checker Events

		private void spellingMenu_Click(object sender, System.EventArgs e)
		{
			this.spelling.SpellCheck(this.currentText.Text);
		}

		private void TextForm_Load(object sender, System.EventArgs e)
		{
			// the following prevents the Spelling form from being hiden
			this.spelling.SpellingForm.Owner = this;
			this.spelling.Dictionary.DictionaryFile = @"..\..\..\Dictionaries\en_US.dic";
			this.spelling.Dictionary.Initialize();
			
		}

		private void undoMenu_Click(object sender, System.EventArgs e)
		{
			this.currentText.Undo();
		}

		private void wordWrapMenu_Click(object sender, System.EventArgs e)
		{
			if (wordWrapMenu.Checked) 
				wordWrapMenu.Checked = false;
			else 
				wordWrapMenu.Checked = true;
		
			this.currentText.WordWrap = wordWrapMenu.Checked;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TextForm));
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.fileMenu = new System.Windows.Forms.MenuItem();
			this.newMenu = new System.Windows.Forms.MenuItem();
			this.openMenu = new System.Windows.Forms.MenuItem();
			this.saveMenu = new System.Windows.Forms.MenuItem();
			this.saveAsMenu = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.exitMenu = new System.Windows.Forms.MenuItem();
			this.editMenu = new System.Windows.Forms.MenuItem();
			this.undoMenu = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.cutMenu = new System.Windows.Forms.MenuItem();
			this.copyMenu = new System.Windows.Forms.MenuItem();
			this.pasteMenu = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.selectAllMenu = new System.Windows.Forms.MenuItem();
			this.formatMenu = new System.Windows.Forms.MenuItem();
			this.wordWrapMenu = new System.Windows.Forms.MenuItem();
			this.fontMenu = new System.Windows.Forms.MenuItem();
			this.toolsMenu = new System.Windows.Forms.MenuItem();
			this.spellingMenu = new System.Windows.Forms.MenuItem();
			this.helpMenu = new System.Windows.Forms.MenuItem();
			this.aboutMenu = new System.Windows.Forms.MenuItem();
			this.editToolBar = new System.Windows.Forms.ToolBar();
			this.newBarButton = new System.Windows.Forms.ToolBarButton();
			this.openBarButton = new System.Windows.Forms.ToolBarButton();
			this.saveBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.cutBarButton = new System.Windows.Forms.ToolBarButton();
			this.copyBarButton = new System.Windows.Forms.ToolBarButton();
			this.pasteBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
			this.undoBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton11 = new System.Windows.Forms.ToolBarButton();
			this.spellBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.currentText = new System.Windows.Forms.TextBox();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.fileMenu,
																					 this.editMenu,
																					 this.formatMenu,
																					 this.toolsMenu,
																					 this.helpMenu});
			// 
			// fileMenu
			// 
			this.fileMenu.Index = 0;
			this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.newMenu,
																					 this.openMenu,
																					 this.saveMenu,
																					 this.saveAsMenu,
																					 this.menuItem11,
																					 this.exitMenu});
			this.fileMenu.Text = "File";
			// 
			// newMenu
			// 
			this.newMenu.Index = 0;
			this.newMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.newMenu.Text = "New";
			this.newMenu.Click += new System.EventHandler(this.newMenu_Click);
			// 
			// openMenu
			// 
			this.openMenu.Index = 1;
			this.openMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.openMenu.Text = "Open...";
			this.openMenu.Click += new System.EventHandler(this.openMenu_Click);
			// 
			// saveMenu
			// 
			this.saveMenu.Index = 2;
			this.saveMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.saveMenu.Text = "Save";
			this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
			// 
			// saveAsMenu
			// 
			this.saveAsMenu.Index = 3;
			this.saveAsMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.saveAsMenu.Text = "Save As...";
			this.saveAsMenu.Click += new System.EventHandler(this.saveAsMenu_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 4;
			this.menuItem11.Text = "-";
			// 
			// exitMenu
			// 
			this.exitMenu.Index = 5;
			this.exitMenu.Text = "Exit";
			this.exitMenu.Click += new System.EventHandler(this.exitMenu_Click);
			// 
			// editMenu
			// 
			this.editMenu.Index = 1;
			this.editMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.undoMenu,
																					 this.menuItem14,
																					 this.cutMenu,
																					 this.copyMenu,
																					 this.pasteMenu,
																					 this.menuItem18,
																					 this.selectAllMenu});
			this.editMenu.Text = "Edit";
			// 
			// undoMenu
			// 
			this.undoMenu.Index = 0;
			this.undoMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.undoMenu.Text = "Undo";
			this.undoMenu.Click += new System.EventHandler(this.undoMenu_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 1;
			this.menuItem14.Text = "-";
			// 
			// cutMenu
			// 
			this.cutMenu.Index = 2;
			this.cutMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.cutMenu.Text = "Cut";
			this.cutMenu.Click += new System.EventHandler(this.cutMenu_Click);
			// 
			// copyMenu
			// 
			this.copyMenu.Index = 3;
			this.copyMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.copyMenu.Text = "Copy";
			this.copyMenu.Click += new System.EventHandler(this.copyMenu_Click);
			// 
			// pasteMenu
			// 
			this.pasteMenu.Index = 4;
			this.pasteMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.pasteMenu.Text = "Paste";
			this.pasteMenu.Click += new System.EventHandler(this.pasteMenu_Click);
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 5;
			this.menuItem18.Text = "-";
			// 
			// selectAllMenu
			// 
			this.selectAllMenu.Index = 6;
			this.selectAllMenu.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.selectAllMenu.Text = "Select All";
			this.selectAllMenu.Click += new System.EventHandler(this.selectAllMenu_Click);
			// 
			// formatMenu
			// 
			this.formatMenu.Index = 2;
			this.formatMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.wordWrapMenu,
																					   this.fontMenu});
			this.formatMenu.Text = "Format";
			// 
			// wordWrapMenu
			// 
			this.wordWrapMenu.Checked = true;
			this.wordWrapMenu.DefaultItem = true;
			this.wordWrapMenu.Index = 0;
			this.wordWrapMenu.Text = "Word Wrap";
			this.wordWrapMenu.Click += new System.EventHandler(this.wordWrapMenu_Click);
			// 
			// fontMenu
			// 
			this.fontMenu.Index = 1;
			this.fontMenu.Text = "Font...";
			this.fontMenu.Click += new System.EventHandler(this.fontMenu_Click);
			// 
			// toolsMenu
			// 
			this.toolsMenu.Index = 3;
			this.toolsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.spellingMenu});
			this.toolsMenu.Text = "Tools";
			// 
			// spellingMenu
			// 
			this.spellingMenu.Index = 0;
			this.spellingMenu.Shortcut = System.Windows.Forms.Shortcut.F7;
			this.spellingMenu.Text = "Spelling...";
			this.spellingMenu.Click += new System.EventHandler(this.spellingMenu_Click);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 4;
			this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.aboutMenu});
			this.helpMenu.Text = "Help";
			// 
			// aboutMenu
			// 
			this.aboutMenu.Index = 0;
			this.aboutMenu.Text = "About";
			this.aboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
			// 
			// editToolBar
			// 
			this.editToolBar.AutoSize = false;
			this.editToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						   this.newBarButton,
																						   this.openBarButton,
																						   this.saveBarButton,
																						   this.toolBarButton4,
																						   this.cutBarButton,
																						   this.copyBarButton,
																						   this.pasteBarButton,
																						   this.toolBarButton8,
																						   this.undoBarButton,
																						   this.toolBarButton11,
																						   this.spellBarButton});
			this.editToolBar.ButtonSize = new System.Drawing.Size(24, 24);
			this.editToolBar.DropDownArrows = true;
			this.editToolBar.ImageList = this.toolBarImages;
			this.editToolBar.Location = new System.Drawing.Point(0, 0);
			this.editToolBar.Name = "editToolBar";
			this.editToolBar.ShowToolTips = true;
			this.editToolBar.Size = new System.Drawing.Size(608, 32);
			this.editToolBar.TabIndex = 0;
			this.editToolBar.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.editToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.editToolBar_ButtonClick);
			// 
			// newBarButton
			// 
			this.newBarButton.ImageIndex = 4;
			this.newBarButton.ToolTipText = "New";
			// 
			// openBarButton
			// 
			this.openBarButton.ImageIndex = 5;
			this.openBarButton.ToolTipText = "Open";
			// 
			// saveBarButton
			// 
			this.saveBarButton.ImageIndex = 8;
			this.saveBarButton.ToolTipText = "Save";
			// 
			// toolBarButton4
			// 
			this.toolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// cutBarButton
			// 
			this.cutBarButton.ImageIndex = 1;
			this.cutBarButton.ToolTipText = "Cut";
			// 
			// copyBarButton
			// 
			this.copyBarButton.ImageIndex = 0;
			this.copyBarButton.ToolTipText = "Copy";
			// 
			// pasteBarButton
			// 
			this.pasteBarButton.ImageIndex = 6;
			this.pasteBarButton.ToolTipText = "Paste";
			// 
			// toolBarButton8
			// 
			this.toolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// undoBarButton
			// 
			this.undoBarButton.ImageIndex = 10;
			this.undoBarButton.ToolTipText = "Undo";
			// 
			// toolBarButton11
			// 
			this.toolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// spellBarButton
			// 
			this.spellBarButton.ImageIndex = 9;
			this.spellBarButton.ToolTipText = "Spelling";
			// 
			// toolBarImages
			// 
			this.toolBarImages.ImageSize = new System.Drawing.Size(16, 16);
			this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
			this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// saveDialog
			// 
			this.saveDialog.FileName = "untitled";
			this.saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// openDialog
			// 
			this.openDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// currentText
			// 
			this.currentText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.currentText.Location = new System.Drawing.Point(0, 32);
			this.currentText.Multiline = true;
			this.currentText.Name = "currentText";
			this.currentText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.currentText.Size = new System.Drawing.Size(608, 345);
			this.currentText.TabIndex = 1;
			this.currentText.Text = "Becuase people are realy bad spelers, ths produc was was desinged to prevent spel" +
				"ing erors in a text area like ths.";
			this.currentText.TextChanged += new System.EventHandler(this.currentText_TextChanged);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 377);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(608, 24);
			this.statusBar.TabIndex = 2;
			this.statusBar.Text = "untitled";
			// 
			// spelling
			// 
			this.spelling.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.spelling_MisspelledWord);
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			this.spelling.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.spelling_DoubledWord);
			// 
			// TextForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 401);
			this.Controls.Add(this.currentText);
			this.Controls.Add(this.editToolBar);
			this.Controls.Add(this.statusBar);
			this.Menu = this.mainMenu;
			this.Name = "TextForm";
			this.Text = "NetSpell Text Editor";
			this.Load += new System.EventHandler(this.TextForm_Load);
			this.ResumeLayout(false);

		}
#endregion


	}
}
