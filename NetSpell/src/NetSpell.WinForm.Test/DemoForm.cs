using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace NetSpell.WinForm.Test
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DemoForm : System.Windows.Forms.Form
	{
		private NetSpell.SpellChecker.Spelling spelling;
		private NetSpell.SpellChecker.Dictionary.WordDictionary wordDictionary;
		internal System.Windows.Forms.Button spellButton;
		internal System.Windows.Forms.RichTextBox demoRichText;
		private System.ComponentModel.IContainer components;

		public DemoForm()
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
			System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
			this.spellButton = new System.Windows.Forms.Button();
			this.demoRichText = new System.Windows.Forms.RichTextBox();
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			this.wordDictionary = new NetSpell.SpellChecker.Dictionary.WordDictionary(this.components);
			this.SuspendLayout();
			// 
			// spellButton
			// 
			this.spellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.spellButton.Location = new System.Drawing.Point(344, 368);
			this.spellButton.Name = "spellButton";
			this.spellButton.Size = new System.Drawing.Size(80, 23);
			this.spellButton.TabIndex = 3;
			this.spellButton.Text = "Spell Check";
			this.spellButton.Click += new System.EventHandler(this.spellButton_Click);
			// 
			// demoRichText
			// 
			this.demoRichText.AcceptsTab = true;
			this.demoRichText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.demoRichText.AutoWordSelection = true;
			this.demoRichText.Location = new System.Drawing.Point(16, 16);
			this.demoRichText.Name = "demoRichText";
			this.demoRichText.ShowSelectionMargin = true;
			this.demoRichText.Size = new System.Drawing.Size(408, 344);
			this.demoRichText.TabIndex = 2;
			this.demoRichText.Text = "Becuase people are realy bad spelers, ths produc was desinged to prevent speling " +
				"erors in a text area like ths.";
			// 
			// spelling
			// 
			this.spelling.Dictionary = this.wordDictionary;
			this.spelling.ReplacedWord += new NetSpell.SpellChecker.Spelling.ReplacedWordEventHandler(this.spelling_ReplacedWord);
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			this.spelling.DeletedWord += new NetSpell.SpellChecker.Spelling.DeletedWordEventHandler(this.spelling_DeletedWord);
			// 
			// wordDictionary
			// 
			this.wordDictionary.DictionaryFolder = ((string)(configurationAppSettings.GetValue("wordDictionary.DictionaryFolder", typeof(string))));
			// 
			// DemoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 406);
			this.Controls.Add(this.spellButton);
			this.Controls.Add(this.demoRichText);
			this.Name = "DemoForm";
			this.Text = "NetSpell Demo";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new DemoForm());
		}

		private void spellButton_Click(object sender, System.EventArgs e)
		{
			this.spelling.Text = this.demoRichText.Text;
			this.spelling.SpellCheck();

		}

		private void spelling_DeletedWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
		{
			int start = this.demoRichText.SelectionStart;
			int length = this.demoRichText.SelectionLength;

			this.demoRichText.Select(e.TextIndex, e.Word.Length);
			this.demoRichText.SelectedText = "";

			if(start > this.demoRichText.Text.Length)
				start = this.demoRichText.Text.Length;

			if((start + length) > this.demoRichText.Text.Length)
				length = 0;

			this.demoRichText.Select(start, length);
		}

		private void spelling_ReplacedWord(object sender, NetSpell.SpellChecker.ReplaceWordEventArgs e)
		{
			int start = this.demoRichText.SelectionStart;
			int length = this.demoRichText.SelectionLength;

			this.demoRichText.Select(e.TextIndex, e.Word.Length);
			this.demoRichText.SelectedText = e.ReplacementWord;

			if(start > this.demoRichText.Text.Length)
				start = this.demoRichText.Text.Length;

			if((start + length) > this.demoRichText.Text.Length)
				length = 0;

			this.demoRichText.Select(start, length);
		}

		private void spelling_EndOfText(object sender, System.EventArgs e)
		{
			Console.WriteLine("EndOfText");
		}
	}
}
