// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.IO;
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
using NetSpell.SpellChecker.Dictionary;

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
		protected System.Web.UI.HtmlControls.HtmlInputHidden IgnoreList;
		protected System.Web.UI.WebControls.Panel SpellcheckComplete;
		protected NetSpell.SpellChecker.Spelling SpellChecker;
		protected System.Web.UI.HtmlControls.HtmlGenericControl SpellingBody;
		protected System.Web.UI.WebControls.Panel SuggestionForm;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ReplaceKeyList;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ReplaceValueList;
		protected System.Web.UI.WebControls.Label CurrentWord;
		protected System.Web.UI.WebControls.TextBox ReplacementWord;
		protected System.Web.UI.WebControls.ListBox Suggestions;
		protected System.Web.UI.WebControls.Button IgnoreButton;
		protected System.Web.UI.WebControls.Button IgnoreAllButton;
		protected System.Web.UI.WebControls.Button AddButton;
		protected System.Web.UI.WebControls.Button ReplaceButton;
		protected System.Web.UI.WebControls.Button ReplaceAllButton;
		protected System.Web.UI.WebControls.HyperLink NetSpellLink;
		protected System.Web.UI.HtmlControls.HtmlInputHidden WordIndex;

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.Dictionary.Add(this.SpellChecker.CurrentWord);
			this.SpellChecker.SpellCheck();
		}

		private void IgnoreAllButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.IgnoreAllWord();
			this.SpellChecker.SpellCheck();
		}

		private void IgnoreButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.IgnoreWord();
			this.SpellChecker.SpellCheck();
		}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			Suggestions.Attributes.Add("onChange", "javascript: changeWord(this);");
			SpellingBody.Attributes.Add("onLoad", "javascript: updateCallingPage();");

			// loading dictionary from cache
			WordDictionary dic = (WordDictionary)HttpContext.Current.Cache["WordDictionary"];
			
			if (dic != null)
			{
				this.SpellChecker.Dictionary = dic;
			}

			if (dic == null || !this.SpellChecker.Dictionary.Initialized)
			{
				string fileName = ConfigurationSettings.AppSettings["DictionaryFile"];
				string folderName = ConfigurationSettings.AppSettings["DictionaryFolder"];
				folderName =  Server.MapPath(folderName);

				this.SpellChecker.Dictionary.DictionaryFile = fileName;
				this.SpellChecker.Dictionary.DictionaryFolder = folderName;
				this.SpellChecker.Dictionary.Initialize();

				// Store the Dictionary in cache
				HttpContext.Current.Cache.Insert("WordDictionary", this.SpellChecker.Dictionary, 
					new CacheDependency(Path.Combine(folderName, 
					this.SpellChecker.Dictionary.DictionaryFile)));

			}

			
			// start spell checking
			this.LoadValues();

			if(Request.Params["SpellCheck"] != null)
			{
				this.SpellChecker.SpellCheck();
			}
			
		}

		private void ReplaceAllButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.ReplaceAllWord(this.ReplacementWord.Text);
			this.CurrentText.Value = this.SpellChecker.Text;
			this.SpellChecker.SpellCheck();
		}

		private void ReplaceButton_Click(object sender, System.EventArgs e)
		{
			this.SpellChecker.ReplaceWord(this.ReplacementWord.Text);
			this.CurrentText.Value = this.SpellChecker.Text;
			this.SpellChecker.SpellCheck();
		}

		private void SpellChecker_DoubledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.SaveValues();
			this.CurrentWord.Text = this.SpellChecker.CurrentWord;

			this.SuggestionForm.Visible = true;
			this.SpellcheckComplete.Visible = false;

			this.Suggestions.Items.Clear();
			this.ReplacementWord.Text = string.Empty;
		}

		private void SpellChecker_EndOfText(object sender, System.EventArgs args)
		{
			this.SaveValues();

			this.SuggestionForm.Visible = false;
			this.SpellcheckComplete.Visible = true;

		}

		private void SpellChecker_MisspelledWord(object sender, NetSpell.SpellChecker.SpellingEventArgs args)
		{
			this.SaveValues();
			this.CurrentWord.Text = this.SpellChecker.CurrentWord;

			this.SuggestionForm.Visible = true;
			this.SpellcheckComplete.Visible = false;

			this.SpellChecker.Suggest();

			this.Suggestions.DataSource = this.SpellChecker.Suggestions;
			this.Suggestions.DataBind();

			this.ReplacementWord.Text = string.Empty;
		}

		private void SaveValues()
		{
			this.CurrentText.Value = this.SpellChecker.Text;
			this.WordIndex.Value = this.SpellChecker.WordIndex.ToString();

			// save ignore words
			string[] ignore = (string[])this.SpellChecker.IgnoreList.ToArray(typeof(string));
			this.IgnoreList.Value = String.Join("|", ignore);

			// save replace words
			ArrayList tempArray = new ArrayList(this.SpellChecker.ReplaceList.Keys);
			string[] replaceKey = (string[])tempArray.ToArray(typeof(string));
			this.ReplaceKeyList.Value = String.Join("|", replaceKey);

			tempArray = new ArrayList(this.SpellChecker.ReplaceList.Values);
			string[] replaceValue = (string[])tempArray.ToArray(typeof(string));
			this.ReplaceValueList.Value = String.Join("|", replaceValue);

			// saving user words
			tempArray = new ArrayList(this.SpellChecker.Dictionary.UserWords.Keys);
			string[] userWords = (string[])tempArray.ToArray(typeof(string));
			Response.Cookies["UserWords"].Value = String.Join("|", userWords);;
            Response.Cookies["UserWords"].Path = "/";
            Response.Cookies["UserWords"].Expires = DateTime.Now.AddMonths(1);

		}

		private void LoadValues()
		{
			if (Request.Params["CurrentText"] != null)
			{
				this.SpellChecker.Text = Request.Params["CurrentText"];
			}

			if (Request.Params["WordIndex"] != null)
			{
				this.SpellChecker.WordIndex = int.Parse(Request.Params["WordIndex"]);
			}

			string ignoreList;
			string[] replaceKeys;
			string[] replaceValues;
			string[] userWords;

			// restore ignore list
			if (Request.Params["IgnoreList"] != null)
			{
				ignoreList = Request.Params["IgnoreList"];
				this.SpellChecker.IgnoreList.Clear();
				this.SpellChecker.IgnoreList.AddRange(ignoreList.Split('|'));
			}

			// restore replace list
			if (Request.Params["ReplaceKeyList"] != null 
				&& Request.Params["ReplaceValueList"] != null)
			{
				replaceKeys = Request.Params["ReplaceKeyList"].Split('|');
				replaceValues = Request.Params["ReplaceValueList"].Split('|');

				this.SpellChecker.ReplaceList.Clear();
				if (replaceKeys.Length == replaceValues.Length)
				{
					for (int i = 0; i < replaceKeys.Length; i++)
					{
						if(replaceKeys[i].Length > 0)
						{
							this.SpellChecker.ReplaceList.Add(replaceKeys[i], replaceValues[i]);
						}
					}
				}
			}

			// restore user words
			this.SpellChecker.Dictionary.UserWords.Clear();
			if (Request.Cookies["UserWords"] != null)
			{
				userWords = Request.Cookies["UserWords"].Value.Split('|');
				for (int i = 0; i < userWords.Length; i++)
				{
					if(userWords[i].Length > 0) 
					{
						this.SpellChecker.Dictionary.UserWords.Add(userWords[i], userWords[i]);
					}
				}
			}
		}
		#region Web Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
			this.SpellChecker = new NetSpell.SpellChecker.Spelling(this.components);
			// 
			// SpellChecker
			// 
			this.SpellChecker.ShowDialog = false;
			this.SpellChecker.MisspelledWord += new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(this.SpellChecker_MisspelledWord);
			this.SpellChecker.EndOfText += new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(this.SpellChecker_EndOfText);
			this.SpellChecker.DoubledWord += new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(this.SpellChecker_DoubledWord);
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
