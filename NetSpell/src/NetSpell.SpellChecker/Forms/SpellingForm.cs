// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Globalization;

using NetSpell.SpellChecker;

namespace NetSpell.SpellChecker.Forms
{
	/// <summary>
	/// The SpellingForm is used to display suggestions when there is a misspelled word
	/// </summary>
	public class SpellingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Button CancelBtn;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button IgnoreAllButton;
		private System.Windows.Forms.Button IgnoreButton;
		private System.Windows.Forms.Button OptionsButton;
		private System.Windows.Forms.Button ReplaceAllButton;
		private System.Windows.Forms.Button ReplaceButton;
		private System.Windows.Forms.Label ReplaceLabel;
		private System.Windows.Forms.TextBox ReplacementWord;
		private NetSpell.SpellChecker.Spelling SpellChecker;
		private System.Windows.Forms.StatusBar spellStatus;
		private System.Windows.Forms.StatusBarPanel statusPaneCount;
		private System.Windows.Forms.StatusBarPanel statusPaneIndex;
		private System.Windows.Forms.StatusBarPanel statusPaneWord;
		private System.Windows.Forms.ListBox SuggestionList;
		private System.Windows.Forms.Label SuggestionsLabel;
		private System.Windows.Forms.RichTextBox TextBeingChecked;
		private System.Windows.Forms.Label TextLabel;

