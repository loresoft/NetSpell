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
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Diagnostics;
using NetSpell.SpellChecker;

namespace NetSpell.DictionaryBuild
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmDictionary : System.Windows.Forms.Form
	{

		private ArrayList _fileList = new ArrayList();
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnLoadList;
		private System.Windows.Forms.Button btnSaveList;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private PerformanceTimer timer = new PerformanceTimer();
		private System.Windows.Forms.TextBox txtLog;


		public frmDictionary()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			openFileDialog1.FilterIndex = 3 ;
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				listBox1.Items.AddRange(openFileDialog1.FileNames);
			}

		}

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			Dictionary dict = new Dictionary();
			
			timer.StartTimer();
			// adding words ...
			for (int i = 0; i < listBox1.Items.Count; i++) 
			{
				WriteLog("Adding words from file " + listBox1.Items[i].ToString());
				
				StreamReader tempFile = new StreamReader(File.OpenRead(listBox1.Items[i].ToString()), Encoding.UTF8);
				string fileContents = tempFile.ReadToEnd();
				tempFile.Close();

				fileContents = fileContents.Replace("\r", "");
				
				dict.WordList.AddRange(fileContents.Split());
			}

			dict.WordList.Sort();

			DoubleMetaphone meta = new DoubleMetaphone();
			string lastWord = "";
			
			// cleaning word list
			WriteLog("Cleaning and Sorting word list ... ");

			for (int i = 0; i < dict.WordList.Count; i++)
			{
				if(dict.WordList[i].ToString().Length == 0 || dict.WordList[i].ToString() == lastWord) 
				{
					// if null line or duplicate word, remove
					dict.WordList.RemoveAt(i);
					i--;
				}
				else
				{
					lastWord = dict.WordList[i].ToString();
					meta.GenerateMetaphone(dict.WordList[i].ToString());
					// add Metaphone codes to end of word
					dict.WordList[i] += "|" + meta.PrimaryCode + "|" + meta.SecondaryCode + "|"; 
				}
			}

			WriteLog("Create Time:" + timer.ElapsedTime.ToString() + " sec");
			WriteLog("Saving Word List ... ");
			
			if(saveFileDialog1.ShowDialog() == DialogResult.OK) 
			{
				timer.StartTimer();
				dict.Save(saveFileDialog1.FileName);
				WriteLog("Save Time:" + timer.ElapsedTime.ToString() + " sec");
			}
			WriteLog(dict.WordList.Count.ToString() + " words in Word List");

		}

		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			Dictionary dict = new Dictionary();
			
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				WriteLog("Loading ...");

				timer.StartTimer();
				dict.Load(openFileDialog1.FileName);

				WriteLog("Load Time:" + timer.ElapsedTime.ToString() + " sec");
				WriteLog(dict.WordList.Count.ToString() + " words in Word List");
			}
		}

		private void btnLoadList_Click(object sender, System.EventArgs e)
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				WriteLog("Loading File List ...");

				XmlSerializer serializer = new XmlSerializer(typeof(ArrayList));
				FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open);
				_fileList = (ArrayList) serializer.Deserialize(fs);
				listBox1.DataSource = null;
				listBox1.DataSource = _fileList;
				fs.Close();

				statusBar1.Text = "";
			}
		}

		private void btnSaveList_Click(object sender, System.EventArgs e)
		{
			
			if(saveFileDialog1.ShowDialog() == DialogResult.OK) 
			{
				
				WriteLog("Saving File List ...");

				_fileList = new ArrayList(listBox1.Items);
				XmlSerializer serializer = new XmlSerializer(typeof(ArrayList));
				TextWriter writer = new StreamWriter(saveFileDialog1.FileName);
				serializer.Serialize(writer, _fileList);
				writer.Close();

				statusBar1.Text = "";
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmDictionary());
		}
		
		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		
		private void WriteLog(string message)
		{
			statusBar1.Text = message;
			this.txtLog.Text += message + "\r\n";
			this.txtLog.ScrollToCaret();
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
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnLoadList = new System.Windows.Forms.Button();
			this.btnSaveList = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 297);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(592, 16);
			this.statusBar1.TabIndex = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem11});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem10});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Add File";
			this.menuItem2.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Save File List";
			this.menuItem4.Click += new System.EventHandler(this.btnSaveList_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "Load File List";
			this.menuItem5.Click += new System.EventHandler(this.btnLoadList_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "Create Dictionary";
			this.menuItem7.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "Load Dictionary";
			this.menuItem8.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 8;
			this.menuItem10.Text = "Exit";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem12});
			this.menuItem11.Text = "Help";
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 0;
			this.menuItem12.Text = "About";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabPage1,
																					  this.tabPage2});
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(592, 297);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.listBox1,
																				   this.panel1});
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(584, 271);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Files";
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.btnLoadList,
																				 this.btnSaveList,
																				 this.btnLoad,
																				 this.btnCreate,
																				 this.btnAdd});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(464, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(120, 271);
			this.panel1.TabIndex = 6;
			// 
			// btnLoadList
			// 
			this.btnLoadList.Location = new System.Drawing.Point(8, 96);
			this.btnLoadList.Name = "btnLoadList";
			this.btnLoadList.Size = new System.Drawing.Size(104, 23);
			this.btnLoadList.TabIndex = 8;
			this.btnLoadList.Text = "Load File List";
			this.btnLoadList.Click += new System.EventHandler(this.btnLoadList_Click);
			// 
			// btnSaveList
			// 
			this.btnSaveList.Location = new System.Drawing.Point(8, 64);
			this.btnSaveList.Name = "btnSaveList";
			this.btnSaveList.Size = new System.Drawing.Size(104, 23);
			this.btnSaveList.TabIndex = 7;
			this.btnSaveList.Text = "Save File List";
			this.btnSaveList.Click += new System.EventHandler(this.btnSaveList_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(8, 176);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(104, 23);
			this.btnLoad.TabIndex = 6;
			this.btnLoad.Text = "Load Dictionary";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(8, 144);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(104, 23);
			this.btnCreate.TabIndex = 5;
			this.btnCreate.Text = "Create Dictionary";
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(8, 8);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(104, 23);
			this.btnAdd.TabIndex = 4;
			this.btnAdd.Text = "Add File";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.Name = "listBox1";
			this.listBox1.ScrollAlwaysVisible = true;
			this.listBox1.Size = new System.Drawing.Size(464, 264);
			this.listBox1.TabIndex = 5;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				   this.txtLog});
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(624, 335);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Log";
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(624, 335);
			this.txtLog.TabIndex = 0;
			this.txtLog.Text = "";
			this.txtLog.WordWrap = false;
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "XML Files (*.xml)|*.xml|Dictionary files (*.dic)|*.dic|Text files (*.txt)|*.txt|A" +
				"ll files (*.*)|*.*";
			this.openFileDialog1.Multiselect = true;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.FileName = "doc1";
			this.saveFileDialog1.Filter = "XML Files (*.xml)|*.xml|Dictionary files (*.dic)|*.dic|Text files (*.txt)|*.txt|A" +
				"ll files (*.*)|*.*";
			this.saveFileDialog1.FilterIndex = 2;
			// 
			// frmDictionary
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(592, 313);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl1,
																		  this.statusBar1});
			this.Menu = this.mainMenu1;
			this.Name = "frmDictionary";
			this.Text = "Dictionary Build";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
#endregion

	}
}
