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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Configuration;
using NetSpell.SpellChecker;

namespace NetSpell.Demo.Web
{
	/// <summary>
	/// Summary description for spell.
	/// </summary>
	public class spell : System.Web.UI.Page
	{
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Button CancelButton;
		protected System.Web.UI.HtmlControls.HtmlInputHidden CurrentText;
		protected System.Web.UI.WebControls.Label CurrentWord;
		protected System.Web.UI.WebControls.Button IgnoreAllButton;
		protected System.Web.UI.WebControls.Button IgnoreButton;
		protected System.Web.UI.HtmlControls.HtmlInputHidden IgnoreList;
		protected System.Web.UI.WebControls.Button ReplaceAllButton;
		protected System.Web.UI.WebControls.Button ReplaceButton;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ReplaceList;
		protected System.Web.UI.WebControls.TextBox ReplacementWord;
		protected System.Web.UI.WebControls.Panel SpellcheckComplete;
		protected NetSpell.SpellChecker.Spelling spelling;
		protected System.Web.UI.HtmlControls.HtmlGenericControl SpellingBody;
		protected System.Web.UI.WebControls.Panel SuggestionForm;
		protected System.Web.UI.WebControls.ListBox Suggestions;
		protected System.Web.UI.HtmlControls.HtmlInputHidden WordIndex;

		private void IgnoreAllButton_Click(object sender, System.EventArgs e)
		{
			this.spelling.IgnoreAllWord();
			this.spelling.SpellCheck();
		}

		private void IgnoreButton_Click(object sender, System.EventArgs e)
		{
			this.spelling.IgnoreWord();
			this.spelling.SpellCheck();
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			Suggestions.Attributes.Add("onChange", "javascript: changeWord(this);");
			SpellingBody.Attributes.Add("onLoad", "javascript: updateCallingPage();");

			// loading dictionary from cache
			Dictionary dict = (Dictionary)HttpContext.Current.Cache["MainDictionary"];
			if (dict == null)
			{
				string fileName = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["DictionaryFile"]);
				dict = new Dictionary(fileName);
				// Store the Dictionary in cache
                HttpContext.Current.Cache.Insert("MainDictionary", dict, new CacheDependency(fileName));

			}
			this.spelling.Dictionaries.Add(dict);

			// start spell checking

			if (Request.Params["CurrentText"] != null)
			{
				this.spelling.Text = Request.Params["CurrentText"];
			}

			if (Request.Params["WordIndex"] != null)
			{
				this.spelling.WordIndex = int.Parse(Request.Params["WordIndex"]);
			}

			if(Request.Params["SpellCheck"] != null)
			{
				this.spelling.SpellCheck();
			}
			
		}

		private void ReplaceAllButton_Click(object sender, System.EventArgs e)
		{
			this.spelling.ReplaceAllWord(this.ReplacementWord.Text);
			this.CurrentText.Value = this.spelling.Text;
			this.spelling.SpellCheck();
		}

		private void ReplaceButton_Click(object sender, System.EventArgs e)
		{
			this.spelling.ReplaceWord(this.ReplacementWord.Text);
			this.CurrentText.Value = this.spelling.Text;
			this.spelling.SpellCheck();
		}

		private void spelling_DoubledWord(object sender, NetSpell.SpellChecker.WordEventArgs args)
		{
			this.CurrentText.Value = this.spelling.Text;
			this.WordIndex.Value = this.spelling.WordIndex.ToString();
			this.CurrentWord.Text = this.spelling.CurrentWord;

			this.SuggestionForm.Visible = true;
			this.SpellcheckComplete.Visible = false;

			this.Suggestions.Items.Clear();
			this.ReplacementWord.Text = string.Empty;
		}

		private void spelling_EndOfText(object sender, System.EventArgs args)
		{
			this.CurrentText.Value = this.spelling.Text;
			this.WordIndex.Value = this.spelling.WordIndex.ToString();

			this.SuggestionForm.Visible = false;
			this.SpellcheckComplete.Visible = true;

		}

		private void spelling_MisspelledWord(object sender, NetSpell.SpellChecker.WordEventArgs args)
		{
			this.CurrentText.Value = this.spelling.Text;
			this.WordIndex.Value = this.spelling.WordIndex.ToString();
			this.CurrentWord.Text = this.spelling.CurrentWord;

			this.SuggestionForm.Visible = true;
			this.SpellcheckComplete.Visible = false;

			this.spelling.Suggest();

			this.Suggestions.DataSource = this.spelling.Suggestions;
			this.Suggestions.DataBind();

			this.ReplacementWord.Text = string.Empty;
		}

#region Web Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
			this.spelling = new NetSpell.SpellChecker.Spelling(this.components);
			// 
			// spelling
			// 
			this.spelling.ShowDialog = false;
			this.spelling.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.spelling_MisspelledWord);
			this.spelling.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.spelling_EndOfText);
			this.spelling.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.spelling_DoubledWord);
			this.IgnoreButton.Click += new System.EventHandler(this.IgnoreButton_Click);
			this.IgnoreAllButton.Click += new System.EventHandler(this.IgnoreAllButton_Click);
			this.ReplaceButton.Click += new System.EventHandler(this.ReplaceButton_Click);
			this.ReplaceAllButton.Click += new System.EventHandler(this.ReplaceAllButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
#endregion

	}
}
