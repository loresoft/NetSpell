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

namespace NetSpell.SpellChecker
{
	/// <summary>
	/// Summary description for SpellingForm.
	/// </summary>
	public class SpellingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button CancelBtn;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button IgnoreAllButton;
		private System.Windows.Forms.Button IgnoreButton;
		private System.Windows.Forms.Button OptionsButton;
		private System.Windows.Forms.Button ReplaceAllButton;
		private System.Windows.Forms.Button ReplaceButton;
		private System.Windows.Forms.Label ReplaceLabel;
		private System.Windows.Forms.TextBox ReplacementWord;
		private System.Windows.Forms.StatusBar spellStatus;
		private System.Windows.Forms.StatusBarPanel statusPaneCount;
		private System.Windows.Forms.StatusBarPanel statusPaneIndex;
		private System.Windows.Forms.StatusBarPanel statusPaneWord;
		private System.Windows.Forms.ListBox SuggestionList;
		private System.Windows.Forms.Label SuggestionsLabel;
		private System.Windows.Forms.RichTextBox TextBeingChecked;
		private System.Windows.Forms.Label TextLabel;
		
		private NetSpell.SpellChecker.Spelling SpellChecker;
		/// <summary>
		///     Default Constructor
		/// </summary>
		public SpellingForm(Spelling spell)
		{
			this.SpellChecker = spell;
			InitializeComponent();			
		}

		private void CancelBtn_Click(object sender, System.EventArgs e)
		{
			this.Hide();
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

		private void SpellingForm_Load(object sender, System.EventArgs e)
		{
			this.TextBeingChecked.Text = SpellChecker.Text;
			this.statusPaneWord.Text = "";
			this.statusPaneCount.Text = "Word: 0 of 0";
			this.statusPaneIndex.Text = "Index: 0";
			this.SuggestionList.Items.Clear();
			
		}

		private void SuggestButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.Text = this.ReplacementWord.Text;
			this.TextBeingChecked.Text = this.ReplacementWord.Text;
			this.SpellChecker.SpellCheck();
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
			this.SuggestionList.TabIndex = 5;
			this.SuggestionList.SelectedIndexChanged += new System.EventHandler(this.SuggestionList_SelectedIndexChanged);
			// 
			// ReplacementWord
			// 
			this.ReplacementWord.Location = new System.Drawing.Point(8, 128);
			this.ReplacementWord.Name = "ReplacementWord";
			this.ReplacementWord.Size = new System.Drawing.Size(336, 20);
			this.ReplacementWord.TabIndex = 3;
			this.ReplacementWord.Text = "";
			// 
			// ReplaceLabel
			// 
			this.ReplaceLabel.AutoSize = true;
			this.ReplaceLabel.Location = new System.Drawing.Point(8, 112);
			this.ReplaceLabel.Name = "ReplaceLabel";
			this.ReplaceLabel.Size = new System.Drawing.Size(72, 16);
			this.ReplaceLabel.TabIndex = 2;
			this.ReplaceLabel.Text = "Replace &with:";
			// 
			// SuggestionsLabel
			// 
			this.SuggestionsLabel.AutoSize = true;
			this.SuggestionsLabel.Location = new System.Drawing.Point(8, 160);
			this.SuggestionsLabel.Name = "SuggestionsLabel";
			this.SuggestionsLabel.Size = new System.Drawing.Size(70, 16);
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
			this.ReplaceButton.Location = new System.Drawing.Point(360, 104);
			this.ReplaceButton.Name = "ReplaceButton";
			this.ReplaceButton.TabIndex = 9;
			this.ReplaceButton.Text = "&Replace";
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// ReplaceAllButton
			// 
			this.ReplaceAllButton.Location = new System.Drawing.Point(360, 136);
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
			this.TextBeingChecked.Size = new System.Drawing.Size(336, 72);
			this.TextBeingChecked.TabIndex = 1;
			this.TextBeingChecked.TabStop = false;
			this.TextBeingChecked.Text = "";
			// 
			// TextLabel
			// 
			this.TextLabel.AutoSize = true;
			this.TextLabel.Location = new System.Drawing.Point(8, 8);
			this.TextLabel.Name = "TextLabel";
			this.TextLabel.Size = new System.Drawing.Size(109, 16);
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
			// OptionsButton
			// 
			this.OptionsButton.Location = new System.Drawing.Point(360, 192);
			this.OptionsButton.Name = "OptionsButton";
			this.OptionsButton.TabIndex = 15;
			this.OptionsButton.Text = "&Options";
			this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
			// 
			// SpellingForm
			// 
			this.AcceptButton = this.IgnoreButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(450, 304);
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

		private void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.WordEventArgs args)
		{
			//display form
			if (!this.Visible) this.Show();

			this.TextBeingChecked.Text = SpellChecker.Text;
			//turn off ignore all option on double word
			this.IgnoreAllButton.Enabled = false;
			this.ReplaceAllButton.Enabled = false;

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
			this.statusPaneCount.Text = string.Format("Word: {0} of {1}", 
				args.WordIndex.ToString(),SpellChecker.WordCount.ToString());
			this.statusPaneIndex.Text = string.Format("Index: {0}", 
				args.TextIndex.ToString());
			
			//display suggestions
			this.SuggestionList.BeginUpdate();
			this.SuggestionList.SelectedIndex = -1;
			this.SuggestionList.Items.Clear();
			this.SuggestionList.EndUpdate();
			//reset replacement word
			this.ReplacementWord.Text = string.Empty;
			this.ReplacementWord.Focus();
		}

		private void SpellChecker_EndOfText(object sender, System.EventArgs args)
		{
			this.Hide();
			MessageBox.Show(this, "Spell Check Complete.", "Spell Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.WordEventArgs args)
		{
			//display form
			//this.Show();
			if (!this.Visible) this.Show();
			
			this.TextBeingChecked.Text = SpellChecker.Text;
			//turn on ignore all option
			this.IgnoreAllButton.Enabled = true;
			this.ReplaceAllButton.Enabled = true;

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
			this.statusPaneCount.Text = string.Format("Word:{0} of {1}", args.WordIndex.ToString(),SpellChecker.WordCount.ToString());
			this.statusPaneIndex.Text = "Index: " + args.TextIndex.ToString();
			//generate suggestions
			SpellChecker.Suggest();
			//display suggestions
			this.SuggestionList.BeginUpdate();
			this.SuggestionList.SelectedIndex = -1;
			this.SuggestionList.Items.Clear();
			this.SuggestionList.Items.AddRange((string[])SpellChecker.Suggestions.ToArray(typeof(string)));
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

		private void SpellingForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			this.Hide();

		}






	}
}
