using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Text;

namespace NetSpell.Demo.Windows
{
	/// <summary>
	/// Summary description for DocumentForm.
	/// </summary>
	public class DocumentForm : System.Windows.Forms.Form
	{

		private bool _Changed = false;
		private string _FileName = "untitled";
		private System.Windows.Forms.ColorDialog colorDialog;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu contextMenu;
		private System.Windows.Forms.MenuItem contextMenuCopy;
		private System.Windows.Forms.MenuItem contextMenuCut;
		private System.Windows.Forms.MenuItem contextMenuPaste;
		private System.Windows.Forms.MenuItem contextMenuSelectAll;
		private System.Windows.Forms.MenuItem contextMenuUndo;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuEdit;
		private System.Windows.Forms.MenuItem menuEditCopy;
		private System.Windows.Forms.MenuItem menuEditCut;
		private System.Windows.Forms.MenuItem menuEditPaste;
		private System.Windows.Forms.MenuItem menuEditRedo;
		private System.Windows.Forms.MenuItem menuEditSelectAll;
		private System.Windows.Forms.MenuItem menuEditUndo;
		private System.Windows.Forms.MenuItem menuFile;
		private System.Windows.Forms.MenuItem menuFileClose;
		private System.Windows.Forms.MenuItem menuFileCloseAll;
		private System.Windows.Forms.MenuItem menuFilePageSetup;
		private System.Windows.Forms.MenuItem menuFilePrint;
		private System.Windows.Forms.MenuItem menuFilePrintPrivew;
		private System.Windows.Forms.MenuItem menuFileSave;
		private System.Windows.Forms.MenuItem menuFileSaveAll;
		private System.Windows.Forms.MenuItem menuFileSaveAs;
		private System.Windows.Forms.MenuItem menuFormat;
		private System.Windows.Forms.MenuItem menuFormatFont;
		private System.Windows.Forms.MenuItem menuFormatWrap;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuTools;
		private System.Windows.Forms.MenuItem menuToolsSpelling;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog;
		private System.Windows.Forms.PrintDialog printDialog;
		private System.Drawing.Printing.PrintDocument printDocument;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		internal System.Windows.Forms.RichTextBox Document;

		public DocumentForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		private void Document_SelectionChanged(object sender, System.EventArgs e)
		{
			
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;

				main.UpdateButtons(Document.SelectionFont.Style, 
					Document.SelectionBullet,
					Document.SelectionAlignment);
			}		
		}

		private void Document_TextChanged(object sender, System.EventArgs e)
		{
			this.Changed = true;
		}

