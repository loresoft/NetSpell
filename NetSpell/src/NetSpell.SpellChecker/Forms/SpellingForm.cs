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
		private System.ComponentModel.IContainer components;
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
			this.SuggestionList.AccessibleDescription = resources.GetString("SuggestionList.AccessibleDescription");
			this.SuggestionList.AccessibleName = resources.GetString("SuggestionList.AccessibleName");
			this.SuggestionList.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("SuggestionList.Anchor")));
			this.SuggestionList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SuggestionList.BackgroundImage")));
			this.SuggestionList.ColumnWidth = ((int)(resources.GetObject("SuggestionList.ColumnWidth")));
			this.SuggestionList.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("SuggestionList.Dock")));
			this.SuggestionList.Enabled = ((bool)(resources.GetObject("SuggestionList.Enabled")));
			this.SuggestionList.Font = ((System.Drawing.Font)(resources.GetObject("SuggestionList.Font")));
			this.SuggestionList.HorizontalExtent = ((int)(resources.GetObject("SuggestionList.HorizontalExtent")));
			this.SuggestionList.HorizontalScrollbar = ((bool)(resources.GetObject("SuggestionList.HorizontalScrollbar")));
			this.SuggestionList.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("SuggestionList.ImeMode")));
			this.SuggestionList.IntegralHeight = ((bool)(resources.GetObject("SuggestionList.IntegralHeight")));
			this.SuggestionList.ItemHeight = ((int)(resources.GetObject("SuggestionList.ItemHeight")));
			this.SuggestionList.Location = ((System.Drawing.Point)(resources.GetObject("SuggestionList.Location")));
			this.SuggestionList.Name = "SuggestionList";
			this.SuggestionList.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("SuggestionList.RightToLeft")));
			this.SuggestionList.ScrollAlwaysVisible = ((bool)(resources.GetObject("SuggestionList.ScrollAlwaysVisible")));
			this.SuggestionList.Size = ((System.Drawing.Size)(resources.GetObject("SuggestionList.Size")));
			this.SuggestionList.TabIndex = ((int)(resources.GetObject("SuggestionList.TabIndex")));
			this.SuggestionList.Visible = ((bool)(resources.GetObject("SuggestionList.Visible")));
			this.SuggestionList.DoubleClick += new System.EventHandler(this.SuggestionList_DoubleClick);
			this.SuggestionList.SelectedIndexChanged += new System.EventHandler(this.SuggestionList_SelectedIndexChanged);
			// 
			// ReplacementWord
			// 
			this.ReplacementWord.AccessibleDescription = resources.GetString("ReplacementWord.AccessibleDescription");
			this.ReplacementWord.AccessibleName = resources.GetString("ReplacementWord.AccessibleName");
			this.ReplacementWord.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ReplacementWord.Anchor")));
			this.ReplacementWord.AutoSize = ((bool)(resources.GetObject("ReplacementWord.AutoSize")));
			this.ReplacementWord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ReplacementWord.BackgroundImage")));
			this.ReplacementWord.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ReplacementWord.Dock")));
			this.ReplacementWord.Enabled = ((bool)(resources.GetObject("ReplacementWord.Enabled")));
			this.ReplacementWord.Font = ((System.Drawing.Font)(resources.GetObject("ReplacementWord.Font")));
			this.ReplacementWord.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ReplacementWord.ImeMode")));
			this.ReplacementWord.Location = ((System.Drawing.Point)(resources.GetObject("ReplacementWord.Location")));
			this.ReplacementWord.MaxLength = ((int)(resources.GetObject("ReplacementWord.MaxLength")));
			this.ReplacementWord.Multiline = ((bool)(resources.GetObject("ReplacementWord.Multiline")));
			this.ReplacementWord.Name = "ReplacementWord";
			this.ReplacementWord.PasswordChar = ((char)(resources.GetObject("ReplacementWord.PasswordChar")));
			this.ReplacementWord.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ReplacementWord.RightToLeft")));
			this.ReplacementWord.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("ReplacementWord.ScrollBars")));
			this.ReplacementWord.Size = ((System.Drawing.Size)(resources.GetObject("ReplacementWord.Size")));
			this.ReplacementWord.TabIndex = ((int)(resources.GetObject("ReplacementWord.TabIndex")));
			this.ReplacementWord.Text = resources.GetString("ReplacementWord.Text");
			this.ReplacementWord.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("ReplacementWord.TextAlign")));
			this.ReplacementWord.Visible = ((bool)(resources.GetObject("ReplacementWord.Visible")));
			this.ReplacementWord.WordWrap = ((bool)(resources.GetObject("ReplacementWord.WordWrap")));
			// 
			// ReplaceLabel
			// 
			this.ReplaceLabel.AccessibleDescription = resources.GetString("ReplaceLabel.AccessibleDescription");
			this.ReplaceLabel.AccessibleName = resources.GetString("ReplaceLabel.AccessibleName");
			this.ReplaceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ReplaceLabel.Anchor")));
			this.ReplaceLabel.AutoSize = ((bool)(resources.GetObject("ReplaceLabel.AutoSize")));
			this.ReplaceLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ReplaceLabel.Dock")));
			this.ReplaceLabel.Enabled = ((bool)(resources.GetObject("ReplaceLabel.Enabled")));
			this.ReplaceLabel.Font = ((System.Drawing.Font)(resources.GetObject("ReplaceLabel.Font")));
			this.ReplaceLabel.Image = ((System.Drawing.Image)(resources.GetObject("ReplaceLabel.Image")));
			this.ReplaceLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceLabel.ImageAlign")));
			this.ReplaceLabel.ImageIndex = ((int)(resources.GetObject("ReplaceLabel.ImageIndex")));
			this.ReplaceLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ReplaceLabel.ImeMode")));
			this.ReplaceLabel.Location = ((System.Drawing.Point)(resources.GetObject("ReplaceLabel.Location")));
			this.ReplaceLabel.Name = "ReplaceLabel";
			this.ReplaceLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ReplaceLabel.RightToLeft")));
			this.ReplaceLabel.Size = ((System.Drawing.Size)(resources.GetObject("ReplaceLabel.Size")));
			this.ReplaceLabel.TabIndex = ((int)(resources.GetObject("ReplaceLabel.TabIndex")));
			this.ReplaceLabel.Text = resources.GetString("ReplaceLabel.Text");
			this.ReplaceLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceLabel.TextAlign")));
			this.ReplaceLabel.Visible = ((bool)(resources.GetObject("ReplaceLabel.Visible")));
			// 
			// SuggestionsLabel
			// 
			this.SuggestionsLabel.AccessibleDescription = resources.GetString("SuggestionsLabel.AccessibleDescription");
			this.SuggestionsLabel.AccessibleName = resources.GetString("SuggestionsLabel.AccessibleName");
			this.SuggestionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("SuggestionsLabel.Anchor")));
			this.SuggestionsLabel.AutoSize = ((bool)(resources.GetObject("SuggestionsLabel.AutoSize")));
			this.SuggestionsLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("SuggestionsLabel.Dock")));
			this.SuggestionsLabel.Enabled = ((bool)(resources.GetObject("SuggestionsLabel.Enabled")));
			this.SuggestionsLabel.Font = ((System.Drawing.Font)(resources.GetObject("SuggestionsLabel.Font")));
			this.SuggestionsLabel.Image = ((System.Drawing.Image)(resources.GetObject("SuggestionsLabel.Image")));
			this.SuggestionsLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("SuggestionsLabel.ImageAlign")));
			this.SuggestionsLabel.ImageIndex = ((int)(resources.GetObject("SuggestionsLabel.ImageIndex")));
			this.SuggestionsLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("SuggestionsLabel.ImeMode")));
			this.SuggestionsLabel.Location = ((System.Drawing.Point)(resources.GetObject("SuggestionsLabel.Location")));
			this.SuggestionsLabel.Name = "SuggestionsLabel";
			this.SuggestionsLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("SuggestionsLabel.RightToLeft")));
			this.SuggestionsLabel.Size = ((System.Drawing.Size)(resources.GetObject("SuggestionsLabel.Size")));
			this.SuggestionsLabel.TabIndex = ((int)(resources.GetObject("SuggestionsLabel.TabIndex")));
			this.SuggestionsLabel.Text = resources.GetString("SuggestionsLabel.Text");
			this.SuggestionsLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("SuggestionsLabel.TextAlign")));
			this.SuggestionsLabel.Visible = ((bool)(resources.GetObject("SuggestionsLabel.Visible")));
			// 
			// IgnoreButton
			// 
			this.IgnoreButton.AccessibleDescription = resources.GetString("IgnoreButton.AccessibleDescription");
			this.IgnoreButton.AccessibleName = resources.GetString("IgnoreButton.AccessibleName");
			this.IgnoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("IgnoreButton.Anchor")));
			this.IgnoreButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("IgnoreButton.BackgroundImage")));
			this.IgnoreButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("IgnoreButton.Dock")));
			this.IgnoreButton.Enabled = ((bool)(resources.GetObject("IgnoreButton.Enabled")));
			this.IgnoreButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("IgnoreButton.FlatStyle")));
			this.IgnoreButton.Font = ((System.Drawing.Font)(resources.GetObject("IgnoreButton.Font")));
			this.IgnoreButton.Image = ((System.Drawing.Image)(resources.GetObject("IgnoreButton.Image")));
			this.IgnoreButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("IgnoreButton.ImageAlign")));
			this.IgnoreButton.ImageIndex = ((int)(resources.GetObject("IgnoreButton.ImageIndex")));
			this.IgnoreButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("IgnoreButton.ImeMode")));
			this.IgnoreButton.Location = ((System.Drawing.Point)(resources.GetObject("IgnoreButton.Location")));
			this.IgnoreButton.Name = "IgnoreButton";
			this.IgnoreButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("IgnoreButton.RightToLeft")));
			this.IgnoreButton.Size = ((System.Drawing.Size)(resources.GetObject("IgnoreButton.Size")));
			this.IgnoreButton.TabIndex = ((int)(resources.GetObject("IgnoreButton.TabIndex")));
			this.IgnoreButton.Text = resources.GetString("IgnoreButton.Text");
			this.IgnoreButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("IgnoreButton.TextAlign")));
			this.IgnoreButton.Visible = ((bool)(resources.GetObject("IgnoreButton.Visible")));
			this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
			// 
			// ReplaceButton
			// 
			this.ReplaceButton.AccessibleDescription = resources.GetString("ReplaceButton.AccessibleDescription");
			this.ReplaceButton.AccessibleName = resources.GetString("ReplaceButton.AccessibleName");
			this.ReplaceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ReplaceButton.Anchor")));
			this.ReplaceButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ReplaceButton.BackgroundImage")));
			this.ReplaceButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ReplaceButton.Dock")));
			this.ReplaceButton.Enabled = ((bool)(resources.GetObject("ReplaceButton.Enabled")));
			this.ReplaceButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ReplaceButton.FlatStyle")));
			this.ReplaceButton.Font = ((System.Drawing.Font)(resources.GetObject("ReplaceButton.Font")));
			this.ReplaceButton.Image = ((System.Drawing.Image)(resources.GetObject("ReplaceButton.Image")));
			this.ReplaceButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceButton.ImageAlign")));
			this.ReplaceButton.ImageIndex = ((int)(resources.GetObject("ReplaceButton.ImageIndex")));
			this.ReplaceButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ReplaceButton.ImeMode")));
			this.ReplaceButton.Location = ((System.Drawing.Point)(resources.GetObject("ReplaceButton.Location")));
			this.ReplaceButton.Name = "ReplaceButton";
			this.ReplaceButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ReplaceButton.RightToLeft")));
			this.ReplaceButton.Size = ((System.Drawing.Size)(resources.GetObject("ReplaceButton.Size")));
			this.ReplaceButton.TabIndex = ((int)(resources.GetObject("ReplaceButton.TabIndex")));
			this.ReplaceButton.Text = resources.GetString("ReplaceButton.Text");
			this.ReplaceButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceButton.TextAlign")));
			this.ReplaceButton.Visible = ((bool)(resources.GetObject("ReplaceButton.Visible")));
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			// 
			// ReplaceAllButton
			// 
			this.ReplaceAllButton.AccessibleDescription = resources.GetString("ReplaceAllButton.AccessibleDescription");
			this.ReplaceAllButton.AccessibleName = resources.GetString("ReplaceAllButton.AccessibleName");
			this.ReplaceAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ReplaceAllButton.Anchor")));
			this.ReplaceAllButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ReplaceAllButton.BackgroundImage")));
			this.ReplaceAllButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ReplaceAllButton.Dock")));
			this.ReplaceAllButton.Enabled = ((bool)(resources.GetObject("ReplaceAllButton.Enabled")));
			this.ReplaceAllButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("ReplaceAllButton.FlatStyle")));
			this.ReplaceAllButton.Font = ((System.Drawing.Font)(resources.GetObject("ReplaceAllButton.Font")));
			this.ReplaceAllButton.Image = ((System.Drawing.Image)(resources.GetObject("ReplaceAllButton.Image")));
			this.ReplaceAllButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceAllButton.ImageAlign")));
			this.ReplaceAllButton.ImageIndex = ((int)(resources.GetObject("ReplaceAllButton.ImageIndex")));
			this.ReplaceAllButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ReplaceAllButton.ImeMode")));
			this.ReplaceAllButton.Location = ((System.Drawing.Point)(resources.GetObject("ReplaceAllButton.Location")));
			this.ReplaceAllButton.Name = "ReplaceAllButton";
			this.ReplaceAllButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ReplaceAllButton.RightToLeft")));
			this.ReplaceAllButton.Size = ((System.Drawing.Size)(resources.GetObject("ReplaceAllButton.Size")));
			this.ReplaceAllButton.TabIndex = ((int)(resources.GetObject("ReplaceAllButton.TabIndex")));
			this.ReplaceAllButton.Text = resources.GetString("ReplaceAllButton.Text");
			this.ReplaceAllButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("ReplaceAllButton.TextAlign")));
			this.ReplaceAllButton.Visible = ((bool)(resources.GetObject("ReplaceAllButton.Visible")));
			this.ReplaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			// 
			// IgnoreAllButton
			// 
			this.IgnoreAllButton.AccessibleDescription = resources.GetString("IgnoreAllButton.AccessibleDescription");
			this.IgnoreAllButton.AccessibleName = resources.GetString("IgnoreAllButton.AccessibleName");
			this.IgnoreAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("IgnoreAllButton.Anchor")));
			this.IgnoreAllButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("IgnoreAllButton.BackgroundImage")));
			this.IgnoreAllButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("IgnoreAllButton.Dock")));
			this.IgnoreAllButton.Enabled = ((bool)(resources.GetObject("IgnoreAllButton.Enabled")));
			this.IgnoreAllButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("IgnoreAllButton.FlatStyle")));
			this.IgnoreAllButton.Font = ((System.Drawing.Font)(resources.GetObject("IgnoreAllButton.Font")));
			this.IgnoreAllButton.Image = ((System.Drawing.Image)(resources.GetObject("IgnoreAllButton.Image")));
			this.IgnoreAllButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("IgnoreAllButton.ImageAlign")));
			this.IgnoreAllButton.ImageIndex = ((int)(resources.GetObject("IgnoreAllButton.ImageIndex")));
			this.IgnoreAllButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("IgnoreAllButton.ImeMode")));
			this.IgnoreAllButton.Location = ((System.Drawing.Point)(resources.GetObject("IgnoreAllButton.Location")));
			this.IgnoreAllButton.Name = "IgnoreAllButton";
			this.IgnoreAllButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("IgnoreAllButton.RightToLeft")));
			this.IgnoreAllButton.Size = ((System.Drawing.Size)(resources.GetObject("IgnoreAllButton.Size")));
			this.IgnoreAllButton.TabIndex = ((int)(resources.GetObject("IgnoreAllButton.TabIndex")));
			this.IgnoreAllButton.Text = resources.GetString("IgnoreAllButton.Text");
			this.IgnoreAllButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("IgnoreAllButton.TextAlign")));
			this.IgnoreAllButton.Visible = ((bool)(resources.GetObject("IgnoreAllButton.Visible")));
			this.IgnoreAllButton.Click += new System.EventHandler(this.IgnoreAllButton_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.AccessibleDescription = resources.GetString("CancelBtn.AccessibleDescription");
			this.CancelBtn.AccessibleName = resources.GetString("CancelBtn.AccessibleName");
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("CancelBtn.Anchor")));
			this.CancelBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CancelBtn.BackgroundImage")));
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("CancelBtn.Dock")));
			this.CancelBtn.Enabled = ((bool)(resources.GetObject("CancelBtn.Enabled")));
			this.CancelBtn.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("CancelBtn.FlatStyle")));
			this.CancelBtn.Font = ((System.Drawing.Font)(resources.GetObject("CancelBtn.Font")));
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("CancelBtn.ImageAlign")));
			this.CancelBtn.ImageIndex = ((int)(resources.GetObject("CancelBtn.ImageIndex")));
			this.CancelBtn.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("CancelBtn.ImeMode")));
			this.CancelBtn.Location = ((System.Drawing.Point)(resources.GetObject("CancelBtn.Location")));
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("CancelBtn.RightToLeft")));
			this.CancelBtn.Size = ((System.Drawing.Size)(resources.GetObject("CancelBtn.Size")));
			this.CancelBtn.TabIndex = ((int)(resources.GetObject("CancelBtn.TabIndex")));
			this.CancelBtn.Text = resources.GetString("CancelBtn.Text");
			this.CancelBtn.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("CancelBtn.TextAlign")));
			this.CancelBtn.Visible = ((bool)(resources.GetObject("CancelBtn.Visible")));
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
			// 
			// TextBeingChecked
			// 
			this.TextBeingChecked.AccessibleDescription = resources.GetString("TextBeingChecked.AccessibleDescription");
			this.TextBeingChecked.AccessibleName = resources.GetString("TextBeingChecked.AccessibleName");
			this.TextBeingChecked.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("TextBeingChecked.Anchor")));
			this.TextBeingChecked.AutoSize = ((bool)(resources.GetObject("TextBeingChecked.AutoSize")));
			this.TextBeingChecked.BackColor = System.Drawing.SystemColors.Window;
			this.TextBeingChecked.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TextBeingChecked.BackgroundImage")));
			this.TextBeingChecked.BulletIndent = ((int)(resources.GetObject("TextBeingChecked.BulletIndent")));
			this.TextBeingChecked.DetectUrls = false;
			this.TextBeingChecked.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("TextBeingChecked.Dock")));
			this.TextBeingChecked.Enabled = ((bool)(resources.GetObject("TextBeingChecked.Enabled")));
			this.TextBeingChecked.Font = ((System.Drawing.Font)(resources.GetObject("TextBeingChecked.Font")));
			this.TextBeingChecked.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("TextBeingChecked.ImeMode")));
			this.TextBeingChecked.Location = ((System.Drawing.Point)(resources.GetObject("TextBeingChecked.Location")));
			this.TextBeingChecked.MaxLength = ((int)(resources.GetObject("TextBeingChecked.MaxLength")));
			this.TextBeingChecked.Multiline = ((bool)(resources.GetObject("TextBeingChecked.Multiline")));
			this.TextBeingChecked.Name = "TextBeingChecked";
			this.TextBeingChecked.ReadOnly = true;
			this.TextBeingChecked.RightMargin = ((int)(resources.GetObject("TextBeingChecked.RightMargin")));
			this.TextBeingChecked.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("TextBeingChecked.RightToLeft")));
			this.TextBeingChecked.ScrollBars = ((System.Windows.Forms.RichTextBoxScrollBars)(resources.GetObject("TextBeingChecked.ScrollBars")));
			this.TextBeingChecked.Size = ((System.Drawing.Size)(resources.GetObject("TextBeingChecked.Size")));
			this.TextBeingChecked.TabIndex = ((int)(resources.GetObject("TextBeingChecked.TabIndex")));
			this.TextBeingChecked.TabStop = false;
			this.TextBeingChecked.Text = resources.GetString("TextBeingChecked.Text");
			this.TextBeingChecked.Visible = ((bool)(resources.GetObject("TextBeingChecked.Visible")));
			this.TextBeingChecked.WordWrap = ((bool)(resources.GetObject("TextBeingChecked.WordWrap")));
			this.TextBeingChecked.ZoomFactor = ((System.Single)(resources.GetObject("TextBeingChecked.ZoomFactor")));
			// 
			// TextLabel
			// 
			this.TextLabel.AccessibleDescription = resources.GetString("TextLabel.AccessibleDescription");
			this.TextLabel.AccessibleName = resources.GetString("TextLabel.AccessibleName");
			this.TextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("TextLabel.Anchor")));
			this.TextLabel.AutoSize = ((bool)(resources.GetObject("TextLabel.AutoSize")));
			this.TextLabel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("TextLabel.Dock")));
			this.TextLabel.Enabled = ((bool)(resources.GetObject("TextLabel.Enabled")));
			this.TextLabel.Font = ((System.Drawing.Font)(resources.GetObject("TextLabel.Font")));
			this.TextLabel.Image = ((System.Drawing.Image)(resources.GetObject("TextLabel.Image")));
			this.TextLabel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("TextLabel.ImageAlign")));
			this.TextLabel.ImageIndex = ((int)(resources.GetObject("TextLabel.ImageIndex")));
			this.TextLabel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("TextLabel.ImeMode")));
			this.TextLabel.Location = ((System.Drawing.Point)(resources.GetObject("TextLabel.Location")));
			this.TextLabel.Name = "TextLabel";
			this.TextLabel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("TextLabel.RightToLeft")));
			this.TextLabel.Size = ((System.Drawing.Size)(resources.GetObject("TextLabel.Size")));
			this.TextLabel.TabIndex = ((int)(resources.GetObject("TextLabel.TabIndex")));
			this.TextLabel.Text = resources.GetString("TextLabel.Text");
			this.TextLabel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("TextLabel.TextAlign")));
			this.TextLabel.Visible = ((bool)(resources.GetObject("TextLabel.Visible")));
			// 
			// spellStatus
			// 
			this.spellStatus.AccessibleDescription = resources.GetString("spellStatus.AccessibleDescription");
			this.spellStatus.AccessibleName = resources.GetString("spellStatus.AccessibleName");
			this.spellStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("spellStatus.Anchor")));
			this.spellStatus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("spellStatus.BackgroundImage")));
			this.spellStatus.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("spellStatus.Dock")));
			this.spellStatus.Enabled = ((bool)(resources.GetObject("spellStatus.Enabled")));
			this.spellStatus.Font = ((System.Drawing.Font)(resources.GetObject("spellStatus.Font")));
			this.spellStatus.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("spellStatus.ImeMode")));
			this.spellStatus.Location = ((System.Drawing.Point)(resources.GetObject("spellStatus.Location")));
			this.spellStatus.Name = "spellStatus";
			this.spellStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						   this.statusPaneWord,
																						   this.statusPaneCount,
																						   this.statusPaneIndex});
			this.spellStatus.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("spellStatus.RightToLeft")));
			this.spellStatus.ShowPanels = true;
			this.spellStatus.Size = ((System.Drawing.Size)(resources.GetObject("spellStatus.Size")));
			this.spellStatus.TabIndex = ((int)(resources.GetObject("spellStatus.TabIndex")));
			this.spellStatus.Text = resources.GetString("spellStatus.Text");
			this.spellStatus.Visible = ((bool)(resources.GetObject("spellStatus.Visible")));
			// 
			// statusPaneWord
			// 
			this.statusPaneWord.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("statusPaneWord.Alignment")));
			this.statusPaneWord.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusPaneWord.Icon = ((System.Drawing.Icon)(resources.GetObject("statusPaneWord.Icon")));
			this.statusPaneWord.MinWidth = ((int)(resources.GetObject("statusPaneWord.MinWidth")));
			this.statusPaneWord.Text = resources.GetString("statusPaneWord.Text");
			this.statusPaneWord.ToolTipText = resources.GetString("statusPaneWord.ToolTipText");
			this.statusPaneWord.Width = ((int)(resources.GetObject("statusPaneWord.Width")));
			// 
			// statusPaneCount
			// 
			this.statusPaneCount.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("statusPaneCount.Alignment")));
			this.statusPaneCount.Icon = ((System.Drawing.Icon)(resources.GetObject("statusPaneCount.Icon")));
			this.statusPaneCount.MinWidth = ((int)(resources.GetObject("statusPaneCount.MinWidth")));
			this.statusPaneCount.Text = resources.GetString("statusPaneCount.Text");
			this.statusPaneCount.ToolTipText = resources.GetString("statusPaneCount.ToolTipText");
			this.statusPaneCount.Width = ((int)(resources.GetObject("statusPaneCount.Width")));
			// 
			// statusPaneIndex
			// 
			this.statusPaneIndex.Alignment = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("statusPaneIndex.Alignment")));
			this.statusPaneIndex.Icon = ((System.Drawing.Icon)(resources.GetObject("statusPaneIndex.Icon")));
			this.statusPaneIndex.MinWidth = ((int)(resources.GetObject("statusPaneIndex.MinWidth")));
			this.statusPaneIndex.Text = resources.GetString("statusPaneIndex.Text");
			this.statusPaneIndex.ToolTipText = resources.GetString("statusPaneIndex.ToolTipText");
			this.statusPaneIndex.Width = ((int)(resources.GetObject("statusPaneIndex.Width")));
			// 
			// OptionsButton
			// 
			this.OptionsButton.AccessibleDescription = resources.GetString("OptionsButton.AccessibleDescription");
			this.OptionsButton.AccessibleName = resources.GetString("OptionsButton.AccessibleName");
			this.OptionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("OptionsButton.Anchor")));
			this.OptionsButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OptionsButton.BackgroundImage")));
			this.OptionsButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("OptionsButton.Dock")));
			this.OptionsButton.Enabled = ((bool)(resources.GetObject("OptionsButton.Enabled")));
			this.OptionsButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("OptionsButton.FlatStyle")));
			this.OptionsButton.Font = ((System.Drawing.Font)(resources.GetObject("OptionsButton.Font")));
			this.OptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("OptionsButton.Image")));
			this.OptionsButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("OptionsButton.ImageAlign")));
			this.OptionsButton.ImageIndex = ((int)(resources.GetObject("OptionsButton.ImageIndex")));
			this.OptionsButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("OptionsButton.ImeMode")));
			this.OptionsButton.Location = ((System.Drawing.Point)(resources.GetObject("OptionsButton.Location")));
			this.OptionsButton.Name = "OptionsButton";
			this.OptionsButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("OptionsButton.RightToLeft")));
			this.OptionsButton.Size = ((System.Drawing.Size)(resources.GetObject("OptionsButton.Size")));
			this.OptionsButton.TabIndex = ((int)(resources.GetObject("OptionsButton.TabIndex")));
			this.OptionsButton.Text = resources.GetString("OptionsButton.Text");
			this.OptionsButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("OptionsButton.TextAlign")));
			this.OptionsButton.Visible = ((bool)(resources.GetObject("OptionsButton.Visible")));
			this.OptionsButton.Click += new System.EventHandler(this.OptionsButton_Click);
			// 
			// AddButton
			// 
			this.AddButton.AccessibleDescription = resources.GetString("AddButton.AccessibleDescription");
			this.AddButton.AccessibleName = resources.GetString("AddButton.AccessibleName");
			this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("AddButton.Anchor")));
			this.AddButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddButton.BackgroundImage")));
			this.AddButton.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("AddButton.Dock")));
			this.AddButton.Enabled = ((bool)(resources.GetObject("AddButton.Enabled")));
			this.AddButton.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("AddButton.FlatStyle")));
			this.AddButton.Font = ((System.Drawing.Font)(resources.GetObject("AddButton.Font")));
			this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
			this.AddButton.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("AddButton.ImageAlign")));
			this.AddButton.ImageIndex = ((int)(resources.GetObject("AddButton.ImageIndex")));
			this.AddButton.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("AddButton.ImeMode")));
			this.AddButton.Location = ((System.Drawing.Point)(resources.GetObject("AddButton.Location")));
			this.AddButton.Name = "AddButton";
			this.AddButton.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("AddButton.RightToLeft")));
			this.AddButton.Size = ((System.Drawing.Size)(resources.GetObject("AddButton.Size")));
			this.AddButton.TabIndex = ((int)(resources.GetObject("AddButton.TabIndex")));
			this.AddButton.Text = resources.GetString("AddButton.Text");
			this.AddButton.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("AddButton.TextAlign")));
			this.AddButton.Visible = ((bool)(resources.GetObject("AddButton.Visible")));
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// SpellingForm
			// 
			this.AcceptButton = this.IgnoreButton;
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScaleBaseSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScaleBaseSize")));
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.CancelBtn;
			this.ClientSize = ((System.Drawing.Size)(resources.GetObject("$this.ClientSize")));
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
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.MaximizeBox = false;
			this.MaximumSize = ((System.Drawing.Size)(resources.GetObject("$this.MaximumSize")));
			this.MinimizeBox = false;
			this.MinimumSize = ((System.Drawing.Size)(resources.GetObject("$this.MinimumSize")));
			this.Name = "SpellingForm";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.StartPosition = ((System.Windows.Forms.FormStartPosition)(resources.GetObject("$this.StartPosition")));
			this.Text = resources.GetString("$this.Text");
			this.Closing += new System.ComponentModel.CancelEventHandler(this.SpellingForm_Closing);
			this.Load += new System.EventHandler(this.SpellingForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.statusPaneWord)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusPaneIndex)).EndInit();
			this.ResumeLayout(false);

		}
#endregion

#region Spelling Events

		private void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.UpdateDisplay(this.SpellChecker.Text, args.Word, 
				args.WordIndex, args.TextIndex);

			//turn off ignore all option on double word
			this.IgnoreAllButton.Enabled = false;
			this.ReplaceAllButton.Enabled = false;
			this.AddButton.Enabled = false;
		}
		private void SpellChecker_EndOfText(object sender, System.EventArgs args)
		{
			this.UpdateDisplay(this.SpellChecker.Text, "", 0, 0);

			MessageBox.Show(this, "Spell Check Complete.", "Spell Check", 
				MessageBoxButtons.OK, MessageBoxIcon.Information);

			this.Hide();
			if (this.Owner != null) this.Owner.Activate();
		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.UpdateDisplay(this.SpellChecker.Text, args.Word, 
				args.WordIndex, args.TextIndex);

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
				wordIndex.ToString(), this.SpellChecker.WordCount.ToString());
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
