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
using Win32;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public class OptionForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl optionsTabControl;
		private System.Windows.Forms.TabPage generalTab;
		private System.Windows.Forms.TabPage hotKeyTab;
		private System.Windows.Forms.TabPage dictionaryTab;
		private System.Windows.Forms.CheckBox LoadWithWindowsCheck;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.CheckBox ShiftCheck;
		private System.Windows.Forms.CheckBox CtrlCheck;
		private System.Windows.Forms.CheckBox AltCheck;
		private System.Windows.Forms.CheckBox WinCheck;
		private System.Windows.Forms.Label PlusLabel;
		private System.Windows.Forms.TextBox KeyTextBox;
		private System.Windows.Forms.Label HotKeyDescLabel;
		private System.Windows.Forms.ListView DictionaryList;
		private System.Windows.Forms.ColumnHeader nameColumn;
		private System.Windows.Forms.ColumnHeader wordsColumn;
		private System.Windows.Forms.CheckBox IgnoreUpperCheck;
		private System.Windows.Forms.CheckBox IgnoreDigitsCheck;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OptionForm()
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
			this.LoadWithWindowsCheck = new System.Windows.Forms.CheckBox();
			this.IgnoreUpperCheck = new System.Windows.Forms.CheckBox();
			this.IgnoreDigitsCheck = new System.Windows.Forms.CheckBox();
			this.hotKeyTab = new System.Windows.Forms.TabPage();
			this.HotKeyDescLabel = new System.Windows.Forms.Label();
			this.KeyTextBox = new System.Windows.Forms.TextBox();
			this.PlusLabel = new System.Windows.Forms.Label();
			this.WinCheck = new System.Windows.Forms.CheckBox();
			this.AltCheck = new System.Windows.Forms.CheckBox();
			this.CtrlCheck = new System.Windows.Forms.CheckBox();
			this.ShiftCheck = new System.Windows.Forms.CheckBox();
			this.dictionaryTab = new System.Windows.Forms.TabPage();
			this.DictionaryList = new System.Windows.Forms.ListView();
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			this.wordsColumn = new System.Windows.Forms.ColumnHeader();
			this.OkButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.optionsTabControl.SuspendLayout();
			this.generalTab.SuspendLayout();
			this.hotKeyTab.SuspendLayout();
			this.dictionaryTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionsTabControl
			// 
			this.optionsTabControl.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.optionsTabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																							this.generalTab,
																							this.hotKeyTab,
																							this.dictionaryTab});
			this.optionsTabControl.Location = new System.Drawing.Point(8, 8);
			this.optionsTabControl.Name = "optionsTabControl";
			this.optionsTabControl.SelectedIndex = 0;
			this.optionsTabControl.Size = new System.Drawing.Size(352, 184);
			this.optionsTabControl.TabIndex = 0;
			// 
			// generalTab
			// 
			this.generalTab.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.LoadWithWindowsCheck,
																					 this.IgnoreUpperCheck,
																					 this.IgnoreDigitsCheck});
			this.generalTab.Location = new System.Drawing.Point(4, 22);
			this.generalTab.Name = "generalTab";
			this.generalTab.Size = new System.Drawing.Size(344, 158);
			this.generalTab.TabIndex = 0;
			this.generalTab.Text = "General";
			// 
			// LoadWithWindowsCheck
			// 
			this.LoadWithWindowsCheck.Checked = true;
			this.LoadWithWindowsCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.LoadWithWindowsCheck.Location = new System.Drawing.Point(32, 104);
			this.LoadWithWindowsCheck.Name = "LoadWithWindowsCheck";
			this.LoadWithWindowsCheck.Size = new System.Drawing.Size(288, 24);
			this.LoadWithWindowsCheck.TabIndex = 2;
			this.LoadWithWindowsCheck.Text = "Load When Windows Starts";
			// 
			// IgnoreUpperCheck
			// 
			this.IgnoreUpperCheck.Checked = true;
			this.IgnoreUpperCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IgnoreUpperCheck.Location = new System.Drawing.Point(32, 48);
			this.IgnoreUpperCheck.Name = "IgnoreUpperCheck";
			this.IgnoreUpperCheck.Size = new System.Drawing.Size(296, 24);
			this.IgnoreUpperCheck.TabIndex = 1;
			this.IgnoreUpperCheck.Text = "Ignore Words in all Upper Case";
			// 
			// IgnoreDigitsCheck
			// 
			this.IgnoreDigitsCheck.Location = new System.Drawing.Point(32, 24);
			this.IgnoreDigitsCheck.Name = "IgnoreDigitsCheck";
			this.IgnoreDigitsCheck.Size = new System.Drawing.Size(296, 24);
			this.IgnoreDigitsCheck.TabIndex = 0;
			this.IgnoreDigitsCheck.Text = "Ignore Words with Digits";
			// 
			// hotKeyTab
			// 
			this.hotKeyTab.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.HotKeyDescLabel,
																					this.KeyTextBox,
																					this.PlusLabel,
																					this.WinCheck,
																					this.AltCheck,
																					this.CtrlCheck,
																					this.ShiftCheck});
			this.hotKeyTab.Location = new System.Drawing.Point(4, 22);
			this.hotKeyTab.Name = "hotKeyTab";
			this.hotKeyTab.Size = new System.Drawing.Size(344, 158);
			this.hotKeyTab.TabIndex = 1;
			this.hotKeyTab.Text = "Hot Key";
			// 
			// HotKeyDescLabel
			// 
			this.HotKeyDescLabel.Location = new System.Drawing.Point(32, 16);
			this.HotKeyDescLabel.Name = "HotKeyDescLabel";
			this.HotKeyDescLabel.Size = new System.Drawing.Size(272, 23);
			this.HotKeyDescLabel.TabIndex = 6;
			this.HotKeyDescLabel.Text = "Enter the keyboard combination to activate Tray Spell";
			// 
			// KeyTextBox
			// 
			this.KeyTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.KeyTextBox.Location = new System.Drawing.Point(200, 72);
			this.KeyTextBox.MaxLength = 1;
			this.KeyTextBox.Name = "KeyTextBox";
			this.KeyTextBox.Size = new System.Drawing.Size(24, 20);
			this.KeyTextBox.TabIndex = 5;
			this.KeyTextBox.Text = "s";
			// 
			// PlusLabel
			// 
			this.PlusLabel.Location = new System.Drawing.Point(176, 72);
			this.PlusLabel.Name = "PlusLabel";
			this.PlusLabel.Size = new System.Drawing.Size(16, 23);
			this.PlusLabel.TabIndex = 4;
			this.PlusLabel.Text = "+";
			// 
			// WinCheck
			// 
			this.WinCheck.Checked = true;
			this.WinCheck.CheckState = System.Windows.Forms.CheckState.Checked;
			this.WinCheck.Location = new System.Drawing.Point(128, 56);
			this.WinCheck.Name = "WinCheck";
			this.WinCheck.Size = new System.Drawing.Size(48, 24);
			this.WinCheck.TabIndex = 3;
			this.WinCheck.Text = "Win";
			// 
			// AltCheck
			// 
			this.AltCheck.Location = new System.Drawing.Point(128, 80);
			this.AltCheck.Name = "AltCheck";
			this.AltCheck.Size = new System.Drawing.Size(40, 24);
			this.AltCheck.TabIndex = 2;
			this.AltCheck.Text = "Alt";
			// 
			// CtrlCheck
			// 
			this.CtrlCheck.Location = new System.Drawing.Point(80, 80);
			this.CtrlCheck.Name = "CtrlCheck";
			this.CtrlCheck.Size = new System.Drawing.Size(48, 24);
			this.CtrlCheck.TabIndex = 1;
			this.CtrlCheck.Text = "Ctrl";
			// 
			// ShiftCheck
			// 
			this.ShiftCheck.Location = new System.Drawing.Point(80, 56);
			this.ShiftCheck.Name = "ShiftCheck";
			this.ShiftCheck.Size = new System.Drawing.Size(48, 24);
			this.ShiftCheck.TabIndex = 0;
			this.ShiftCheck.Text = "Shift";
			// 
			// dictionaryTab
			// 
			this.dictionaryTab.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.DictionaryList});
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
			this.DictionaryList.TabIndex = 0;
			this.DictionaryList.View = System.Windows.Forms.View.Details;
			// 
			// nameColumn
			// 
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 230;
			// 
			// wordsColumn
			// 
			this.wordsColumn.Text = "Words";
			this.wordsColumn.Width = 80;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.OkButton.Location = new System.Drawing.Point(192, 200);
			this.OkButton.Name = "OkButton";
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "&OK";
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(280, 200);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.TabIndex = 2;
			this.CancelButton.Text = "&Cancel";
			// 
			// OptionForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 232);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.CancelButton,
																		  this.OkButton,
																		  this.optionsTabControl});
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
			this.hotKeyTab.ResumeLayout(false);
			this.dictionaryTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OptionForm_Load(object sender, System.EventArgs e)
		{
			this.IgnoreDigitsCheck.Checked = SpellingForm.settings.IgnoreWordsWithDigits;
			this.IgnoreUpperCheck.Checked = SpellingForm.settings.IgnoreAllCapsWords;
			this.KeyTextBox.Text = SpellingForm.settings.HotKey.ToString();
			
			if (SpellingForm.settings.HotKeyModifier & KeyModifiers.MOD_ALT = KeyModifiers.MOD_ALT) 
				this.AltCheck.Checked = true;
			else this.AltCheck.Checked = false;

			if (SpellingForm.settings.HotKeyModifier & KeyModifiers.MOD_CONTROL = KeyModifiers.MOD_CONTROL) 
				this.CtrlCheck.Checked = true;
			else this.CtrlCheck.Checked = false;

			if (SpellingForm.settings.HotKeyModifier & KeyModifiers.MOD_SHIFT = KeyModifiers.MOD_SHIFT) 
				this.ShiftCheck.Checked = true;
			else this.ShiftCheck.Checked = false;

			if (SpellingForm.settings.HotKeyModifier & KeyModifiers.MOD_WIN = KeyModifiers.MOD_WIN) 
				this.WinCheck.Checked = true;
			else this.WinCheck.Checked = false;



		}


	}
}
