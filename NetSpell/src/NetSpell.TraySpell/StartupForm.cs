using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using NetSpell.SpellChecker;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class StartupForm : System.Windows.Forms.Form
	{
		
		private SpellingForm _spellingForm = new SpellingForm();
		private System.ComponentModel.IContainer components;
		
		public StartupForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

#region Spelling Events
		private void AttachEvents()
		{
			
		}

		
#endregion //Spelling Events

		/// <summary>
		/// The main entry point for the application.
		/// </summary>


		private void StartupForm_Load(object sender, System.EventArgs e)
		{
			this.AttachEvents();
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

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new StartupForm());
		}

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// StartupForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(178, 78);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StartupForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Startup Form";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.StartupForm_Load);

		}
#endregion

	}

}
