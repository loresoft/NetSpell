// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for HotkeyForm.
	/// </summary>
	public class HotkeyForm : System.Windows.Forms.Form
	{

		private bool _AltModifier;
		private bool _ControlModifier;
		private Keys _HotKey;
		private bool _ShiftModifier;
		private System.Windows.Forms.Button CancelBtn;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox Hotkey;
		private System.Windows.Forms.GroupBox HotKeyGroup;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OkBtn;

		public HotkeyForm()
		{
			InitializeComponent();
			this.LoadHotkeyConfig();
		}

		private void Hotkey_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			try
			{
				if(e.KeyCode != Keys.ControlKey && e.KeyCode != Keys.ShiftKey 
					&& e.KeyCode != Keys.Alt && e.KeyCode != Keys.Menu 
					&& e.KeyCode != Keys.LWin && e.KeyCode != Keys.RWin)
				{
					this.ControlModifier = e.Control;
					this.ShiftModifier = e.Shift;
					this.AltModifier = e.Alt;
					this.HotKey = e.KeyCode;
				}

				if(sender is TextBox)
					((TextBox)sender).Text = this.DisplayHotkey();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void Hotkey_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		private void HotkeyForm_VisibleChanged(object sender, System.EventArgs e)
		{
			if(this.Visible) 
			{
				//this.LoadHotkeyConfig();
				this.Hotkey.Text = this.DisplayHotkey();
			}
		}


		private void OkBtn_Click(object sender, System.EventArgs e)
		{
			this.SaveHotkeyConfig();
		}

		public string DisplayHotkey()
		{
			string sHotKey = "";
			
			try
			{
				if (this.ControlModifier)
				{
					if (sHotKey.Length!=0) sHotKey += " + ";
					sHotKey += "Ctrl";
				}     
				if (this.AltModifier)
				{
					if (sHotKey.Length!=0) sHotKey += " + ";
					sHotKey += "Shift";
				}
				if (this.ShiftModifier)
				{
					if (sHotKey.Length!=0) sHotKey += " + ";
					sHotKey += "Alt";
				}
				if (sHotKey.Length!=0) 
				{
					sHotKey += " + ";
					sHotKey += this.HotKey.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return sHotKey;
		}

		public void LoadHotkeyConfig()
		{
			try
			{
				AppSettingsReader configReader = new AppSettingsReader();

				this.ControlModifier = ((bool)(configReader.GetValue("ControlModifier", typeof(bool))));
				this.AltModifier = ((bool)(configReader.GetValue("AltModifier", typeof(bool))));
				this.ShiftModifier = ((bool)(configReader.GetValue("ShiftModifier", typeof(bool))));
				this.HotKey = ((Keys)(configReader.GetValue("Hotkey", typeof(int))));
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void SaveHotkeyConfig()
		{
			try
			{
				AppSettingsWriter configWriter = new AppSettingsWriter();

				configWriter.SetValue("ControlModifier", this.ControlModifier.ToString());
				configWriter.SetValue("AltModifier", this.AltModifier.ToString());
				configWriter.SetValue("ShiftModifier", this.ShiftModifier.ToString());
				configWriter.SetValue("Hotkey", ((int)this.HotKey).ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("{0}\n\n{1}" ,ex.Message, ex.ToString()), 
					"Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

		public bool AltModifier
		{
			get {return _AltModifier;}
			set {_AltModifier = value;}
		}

		public bool ControlModifier
		{
			get {return _ControlModifier;}
			set {_ControlModifier = value;}
		}

		public Keys HotKey
		{
			get {return _HotKey;}
			set {_HotKey = value;}
		}

		public bool ShiftModifier
		{
			get {return _ShiftModifier;}
			set {_ShiftModifier = value;}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HotkeyForm));
			this.HotKeyGroup = new System.Windows.Forms.GroupBox();
			this.Hotkey = new System.Windows.Forms.TextBox();
			this.OkBtn = new System.Windows.Forms.Button();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.HotKeyGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// HotKeyGroup
			// 
			this.HotKeyGroup.Controls.Add(this.label1);
			this.HotKeyGroup.Controls.Add(this.Hotkey);
			this.HotKeyGroup.Location = new System.Drawing.Point(16, 16);
			this.HotKeyGroup.Name = "HotKeyGroup";
			this.HotKeyGroup.Size = new System.Drawing.Size(192, 88);
			this.HotKeyGroup.TabIndex = 0;
			this.HotKeyGroup.TabStop = false;
			this.HotKeyGroup.Text = "Hot Key";
			// 
			// Hotkey
			// 
			this.Hotkey.Location = new System.Drawing.Point(16, 40);
			this.Hotkey.MaxLength = 1;
			this.Hotkey.Name = "Hotkey";
			this.Hotkey.Size = new System.Drawing.Size(160, 20);
			this.Hotkey.TabIndex = 4;
			this.Hotkey.Text = "";
			this.Hotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.Hotkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Hotkey_KeyDown);
			this.Hotkey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Hotkey_KeyPress);
			// 
			// OkBtn
			// 
			this.OkBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkBtn.Location = new System.Drawing.Point(32, 112);
			this.OkBtn.Name = "OkBtn";
			this.OkBtn.TabIndex = 1;
			this.OkBtn.Text = "OK";
			this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
			// 
			// CancelBtn
			// 
			this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBtn.Location = new System.Drawing.Point(120, 112);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.TabIndex = 2;
			this.CancelBtn.Text = "Cancel";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Enter Hotkey Combination";
			// 
			// HotkeyForm
			// 
			this.AcceptButton = this.OkBtn;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.CancelBtn;
			this.ClientSize = new System.Drawing.Size(226, 152);
			this.Controls.Add(this.CancelBtn);
			this.Controls.Add(this.OkBtn);
			this.Controls.Add(this.HotKeyGroup);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HotkeyForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tray Spell Options ...";
			this.VisibleChanged += new System.EventHandler(this.HotkeyForm_VisibleChanged);
			this.HotKeyGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
