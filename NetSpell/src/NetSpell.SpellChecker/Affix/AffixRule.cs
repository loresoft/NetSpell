using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace NetSpell.SpellChecker.Affix
{
	/// <summary>
	///		Rule for expanding base words
	/// </summary>
	public class AffixRule
	{
		private bool _AllowPrefix = false;
		private AffixEntryCollection _Entries = new AffixEntryCollection();
		private bool _IsPrefix = false;
		private string _Name = "";

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		public AffixRule()
		{
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="entryText" type="string[]">
		///     <para>
		///         the entry records to parse
		///     </para>
		/// </param>
		public AffixRule(string[] entryText)
		{
			this.LoadEntries(entryText);
		}

		/// <summary>
		///     Parses the entry records and populates the EntryCollection
		/// </summary>
		/// <param name="entryText" type="string[]">
		///     <para>
		///         the entry records to parse
		///     </para>
		/// </param>
		public void LoadEntries(string[] entryText)
		{
			// the following are used to split a line by space
			Regex wordRegx = new Regex(@"[^\s]+", RegexOptions.Compiled);
			MatchCollection wordMatches;

			foreach(string line in entryText)
			{
				wordMatches = wordRegx.Matches(line);
				if(wordMatches.Count == 5)
				{
					string stripChars = wordMatches[2].Value;
					string addChars = wordMatches[3].Value;
					string condition = wordMatches[4].Value;

					AffixEntry newEntry = new AffixEntry();

					if(stripChars != "0")
					{
						newEntry.StripCharacters = stripChars;
					}

					newEntry.AddCharacters = addChars;

					if(condition != ".")
					{
						newEntry.Condition = condition;
						newEntry.ConditionRegex = new Regex(string.Format("{0}$", condition), RegexOptions.Compiled);	
					}

					this.Entries.Add(newEntry);
				}
			}
		}

		/// <summary>
		///     Allow a prefix and a suffix
		/// </summary>
		public bool AllowPrefix
		{
			get {return _AllowPrefix;}
			set {_AllowPrefix = value;}
		}

		/// <summary>
		///     Collection of text entries that make up this rule
		/// </summary>
		public AffixEntryCollection Entries
		{
			get {return _Entries;}
			set {_Entries = value;}
		}

		/// <summary>
		///     True if this rule is a prefix
		/// </summary>
		public bool IsPrefix
		{
			get {return _IsPrefix;}
			set {_IsPrefix = value;}
		}

		/// <summary>
		///     Name of the Affix rule
		/// </summary>
		public string Name
		{
			get {return _Name;}
			set {_Name = value;}
		}

	}
}