		private void DocumentForm_Activated(object sender, System.EventArgs e)
		{
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;
				main.EnableEditButtons();
				main.UpdateButtons(Document.SelectionFont.Style, 
					Document.SelectionBullet,
					Document.SelectionAlignment);
			}		
		}

		private void DocumentForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_Changed)
			{
				DialogResult result = MessageBox.Show(this, 
					string.Format("Save changes to {0}?", Path.GetFileName(this.FileName)), 
					"Save Document", MessageBoxButtons.YesNoCancel, 
					MessageBoxIcon.Question);

				switch (result)
				{
					case DialogResult.Yes :
						this.Save();
						break;
					case DialogResult.Cancel :
						e.Cancel = true;
						break;
				}
			}
		}

		private void DocumentForm_Deactivate(object sender, System.EventArgs e)
		{
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;
				main.DisableEditButtons();
			}
		}

		private void DocumentForm_Load(object sender, System.EventArgs e)
		{
			this.menuFormatWrap.Checked = this.Document.WordWrap;
		}

		private void menuEditCopy_Click(object sender, System.EventArgs e)
		{
			this.Copy();
		}

		private void menuEditCut_Click(object sender, System.EventArgs e)
		{
			this.Cut();
		}

		private void menuEditPaste_Click(object sender, System.EventArgs e)
		{
			this.Paste();
		}

		private void menuEditRedo_Click(object sender, System.EventArgs e)
		{
			this.Redo();
		}

		private void menuEditSelectAll_Click(object sender, System.EventArgs e)
		{
			this.Document.SelectAll();
		}

		private void menuEditUndo_Click(object sender, System.EventArgs e)
		{
			this.Undo();
		}

		private void menuFileClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuFileCloseAll_Click(object sender, System.EventArgs e)
		{
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;
				main.CloseAll();
			}
		}

		private void menuFilePageSetup_Click(object sender, System.EventArgs e)
		{
			this.pageSetupDialog.PageSettings = this.printDocument.DefaultPageSettings;
			if (this.pageSetupDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.printDocument.DefaultPageSettings = this.pageSetupDialog.PageSettings;
			}
		} 

		private void menuFilePrint_Click(object sender, System.EventArgs e)
		{
			this.Print();
		}

		private void menuFilePrintPrivew_Click(object sender, System.EventArgs e)
		{
			this.PrintPreview();
		}

		private void menuFileSave_Click(object sender, System.EventArgs e)
		{
			this.Save();
		}

		private void menuFileSaveAll_Click(object sender, System.EventArgs e)
		{
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;
				main.SaveAll();
			}
		}

		private void menuFileSaveAs_Click(object sender, System.EventArgs e)
		{
			this.SaveAs();
		}

		private void menuFormatFont_Click(object sender, System.EventArgs e)
		{
			this.SetFont();
		}

		private void menuFormatWrap_Click(object sender, System.EventArgs e)
		{
			if (this.Document.WordWrap)
			{
				this.menuFormatWrap.Checked = false;
				this.Document.WordWrap = false;
			}
			else
			{
				this.menuFormatWrap.Checked = true;
				this.Document.WordWrap = true;
			}
		}

		private void menuToolsSpelling_Click(object sender, System.EventArgs e)
		{
			this.SpellCheck();
		}

		private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			startFrom = 0;
		}

		private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			//Calculate the area to render and print
			RECT rectToPrint; 
			rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
			rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
			rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
			rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);

			//Calculate the size of the page
			RECT rectPage; 
			rectPage.Top = (int)(e.PageBounds.Top * anInch);
			rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
			rectPage.Left = (int)(e.PageBounds.Left * anInch);
			rectPage.Right = (int)(e.PageBounds.Right * anInch);

			IntPtr hdc = e.Graphics.GetHdc();

			FORMATRANGE fmtRange;
			fmtRange.chrg.cpMax = this.Document.TextLength;	//Indicate character from to character to 
			fmtRange.chrg.cpMin = startFrom;
			fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
			fmtRange.hdcTarget = hdc;              //Point at printer hDC
			fmtRange.rc = rectToPrint;             //Indicate the area on page to print
			fmtRange.rcPage = rectPage;            //Indicate size of page

			IntPtr res = IntPtr.Zero;

			IntPtr wparam = IntPtr.Zero;
			wparam = new IntPtr(1);

			//Get the pointer to the FORMATRANGE structure in memory
			IntPtr lparam= IntPtr.Zero;
			lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
			Marshal.StructureToPtr(fmtRange, lparam, false);

			//Send the rendered data for printing 
			res = SendMessage(this.Document.Handle, EM_FORMATRANGE, wparam, lparam);

			//Free the block of memory allocated
			Marshal.FreeCoTaskMem(lparam);

			//Release the device context handle obtained by a previous call
			e.Graphics.ReleaseHdc(hdc);

			//Return last + 1 character printer
			startFrom = res.ToInt32();
			// Check for more pages
			if (startFrom < this.Document.TextLength)
				e.HasMorePages = true;
			else
				e.HasMorePages = false;

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

		internal void Alignment(HorizontalAlignment alignment)
		{
			Document.SelectionAlignment = alignment;
		}

		internal void Bullets(bool value)
		{
			Document.SelectionBullet = value;
		}

		internal void Copy()
		{
			this.Document.Copy();
		}

		internal void Cut()
		{
			this.Document.Cut();
		}

		internal bool Open()
		{
			if (this.openDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.FileName = this.openDialog.FileName.ToString();
				FileStream fs = new FileStream(this.FileName, FileMode.Open, FileAccess.Read);
				this.Document.LoadFile(fs, RichTextBoxStreamType.PlainText);
				fs.Close();
				this.Changed = false;
				return true;
			}
			return false;
		}

		internal void Paste()
		{
			this.Document.Paste();
		}

		internal void Print()
		{
			if(this.printDialog.ShowDialog(this) == DialogResult.OK)
				this.printDocument.Print();
		}

		internal void PrintPreview()
		{
			this.printPreviewDialog.ShowDialog(this);
		}

		internal void Redo()
		{
			this.Document.Redo();
		}

		internal void Save()
		{
			if (!File.Exists(this.FileName))
			{
				this.SaveAs();
			}
			else 
			{
				FileStream fs = new FileStream(this.FileName, FileMode.Create, FileAccess.Write);
				this.Document.SaveFile(fs, RichTextBoxStreamType.PlainText);
				fs.Close();
				this.Changed = false;
			}	
		}

		internal void SaveAs()
		{
			this.saveDialog.FileName = Path.GetFileName(this.FileName);

			if (this.saveDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.FileName = this.saveDialog.FileName;
			}
			else
			{
				return;
			}

			FileStream fs = new FileStream(this.FileName, FileMode.Create, FileAccess.Write);
			this.Document.SaveFile(fs, RichTextBoxStreamType.PlainText);
			fs.Close();
			this.Changed = false;
		}

		internal void SetFont()
		{
			this.fontDialog.Font = Document.SelectionFont;
			if (this.fontDialog.ShowDialog(this) == DialogResult.OK)
			{
				Document.SelectionFont = this.fontDialog.Font;
			}
		}

		internal void SetFontColor()
		{
			this.colorDialog.Color = Document.SelectionColor;
			if (this.colorDialog.ShowDialog(this) == DialogResult.OK)
			{
				Document.SelectionColor = this.colorDialog.Color;
			}
		}

		internal void SpellCheck()
		{
			if (this.MdiParent != null) 
			{
				MainForm main = (MainForm)this.MdiParent;
				main.SpellChecker.Text = this.Document.Text;
				main.SpellChecker.SpellCheck();
			}
		}

		internal void Style(FontStyle style)
		{
			if (Document.SelectionFont != null)
			{
				System.Drawing.Font currentFont = Document.SelectionFont;
				
				Document.SelectionFont = new Font(
					currentFont.FontFamily, 
					currentFont.Size, 
					style);
			}
		}

		internal void Undo ()
		{
			this.Document.Undo();
		}

		internal bool Changed
		{
			get
			{
				return _Changed;
			}
			set
			{
				_Changed = value;
				if (_Changed)
				{
					this.Text = Path.GetFileName(this.FileName) + " *";
				}
				else 
				{
					this.Text = Path.GetFileName(this.FileName);
				}
			}
		}

		internal string FileName
		{
			get
			{
				return _FileName;
			}
			set
			{
				_FileName = value;
				this.Text = Path.GetFileName(_FileName);
			}
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DocumentForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuFile = new System.Windows.Forms.MenuItem();
			this.menuFileClose = new System.Windows.Forms.MenuItem();
			this.menuFileCloseAll = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuFileSave = new System.Windows.Forms.MenuItem();
			this.menuFileSaveAs = new System.Windows.Forms.MenuItem();
			this.menuFileSaveAll = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuFilePrint = new System.Windows.Forms.MenuItem();
			this.menuFilePrintPrivew = new System.Windows.Forms.MenuItem();
			this.menuFilePageSetup = new System.Windows.Forms.MenuItem();
			this.menuEdit = new System.Windows.Forms.MenuItem();
			this.menuEditUndo = new System.Windows.Forms.MenuItem();
			this.menuEditRedo = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuEditCut = new System.Windows.Forms.MenuItem();
			this.menuEditCopy = new System.Windows.Forms.MenuItem();
			this.menuEditPaste = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuEditSelectAll = new System.Windows.Forms.MenuItem();
			this.menuFormat = new System.Windows.Forms.MenuItem();
			this.menuFormatWrap = new System.Windows.Forms.MenuItem();
			this.menuFormatFont = new System.Windows.Forms.MenuItem();
			this.menuTools = new System.Windows.Forms.MenuItem();
			this.menuToolsSpelling = new System.Windows.Forms.MenuItem();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.printDialog = new System.Windows.Forms.PrintDialog();
			this.printDocument = new System.Drawing.Printing.PrintDocument();
			this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
			this.Document = new System.Windows.Forms.RichTextBox();
			this.contextMenu = new System.Windows.Forms.ContextMenu();
			this.contextMenuUndo = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.contextMenuCut = new System.Windows.Forms.MenuItem();
			this.contextMenuCopy = new System.Windows.Forms.MenuItem();
			this.contextMenuPaste = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.contextMenuSelectAll = new System.Windows.Forms.MenuItem();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFile,
																					 this.menuEdit,
																					 this.menuFormat,
																					 this.menuTools});
			// 
			// menuFile
			// 
			this.menuFile.Index = 0;
			this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuFileClose,
																					 this.menuFileCloseAll,
																					 this.menuItem16,
																					 this.menuFileSave,
																					 this.menuFileSaveAs,
																					 this.menuFileSaveAll,
																					 this.menuItem5,
																					 this.menuFilePrint,
																					 this.menuFilePrintPrivew,
																					 this.menuFilePageSetup});
			this.menuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuFile.Text = "File";
			// 
			// menuFileClose
			// 
			this.menuFileClose.Index = 0;
			this.menuFileClose.MergeOrder = 2;
			this.menuFileClose.Text = "Close";
			this.menuFileClose.Click += new System.EventHandler(this.menuFileClose_Click);
			// 
			// menuFileCloseAll
			// 
			this.menuFileCloseAll.Index = 1;
			this.menuFileCloseAll.MergeOrder = 3;
			this.menuFileCloseAll.Text = "Close All";
			this.menuFileCloseAll.Click += new System.EventHandler(this.menuFileCloseAll_Click);
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 2;
			this.menuItem16.MergeOrder = 4;
			this.menuItem16.Text = "-";
			// 
			// menuFileSave
			// 
			this.menuFileSave.Index = 3;
			this.menuFileSave.MergeOrder = 5;
			this.menuFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuFileSave.Text = "Save";
			this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
			// 
			// menuFileSaveAs
			// 
			this.menuFileSaveAs.Index = 4;
			this.menuFileSaveAs.MergeOrder = 6;
			this.menuFileSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
			this.menuFileSaveAs.Text = "Save As...";
			this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
			// 
			// menuFileSaveAll
			// 
			this.menuFileSaveAll.Index = 5;
			this.menuFileSaveAll.MergeOrder = 7;
			this.menuFileSaveAll.Text = "Save All";
			this.menuFileSaveAll.Click += new System.EventHandler(this.menuFileSaveAll_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 6;
			this.menuItem5.MergeOrder = 8;
			this.menuItem5.Text = "-";
			// 
			// menuFilePrint
			// 
			this.menuFilePrint.Index = 7;
			this.menuFilePrint.MergeOrder = 9;
			this.menuFilePrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
			this.menuFilePrint.Text = "Print...";
			this.menuFilePrint.Click += new System.EventHandler(this.menuFilePrint_Click);
			// 
			// menuFilePrintPrivew
			// 
			this.menuFilePrintPrivew.Index = 8;
			this.menuFilePrintPrivew.MergeOrder = 10;
			this.menuFilePrintPrivew.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftP;
			this.menuFilePrintPrivew.Text = "Print Preview";
			this.menuFilePrintPrivew.Click += new System.EventHandler(this.menuFilePrintPrivew_Click);
			// 
			// menuFilePageSetup
			// 
			this.menuFilePageSetup.Index = 9;
			this.menuFilePageSetup.MergeOrder = 11;
			this.menuFilePageSetup.Text = "Page Setup";
			this.menuFilePageSetup.Click += new System.EventHandler(this.menuFilePageSetup_Click);
			// 
			// menuEdit
			// 
			this.menuEdit.Index = 1;
			this.menuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuEditUndo,
																					 this.menuEditRedo,
																					 this.menuItem3,
																					 this.menuEditCut,
																					 this.menuEditCopy,
																					 this.menuEditPaste,
																					 this.menuItem7,
																					 this.menuEditSelectAll});
			this.menuEdit.MergeOrder = 1;
			this.menuEdit.Text = "Edit";
			// 
			// menuEditUndo
			// 
			this.menuEditUndo.Index = 0;
			this.menuEditUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
			this.menuEditUndo.Text = "Undo";
			this.menuEditUndo.Click += new System.EventHandler(this.menuEditUndo_Click);
			// 
			// menuEditRedo
			// 
			this.menuEditRedo.Index = 1;
			this.menuEditRedo.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftZ;
			this.menuEditRedo.Text = "Redo";
			this.menuEditRedo.Click += new System.EventHandler(this.menuEditRedo_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// menuEditCut
			// 
			this.menuEditCut.Index = 3;
			this.menuEditCut.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
			this.menuEditCut.Text = "Cut";
			this.menuEditCut.Click += new System.EventHandler(this.menuEditCut_Click);
			// 
			// menuEditCopy
			// 
			this.menuEditCopy.Index = 4;
			this.menuEditCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
			this.menuEditCopy.Text = "Copy";
			this.menuEditCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
			// 
			// menuEditPaste
			// 
			this.menuEditPaste.Index = 5;
			this.menuEditPaste.Shortcut = System.Windows.Forms.Shortcut.CtrlV;
			this.menuEditPaste.Text = "Paste";
			this.menuEditPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.Text = "-";
			// 
			// menuEditSelectAll
			// 
			this.menuEditSelectAll.Index = 7;
			this.menuEditSelectAll.Shortcut = System.Windows.Forms.Shortcut.CtrlA;
			this.menuEditSelectAll.Text = "Select All";
			this.menuEditSelectAll.Click += new System.EventHandler(this.menuEditSelectAll_Click);
			// 
			// menuFormat
			// 
			this.menuFormat.Index = 2;
			this.menuFormat.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuFormatWrap,
																					   this.menuFormatFont});
			this.menuFormat.MergeOrder = 2;
			this.menuFormat.Text = "Format";
			// 
			// menuFormatWrap
			// 
			this.menuFormatWrap.Checked = true;
			this.menuFormatWrap.Index = 0;
			this.menuFormatWrap.Text = "Word Wrap";
			this.menuFormatWrap.Click += new System.EventHandler(this.menuFormatWrap_Click);
			// 
			// menuFormatFont
			// 
			this.menuFormatFont.Index = 1;
			this.menuFormatFont.Text = "Font...";
			this.menuFormatFont.Click += new System.EventHandler(this.menuFormatFont_Click);
			// 
			// menuTools
			// 
			this.menuTools.Index = 3;
			this.menuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuToolsSpelling});
			this.menuTools.MergeOrder = 3;
			this.menuTools.Text = "Tools";
			// 
			// menuToolsSpelling
			// 
			this.menuToolsSpelling.Index = 0;
			this.menuToolsSpelling.Shortcut = System.Windows.Forms.Shortcut.F7;
			this.menuToolsSpelling.Text = "Spelling ...";
			this.menuToolsSpelling.Click += new System.EventHandler(this.menuToolsSpelling_Click);
			// 
			// printDialog
			// 
			this.printDialog.AllowSomePages = true;
			this.printDialog.Document = this.printDocument;
			// 
			// printDocument
			// 
			this.printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument_BeginPrint);
			this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
			// 
			// printPreviewDialog
			// 
			this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog.Document = this.printDocument;
			this.printPreviewDialog.Enabled = true;
			this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
			this.printPreviewDialog.Location = new System.Drawing.Point(120, 17);
			this.printPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
			this.printPreviewDialog.Name = "printPreviewDialog";
			this.printPreviewDialog.TransparencyKey = System.Drawing.Color.Empty;
			this.printPreviewDialog.UseAntiAlias = true;
			this.printPreviewDialog.Visible = false;
			// 
			// Document
			// 
			this.Document.AcceptsTab = true;
			this.Document.AutoWordSelection = true;
			this.Document.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.Document.ContextMenu = this.contextMenu;
			this.Document.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Document.Location = new System.Drawing.Point(0, 0);
			this.Document.Name = "Document";
			this.Document.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.Document.ShowSelectionMargin = true;
			this.Document.Size = new System.Drawing.Size(520, 446);
			this.Document.TabIndex = 0;
			this.Document.Text = "";
			this.Document.TextChanged += new System.EventHandler(this.Document_TextChanged);
			this.Document.SelectionChanged += new System.EventHandler(this.Document_SelectionChanged);
			// 
			// contextMenu
			// 
			this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.contextMenuUndo,
																						this.menuItem1,
																						this.menuItem2,
																						this.contextMenuCut,
																						this.contextMenuCopy,
																						this.contextMenuPaste,
																						this.menuItem8,
																						this.contextMenuSelectAll});
			// 
			// contextMenuUndo
			// 
			this.contextMenuUndo.Index = 0;
			this.contextMenuUndo.Text = "Undo";
			this.contextMenuUndo.Click += new System.EventHandler(this.menuEditUndo_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 1;
			this.menuItem1.Text = "Redo";
			this.menuItem1.Click += new System.EventHandler(this.menuEditRedo_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 2;
			this.menuItem2.Text = "-";
			// 
			// contextMenuCut
			// 
			this.contextMenuCut.Index = 3;
			this.contextMenuCut.Text = "Cut";
			this.contextMenuCut.Click += new System.EventHandler(this.menuEditCut_Click);
			// 
			// contextMenuCopy
			// 
			this.contextMenuCopy.Index = 4;
			this.contextMenuCopy.Text = "Copy";
			this.contextMenuCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
			// 
			// contextMenuPaste
			// 
			this.contextMenuPaste.Index = 5;
			this.contextMenuPaste.Text = "Paste";
			this.contextMenuPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "-";
			// 
			// contextMenuSelectAll
			// 
			this.contextMenuSelectAll.Index = 7;
			this.contextMenuSelectAll.Text = "Select All";
			this.contextMenuSelectAll.Click += new System.EventHandler(this.menuEditSelectAll_Click);
			// 
			// saveDialog
			// 
			this.saveDialog.DefaultExt = "*.txt";
			this.saveDialog.FileName = "untitled";
			this.saveDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// openDialog
			// 
			this.openDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			// 
			// pageSetupDialog
			// 
			this.pageSetupDialog.Document = this.printDocument;
			// 
			// DocumentForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 446);
			this.Controls.Add(this.Document);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "DocumentForm";
			this.Text = "untitled";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DocumentForm_Closing);
			this.Load += new System.EventHandler(this.DocumentForm_Load);
			this.Activated += new System.EventHandler(this.DocumentForm_Activated);
			this.Deactivate += new System.EventHandler(this.DocumentForm_Deactivate);
			this.ResumeLayout(false);

		}
#endregion

#region RichTextBox Printing Functions
		//Convert the unit used by the .NET framework (1/100 inch) 
		//and the unit used by Win32 API calls (twips 1/1440 inch)
		private const double anInch = 14.4;
		private const int EM_FORMATRANGE  = WM_USER + 57;
		private const int WM_USER  = 0x0400;
		private int startFrom = 0;
		
		[DllImport("USER32.dll")]
		private static extern IntPtr SendMessage (IntPtr hWnd , int msg , IntPtr wp, IntPtr lp);



		[StructLayout(LayoutKind.Sequential)]
			private struct CHARRANGE
		{
			public int cpMin;         //First character of range (0 for start of doc)
			public int cpMax;           //Last character of range (-1 for end of doc)
		}

		[StructLayout(LayoutKind.Sequential)]
			private struct FORMATRANGE
		{
			public IntPtr hdc;             //Actual DC to draw on
			public IntPtr hdcTarget;       //Target DC for determining text formatting
			public RECT rc;                //Region of the DC to draw to (in twips)
			public RECT rcPage;            //Region of the whole DC (page size) (in twips)
			public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
		}
		
		[StructLayout(LayoutKind.Sequential)] 
			private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

#endregion
	}
}
