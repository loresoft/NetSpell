using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Affix;
using NetSpell.SpellChecker.Dictionary.Phonetic;

namespace NetSpell.DictionaryBuild
{
	/// <summary>
	/// Summary description for DictionaryForm.
	/// </summary>
	public class DictionaryForm : System.Windows.Forms.Form
	{
		private bool _Changed = false;
		private string _FileName = "untitled";
		
		// the following is used to split a line by white space
		private Regex _spaceRegx = new Regex(@"[^\s]+", RegexOptions.Compiled);
		private ArrayList _Words = new ArrayList();

		private System.Windows.Forms.Button btnLookup;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TabControl DictionaryTab;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ListBox listAffixWords;
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
		private System.Windows.Forms.TextBox txtAffixKeys;
		private System.Windows.Forms.TextBox txtCurrentWord;
		private System.Windows.Forms.TextBox txtPhoneticCode;
		private System.Windows.Forms.TextBox txtWord;
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

		}

		private void btnLookup_Click(object sender, System.EventArgs e)
		{
			// if saved and words > 0
			if (_Words.Count == 0)
			{
				MessageBox.Show(this, "Dictionary contains no words!", "No Words", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.Changed)
			{
				if (MessageBox.Show(this, "Dictionary should be saved before phonetic cache is added. \r\n \r\n Save Dictonary Now?", 
					"Save Dictonary", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.SaveDictionary();
				}
				else
				{
					return;
				}
			}

			this.Cursor = Cursors.WaitCursor;

			WordDictionary dict = new WordDictionary();
			dict.DictionaryFile = this.FileName;
			dict.Initialize();

			string[] parts = _Words[(int)numUpDownWord.Value].ToString().Split('/');
		
			Word word = new Word();
			word.Value = parts[0];
			if (parts.Length > 1) word.AffixKeys = parts[1];
			if (parts.Length > 2) word.PhoneticCode = parts[2];

			ArrayList words = dict.ExpandWord(word);

			this.listAffixWords.Items.Clear();
			foreach (string tempWord in words)
			{
				this.listAffixWords.Items.Add(tempWord);
			}
			
			this.Cursor = Cursors.Default;
		}

		private void DictionaryForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (_Changed)
			{
				DialogResult result = MessageBox.Show(this, 
					string.Format("Save changes to {0}?", Path.GetFileName(this.FileName)), 
					"Save Dictionary", MessageBoxButtons.YesNoCancel, 
					MessageBoxIcon.Question);

				switch (result)
				{
					case DialogResult.Yes :
						this.SaveDictionary();
						break;
					case DialogResult.Cancel :
						e.Cancel = true;
						break;
				}
			}
		}

		private void form_TextChanged(object sender, System.EventArgs e)
		{
			this.Changed = true;
		}

		private void menuAffix_Click(object sender, System.EventArgs e)
		{
			this.LoadAffix();
		}

		private void menuGenerate_Click(object sender, System.EventArgs e)
		{
			this.GenerateCache();
		}

		private void menuPhonetic_Click(object sender, System.EventArgs e)
		{
			this.LoadPhonetic();
		}

		private void menuWords_Click(object sender, System.EventArgs e)
		{
			this.LoadWords();
		}

		private void numUpDownWord_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			this.UpdateCurrentWord();
		}

		private void numUpDownWord_ValueChanged(object sender, System.EventArgs e)
		{
			this.UpdateCurrentWord();
		}
		
		private void UpdateCurrentWord()
		{
			if ((int)numUpDownWord.Value < _Words.Count)
			{
				this.txtWordCount.Text = _Words.Count.ToString();
				this.txtCurrentWord.Text = _Words[(int)numUpDownWord.Value].ToString();
				this.listAffixWords.Items.Clear();
				string[] parts = _Words[(int)numUpDownWord.Value].ToString().Split('/');

				this.txtWord.Text = parts[0];

				if (parts.Length > 1) this.txtAffixKeys.Text = parts[1];
				if (parts.Length > 2) this.txtPhoneticCode.Text = parts[2];
			}
		}

