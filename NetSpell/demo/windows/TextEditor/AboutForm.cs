// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace TextEditor
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{

		private AssemblyInfo aInfo = new AssemblyInfo();
		private System.Windows.Forms.ListView assembliesListView;
		private System.Windows.Forms.ColumnHeader assemblyColumnHeader;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader dateColumnHeader;
		private System.Windows.Forms.ColumnHeader versionColumnHeader;
		private System.Windows.Forms.GroupBox VersionGroup;
		internal System.Windows.Forms.Label lblCopyright;
		internal System.Windows.Forms.Label lblDescription;
		internal System.Windows.Forms.Label lblTitle;
		internal System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.LinkLabel linkLabel1;
		internal System.Windows.Forms.PictureBox pbIcon;

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			// Fill in loaded modules / version number info list view.
			try 
			{
				// Set this Form's Text & Icon properties by using values from the parent form
				this.Text = "About " + this.Owner.Text;
				this.Icon = this.Owner.Icon;
				// Set this Form's Picture Box's image using the parent's icon 
				// However, we need to convert it to a Bitmap since the Picture Box Control
				// will not accept a raw Icon.
				this.pbIcon.Image = this.Owner.Icon.ToBitmap();

				// Set the labels identitying the Title, Version, and Description by
				// reading Assembly meta-data originally entered in the AssemblyInfo.vb file
				this.lblTitle.Text = aInfo.Title;
				this.lblVersion.Text = String.Format("Version {0}", aInfo.Version);
				this.lblCopyright.Text = aInfo.Copyright;
				this.lblDescription.Text = aInfo.Description;
				//this.lblCompany.Text = aInfo.Company;

				assembliesListView.Items.Clear();

				// Get all modules
				ArrayList localItems = new ArrayList();
				foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
				{
					ListViewItem item = new ListViewItem();
					item.Text = module.ModuleName;

					// Get version info
					FileVersionInfo verInfo = module.FileVersionInfo;
					string versionStr = String.Format("{0}.{1}.{2}.{3}", 
						verInfo.FileMajorPart,
						verInfo.FileMinorPart,
						verInfo.FileBuildPart,
						verInfo.FilePrivatePart);
					item.SubItems.Add(versionStr);

					// Get file date info
					DateTime lastWriteDate = File.GetLastWriteTime(module.FileName);
					string dateStr = lastWriteDate.ToString("MMM dd, yyyy");
					item.SubItems.Add(dateStr);

					assembliesListView.Items.Add(item);

					// Stash assemply related list view items for later
					if (module.ModuleName.ToLower().StartsWith("netspell"))
					{
						localItems.Add(item);
					}
				}

				// Extract the assemply related modules and move them to the top
				for (int i = localItems.Count; i > 0; i--)
				{
					ListViewItem localItem = (ListViewItem)localItems[i-1];
					assembliesListView.Items.Remove(localItem);
					assembliesListView.Items.Insert(0, localItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.ToString(), "About Form Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

#region Constructor / Dispose
		/// <summary>
		/// 
		/// </summary>
		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
#endregion

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.okButton = new System.Windows.Forms.Button();
			this.VersionGroup = new System.Windows.Forms.GroupBox();
			this.assembliesListView = new System.Windows.Forms.ListView();
			this.assemblyColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.versionColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.dateColumnHeader = new System.Windows.Forms.ColumnHeader();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.VersionGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.okButton.Location = new System.Drawing.Point(328, 288);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 0;
			this.okButton.Text = "&Ok";
			// 
			// VersionGroup
			// 
			this.VersionGroup.Controls.Add(this.assembliesListView);
			this.VersionGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.VersionGroup.ForeColor = System.Drawing.SystemColors.Highlight;
			this.VersionGroup.Location = new System.Drawing.Point(8, 136);
			this.VersionGroup.Name = "VersionGroup";
			this.VersionGroup.Size = new System.Drawing.Size(400, 144);
			this.VersionGroup.TabIndex = 9;
			this.VersionGroup.TabStop = false;
			this.VersionGroup.Text = "Version Information";
			// 
			// assembliesListView
			// 
			this.assembliesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.assemblyColumnHeader,
																								 this.versionColumnHeader,
																								 this.dateColumnHeader});
			this.assembliesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.assembliesListView.Location = new System.Drawing.Point(8, 16);
			this.assembliesListView.Name = "assembliesListView";
			this.assembliesListView.Size = new System.Drawing.Size(384, 120);
			this.assembliesListView.TabIndex = 9;
			this.assembliesListView.View = System.Windows.Forms.View.Details;
			// 
			// assemblyColumnHeader
			// 
			this.assemblyColumnHeader.Text = "Module";
			this.assemblyColumnHeader.Width = 160;
			// 
			// versionColumnHeader
			// 
			this.versionColumnHeader.Text = "Version";
			this.versionColumnHeader.Width = 105;
			// 
			// dateColumnHeader
			// 
			this.dateColumnHeader.Text = "Date";
			this.dateColumnHeader.Width = 95;
			// 
			// lblCopyright
			// 
			this.lblCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblCopyright.Location = new System.Drawing.Point(72, 56);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(328, 23);
			this.lblCopyright.TabIndex = 14;
			this.lblCopyright.Text = "Application Copyright";
			// 
			// lblDescription
			// 
			this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblDescription.Location = new System.Drawing.Point(72, 80);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(328, 32);
			this.lblDescription.TabIndex = 13;
			this.lblDescription.Text = "Application Description";
			// 
			// lblVersion
			// 
			this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblVersion.Location = new System.Drawing.Point(72, 32);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(328, 23);
			this.lblVersion.TabIndex = 12;
			this.lblVersion.Text = "Application Version";
			// 
			// lblTitle
			// 
			this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.lblTitle.Location = new System.Drawing.Point(72, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(328, 24);
			this.lblTitle.TabIndex = 11;
			this.lblTitle.Text = "Application Title";
			// 
			// pbIcon
			// 
			this.pbIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.pbIcon.Location = new System.Drawing.Point(16, 8);
			this.pbIcon.Name = "pbIcon";
			this.pbIcon.Size = new System.Drawing.Size(32, 32);
			this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbIcon.TabIndex = 10;
			this.pbIcon.TabStop = false;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(72, 112);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(328, 23);
			this.linkLabel1.TabIndex = 15;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.loresoft.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// AboutForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(418, 320);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.pbIcon);
			this.Controls.Add(this.VersionGroup);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About ...";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.VersionGroup.ResumeLayout(false);
			this.ResumeLayout(false);

		}
#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.loresoft.com");
		}

	}

	public class AssemblyInfo
	{
		private Type myType = typeof(AboutForm);

		/// <summary>
		/// CodeBase of Assembly
		/// </summary>
		public string CodeBase
		{
			get {return myType.Assembly.CodeBase.ToString();}
		}

		/// <summary>
		/// Company of Assembly
		/// </summary>
		public string Company
		{
			get
			{
				Object[] r = myType.Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
				AssemblyCompanyAttribute ct = (AssemblyCompanyAttribute)r[0];
				return ct.Company;
			}
		}
	
		/// <summary>
		/// Copyright of Assembly
		/// </summary>
		public string Copyright
		{
			get
			{
				Object[] r = myType.Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
				AssemblyCopyrightAttribute ct = (AssemblyCopyrightAttribute)r[0];
				return ct.Copyright;
			}
		}

		/// <summary>
		/// Description of Assembly
		/// </summary>
		public string Description
		{
			get
			{
				Object[] r = myType.Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
				AssemblyDescriptionAttribute ct = (AssemblyDescriptionAttribute)r[0];
				return ct.Description;
			}
		}

		/// <summary>
		///		FullName of Assembly
		/// </summary>
		public string FullName
		{
			get {return myType.Assembly.GetName().FullName.ToString();}
		}

		/// <summary>
		/// Name of Assembly
		/// </summary>
		public string Name
		{
			get	{return myType.Assembly.GetName().Name.ToString();}
		}

		/// <summary>
		/// Product of Assembly
		/// </summary>
		public string Product
		{
			get
			{
				Object[] r = myType.Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
				AssemblyProductAttribute ct = (AssemblyProductAttribute)r[0];
				return ct.Product;
			}
		}

		/// <summary>
		/// Title of Assembly
		/// </summary>
		public string Title
		{
			get
			{
				Object[] r = myType.Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
				AssemblyTitleAttribute ct = (AssemblyTitleAttribute)r[0];
				return ct.Title;
			}
		}

		/// <summary>
		/// Version of Assembly
		/// </summary>
		public string Version
		{
			get { return myType.Assembly.GetName().Version.ToString(); }
		}
	}
}