using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SpellAsYouTypeDemo
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private SpellAsYouTypeDemo.AYTTextBox textBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
			this.textBox1 = new AYTTextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(0, 0);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(292, 266);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = @"MyOE is a free mail and news reader developed using the Microsoft® NET® Framework.

We went to MyOE because none of the free available news/mails clients did fit
our needs. We are currently focusing on the following points:

Security: that’s one of the main problems with a
well known mail client; we want it to be native secure by default: no HTML from unknown people, no javascript, attachment are guarded.

Rich and flexible GUI: Most of mail readers have very rigid GUI, no way to change it, we want MyOE to be highly customizable. (For instance: Folder trees, messages trees and messages preview will be organised on dockable windows, so you will be able to organise them in the way you want

Clean storage: Do you think it’s normal that after reinstalling windows you’ve got to reconfigure everything? We don’t! MyOE configuration, accounts and mails/posts will be easy to backup/restore. 

Speed: some clients are very slow, for instance they are checking only one account at at time instead of optimising use of available bandwidth. We want MyOE to be one of the fastest available clients.

As a GPL product, the source code (C#) for MyOE is available on sourceforge, if you want to help us, feel free to post on our forums";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
	}
}
