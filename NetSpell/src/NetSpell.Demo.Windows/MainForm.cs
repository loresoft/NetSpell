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
	public class MainForm : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuFileDemo;
		private System.Windows.Forms.MenuItem menuFileExit;
		private System.Windows.Forms.MenuItem menuFileNew;
		private System.Windows.Forms.MenuItem menuFileOpen;
		private System.Windows.Forms.MenuItem menuHelp;
		private System.Windows.Forms.MenuItem menuHelpAbout;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuWindow;
		private System.Windows.Forms.MenuItem menuWindowCascade;
		private System.Windows.Forms.MenuItem menuWindowHorizontal;
		private System.Windows.Forms.MenuItem menuWindowVertical;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ImageList toolBarImages;
		internal System.Windows.Forms.ToolBarButton boldBarButton;
		internal System.Windows.Forms.ToolBarButton bulletsBarButton;
		internal System.Windows.Forms.ToolBarButton centerBarButton;
		internal System.Windows.Forms.ToolBarButton copyBarButton;
		internal System.Windows.Forms.ToolBarButton cutBarButton;
		internal System.Windows.Forms.ToolBar editToolBar;
		internal System.Windows.Forms.ToolBarButton fontBarButton;
		internal System.Windows.Forms.ToolBarButton fontColorBarButton;
		internal System.Windows.Forms.ToolBarButton highlightBarButton;
		internal System.Windows.Forms.ToolBarButton italicBarButton;
		internal System.Windows.Forms.ToolBarButton leftBarButton;
		internal System.Windows.Forms.ToolBarButton newBarButton;
		internal System.Windows.Forms.ToolBarButton openBarButton;
		internal System.Windows.Forms.ToolBarButton pasteBarButton;
		internal System.Windows.Forms.ToolBarButton printBarButton;
		internal System.Windows.Forms.ToolBarButton printPreviewBarButton;
		internal System.Windows.Forms.ToolBarButton redoBarButton;
		internal System.Windows.Forms.ToolBarButton rightBarButton;
		internal System.Windows.Forms.ToolBarButton saveBarButton;
		internal System.Windows.Forms.ToolBarButton spellBarButton;
		internal NetSpell.SpellChecker.Spelling SpellChecker;
		internal System.Windows.Forms.ToolBarButton toolBarButton1;
		internal System.Windows.Forms.ToolBarButton toolBarButton11;
		internal System.Windows.Forms.ToolBarButton toolBarButton12;
		internal System.Windows.Forms.ToolBarButton toolBarButton2;
		internal System.Windows.Forms.ToolBarButton toolBarButton3;
		internal System.Windows.Forms.ToolBarButton toolBarButton4;
		internal System.Windows.Forms.ToolBarButton toolBarButton5;
		internal System.Windows.Forms.ToolBarButton toolBarButton6;
		internal System.Windows.Forms.ToolBarButton toolBarButton8;
		internal System.Windows.Forms.ToolBarButton underlineBarButton;
		internal System.Windows.Forms.ToolBarButton undoBarButton;
		
		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


		private void editToolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button == newBarButton)
			{
				this.menuFileNew_Click(sender, new EventArgs());
			}
			else if(e.Button == openBarButton)
			{
				this.menuFileOpen_Click(sender, new EventArgs());
			}
			else if(e.Button == cutBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Cut();
			}
			else if(e.Button == copyBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Copy();
			}
			else if(e.Button == pasteBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Paste();
			}
			else if(e.Button == printBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Print();
			}
			else if(e.Button == printPreviewBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).PrintPreview();
			}
			else if(e.Button == redoBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Redo();
			}
			else if(e.Button == saveBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Save();
			}
			else if(e.Button == spellBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).SpellCheck();
			}
			else if(e.Button == undoBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Undo();
			}

			else if(e.Button == fontBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).SetFont();
			}
			else if(e.Button == boldBarButton || e.Button == italicBarButton || e.Button == underlineBarButton)
			{
				System.Drawing.FontStyle newFontStyle = FontStyle.Regular;
				if(this.boldBarButton.Pushed) 
					newFontStyle |= FontStyle.Bold;
				if(this.italicBarButton.Pushed) 
					newFontStyle |= FontStyle.Italic;
				if(this.underlineBarButton.Pushed) 
					newFontStyle |= FontStyle.Underline;

				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Style(newFontStyle);

			}
			
			else if(e.Button == leftBarButton)
			{
				leftBarButton.Pushed = true;
				centerBarButton.Pushed = false;
				rightBarButton.Pushed = false;

				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Alignment(HorizontalAlignment.Left);
			}
			else if(e.Button == centerBarButton)
			{
				leftBarButton.Pushed = false;
				centerBarButton.Pushed = true;
				rightBarButton.Pushed = false;

				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Alignment(HorizontalAlignment.Center);;
			}
			else if(e.Button == rightBarButton)
			{
				centerBarButton.Pushed = false;
				leftBarButton.Pushed = false;
				rightBarButton.Pushed = true;

				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Alignment(HorizontalAlignment.Right);;
			}
			else if(e.Button == bulletsBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).Bullets(bulletsBarButton.Pushed);
			}
	
			else if(e.Button == fontColorBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild).SetFontColor();
			}
			/*
			else if(e.Button == highlightBarButton)
			{
				if(this.ActiveMdiChild != null)
					((DocumentForm)this.ActiveMdiChild);
			}
			*/

		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			this.DisableEditButtons();

			// set dictionary paths
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();

			string folder = ((string)(configurationAppSettings.GetValue("SpellChecker.Dictionary.DictionaryFolder", typeof(string))));
			string dicFile = ((string)(configurationAppSettings.GetValue("SpellChecker.Dictionary.DictionaryFile", typeof(string))));
			string userFile = ((string)(configurationAppSettings.GetValue("SpellChecker.Dictionary.UserFile", typeof(string))));

			if (folder.Length > 0) this.SpellChecker.Dictionary.DictionaryFolder = folder;
			if (dicFile.Length > 0) this.SpellChecker.Dictionary.DictionaryFile = dicFile;
			if (userFile.Length > 0) this.SpellChecker.Dictionary.UserFile = userFile;
			
			// making the MainForm owner of the spell checker
			this.SpellChecker.SpellingForm.Owner = this;

		}

		private void menuFileDemo_Click(object sender, System.EventArgs e)
		{
			DocumentForm newForm = new DocumentForm();
			newForm.MdiParent = this;
			newForm.Show();
			newForm.Document.Text = "Becuase people are realy bad spelers, ths produc was desinged to prevent speling erors in a text area like ths.";
			this.EnableEditButtons();
		}

		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuFileNew_Click(object sender, System.EventArgs e)
		{
			DocumentForm newForm = new DocumentForm();
			newForm.MdiParent = this;
			newForm.Show();
			this.EnableEditButtons();
		}

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			DocumentForm newForm = new DocumentForm();
			if (newForm.Open())
			{
				newForm.MdiParent = this;
				newForm.Show();
				this.EnableEditButtons();
			}
		}

		private void menuHelpAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm();
			about.ShowDialog(this);
		}

		private void menuWindowCascade_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.Cascade);
		}

		private void menuWindowHorizontal_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileHorizontal);
		}

		private void menuWindowVertical_Click(object sender, System.EventArgs e)
		{
			this.LayoutMdi(MdiLayout.TileVertical);
		}

		private void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			if (this.ActiveMdiChild != null)
			{
				((DocumentForm)this.ActiveMdiChild).Document.Text = this.SpellChecker.Text;
			}
		}

		private void SpellChecker_EndOfText(object sender, System.EventArgs args)
		{
			if (this.ActiveMdiChild != null)
			{
				((DocumentForm)this.ActiveMdiChild).Document.Text = this.SpellChecker.Text;
			}
		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			if (this.ActiveMdiChild != null)
			{
				((DocumentForm)this.ActiveMdiChild).Document.Text = this.SpellChecker.Text;
			}
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

		internal void CloseAll()
		{
			foreach (Form child in this.MdiChildren)
			{
				child.Close();
			}
		}

		internal void DisableEditButtons()
		{
			this.saveBarButton.Enabled = false;
			this.printBarButton.Enabled = false;
			this.printPreviewBarButton.Enabled = false;
			this.spellBarButton.Enabled = false;
			this.cutBarButton.Enabled = false;
			this.copyBarButton.Enabled = false;
			this.pasteBarButton.Enabled = false;
			this.undoBarButton.Enabled = false;
			this.redoBarButton.Enabled = false;

			this.fontBarButton.Enabled = false;
			this.boldBarButton.Enabled = false;
			this.italicBarButton.Enabled = false;
			this.underlineBarButton.Enabled = false;
			this.leftBarButton.Enabled = false;
			this.centerBarButton.Enabled = false;
			this.rightBarButton.Enabled = false;
			this.bulletsBarButton.Enabled = false;
			this.fontColorBarButton.Enabled = false;
			this.highlightBarButton.Enabled = false;

		}
		
		internal void EnableEditButtons()
		{
			this.saveBarButton.Enabled = true;
			this.printBarButton.Enabled = true;
			this.printPreviewBarButton.Enabled = true;
			this.spellBarButton.Enabled = true;
			this.cutBarButton.Enabled = true;
			this.copyBarButton.Enabled = true;
			this.pasteBarButton.Enabled = true;
			this.undoBarButton.Enabled = true;
			this.redoBarButton.Enabled = true;

			this.fontBarButton.Enabled = true;
			this.boldBarButton.Enabled = true;
			this.italicBarButton.Enabled = true;
			this.underlineBarButton.Enabled = true;
			this.leftBarButton.Enabled = true;
			this.centerBarButton.Enabled = true;
			this.rightBarButton.Enabled = true;
			this.bulletsBarButton.Enabled = true;
			this.fontColorBarButton.Enabled = true;
			this.highlightBarButton.Enabled = true;
		}

		internal void SaveAll()
		{
			foreach (DocumentForm child in this.MdiChildren)
			{
				child.Save();
			}
		}

		internal void UpdateButtons(FontStyle style, bool bullet, HorizontalAlignment alignment)
		{
			if((style & FontStyle.Bold) == FontStyle.Bold)
				this.boldBarButton.Pushed = true;
			else 
				this.boldBarButton.Pushed = false;

			if((style & FontStyle.Italic) == FontStyle.Italic)
				this.italicBarButton.Pushed = true;
			else 
				this.italicBarButton.Pushed = false;

			if((style & FontStyle.Underline) == FontStyle.Underline)
				this.underlineBarButton.Pushed = true;
			else 
				this.underlineBarButton.Pushed = false;

			this.bulletsBarButton.Pushed = bullet;

			switch (alignment) 
			{
				case HorizontalAlignment.Left :
					this.leftBarButton.Pushed = true;
					this.centerBarButton.Pushed = false;
					this.rightBarButton.Pushed = false;
					break;
				case HorizontalAlignment.Center :
					this.leftBarButton.Pushed = false;
					this.centerBarButton.Pushed = true;
					this.rightBarButton.Pushed = false;
					break;
				case HorizontalAlignment.Right :
					this.leftBarButton.Pushed = false;
					this.centerBarButton.Pushed = false;
					this.rightBarButton.Pushed = true;
					break;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuFileNew = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuFileDemo = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.menuWindow = new System.Windows.Forms.MenuItem();
			this.menuWindowHorizontal = new System.Windows.Forms.MenuItem();
			this.menuWindowVertical = new System.Windows.Forms.MenuItem();
			this.menuWindowCascade = new System.Windows.Forms.MenuItem();
			this.menuHelp = new System.Windows.Forms.MenuItem();
			this.menuHelpAbout = new System.Windows.Forms.MenuItem();
			this.editToolBar = new System.Windows.Forms.ToolBar();
			this.newBarButton = new System.Windows.Forms.ToolBarButton();
			this.openBarButton = new System.Windows.Forms.ToolBarButton();
			this.saveBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton4 = new System.Windows.Forms.ToolBarButton();
			this.printBarButton = new System.Windows.Forms.ToolBarButton();
			this.printPreviewBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
			this.spellBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.cutBarButton = new System.Windows.Forms.ToolBarButton();
			this.copyBarButton = new System.Windows.Forms.ToolBarButton();
			this.pasteBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
			this.undoBarButton = new System.Windows.Forms.ToolBarButton();
			this.redoBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton11 = new System.Windows.Forms.ToolBarButton();
			this.fontBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.boldBarButton = new System.Windows.Forms.ToolBarButton();
			this.italicBarButton = new System.Windows.Forms.ToolBarButton();
			this.underlineBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
			this.leftBarButton = new System.Windows.Forms.ToolBarButton();
			this.centerBarButton = new System.Windows.Forms.ToolBarButton();
			this.rightBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton12 = new System.Windows.Forms.ToolBarButton();
			this.bulletsBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.fontColorBarButton = new System.Windows.Forms.ToolBarButton();
			this.highlightBarButton = new System.Windows.Forms.ToolBarButton();
			this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
			this.SpellChecker = new NetSpell.SpellChecker.Spelling(this.components);
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 417);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(640, 16);
			this.statusBar.TabIndex = 2;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFile,
																					 this.menuWindow,
																					 this.menuHelp});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFileNew,
																					 this.menuFileOpen,
																					 this.menuItem1,
																					 this.menuFileDemo,
																					 this.menuItem2,
																					 this.menuFileExit});
			this.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuFile.Text = "File";
			// 
			// menuFileNew
			// 
			this.menuFileNew.Index = 0;
			this.menuFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuFileNew.Text = "New";
			this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
			// 
			// menuFileOpen
			// 
			this.menuFileOpen.Index = 1;
			this.menuFileOpen.MergeOrder = 1;
			this.menuFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuFileOpen.Text = "Open...";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.MergeOrder = 12;
			this.menuItem1.Text = "-";
			// 
			// menuFileDemo
			// 
			this.menuFileDemo.Index = 3;
			this.menuFileDemo.MergeOrder = 13;
			this.menuFileDemo.Text = "NetSpell Demo";
			this.menuFileDemo.Click += new System.EventHandler(this.menuFileDemo_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.MergeOrder = 14;
			this.menuItem2.Text = "-";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Index = 5;
			this.menuFileExit.MergeOrder = 15;
			this.menuFileExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuFileExit.Text = "Exit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
			// 
			// menuWindow
			// 
			this.menuWindow.Index = 1;
			this.menuWindow.MdiList = true;
			this.menuWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuWindowHorizontal,
																					   this.menuWindowVertical,
																					   this.menuWindowCascade});
			this.menuWindow.MergeOrder = 4;
			this.menuWindow.Text = "Window";
			// 
			// menuWindowHorizontal
			// 
			this.menuWindowHorizontal.Index = 0;
			this.menuWindowHorizontal.Text = "Tile Horizontal";
			this.menuWindowHorizontal.Click += new System.EventHandler(this.menuWindowHorizontal_Click);
			// 
			// menuWindowVertical
			// 
			this.menuWindowVertical.Index = 1;
			this.menuWindowVertical.Text = "Tile Vertical";
			this.menuWindowVertical.Click += new System.EventHandler(this.menuWindowVertical_Click);
			// 
			// menuWindowCascade
			// 
			this.menuWindowCascade.Index = 2;
			this.menuWindowCascade.Text = "Cascade";
			this.menuWindowCascade.Click += new System.EventHandler(this.menuWindowCascade_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.Index = 2;
			this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuHelpAbout});
			this.menuHelp.MergeOrder = 5;
			this.menuHelp.Text = "Help";
			// 
			// menuHelpAbout
			// 
			this.menuHelpAbout.Index = 0;
			this.menuHelpAbout.Text = "About";
			this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
			// 
			// editToolBar
			// 
			this.editToolBar.AutoSize = false;
			this.editToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						   this.newBarButton,
																						   this.openBarButton,
																						   this.saveBarButton,
																						   this.toolBarButton4,
																						   this.printBarButton,
																						   this.printPreviewBarButton,
																						   this.toolBarButton5,
																						   this.spellBarButton,
																						   this.toolBarButton1,
																						   this.cutBarButton,
																						   this.copyBarButton,
																						   this.pasteBarButton,
																						   this.toolBarButton8,
																						   this.undoBarButton,
																						   this.redoBarButton,
																						   this.toolBarButton11,
																						   this.fontBarButton,
																						   this.toolBarButton2,
																						   this.boldBarButton,
																						   this.italicBarButton,
																						   this.underlineBarButton,
																						   this.toolBarButton6,
																						   this.leftBarButton,
																						   this.centerBarButton,
																						   this.rightBarButton,
																						   this.toolBarButton12,
																						   this.bulletsBarButton,
																						   this.toolBarButton3,
																						   this.fontColorBarButton,
																						   this.highlightBarButton});
			this.editToolBar.ButtonSize = new System.Drawing.Size(24, 24);
			this.editToolBar.DropDownArrows = true;
			this.editToolBar.ImageList = this.toolBarImages;
			this.editToolBar.Location = new System.Drawing.Point(0, 0);
			this.editToolBar.Name = "editToolBar";
			this.editToolBar.ShowToolTips = true;
			this.editToolBar.Size = new System.Drawing.Size(640, 32);
			this.editToolBar.TabIndex = 4;
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
			// printBarButton
			// 
			this.printBarButton.ImageIndex = 7;
			this.printBarButton.ToolTipText = "Print";
			// 
			// printPreviewBarButton
			// 
			this.printPreviewBarButton.ImageIndex = 3;
			this.printPreviewBarButton.ToolTipText = "Print Preview";
			// 
			// toolBarButton5
			// 
			this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// spellBarButton
			// 
			this.spellBarButton.ImageIndex = 9;
			this.spellBarButton.ToolTipText = "Spell Check";
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
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
			// redoBarButton
			// 
			this.redoBarButton.ImageIndex = 11;
			this.redoBarButton.ToolTipText = "Redo";
			// 
			// toolBarButton11
			// 
			this.toolBarButton11.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// fontBarButton
			// 
			this.fontBarButton.ImageIndex = 13;
			this.fontBarButton.ToolTipText = "Font";
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// boldBarButton
			// 
			this.boldBarButton.ImageIndex = 14;
			this.boldBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.boldBarButton.ToolTipText = "Bold";
			// 
			// italicBarButton
			// 
			this.italicBarButton.ImageIndex = 15;
			this.italicBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.italicBarButton.ToolTipText = "Italic";
			// 
			// underlineBarButton
			// 
			this.underlineBarButton.ImageIndex = 16;
			this.underlineBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.underlineBarButton.ToolTipText = "Underline";
			// 
			// toolBarButton6
			// 
			this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// leftBarButton
			// 
			this.leftBarButton.ImageIndex = 17;
			this.leftBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.leftBarButton.ToolTipText = "Align Left";
			// 
			// centerBarButton
			// 
			this.centerBarButton.ImageIndex = 18;
			this.centerBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.centerBarButton.ToolTipText = "Align Center";
			// 
			// rightBarButton
			// 
			this.rightBarButton.ImageIndex = 19;
			this.rightBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.rightBarButton.ToolTipText = "Align Right";
			// 
			// toolBarButton12
			// 
			this.toolBarButton12.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// bulletsBarButton
			// 
			this.bulletsBarButton.ImageIndex = 20;
			this.bulletsBarButton.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.bulletsBarButton.ToolTipText = "Bullets";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// fontColorBarButton
			// 
			this.fontColorBarButton.ImageIndex = 22;
			this.fontColorBarButton.ToolTipText = "Font Color";
			// 
			// highlightBarButton
			// 
			this.highlightBarButton.ImageIndex = 23;
			this.highlightBarButton.ToolTipText = "Highlight";
			// 
			// toolBarImages
			// 
			this.toolBarImages.ImageSize = new System.Drawing.Size(16, 16);
			this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
			this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// SpellChecker
			// 
			this.SpellChecker.IgnoreAllCapsWords = ((bool)(configurationAppSettings.GetValue("SpellChecker.IgnoreAllCapsWords", typeof(bool))));
			this.SpellChecker.IgnoreHtml = ((bool)(configurationAppSettings.GetValue("SpellChecker.IgnoreHtml", typeof(bool))));
			this.SpellChecker.IgnoreWordsWithDigits = ((bool)(configurationAppSettings.GetValue("SpellChecker.IgnoreWordsWithDigits", typeof(bool))));
			this.SpellChecker.MaxSuggestions = ((int)(configurationAppSettings.GetValue("SpellChecker.MaxSuggestions", typeof(int))));
			this.SpellChecker.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
			this.SpellChecker.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
			this.SpellChecker.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 433);
			this.Controls.Add(this.editToolBar);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "NetSpell Text Editor";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
#endregion

	}
}
