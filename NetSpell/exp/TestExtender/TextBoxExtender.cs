using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace TestExtender
{
	/// <summary>
	/// Summary description for Component1.
	/// </summary>
	[ProvideProperty("SpellCheck", typeof(TextBoxBase))]
	public class TextBoxExtender : Component, ISupportInitialize, IExtenderProvider, IMessageFilter
	{

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private Hashtable textBoxes = new Hashtable();
		private Hashtable controlHandels = new Hashtable();


		public TextBoxExtender(System.ComponentModel.IContainer container)
		{
			/// Required for Windows.Forms Class Composition Designer support
			container.Add(this);
			InitializeComponent();
			Initialize();
		}

		public TextBoxExtender()
		{
			/// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();
			Initialize();
		}

		private void Initialize()
		{
			Application.AddMessageFilter(this);
		}

		[
		Category("SpellChecker"),
		Description("Realtime Spell Check this Control"),
		]
		public bool GetSpellCheck(TextBoxBase extendee)
		{
			if(this.textBoxes.Contains(extendee))
				return (bool)this.textBoxes[extendee];

			return false;
		}

		public void SetSpellCheck(TextBoxBase extendee, bool value)
		{
			if(value)
			{
				this.textBoxes[extendee] = value;
				this.controlHandels[extendee.Handle] = extendee;
				extendee.TextChanged +=new EventHandler(extendee_TextChanged);
				extendee.MouseDown += new MouseEventHandler(extendee_MouseDown);
				extendee.MouseUp += new MouseEventHandler(extendee_MouseUp);
				extendee.KeyDown += new KeyEventHandler(extendee_KeyDown);
				extendee.KeyUp +=new KeyEventHandler(extendee_KeyUp);
				
			}
			else
			{
				this.textBoxes.Remove(extendee);
				this.controlHandels.Remove(extendee.Handle);
				extendee.TextChanged -=new EventHandler(extendee_TextChanged);
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


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	
		#region IExtenderProvider Members

		public bool CanExtend(object extendee)
		{
			if (extendee is TextBoxBase)
				return true; 

			return false;
		}

		#endregion

		#region IMessageFilter Members

		private const int WM_PAINT = 15; 

		public bool PreFilterMessage(ref Message m)
		{
			// only listen to extended controls
			if(this.controlHandels.Contains(m.HWnd))
			{
				switch (m.Msg) 
				{
					case WM_PAINT:
						TextBoxBase textBox = (TextBoxBase)this.controlHandels[m.HWnd];
						Console.WriteLine("PAINT {0}", textBox.Name);
						break;
				}
			}

			// never handle messages
			return false;
		}

		#endregion
	
		#region ISupportInitialize Members

		public void BeginInit()
		{
			// TODO:  Add TextBoxExtender.BeginInit implementation
		}

		public void EndInit()
		{
			// TODO:  Add TextBoxExtender.EndInit implementation
		}

		#endregion

		private void extendee_TextChanged(object sender, EventArgs e)
		{
			if(sender is TextBoxBase)
			{
				TextBoxBase textBox = (TextBoxBase)sender;
				Console.WriteLine("TextChanged:{0}", textBox.Name);
			}
		}

		private void extendee_Paint(object sender, PaintEventArgs e)
		{

		}

		private void extendee_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void extendee_MouseUp(object sender, MouseEventArgs e)
		{

		}

		private void extendee_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private void extendee_KeyUp(object sender, KeyEventArgs e)
		{

		}
	}
}
