using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace NetSpell.DictionaryBuild
{
	/// <summary>
	/// Summary description for DictionaryForm.
	/// </summary>
	public class DictionaryForm : System.Windows.Forms.Form
	{
		private bool _Changed = false;
		private string _FileName = "untitled";
		private ArrayList _Words = new ArrayList();
		private System.Windows.Forms.Button btnSearch;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TabControl DictionaryTab;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuAffix;
		private System.Windows.Forms.MenuItem menuDictionary;
		private System.Windows.Forms.MenuItem menuGenerate;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuPhonetic;
		private System.Windows.Forms.MenuItem menuWords;
		private System.Windows.Forms.NumericUpDown numUpDownWord;
		private System.Windows.Forms.OpenFileDialog openAffixDialog;
		private System.Windows.Forms.OpenFileDialog openDictionaryDialog;
		private System.Windows.Forms.OpenFileDialog openPhoneticDialog;
		private System.Windows.Forms.OpenFileDialog openWordsDialog;
		private System.Windows.Forms.SaveFileDialog saveDictionaryDialog;
		private System.Windows.Forms.TabPage tabCopyright;
		private System.Windows.Forms.TabPage tabNearMiss;
		private System.Windows.Forms.TabPage tabPhonetic;
		private System.Windows.Forms.TabPage tabPrefix;
		private System.Windows.Forms.TabPage tabSuffix;
		private System.Windows.Forms.TabPage tabWords;
		private System.Windows.Forms.TextBox txtCurrentWord;
		private System.Windows.Forms.TextBox txtSearchWord;
		private System.Windows.Forms.TextBox txtWordCount;
		internal System.Windows.Forms.TextBox txtCopyright;
		internal System.Windows.Forms.TextBox txtPhonetic;
		internal System.Windows.Forms.TextBox txtPrefix;
		internal System.Windows.Forms.TextBox txtReplace;
		internal System.Windows.Forms.TextBox txtSuffix;
		internal System.Windows.Forms.TextBox txtTry;
		

		public DictionaryForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			int result = _Words.IndexOf(this.txtSearchWord.Text);
			if (result > 0)
			{
				this.numUpDownWord.Value = result;
			}
		}

		private void DictionaryForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_Changed)
			{
				if (MessageBox.Show(this, "Save Dictionary?", 
					"Save Dictionary", MessageBoxButtons.YesNo, 
					MessageBoxIcon.Question) == DialogResult.Yes)
				{
					SaveDictionary();
				}
			}
		}

		private void form_TextChanged(object sender, System.EventArgs e)
		{
			if (!_Changed) _Changed = true;
		}

		private void menuAffix_Click(object sender, System.EventArgs e)
		{
			this.LoadAffix();
		}

		private void menuGenerate_Click(object sender, System.EventArgs e)
		{
		
		}

		private void menuPhonetic_Click(object sender, System.EventArgs e)
		{
			this.LoadPhonetic();
		}

		private void menuWords_Click(object sender, System.EventArgs e)
		{
			this.LoadWords();
		}

		private void numUpDownWord_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((int)numUpDownWord.Value < _Words.Count)
			{
				this.txtCurrentWord.Text = _Words[(int)numUpDownWord.Value].ToString();
			}
		}

		private void numUpDownWord_ValueChanged(object sender, System.EventArgs e)
		{
			if ((int)numUpDownWord.Value < _Words.Count)
			{
				this.txtCurrentWord.Text = _Words[(int)numUpDownWord.Value].ToString();
			}
		}

		public void LoadAffix()
		{

		}

		public void LoadPhonetic()
		{

		}

		public void LoadWords()
		{

		}
		

		public bool OpenDictionary()
		{
			if (this.openDictionaryDialog.ShowDialog(this) == DialogResult.OK)
			{
				// open dictionary file
				FileStream fs = (FileStream)openDictionaryDialog.OpenFile();
				StreamReader sr = new StreamReader(fs, Encoding.UTF8);

				string currentSection = "";
				
				this.txtCopyright.Text = "";
				this.txtTry.Text = "";
				this.txtReplace.Text = "";
				this.txtPrefix.Text = "";
				this.txtSuffix.Text = "";
				this.txtPhonetic.Text = "";

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
							case "[Words]" :
								currentSection = tempLine;
								break;
							default :	
							switch (currentSection)
							{
								case "[Copyright]" :
									this.txtCopyright.AppendText(tempLine + "\r\n");
									break;
								case "[Try]" : 
									if (tempLine.Length > 0)
									{
										this.txtTry.AppendText(tempLine);
									}
									break;
								case "[Replace]" : 
									this.txtReplace.AppendText(tempLine + "\r\n");
									break;
								case "[Prefix]" :
									this.txtPrefix.AppendText(tempLine + "\r\n");
									break;
								case "[Suffix]" :
									this.txtSuffix.AppendText(tempLine + "\r\n");
									break;
								case "[Phonetic]" :
									this.txtPhonetic.AppendText(tempLine + "\r\n");
									break;
								case "[Words]" :
									if (tempLine.Length > 0)
									{
										_Words.Add(tempLine);
									}
									
									break;
							} // switch section
								break;
						} // switch temp line
				} // while

				// close reader
				sr.Close();
				// close stream
				fs.Close();
				
				this.txtWordCount.Text = _Words.Count.ToString();
				if (_Words.Count > 0)
				{
					this.txtCurrentWord.Text = _Words[0].ToString();
					this.numUpDownWord.Maximum = _Words.Count - 1;
				}
				else
				{
					this.numUpDownWord.Maximum = 0;
				}
				this.FileName = this.openDictionaryDialog.FileName;

				return true;
			}

			return false;
		}

		public void SaveDictionary()
		{
			if (!File.Exists(this.FileName))
			{
				if (this.saveDictionaryDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.FileName = this.saveDictionaryDialog.FileName;
				}
				else
				{
					return;
				}
			}

			// save dictionary file
			FileStream fs = new FileStream(this.FileName, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
			sw.NewLine = "\n"; // unix line ends

			// copyright
			sw.WriteLine("[Copyright]");
			sw.WriteLine(this.txtCopyright.Text);
			sw.WriteLine();  
			// try
			sw.WriteLine("[Try]");
			sw.WriteLine(this.txtTry.Text);
			sw.WriteLine(); 
			// replace
			sw.WriteLine("[Replace]");
			sw.WriteLine(this.txtReplace.Text);
			// prefix
			sw.WriteLine("[Prefix]");
			sw.WriteLine(this.txtPrefix.Text);
			// suffix
			sw.WriteLine("[Suffix]");
			sw.WriteLine(this.txtSuffix.Text);
			// phonetic
			sw.WriteLine("[Phonetic]");
			sw.WriteLine(this.txtPhonetic.Text);
			// words
			sw.WriteLine("[Words]");
			foreach (string tempWord in _Words)
			{
				sw.WriteLine(tempWord);
			}
			sw.Close();
			fs.Close();

			this.Changed = false;
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


		internal bool Changed
		{
			get {return _Changed;}
			set 
			{
				_Changed = value;
				if (_Changed)
				{
					this.Text = this.FileName + " *";
				}
				else 
				{
					this.Text = this.FileName;
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
				this.Text = _FileName;
			}
		}


		internal ArrayList Words
		{
			get {return _Words;}
			set {_Words = value;}
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.DictionaryTab = new System.Windows.Forms.TabControl();
			this.tabCopyright = new System.Windows.Forms.TabPage();
			this.txtCopyright = new System.Windows.Forms.TextBox();
			this.tabNearMiss = new System.Windows.Forms.TabPage();
			this.txtReplace = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTry = new System.Windows.Forms.TextBox();
			this.tabPrefix = new System.Windows.Forms.TabPage();
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.tabSuffix = new System.Windows.Forms.TabPage();
			this.txtSuffix = new System.Windows.Forms.TextBox();
			this.tabPhonetic = new System.Windows.Forms.TabPage();
			this.txtPhonetic = new System.Windows.Forms.TextBox();
			this.tabWords = new System.Windows.Forms.TabPage();
			this.btnSearch = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtSearchWord = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCurrentWord = new System.Windows.Forms.TextBox();
			this.numUpDownWord = new System.Windows.Forms.NumericUpDown();
			this.txtWordCount = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuDictionary = new System.Windows.Forms.MenuItem();
			this.menuWords = new System.Windows.Forms.MenuItem();
			this.menuAffix = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuPhonetic = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuGenerate = new System.Windows.Forms.MenuItem();
			this.saveDictionaryDialog = new System.Windows.Forms.SaveFileDialog();
			this.openDictionaryDialog = new System.Windows.Forms.OpenFileDialog();
			this.openWordsDialog = new System.Windows.Forms.OpenFileDialog();
			this.openAffixDialog = new System.Windows.Forms.OpenFileDialog();
			this.openPhoneticDialog = new System.Windows.Forms.OpenFileDialog();
			this.DictionaryTab.SuspendLayout();
			this.tabCopyright.SuspendLayout();
			this.tabNearMiss.SuspendLayout();
			this.tabPrefix.SuspendLayout();
			this.tabSuffix.SuspendLayout();
			this.tabPhonetic.SuspendLayout();
			this.tabWords.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numUpDownWord)).BeginInit();
			this.SuspendLayout();
			// 
			// DictionaryTab
			// 
			this.DictionaryTab.Controls.Add(this.tabCopyright);
			this.DictionaryTab.Controls.Add(this.tabNearMiss);
			this.DictionaryTab.Controls.Add(this.tabPrefix);
			this.DictionaryTab.Controls.Add(this.tabSuffix);
			this.DictionaryTab.Controls.Add(this.tabPhonetic);
			this.DictionaryTab.Controls.Add(this.tabWords);
			this.DictionaryTab.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DictionaryTab.Location = new System.Drawing.Point(0, 0);
			this.DictionaryTab.Name = "DictionaryTab";
			this.DictionaryTab.SelectedIndex = 0;
			this.DictionaryTab.Size = new System.Drawing.Size(552, 478);
			this.DictionaryTab.TabIndex = 0;
			// 
			// tabCopyright
			// 
			this.tabCopyright.Controls.Add(this.txtCopyright);
			this.tabCopyright.Location = new System.Drawing.Point(4, 22);
			this.tabCopyright.Name = "tabCopyright";
			this.tabCopyright.Size = new System.Drawing.Size(544, 452);
			this.tabCopyright.TabIndex = 0;
			this.tabCopyright.Text = "Copyright Text";
			// 
			// txtCopyright
			// 
			this.txtCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtCopyright.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCopyright.Location = new System.Drawing.Point(0, 0);
			this.txtCopyright.Multiline = true;
			this.txtCopyright.Name = "txtCopyright";
			this.txtCopyright.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtCopyright.Size = new System.Drawing.Size(544, 452);
			this.txtCopyright.TabIndex = 0;
			this.txtCopyright.Text = "";
			this.txtCopyright.WordWrap = false;
			this.txtCopyright.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// tabNearMiss
			// 
			this.tabNearMiss.Controls.Add(this.txtReplace);
			this.tabNearMiss.Controls.Add(this.label2);
			this.tabNearMiss.Controls.Add(this.label1);
			this.tabNearMiss.Controls.Add(this.txtTry);
			this.tabNearMiss.Location = new System.Drawing.Point(4, 22);
			this.tabNearMiss.Name = "tabNearMiss";
			this.tabNearMiss.Size = new System.Drawing.Size(544, 452);
			this.tabNearMiss.TabIndex = 1;
			this.tabNearMiss.Text = "Near Miss Data";
			// 
			// txtReplace
			// 
			this.txtReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtReplace.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtReplace.Location = new System.Drawing.Point(16, 80);
			this.txtReplace.Multiline = true;
			this.txtReplace.Name = "txtReplace";
			this.txtReplace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtReplace.Size = new System.Drawing.Size(512, 360);
			this.txtReplace.TabIndex = 4;
			this.txtReplace.Text = "";
			this.txtReplace.WordWrap = false;
			this.txtReplace.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(392, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "&Replace Characters";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(392, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "&Try Characters";
			// 
			// txtTry
			// 
			this.txtTry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtTry.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTry.Location = new System.Drawing.Point(16, 32);
			this.txtTry.Name = "txtTry";
			this.txtTry.Size = new System.Drawing.Size(512, 20);
			this.txtTry.TabIndex = 0;
			this.txtTry.Text = "";
			this.txtTry.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// tabPrefix
			// 
			this.tabPrefix.Controls.Add(this.txtPrefix);
			this.tabPrefix.Location = new System.Drawing.Point(4, 22);
			this.tabPrefix.Name = "tabPrefix";
			this.tabPrefix.Size = new System.Drawing.Size(544, 452);
			this.tabPrefix.TabIndex = 2;
			this.tabPrefix.Text = "Prefix Rules";
			// 
			// txtPrefix
			// 
			this.txtPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPrefix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPrefix.Location = new System.Drawing.Point(0, 0);
			this.txtPrefix.Multiline = true;
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtPrefix.Size = new System.Drawing.Size(544, 452);
			this.txtPrefix.TabIndex = 2;
			this.txtPrefix.Text = "";
			this.txtPrefix.WordWrap = false;
			this.txtPrefix.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// tabSuffix
			// 
			this.tabSuffix.Controls.Add(this.txtSuffix);
			this.tabSuffix.Location = new System.Drawing.Point(4, 22);
			this.tabSuffix.Name = "tabSuffix";
			this.tabSuffix.Size = new System.Drawing.Size(544, 452);
			this.tabSuffix.TabIndex = 3;
			this.tabSuffix.Text = "Suffix Rules";
			// 
			// txtSuffix
			// 
			this.txtSuffix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSuffix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSuffix.Location = new System.Drawing.Point(0, 0);
			this.txtSuffix.Multiline = true;
			this.txtSuffix.Name = "txtSuffix";
			this.txtSuffix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtSuffix.Size = new System.Drawing.Size(544, 452);
			this.txtSuffix.TabIndex = 2;
			this.txtSuffix.Text = "";
			this.txtSuffix.WordWrap = false;
			this.txtSuffix.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// tabPhonetic
			// 
			this.tabPhonetic.Controls.Add(this.txtPhonetic);
			this.tabPhonetic.Location = new System.Drawing.Point(4, 22);
			this.tabPhonetic.Name = "tabPhonetic";
			this.tabPhonetic.Size = new System.Drawing.Size(544, 452);
			this.tabPhonetic.TabIndex = 4;
			this.tabPhonetic.Text = "Phonetic Rules";
			// 
			// txtPhonetic
			// 
			this.txtPhonetic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPhonetic.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPhonetic.Location = new System.Drawing.Point(0, 0);
			this.txtPhonetic.Multiline = true;
			this.txtPhonetic.Name = "txtPhonetic";
			this.txtPhonetic.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtPhonetic.Size = new System.Drawing.Size(544, 452);
			this.txtPhonetic.TabIndex = 2;
			this.txtPhonetic.Text = "";
			this.txtPhonetic.WordWrap = false;
			this.txtPhonetic.TextChanged += new System.EventHandler(this.form_TextChanged);
			// 
			// tabWords
			// 
			this.tabWords.Controls.Add(this.btnSearch);
			this.tabWords.Controls.Add(this.label5);
			this.tabWords.Controls.Add(this.txtSearchWord);
			this.tabWords.Controls.Add(this.label4);
			this.tabWords.Controls.Add(this.txtCurrentWord);
			this.tabWords.Controls.Add(this.numUpDownWord);
			this.tabWords.Controls.Add(this.txtWordCount);
			this.tabWords.Controls.Add(this.label3);
			this.tabWords.Location = new System.Drawing.Point(4, 22);
			this.tabWords.Name = "tabWords";
			this.tabWords.Size = new System.Drawing.Size(544, 452);
			this.tabWords.TabIndex = 5;
			this.tabWords.Text = "Word List";
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(424, 160);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.TabIndex = 7;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(80, 160);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "Find Word:";
			// 
			// txtSearchWord
			// 
			this.txtSearchWord.Location = new System.Drawing.Point(152, 161);
			this.txtSearchWord.Name = "txtSearchWord";
			this.txtSearchWord.Size = new System.Drawing.Size(256, 20);
			this.txtSearchWord.TabIndex = 5;
			this.txtSearchWord.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(48, 120);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 17);
			this.label4.TabIndex = 4;
			this.label4.Text = "Current Word Text:";
			// 
			// txtCurrentWord
			// 
			this.txtCurrentWord.Location = new System.Drawing.Point(152, 120);
			this.txtCurrentWord.Name = "txtCurrentWord";
			this.txtCurrentWord.ReadOnly = true;
			this.txtCurrentWord.Size = new System.Drawing.Size(256, 20);
			this.txtCurrentWord.TabIndex = 3;
			this.txtCurrentWord.Text = "";
			// 
			// numUpDownWord
			// 
			this.numUpDownWord.Location = new System.Drawing.Point(424, 120);
			this.numUpDownWord.Name = "numUpDownWord";
			this.numUpDownWord.Size = new System.Drawing.Size(72, 20);
			this.numUpDownWord.TabIndex = 2;
			this.numUpDownWord.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numUpDownWord_KeyPress);
			this.numUpDownWord.ValueChanged += new System.EventHandler(this.numUpDownWord_ValueChanged);
			// 
			// txtWordCount
			// 
			this.txtWordCount.Location = new System.Drawing.Point(152, 88);
			this.txtWordCount.Name = "txtWordCount";
			this.txtWordCount.ReadOnly = true;
			this.txtWordCount.Size = new System.Drawing.Size(64, 20);
			this.txtWordCount.TabIndex = 1;
			this.txtWordCount.Text = "0";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(48, 90);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Base Word Count:";
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuDictionary});
			// 
			// menuDictionary
			// 
			this.menuDictionary.Index = 0;
			this.menuDictionary.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuWords,
																						   this.menuAffix,
																						   this.menuItem3,
																						   this.menuPhonetic,
																						   this.menuItem5,
																						   this.menuGenerate});
			this.menuDictionary.MergeOrder = 3;
			this.menuDictionary.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.menuDictionary.Text = "Dictionary";
			// 
			// menuWords
			// 
			this.menuWords.Index = 0;
			this.menuWords.Text = "Add OpenOffice Word List";
			this.menuWords.Click += new System.EventHandler(this.menuWords_Click);
			// 
			// menuAffix
			// 
			this.menuAffix.Index = 1;
			this.menuAffix.Text = "Add OpenOffice Affix Data";
			this.menuAffix.Click += new System.EventHandler(this.menuAffix_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// menuPhonetic
			// 
			this.menuPhonetic.Index = 3;
			this.menuPhonetic.Text = "Add Aspell Phonetic Rules";
			this.menuPhonetic.Click += new System.EventHandler(this.menuPhonetic_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 4;
			this.menuItem5.Text = "-";
			// 
			// menuGenerate
			// 
			this.menuGenerate.Index = 5;
			this.menuGenerate.Text = "Generate Phonetic Code";
			this.menuGenerate.Click += new System.EventHandler(this.menuGenerate_Click);
			// 
			// saveDictionaryDialog
			// 
			this.saveDictionaryDialog.DefaultExt = "dic";
			this.saveDictionaryDialog.Filter = "Dictionary files (*.dic)|*.dic|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openDictionaryDialog
			// 
			this.openDictionaryDialog.Filter = "Dictionary files (*.dic)|*.dic|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openWordsDialog
			// 
			this.openWordsDialog.Filter = "Dictionary files (*.dic)|*.dic|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openAffixDialog
			// 
			this.openAffixDialog.FileName = "en_US.aff";
			this.openAffixDialog.Filter = "Affix files (*.aff)|*.aff|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openPhoneticDialog
			// 
			this.openPhoneticDialog.FileName = "en_phonet.dat";
			this.openPhoneticDialog.Filter = "Phonetic files (*.dat)|*.dat|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// DictionaryForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 478);
			this.Controls.Add(this.DictionaryTab);
			this.Menu = this.mainMenu;
			this.Name = "DictionaryForm";
			this.Text = "untitled";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DictionaryForm_Closing);
			this.DictionaryTab.ResumeLayout(false);
			this.tabCopyright.ResumeLayout(false);
			this.tabNearMiss.ResumeLayout(false);
			this.tabPrefix.ResumeLayout(false);
			this.tabSuffix.ResumeLayout(false);
			this.tabPhonetic.ResumeLayout(false);
			this.tabWords.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numUpDownWord)).EndInit();
			this.ResumeLayout(false);

		}
#endregion

	}
}
