// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text;

using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Affix;
using NetSpell.SpellChecker.Dictionary.Phonetic;


namespace NetSpell.DictionaryBuild
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{

		private ArrayList _fileList = new ArrayList();
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBarButton copyBarButton;
		private System.Windows.Forms.ToolBarButton cutBarButton;
		private System.Windows.Forms.ToolBar editToolBar;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuEdit;
		private System.Windows.Forms.MenuItem menuEditCopy;
		private System.Windows.Forms.MenuItem menuEditCut;
		private System.Windows.Forms.MenuItem menuEditPaste;
		private System.Windows.Forms.MenuItem menuEditSelect;
		private System.Windows.Forms.MenuItem menuEditUndo;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuFileClose;
		private System.Windows.Forms.MenuItem menuFileCloseAll;
		private System.Windows.Forms.MenuItem menuFileExit;
		private System.Windows.Forms.MenuItem menuFileNew;
		private System.Windows.Forms.MenuItem menuFileOpen;
		private System.Windows.Forms.MenuItem menuFileSave;
		private System.Windows.Forms.MenuItem menuFileSaveAll;
		private System.Windows.Forms.MenuItem menuHelp;
		private System.Windows.Forms.MenuItem menuHelpAbout;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuWindow;
		private System.Windows.Forms.MenuItem menuWindowCascade;
		private System.Windows.Forms.MenuItem menuWindowHorizontal;
		private System.Windows.Forms.MenuItem menuWindowVertical;
		private System.Windows.Forms.ToolBarButton newBarButton;
		private System.Windows.Forms.ToolBarButton openBarButton;
		private System.Windows.Forms.ToolBarButton pasteBarButton;
		private System.Windows.Forms.ToolBarButton saveBarButton;
		private System.Windows.Forms.ToolBarButton toolBarButton11;
		private System.Windows.Forms.ToolBarButton toolBarButton4;
		private System.Windows.Forms.ToolBarButton toolBarButton8;
		private System.Windows.Forms.ImageList toolBarImages;
		internal System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ToolBarButton undoBarButton;


		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		private void buttonUpdateCode_Click(object sender, System.EventArgs e)
		{
/*
			ArrayList words = new ArrayList();

			if(this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
			
				// step 1 load dictionary
				WordDictionary dict = new WordDictionary();
				dict.DictionaryFile = this.openFileDialog1.FileName;
				dict.Initialize();

				// step 2 load dictionary file again
				// open dictionary file
				FileStream fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				StreamReader sr = new StreamReader(fs, Encoding.UTF7);

				string currentSection = "";
				long wordsPostion = 0;

				// read line by line
				while (sr.Peek() >= 0) 
				{
					string tempLine = sr.ReadLine().Trim();
					switch (tempLine)
					{
						case "[Copyright]" :
						case "[Try]" : 
						case "[Replace]" : 
						case "[Prefix]" :
						case "[Suffix]" :
						case "[Phonetic]" :
							words.Add(tempLine);
							break;
						case "[Words]" :
							// set current section that is being parsed
							currentSection = tempLine;
							words.Add(tempLine);
							break;
						default :	
							// Read all words in
							if (currentSection == "[Words]")
							{
								string[] parts = tempLine.Split('/');
								// part 1 = base word
								string tempWord = parts[0];
								// part 2 = affix keys
								string tempKeys = "";
								if (parts.Length >= 2) 
								{
									tempKeys = parts[1];
								}
								// part 3 = phonetic code
								string tempCode = dict.PhoneticCode(tempWord);
								
								if (tempCode.Length > 0)
								{
									words.Add(string.Format("{0}/{1}/{2}", tempWord, tempKeys, tempCode));
								}
								else 
								{
									words.Add(tempLine);
								}
							}
							else
							{
								words.Add(tempLine);
							}
							break;
					}
				}

				// close reader
				sr.Close();
				// close stream
				fs.Close();

				// 6 Write words back to file
				fs = new FileStream(this.openFileDialog1.FileName, FileMode.Open, FileAccess.Write, FileShare.Write);
				StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
				sw.NewLine = "\n";
				foreach (string tempLine in words)
				{
					sw.WriteLine(tempLine);
				}
				// close writer
				sw.Close();
				// close stream
				fs.Close();
			}
			*/
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
			else if(e.Button == saveBarButton)
			{
				this.menuFileSave_Click(sender, new EventArgs());
			}
			else if(e.Button == cutBarButton)
			{
				this.menuEditCut_Click(sender, new EventArgs());
			}
			else if(e.Button == copyBarButton)
			{
				this.menuEditCopy_Click(sender, new EventArgs());
			}
			else if(e.Button == pasteBarButton)
			{
				this.menuEditPaste_Click(sender, new EventArgs());
			}
			else if(e.Button == undoBarButton)
			{
				this.menuEditUndo_Click(sender, new EventArgs());
			}
		}

		private TextBox GetActiveTextBox()
		{
			if (this.ActiveMdiChild != null)
			{
				if (this.ActiveMdiChild.ActiveControl != null)
				{
					if (this.ActiveMdiChild.ActiveControl.GetType() == typeof(TextBox))
					{
						return (TextBox)this.ActiveMdiChild.ActiveControl;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//this.menuFileCloseAll_Click(sender, new EventArgs());
		}

		private void menuEditCopy_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Copy();
			}
		}

		private void menuEditCut_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Cut();
			}

		}

		private void menuEditPaste_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Paste();
			}

		}

		private void menuEditSelect_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.SelectAll();
			}

		}

		private void menuEditUndo_Click(object sender, System.EventArgs e)
		{
			TextBox current = GetActiveTextBox();
			if (current != null)
			{
				current.Undo();
			}

		}

		private void menuFileClose_Click(object sender, System.EventArgs e)
		{
			if (this.ActiveMdiChild != null)
			{
				this.ActiveMdiChild.Close();
			}
		}

		private void menuFileCloseAll_Click(object sender, System.EventArgs e)
		{
			foreach (Form child in this.MdiChildren)
			{
				child.Close();
			}
		}

		private void menuFileExit_Click(object sender, System.EventArgs e)
		{
			this.menuFileCloseAll_Click(sender, e);
			Application.Exit();
		}

		private void menuFileNew_Click(object sender, System.EventArgs e)
		{
			DictionaryForm newForm = new DictionaryForm();
			newForm.MdiParent = this;
			newForm.Show();
		}

		private void menuFileOpen_Click(object sender, System.EventArgs e)
		{
			DictionaryForm newForm = new DictionaryForm();

			if (newForm.OpenDictionary())
			{
				newForm.MdiParent = this;
				newForm.Show();
			}
		}

		private void menuFileSave_Click(object sender, System.EventArgs e)
		{
			if (this.ActiveMdiChild != null)
			{
				DictionaryForm child = (DictionaryForm)this.ActiveMdiChild;
				child.SaveDictionary();
			}
		}

		private void menuFileSaveAll_Click(object sender, System.EventArgs e)
		{
			foreach (DictionaryForm child in this.MdiChildren)
			{
				child.SaveDictionary();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuFileNew = new System.Windows.Forms.MenuItem();
			this.menuFileOpen = new System.Windows.Forms.MenuItem();
			this.menuFileClose = new System.Windows.Forms.MenuItem();
			this.menuFileCloseAll = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuFileSave = new System.Windows.Forms.MenuItem();
			this.menuFileSaveAll = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuFileExit = new System.Windows.Forms.MenuItem();
			this.menuEdit = new System.Windows.Forms.MenuItem();
			this.menuEditUndo = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuEditCut = new System.Windows.Forms.MenuItem();
			this.menuEditCopy = new System.Windows.Forms.MenuItem();
			this.menuEditPaste = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuEditSelect = new System.Windows.Forms.MenuItem();
			this.menuWindow = new System.Windows.Forms.MenuItem();
			this.menuWindowHorizontal = new System.Windows.Forms.MenuItem();
			this.menuWindowVertical = new System.Windows.Forms.MenuItem();
			this.menuWindowCascade = new System.Windows.Forms.MenuItem();
			this.menuHelp = new System.Windows.Forms.MenuItem();
			this.menuHelpAbout = new System.Windows.Forms.MenuItem();
			this.toolBarImages = new System.Windows.Forms.ImageList(this.components);
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
			this.SuspendLayout();
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 401);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(648, 16);
			this.statusBar.TabIndex = 0;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFile,
																					 this.menuEdit,
																					 this.menuWindow,
																					 this.menuHelp});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFileNew,
																					 this.menuFileOpen,
																					 this.menuFileClose,
																					 this.menuFileCloseAll,
																					 this.menuItem5,
																					 this.menuFileSave,
																					 this.menuFileSaveAll,
																					 this.menuItem9,
																					 this.menuFileExit});
			this.menuFile.Text = "File";
			// 
			// menuFileNew
			// 
			this.menuFileNew.Index = 0;
			this.menuFileNew.Text = "New";
			this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
			// 
			// menuFileOpen
			// 
			this.menuFileOpen.Index = 1;
			this.menuFileOpen.Text = "Open...";
			this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
			// 
			// menuFileClose
			// 
			this.menuFileClose.Index = 2;
			this.menuFileClose.Text = "Close";
			this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
			// 
			// menuFileCloseAll
			// 
			this.menuFileCloseAll.Index = 3;
			this.menuFileCloseAll.Text = "Close All";
			this.menuFileCloseAll.Click += new System.EventHandler(this.menuFileCloseAll_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "-";
			// 
			// menuFileSave
			// 
			this.menuFileSave.Index = 5;
			this.menuFileSave.Text = "Save";
			this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
			// 
			// menuFileSaveAll
			// 
			this.menuFileSaveAll.Index = 6;
			this.menuFileSaveAll.Text = "Save All";
			this.menuFileSaveAll.Click += new System.EventHandler(this.menuFileSaveAll_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// menuFileExit
			// 
			this.menuFileExit.Index = 8;
			this.menuFileExit.Text = "Exit";
			this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
			// 
			// menuEdit
			// 
			this.menuEdit.Index = 1;
			this.menuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuEditUndo,
																					 this.menuItem3,
																					 this.menuEditCut,
																					 this.menuEditCopy,
																					 this.menuEditPaste,
																					 this.menuItem8,
																					 this.menuEditSelect});
			this.menuEdit.Text = "Edit";
			// 
			// menuEditUndo
			// 
			this.menuEditUndo.Index = 0;
			this.menuEditUndo.Text = "Undo";
			this.menuEditUndo.Click += new System.EventHandler(this.menuEditUndo_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuEditCut
			// 
			this.menuEditCut.Index = 2;
			this.menuEditCut.Text = "Cut";
			this.menuEditCut.Click += new System.EventHandler(this.menuEditCut_Click);
			// 
			// menuEditCopy
			// 
			this.menuEditCopy.Index = 3;
			this.menuEditCopy.Text = "Copy";
			this.menuEditCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
			// 
			// menuEditPaste
			// 
			this.menuEditPaste.Index = 4;
			this.menuEditPaste.Text = "Paste";
			this.menuEditPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 5;
			this.menuItem8.Text = "-";
			// 
			// menuEditSelect
			// 
			this.menuEditSelect.Index = 6;
			this.menuEditSelect.Text = "Select All";
			this.menuEditSelect.Click += new System.EventHandler(this.menuEditSelect_Click);
			// 
			// menuWindow
			// 
			this.menuWindow.Index = 2;
			this.menuWindow.MdiList = true;
			this.menuWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuWindowHorizontal,
																					   this.menuWindowVertical,
																					   this.menuWindowCascade});
			this.menuWindow.MergeOrder = 7;
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
			this.menuHelp.Index = 3;
			this.menuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuHelpAbout});
			this.menuHelp.MergeOrder = 8;
			this.menuHelp.Text = "Help";
			// 
			// menuHelpAbout
			// 
			this.menuHelpAbout.Index = 0;
			this.menuHelpAbout.Text = "About";
			this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
			// 
			// toolBarImages
			// 
			this.toolBarImages.ImageSize = new System.Drawing.Size(16, 16);
			this.toolBarImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImages.ImageStream")));
			this.toolBarImages.TransparentColor = System.Drawing.Color.Transparent;
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
																						   this.toolBarButton11});
			this.editToolBar.ButtonSize = new System.Drawing.Size(24, 24);
			this.editToolBar.DropDownArrows = true;
			this.editToolBar.ImageList = this.toolBarImages;
			this.editToolBar.Location = new System.Drawing.Point(0, 0);
			this.editToolBar.Name = "editToolBar";
			this.editToolBar.ShowToolTips = true;
			this.editToolBar.Size = new System.Drawing.Size(648, 32);
			this.editToolBar.TabIndex = 1;
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
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(648, 417);
			this.Controls.Add(this.editToolBar);
			this.Controls.Add(this.statusBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "Dictionary Build";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.ResumeLayout(false);

		}
#endregion


	}
}