		/// <summary>
		///     Default Constructor
		/// </summary>
		public SpellingForm(Spelling spell)
		{
			this.SpellChecker = spell;
			this.AttachEvents();
			InitializeComponent();			
		}

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.Dictionary.Add(this.SpellChecker.CurrentWord);
			this.SpellChecker.SpellCheck();
		}

		private void CancelBtn_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			if (this.Owner != null) this.Owner.Activate();
		}
			
		private void IgnoreAllButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.IgnoreAllWord();
			this.SpellChecker.SpellCheck();
		}

		private void IgnoreButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.IgnoreWord();
			this.SpellChecker.SpellCheck();
		}

		private void OptionsButton_Click(object sender, System.EventArgs e)
		{
			OptionForm options = new OptionForm(ref this.SpellChecker);
			options.ShowDialog(this);
			if (options.DialogResult == DialogResult.OK) 
			{
				this.SpellChecker.SpellCheck();
			}
		}

		private void ReplaceAllButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.ReplaceAllWord(this.ReplacementWord.Text);
			this.TextBeingChecked.Text = this.SpellChecker.Text;
			this.SpellChecker.SpellCheck();
		}

		private void ReplaceButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.ReplaceWord(this.ReplacementWord.Text);
			this.TextBeingChecked.Text = this.SpellChecker.Text;
			this.SpellChecker.SpellCheck();
		}

		private void SpellingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
			if (this.Owner != null) this.Owner.Activate();
		}

		private void SpellingForm_Load(object sender, System.EventArgs e)
		{
			this.TextBeingChecked.Text = SpellChecker.Text;
			this.statusPaneWord.Text = "";
			this.statusPaneCount.Text = "Word: 0 of 0";
			this.statusPaneIndex.Text = "Index: 0";
			this.SuggestionList.Items.Clear();
		}


		private void SuggestionList_DoubleClick(object sender, System.EventArgs e)
		{
			this.ReplaceButton_Click(sender, e);
		}

		private void SuggestionList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.SuggestionList.SelectedIndex >= 0)
				this.ReplacementWord.Text = this.SuggestionList.SelectedItem.ToString();
		}
		/// <summary>
		///		Clean up any resources being used.
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

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SpellingForm));
			this.SuggestionList = new System.Windows.Forms.ListBox();
			this.ReplacementWord = new System.Windows.Forms.TextBox();
			this.ReplaceLabel = new System.Windows.Forms.Label();
			this.SuggestionsLabel = new System.Windows.Forms.Label();
			this.IgnoreButton = new System.Windows.Forms.Button();
			this.ReplaceButton = new System.Windows.Forms.Button();
			this.ReplaceAllButton = new System.Windows.Forms.Button();
			this.IgnoreAllButton = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.TextBeingChecked = new System.Windows.Forms.RichTextBox();
			this.TextLabel = new System.Windows.Forms.Label();
			this.spellStatus = new System.Windows.Forms.StatusBar();
			this.statusPaneWord = new System.Windows.Forms.StatusBarPanel();
			this.statusPaneCount = new System.Windows.Forms.StatusBarPanel();
			this.statusPaneIndex = new System.Windows.Forms.StatusBarPanel();
			this.OptionsButton = new System.Windows.Forms.Button();
			this.AddButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneWord)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneIndex)).BeginInit();
			this.SuspendLayout();
			// 
			// SuggestionList
			// 
			this.SuggestionList.Location = new System.Drawing.Point(8, 176);
			this.SuggestionList.Name = "SuggestionList";
			this.SuggestionList.Size = new System.Drawing.Size(336, 95);
			this.SuggestionList.TabIndex = 4;
			this.SuggestionList.DoubleClick += new System.EventHandler(this.SuggestionList_DoubleClick);
			this.SuggestionList.SelectedIndexChanged += new System.EventHandler(this.SuggestionList_SelectedIndexChanged);
			// 
			// ReplacementWord
			// 
			this.ReplacementWord.Location = new System.Drawing.Point(8, 128);
			this.ReplacementWord.Name = "ReplacementWord";
			this.ReplacementWord.Size = new System.Drawing.Size(336, 20);
			this.ReplacementWord.TabIndex = 2;
			this.ReplacementWord.Text = "";
			// 
			// ReplaceLabel
			// 
			this.ReplaceLabel.AutoSize = true;
			this.ReplaceLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ReplaceLabel.Location = new System.Drawing.Point(8, 112);
			this.ReplaceLabel.Name = "ReplaceLabel";
			this.ReplaceLabel.Size = new System.Drawing.Size(72, 16);
			this.ReplaceLabel.TabIndex = 1;
			this.ReplaceLabel.Text = "Replace &with:";
			// 
			// SuggestionsLabel
			// 
			this.SuggestionsLabel.AutoSize = true;
			this.SuggestionsLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.SuggestionsLabel.Location = new System.Drawing.Point(8, 160);
			this.SuggestionsLabel.Name = "SuggestionsLabel";
			this.SuggestionsLabel.Size = new System.Drawing.Size(70, 16);
			this.SuggestionsLabel.TabIndex = 3;
			this.SuggestionsLabel.Text = "S&uggestions:";
			// 
			// IgnoreButton
			// 
			this.IgnoreButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.IgnoreButton.Location = new System.Drawing.Point(360, 24);
			this.IgnoreButton.Name = "IgnoreButton";
			this.IgnoreButton.TabIndex = 5;
			this.IgnoreButton.Text = "&Ignore";
			this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
			// 
			// ReplaceButton
			// 
			this.ReplaceButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ReplaceButton.Location = new System.Drawing.Point(360, 136);
			this.ReplaceButton.Name = "ReplaceButton";
			this.ReplaceButton.TabIndex = 8;
			this.ReplaceButton.Text = "&Replace";
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// ReplaceAllButton
			// 
			this.ReplaceAllButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.ReplaceAllButton.Location = new System.Drawing.Point(360, 168);
			this.ReplaceAllButton.Name = "ReplaceAllButton";
			this.ReplaceAllButton.TabIndex = 9;
			this.ReplaceAllButton.Text = "Replace A&ll";
			this.ReplaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			// 
			// IgnoreAllButton
			// 
			this.IgnoreAllButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.IgnoreAllButton.Location = new System.Drawing.Point(360, 56);
			this.IgnoreAllButton.Name = "IgnoreAllButton";
			this.IgnoreAllButton.TabIndex = 6;
			this.IgnoreAllButton.Text = "I&gnore All";
			this.IgnoreAllButton.Click += new System.EventHandler(this.IgnoreAllButton_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.CancelBtn.Location = new System.Drawing.Point(360, 248);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.TabIndex = 11;
			this.CancelBtn.Text = "&Cancel";
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// TextBeingChecked
			// 
			this.TextBeingChecked.BackColor = System.Drawing.SystemColors.Window;
			this.TextBeingChecked.DetectUrls = false;
			this.TextBeingChecked.Location = new System.Drawing.Point(8, 24);
			this.TextBeingChecked.Name = "TextBeingChecked";
			this.TextBeingChecked.ReadOnly = true;
			this.TextBeingChecked.Size = new System.Drawing.Size(336, 72);
			this.TextBeingChecked.TabIndex = 1;
			this.TextBeingChecked.TabStop = false;
			this.TextBeingChecked.Text = "";
			// 
			// TextLabel
			// 
			this.TextLabel.AutoSize = true;
			this.TextLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.TextLabel.Location = new System.Drawing.Point(8, 8);
			this.TextLabel.Name = "TextLabel";
			this.TextLabel.Size = new System.Drawing.Size(109, 16);
			this.TextLabel.TabIndex = 0;
			this.TextLabel.Text = "Text Being Checked:";
			// 
			// spellStatus
			// 
			this.spellStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
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
			// OptionsButton
			// 
			this.OptionsButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.OptionsButton.Location = new System.Drawing.Point(360, 208);
			this.OptionsButton.Name = "OptionsButton";
			this.OptionsButton.TabIndex = 10;
			this.OptionsButton.Text = "&Options";
			this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
			// 
			// AddButton
			// 
			this.AddButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.AddButton.Location = new System.Drawing.Point(360, 96);
			this.AddButton.Name = "AddButton";
			this.AddButton.TabIndex = 7;
			this.AddButton.Text = "&Add";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// SpellingForm
			// 
			this.AcceptButton = this.IgnoreButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(450, 304);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.OptionsButton);
			this.Controls.Add(this.spellStatus);
			this.Controls.Add(this.TextLabel);
			this.Controls.Add(this.SuggestionsLabel);
			this.Controls.Add(this.ReplaceLabel);
			this.Controls.Add(this.ReplacementWord);
			this.Controls.Add(this.TextBeingChecked);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.ReplaceAllButton);
			this.Controls.Add(this.IgnoreAllButton);
			this.Controls.Add(this.ReplaceButton);
			this.Controls.Add(this.IgnoreButton);
			this.Controls.Add(this.SuggestionList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SpellingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Spell Check";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SpellingForm_Closing);
			this.Load += new System.EventHandler(this.SpellingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusPaneWord)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneIndex)).EndInit();
			this.ResumeLayout(false);

		}