		public void GenerateCache()
		{

			// if saved and words > 0
			if (_Words.Count == 0)
			{
				MessageBox.Show(this, "Dictionary contains no words!", "No Words", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (this.Changed)
			{
				if (MessageBox.Show(this, "Dictionary should be saved before phonetic cache is added. \r\n \r\n Save Dictonary Now?", 
					"Save Dictonary", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.SaveDictionary();
				}
				else
				{
					return;
				}
			}

			this.Cursor = Cursors.WaitCursor;
			// load dictionary
			WordDictionary dict = new WordDictionary();
			dict.DictionaryFile = this.FileName;
			dict.Initialize();
			this.Cursor = Cursors.Default;

			if (dict.PhoneticRules.Count == 0)
			{
				MessageBox.Show(this, "Dictionary does not contain phonetic rules!", "No Phonetic Rules", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			MainForm main = (MainForm)this.MdiParent;

			this.Cursor = Cursors.WaitCursor;
			for (int i = 0; i < _Words.Count; i++)
			{
				if (i % 1000 == 0)
				{
					main.statusBar.Text = string.Format("Word {0} of {1}", i, _Words.Count);
					Application.DoEvents();
				}

				string[] parts = _Words[i].ToString().Split('/');
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
					_Words[i] = string.Format("{0}/{1}/{2}", tempWord, tempKeys, tempCode);
				}
				
			}
			main.statusBar.Text = "";

			this.Changed = true;
			this.Cursor = Cursors.Default;
			MessageBox.Show(this, "Cache created successfully", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void LoadAffix()
		{
			if (this.openAffixDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.Cursor = Cursors.WaitCursor;
				this.txtTry.Text = "";
				this.txtReplace.Text = "";
				this.txtPrefix.Text = "";
				this.txtSuffix.Text = "";
				
				// open dictionary file
				FileStream fs = (FileStream)openAffixDialog.OpenFile();
				StreamReader sr = new StreamReader(fs, Encoding.UTF7);

				// read line by line
				while (sr.Peek() >= 0) 
				{
					string tempLine = sr.ReadLine().Trim();
					if (tempLine.Length > 3)
					{
						switch (tempLine.Substring(0, 3))
						{
							case "TRY" :
								this.txtTry.Text = tempLine.Substring(4);
								break;
							case "PFX" :
								this.txtPrefix.AppendText(tempLine.Substring(4) + "\r\n");
								break;
							case "SFX" :
								this.txtSuffix.AppendText(tempLine.Substring(4) + "\r\n");
								break;
							case "REP" :
								if (!char.IsNumber(tempLine.Substring(4)[0]))
								{
									this.txtReplace.AppendText(tempLine.Substring(4) + "\r\n");
								}
								break;
						}
					}

				}
				// close reader
				sr.Close();
				// close stream
				fs.Close();
			}
			this.Cursor = Cursors.Default;
		}

		public void LoadPhonetic()
		{
			if (this.openPhoneticDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.Cursor = Cursors.WaitCursor;
				// open dictionary file
				FileStream fs = (FileStream)openPhoneticDialog.OpenFile();
				StreamReader sr = new StreamReader(fs, Encoding.UTF7);

				this.txtPhonetic.Text = "";

				// read line by line
				while (sr.Peek() >= 0) 
				{
					string tempLine = sr.ReadLine().Trim();
					if (!tempLine.StartsWith("#") 
						&& !tempLine.StartsWith("version") 
						&& !tempLine.StartsWith("followup")
						&& !tempLine.StartsWith("collapse_result")
						&& tempLine.Length > 0)
					{
						this.txtPhonetic.AppendText(tempLine + "\r\n");
					}
				}
				// close reader
				sr.Close();
				// close stream
				fs.Close();
			}
			this.Cursor = Cursors.Default;
		}

		public void LoadWords()
		{
			if (this.openWordsDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.Cursor = Cursors.WaitCursor;
				// open dictionary file
				FileStream fs = (FileStream)openWordsDialog.OpenFile();
				StreamReader sr = new StreamReader(fs, Encoding.UTF7);

				_Words.Clear();
				Hashtable tempWordList = new Hashtable();

				// read line by line
				while (sr.Peek() >= 0) 
				{
					string tempLine = sr.ReadLine().Trim();
					if (!char.IsNumber(tempLine[0]))
					{
						
						string[] parts = tempLine.Split('/');
						string word = parts[0];
						string affixKeys = "";
						if(parts.Length > 1) affixKeys = parts[1];

						// look for duplicate words
						if(tempWordList.ContainsKey(word))
						{
							// merge affix keys on duplicate words
							string[] tempParts = tempWordList[word].ToString().Split('/');
							string oldKeys = "";
							if(tempParts.Length > 1) oldKeys = tempParts[1];

							foreach (char key in oldKeys)
							{
								// if the new affix keys do not contain old key, add old key to new keys
								if(affixKeys.IndexOf(key) == -1)
								{
									affixKeys += key.ToString();
								}
							}
							// only update if have keys
							if(affixKeys.Length > 0)
							{
								tempWordList[word] = string.Format("{0}/{1}", word, affixKeys);
							}
						}
						else
						{
							tempWordList.Add(word, tempLine);
						}
					}
				}
				// close reader
				sr.Close();
				// close stream
				fs.Close();

				_Words.AddRange(tempWordList.Values);
				_Words.Sort();

				this.numUpDownWord.Value = 0;
				this.numUpDownWord.Maximum = _Words.Count;
				this.UpdateCurrentWord();
				this.Changed = true;
			}
			this.Cursor = Cursors.Default;
		}

		public bool OpenDictionary()
		{
			if (this.openDictionaryDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.Cursor = Cursors.WaitCursor;
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
					if (tempLine.Length > 0)
					{
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
									this.txtTry.AppendText(tempLine);
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
									_Words.Add(tempLine);
									break;
							} // switch section
								break;
						} // switch temp line
					}
				} // while

				// close reader
				sr.Close();
				// close stream
				fs.Close();
				
				this.txtWordCount.Text = _Words.Count.ToString();
				if (_Words.Count > 0)
				{
					this.UpdateCurrentWord();
					this.numUpDownWord.Maximum = _Words.Count - 1;
				}
				else
				{
					this.numUpDownWord.Maximum = 0;
				}
				this.FileName = this.openDictionaryDialog.FileName;
				this.Cursor = Cursors.Default;
				return true;
			}
			this.Cursor = Cursors.Default;
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
			this.Cursor = Cursors.WaitCursor;
			// save dictionary file
			FileStream fs = new FileStream(this.FileName, FileMode.Create, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
			sw.NewLine = "\n";  // unix line ends

			// copyright
			sw.WriteLine("[Copyright]");
			sw.WriteLine(this.txtCopyright.Text.Replace("\r\n", "\n")); // unix line ends
			// try
			sw.WriteLine("[Try]");
			sw.WriteLine(this.txtTry.Text);
			sw.WriteLine(); 
			// replace
			sw.WriteLine("[Replace]");
			sw.WriteLine(this.txtReplace.Text.Replace("\r\n", "\n")); // unix line ends
			// prefix
			sw.WriteLine("[Prefix]");
			sw.WriteLine(this.txtPrefix.Text.Replace("\r\n", "\n")); // unix line ends
			// suffix
			sw.WriteLine("[Suffix]");
			sw.WriteLine(this.txtSuffix.Text.Replace("\r\n", "\n")); // unix line ends
			// phonetic
			sw.WriteLine("[Phonetic]");
			sw.WriteLine(this.txtPhonetic.Text.Replace("\r\n", "\n")); // unix line ends
			// words
			sw.WriteLine("[Words]");
			_Words.Sort();
			foreach (string tempWord in _Words)
			{
				sw.WriteLine(tempWord);
			}
			sw.Close();
			fs.Close();

			this.Changed = false;
			this.Cursor = Cursors.Default;
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
				this.Text = Path.GetFileName(this.FileName);
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DictionaryForm));
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnLookup = new System.Windows.Forms.Button();
			this.txtPhoneticCode = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtAffixKeys = new System.Windows.Forms.TextBox();
			this.txtWord = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.listAffixWords = new System.Windows.Forms.ListBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
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
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.DictionaryTab.SuspendLayout();
			this.tabCopyright.SuspendLayout();
			this.tabNearMiss.SuspendLayout();
			this.tabPrefix.SuspendLayout();
			this.tabSuffix.SuspendLayout();
			this.tabPhonetic.SuspendLayout();
			this.tabWords.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.tabWords.Controls.Add(this.groupBox1);
			this.tabWords.Controls.Add(this.txtWordCount);
			this.tabWords.Controls.Add(this.label3);
			this.tabWords.Location = new System.Drawing.Point(4, 22);
			this.tabWords.Name = "tabWords";
			this.tabWords.Size = new System.Drawing.Size(544, 452);
			this.tabWords.TabIndex = 5;
			this.tabWords.Text = "Word List";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnLookup);
			this.groupBox1.Controls.Add(this.txtPhoneticCode);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.txtAffixKeys);
			this.groupBox1.Controls.Add(this.txtWord);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.listAffixWords);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.txtCurrentWord);
			this.groupBox1.Controls.Add(this.numUpDownWord);
			this.groupBox1.Location = new System.Drawing.Point(16, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(512, 376);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Current Word";
			// 
			// btnLookup
			// 
			this.btnLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLookup.Location = new System.Drawing.Point(416, 160);
			this.btnLookup.Name = "btnLookup";
			this.btnLookup.TabIndex = 24;
			this.btnLookup.Text = "Lookup";
			this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
			// 
			// txtPhoneticCode
			// 
			this.txtPhoneticCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPhoneticCode.Location = new System.Drawing.Point(144, 112);
			this.txtPhoneticCode.Name = "txtPhoneticCode";
			this.txtPhoneticCode.ReadOnly = true;
			this.txtPhoneticCode.Size = new System.Drawing.Size(256, 20);
			this.txtPhoneticCode.TabIndex = 23;
			this.txtPhoneticCode.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(56, 112);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(88, 23);
			this.label9.TabIndex = 22;
			this.label9.Text = "Phonetic Code:";
			// 
			// txtAffixKeys
			// 
			this.txtAffixKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAffixKeys.Location = new System.Drawing.Point(144, 88);
			this.txtAffixKeys.Name = "txtAffixKeys";
			this.txtAffixKeys.ReadOnly = true;
			this.txtAffixKeys.Size = new System.Drawing.Size(256, 20);
			this.txtAffixKeys.TabIndex = 21;
			this.txtAffixKeys.Text = "";
			// 
			// txtWord
			// 
			this.txtWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtWord.Location = new System.Drawing.Point(144, 64);
			this.txtWord.Name = "txtWord";
			this.txtWord.ReadOnly = true;
			this.txtWord.Size = new System.Drawing.Size(256, 20);
			this.txtWord.TabIndex = 20;
			this.txtWord.Text = "";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 160);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(125, 16);
			this.label6.TabIndex = 19;
			this.label6.Text = "Words from Base Word:";
			// 
			// listAffixWords
			// 
			this.listAffixWords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listAffixWords.Location = new System.Drawing.Point(144, 160);
			this.listAffixWords.Name = "listAffixWords";
			this.listAffixWords.Size = new System.Drawing.Size(256, 69);
			this.listAffixWords.TabIndex = 18;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(80, 88);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(64, 16);
			this.label8.TabIndex = 17;
			this.label8.Text = "Affix Keys:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(72, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 16);
			this.label7.TabIndex = 16;
			this.label7.Text = "Base Word:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "Current Word Raw Text:";
			// 
			// txtCurrentWord
			// 
			this.txtCurrentWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtCurrentWord.Location = new System.Drawing.Point(144, 32);
			this.txtCurrentWord.Name = "txtCurrentWord";
			this.txtCurrentWord.ReadOnly = true;
			this.txtCurrentWord.Size = new System.Drawing.Size(256, 20);
			this.txtCurrentWord.TabIndex = 6;
			this.txtCurrentWord.Text = "";
			// 
			// numUpDownWord
			// 
			this.numUpDownWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numUpDownWord.Location = new System.Drawing.Point(416, 32);
			this.numUpDownWord.Name = "numUpDownWord";
			this.numUpDownWord.Size = new System.Drawing.Size(72, 20);
			this.numUpDownWord.TabIndex = 5;
			this.numUpDownWord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numUpDownWord_KeyUp);
			this.numUpDownWord.ValueChanged += new System.EventHandler(this.numUpDownWord_ValueChanged);
			// 
			// txtWordCount
			// 
			this.txtWordCount.Location = new System.Drawing.Point(152, 24);
			this.txtWordCount.Name = "txtWordCount";
			this.txtWordCount.ReadOnly = true;
			this.txtWordCount.Size = new System.Drawing.Size(64, 20);
			this.txtWordCount.TabIndex = 1;
			this.txtWordCount.Text = "0";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(48, 24);
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
			this.menuGenerate.Text = "Generate Phonetic Cache";
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
			this.openAffixDialog.Filter = "Affix files (*.aff)|*.aff|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openPhoneticDialog
			// 
			this.openPhoneticDialog.Filter = "Phonetic files (*.dat)|*.dat|Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// DictionaryForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(552, 478);
			this.Controls.Add(this.DictionaryTab);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numUpDownWord)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion






	}
}
