using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApplication
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private TestExtender.TextBoxExtender textBoxExtender1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;

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
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.textBoxExtender1 = new TestExtender.TextBoxExtender(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.textBoxExtender1)).BeginInit();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(24, 200);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(616, 256);
			this.textBoxExtender1.SetSpellCheck(this.richTextBox1, true);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(24, 32);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(616, 20);
			this.textBoxExtender1.SetSpellCheck(this.textBox1, true);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// textBox2
			// 
			this.textBox2.AcceptsReturn = true;
			this.textBox2.AcceptsTab = true;
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(24, 80);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox2.Size = new System.Drawing.Size(616, 96);
			this.textBoxExtender1.SetSpellCheck(this.textBox2, true);
			this.textBox2.TabIndex = 3;
			this.textBox2.Text = "";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(616, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Text Box Test:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(616, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Multi-line Text Box Test:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(24, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(616, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Rich Text Box Test:";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(664, 478);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.richTextBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.textBoxExtender1)).EndInit();
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
/*
		public enum UnderlineStyle
		{
			Single = 0,
			Double = 1,
			Wavy = 2,
			ThinWavy = 3,
			Wavy2 = 4,
			Wavy3 = 5,
			Bar = 6,
			ThinBar = 7,
			CurveWavy = 8
		}
 

		private void paintUnderline(Graphics g, Point start, Point end, UnderlineStyle style, Pen pen)
		{
			int num1;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			int num7;
			int num8;
			UnderlineStyle style1;
			style1 = style;
			switch (style1)
			{
				
				case UnderlineStyle.Double:
				{
					g.DrawLine(pen, start, end);
					end.Y = (end.Y + 2);
					start.Y = end.Y;
					g.DrawLine(pen, start, end);
					return;
 
				}
				case UnderlineStyle.Wavy:
				{
					if ((end.X - start.X) > 4)
					{
						num1 = start.X;
						while ((num1 <= (end.X - 2)))
						{
							g.DrawLine(pen, num1, start.Y, (num1 + 2), (start.Y + 2));
							//Console.WriteLine("start.X:{0}; start.Y:{1}; end.X:{2}; end.Y:{3};", 
							//	num1, start.Y, (num1 + 2), (start.Y + 2));

							num1 = (num1 + 4);
  
						}
						for (num2 = (start.X + 2); (num2 <= (end.X - 2)); num2 = (num2 + 4))
						{
							g.DrawLine(pen, num2, (start.Y + 2), (num2 + 2), start.Y);
							//Console.WriteLine("start.X:{0}; start.Y:{1}; end.X:{2}; end.Y:{3};", 
							//	num2, (start.Y + 2), (num2 + 2), start.Y);
  
						}
						return;
 
					}
					g.DrawLine(pen, start, end);
					return;
 
				}
				case UnderlineStyle.CurveWavy:
				{
					if ((end.X - start.X) > 4)
					{
						
						ArrayList points = new ArrayList();
						
						num1 = start.X;
						while ((num1 <= (end.X - 3)))
						{
							points.Add(new Point(num1, start.Y));
							points.Add(new Point((num1 + 2), (start.Y + 2)));
							
							num1 = (num1 + 6);
						}
						Point[] linePoints = (Point[])points.ToArray(typeof(Point));
						g.DrawCurve(pen, linePoints, 0F);
					}
					g.DrawLine(pen, start, end);
					return;
				}
				case UnderlineStyle.ThinWavy:
				{
					if ((end.X - start.X) > 4)
					{
						num7 = start.X;
						while ((num7 <= (end.X - 2)))
						{
							g.DrawLine(pen, num7, (start.Y - 1), (num7 + 2), start.Y);
							num7 = (num7 + 4);
  
						}
						for (num8 = (start.X + 2); (num8 <= (end.X - 2)); num8 = (num8 + 4))
						{
							g.DrawLine(pen, num8, start.Y, (num8 + 2), (start.Y - 1));
  
						}
						return;
 
					}
					g.DrawLine(pen, start, end);
					return;
 
				}
				case UnderlineStyle.Wavy2:
				{
					if ((end.X - start.X) > 4)
					{
						num3 = start.X;
						while ((num3 <= (end.X - 3)))
						{
							g.DrawLine(pen, num3, start.Y, (num3 + 2), (start.Y + 2));
							num3 = (num3 + 6);
  
						}
						for (num4 = (start.X + 3); (num4 <= (end.X - 3)); num4 = (num4 + 6))
						{
							g.DrawLine(pen, num4, (start.Y + 2), (num4 + 3), start.Y);
  
						}
						return;
 
					}
					g.DrawLine(pen, start, end);
					return;
 
				}
				case UnderlineStyle.Wavy3:
				{
					if ((end.X - start.X) > 4)
					{
						num5 = start.X;
						while ((num5 <= (end.X - 3)))
						{
							g.DrawLine(pen, num5, start.Y, (num5 + 3), (start.Y + 2));
							num5 = (num5 + 6);
  
						}
						for (num6 = (start.X + 3); (num6 <= (end.X - 3)); num6 = (num6 + 6))
						{
							g.DrawLine(pen, num6, (start.Y + 2), (num6 + 3), start.Y);
  
						}
						return;
 
					}
					g.DrawLine(pen, start, end);
					return;
 
				}
				case UnderlineStyle.Bar:
				{
					g.DrawLine(pen, start, end);
					g.DrawLine(pen, (start.X - 1), (start.Y + 1), (start.X - 1), (start.Y - 4));
					g.DrawLine(pen, (end.X + 1), (end.Y + 1), (end.X + 1), (end.Y - 4));
					return;
 
				}
				case UnderlineStyle.ThinBar:
				{
					g.DrawLine(pen, start.X, (start.Y - 1), end.X, (end.Y - 1));
					g.DrawLine(pen, start.X, start.Y, start.X, (start.Y - 2));
					g.DrawLine(pen, end.X, end.Y, end.X, (end.Y - 2));
					return;
 
				}
 
			}
			g.DrawLine(pen, start, end);
 


		} 


	
		protected override void OnPaint(PaintEventArgs e)
		{
			this.paintUnderline(e.Graphics, new Point(20, 20), new Point(200, 20), UnderlineStyle.Wavy, new Pen(Color.Red, 1F));
			this.paintUnderline(e.Graphics, new Point(20, 30), new Point(200, 30), UnderlineStyle.Wavy2, new Pen(Color.Red, 1F));
			this.paintUnderline(e.Graphics, new Point(20, 40), new Point(200, 40), UnderlineStyle.Wavy3, new Pen(Color.Red, 1F));
			this.paintUnderline(e.Graphics, new Point(20, 50), new Point(200, 50), UnderlineStyle.ThinWavy, new Pen(Color.Red, 1F));
			this.paintUnderline(e.Graphics, new Point(20, 80), new Point(200, 80), UnderlineStyle.ThinWavy, new Pen(Color.Red, 1F));
			base.OnPaint (e);
		}
		*/
	}
}