#endregion

#region Spelling Events

		private void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
		{
			this.UpdateDisplay(this.SpellChecker.Text, e.Word, 
				e.WordIndex, e.TextIndex);

			//turn off ignore all option on double word
			this.IgnoreAllButton.Enabled = false;
			this.ReplaceAllButton.Enabled = false;
			this.AddButton.Enabled = false;
		}
		private void SpellChecker_EndOfText(object sender, System.EventArgs e)
		{
			this.UpdateDisplay(this.SpellChecker.Text, "", 0, 0);

			if(this.SpellChecker.AlertComplete)
			{
				MessageBox.Show(this, "Spell Check Complete.", "Spell Check", 
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			this.Hide();
			if (this.Owner != null) this.Owner.Activate();
		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs e)
		{
			this.UpdateDisplay(this.SpellChecker.Text, e.Word, 
				e.WordIndex, e.TextIndex);

			//turn on ignore all option
			this.IgnoreAllButton.Enabled = true;
			this.ReplaceAllButton.Enabled = true;
			
			//generate suggestions
			SpellChecker.Suggest();

			//display suggestions
			this.SuggestionList.Items.AddRange((string[])SpellChecker.Suggestions.ToArray(typeof(string)));
		}

		private void UpdateDisplay(string text, string word, int wordIndex, int textIndex)
		{
			//display form
			if (!this.Visible) this.Show();
			this.Activate();
	
			//set text context
			this.TextBeingChecked.ResetText();
			this.TextBeingChecked.SelectionColor = Color.Black;
			
			if(word.Length > 0) 
			{
				//highlight current word
				this.TextBeingChecked.AppendText(text.Substring(0, textIndex));
				this.TextBeingChecked.SelectionColor = Color.Red;
				this.TextBeingChecked.AppendText(word);
				this.TextBeingChecked.SelectionColor = Color.Black;
				this.TextBeingChecked.AppendText(text.Substring(textIndex + word.Length));
			
				//set caret and scroll window
				this.TextBeingChecked.Select(textIndex, 0);
				this.TextBeingChecked.Focus();
				this.TextBeingChecked.ScrollToCaret();
			}
			else
			{
				this.TextBeingChecked.AppendText(text);
			}

			//update status bar
			this.statusPaneWord.Text = word;
			wordIndex++;  //WordIndex is 0 base, display is 1 based
			this.statusPaneCount.Text = string.Format("Word: {0} of {1}", 
				wordIndex.ToString(), this.SpellChecker.WordCount.ToString(CultureInfo.CurrentUICulture));
			this.statusPaneIndex.Text = string.Format("Index: {0}", textIndex.ToString());

			//display suggestions
			this.SuggestionList.BeginUpdate();
			this.SuggestionList.SelectedIndex = -1;
			this.SuggestionList.Items.Clear();
			this.SuggestionList.EndUpdate();

			//reset replacement word
			this.ReplacementWord.Text = string.Empty;
			this.ReplacementWord.Focus();
		}

		/// <summary>
		///     called by spell checker to enable this 
		///     form to handle spell checker events
		/// </summary>
		internal void AttachEvents()
		{
			SpellChecker.MisspelledWord += new Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
			SpellChecker.DoubledWord += new Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
			SpellChecker.EndOfText += new Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
		}

		/// <summary>
		///     called by spell checker to disable this 
		///     form from handling spell checker events
		/// </summary>
		internal void DetachEvents()
		{
			SpellChecker.MisspelledWord -= new Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
			SpellChecker.DoubledWord -= new Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
			SpellChecker.EndOfText -= new Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
		}
#endregion

	}
}
