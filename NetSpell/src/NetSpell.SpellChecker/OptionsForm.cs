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

namespace NetSpell.SpellChecker
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public class OptionForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl optionsTabControl;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TabPage dictionaryTab;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.ListView DictionaryList;
		private System.Windows.Forms.ColumnHeader nameColumn;
		private System.Windows.Forms.ColumnHeader wordsColumn;
		private System.Windows.Forms.CheckBox IgnoreUpperCheck;
		private System.Windows.Forms.CheckBox IgnoreDigitsCheck;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.TextBox MaxSuggestions;
		private System.Windows.Forms.Label lbllabel1;

		private NetSpell.SpellChecker.Spelling SpellChecker;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		///		Default Constroctor
		/// </summary>
		public OptionForm(Spelling spell)
		{
			this.SpellChecker = spell;
			InitializeComponent();
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
			this.optionsTabControl = new System.Windows.Forms.TabControl();
			this.generalTab = new System.Windows.Forms.TabPage();
			this.lbllabel1 = new System.Windows.Forms.Label();
			this.MaxSuggestions = new System.Windows.Forms.TextBox();
			this.IgnoreUpperCheck = new System.Windows.Forms.CheckBox();
			this.IgnoreDigitsCheck = new System.Windows.Forms.CheckBox();
			this.dictionaryTab = new System.Windows.Forms.TabPage();
			this.DictionaryList = new System.Windows.Forms.ListView();
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			this.wordsColumn = new System.Windows.Forms.ColumnHeader();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.optionsTabControl.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.dictionaryTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionsTabControl
			// 
			this.optionsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.optionsTabControl.Controls.Add(this.generalTab);
			this.optionsTabControl.Controls.Add(this.dictionaryTab);
			this.optionsTabControl.Location = new System.Drawing.Point(8, 8);
			this.optionsTabControl.Name = "optionsTabControl";
			this.optionsTabControl.SelectedIndex = 0;
			this.optionsTabControl.Size = new System.Drawing.Size(352, 184);
			this.optionsTabControl.TabIndex = 0;
			// 
			// generalTab
			// 
			this.generalTab.Controls.Add(this.lbllabel1);
			this.generalTab.Controls.Add(this.MaxSuggestions);
			this.generalTab.Controls.Add(this.IgnoreUpperCheck);
			this.generalTab.Controls.Add(this.IgnoreDigitsCheck);
			this.generalTab.Location = new System.Drawing.Point(4, 22);
			this.generalTab.Name = "generalTab";
			this.generalTab.Size = new System.Drawing.Size(344, 158);
			this.generalTab.TabIndex = 0;
			this.generalTab.Text = "General";
			// 
			// lbllabel1
			// 
			this.lbllabel1.Location = new System.Drawing.Point(48, 88);
			this.lbllabel1.Name = "lbllabel1";
			this.lbllabel1.Size = new System.Drawing.Size(264, 16);
			this.lbllabel1.TabIndex = 7;
			this.lbllabel1.Text = "Maximum &Suggestion Count";
			// 
			// MaxSuggestions
			// 
			this.MaxSuggestions.Location = new System.Drawing.Point(24, 86);
			this.MaxSuggestions.Name = "MaxSuggestions";
			this.MaxSuggestions.Size = new System.Drawing.Size(24, 20);
			this.MaxSuggestions.TabIndex = 3;
			this.MaxSuggestions.Text = "25";
			// 
			// IgnoreUpperCheck
			// 
			this.IgnoreUpperCheck.Checked = true;
			this.IgnoreUpperCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IgnoreUpperCheck.Location = new System.Drawing.Point(32, 48);
			this.IgnoreUpperCheck.Name = "IgnoreUpperCheck";
			this.IgnoreUpperCheck.Size = new System.Drawing.Size(296, 24);
			this.IgnoreUpperCheck.TabIndex = 2;
			this.IgnoreUpperCheck.Text = "Ignore Words in all &Upper Case";
			// 
			// IgnoreDigitsCheck
			// 
			this.IgnoreDigitsCheck.Location = new System.Drawing.Point(32, 24);
			this.IgnoreDigitsCheck.Name = "IgnoreDigitsCheck";
			this.IgnoreDigitsCheck.Size = new System.Drawing.Size(296, 24);
			this.IgnoreDigitsCheck.TabIndex = 1;
			this.IgnoreDigitsCheck.Text = "Ignore Words with &Digits";
			// 
			// dictionaryTab
			// 
			this.dictionaryTab.Controls.Add(this.DictionaryList);
			this.dictionaryTab.Location = new System.Drawing.Point(4, 22);
			this.dictionaryTab.Name = "dictionaryTab";
			this.dictionaryTab.Size = new System.Drawing.Size(344, 158);
			this.dictionaryTab.TabIndex = 2;
			this.dictionaryTab.Text = "Dictionaries";
			// 
			// DictionaryList
			// 
			this.DictionaryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.nameColumn,
																							 this.wordsColumn});
			this.DictionaryList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.DictionaryList.LabelWrap = false;
			this.DictionaryList.Location = new System.Drawing.Point(8, 8);
			this.DictionaryList.MultiSelect = false;
			this.DictionaryList.Name = "DictionaryList";
			this.DictionaryList.Size = new System.Drawing.Size(328, 144);
			this.DictionaryList.TabIndex = 4;
			this.DictionaryList.View = System.Windows.Forms.View.Details;
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 232;
			// 
			// wordsColumn
			// 
			this.wordsColumn.Text = "Words";
			this.wordsColumn.Width = 80;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.Location = new System.Drawing.Point(192, 200);
			this.OkButton.Name = "OkButton";
			this.OkButton.TabIndex = 5;
			this.OkButton.Text = "&OK";
			// 
			// CancelBtn
			// 
			this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new System.Drawing.Point(280, 200);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.TabIndex = 6;
			this.CancelBtn.Text = "&Cancel";
			// 
			// OptionForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(368, 232);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.optionsTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.OptionForm_Load);
			this.optionsTabControl.ResumeLayout(false);
			this.generalTab.ResumeLayout(false);
			this.dictionaryTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OptionForm_Load(object sender, System.EventArgs e)
		{
			this.IgnoreDigitsCheck.Checked = this.SpellChecker.IgnoreWordsWithDigits;
			this.IgnoreUpperCheck.Checked = this.SpellChecker.IgnoreAllCapsWords;
			this.MaxSuggestions.Text = this.SpellChecker.MaxSuggestions.ToString();



		}




	}
}
