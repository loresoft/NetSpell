using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary.Affix;
using NetSpell.SpellChecker.Dictionary.Phonetic;

namespace NetSpell.SpellChecker.Dictionary
{

	/// <summary>
	/// Summary description for WordDictionary.
	/// </summary>
	public class WordDictionary : IDictionary, ICollection, IEnumerable 
	{
		private Hashtable _BaseWords = new Hashtable();
		private string _DictionaryFile = "";
		private PhoneticRuleCollection _PhoneticRules = new PhoneticRuleCollection();
		private AffixRuleCollection _PrefixRules = new AffixRuleCollection();
		private ArrayList _ReplaceCharacters = new ArrayList();
		private AffixRuleCollection _SuffixRules = new AffixRuleCollection();
		private string _TryCharacters = "";
		private string _UserFile = "";
		private Hashtable _UserWords = new Hashtable();

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		public WordDictionary()
		{
		}

		/// <summary>
		///     The collection of base words for the dictionary
		/// </summary>
		public Hashtable BaseWords
		{
			get {return _BaseWords;}
			set {_BaseWords = value;}
		}

		/// <summary>
		///     The file name for this dictionary
		/// </summary>
		public string DictionaryFile
		{
			get {return _DictionaryFile;}
			set {_DictionaryFile = value;}
		}


		/// <summary>
		///     Collection of phonetic rules for this dictionary
		/// </summary>
		public PhoneticRuleCollection PhoneticRules
		{
			get {return _PhoneticRules;}
			set {_PhoneticRules = value;}
		}


		/// <summary>
		///     Collection of affix prefixes for the base words in this dictionary
		/// </summary>
		public AffixRuleCollection PrefixRules
		{
			get {return _PrefixRules;}
			set {_PrefixRules = value;}
		}

		/// <summary>
		///     List of characters to use when generating suggestions using the near miss stratigy
		/// </summary>
		public ArrayList ReplaceCharacters
		{
			get {return _ReplaceCharacters;}
			set {_ReplaceCharacters = value;}
		}


		/// <summary>
		///     Collection of affix suffixes for the base words in this dictionary
		/// </summary>
		public AffixRuleCollection SuffixRules
		{
			get {return _SuffixRules;}
			set {_SuffixRules = value;}
		}

		/// <summary>
		///     List of characters to try when generating suggestions using the near miss stratigy
		/// </summary>
		public string TryCharacters
		{
			get {return _TryCharacters;}
			set {_TryCharacters = value;}
		}

		/// <summary>
		///     file name for the user word list for this dictionary
		/// </summary>
		public string UserFile
		{
			get {return _UserFile;}
			set {_UserFile = value;}
		}

		
		/// <summary>
		///     List of user entered words in this dictionary
		/// </summary>
		public Hashtable UserWords
		{
			get {return _UserWords;}
			set {_UserWords = value;}
		}

		/// <summary>
		///     Initializes the dictionary by loading and parsing the
		///     dictionary file and the user file.
		/// </summary>
		public void Initialize()
		{
			// the following is used to split a line by space
			Regex _spaceRegx = new Regex(@"[^\s]+", RegexOptions.Compiled);
			
			string currentSection = "";
			AffixRule currentRule = null;

			FileStream fs = new FileStream(_DictionaryFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			StreamReader sr = new StreamReader(fs, Encoding.UTF7);
			
			while (sr.Peek() >= 0) 
			{
				string tempLine = sr.ReadLine().Trim();
				if (tempLine.Length > 0)
				{
					switch (tempLine)
					{
						case "[Try]" : 
						case "[Replace]" : 
						case "[Prefix]" :
						case "[Suffix]" :
						case "[Phonetic]" :
						case "[Words]" :
							// set current section that is being parsed
							currentSection = tempLine;
							break;
						default :		
							// parse line a place in correct object
							switch (currentSection)
							{
								case "[Try]" : // ISpell try chars
									this.TryCharacters += tempLine;
									break;
								case "[Replace]" : // ISpell replace chars
									this.ReplaceCharacters.Add(tempLine);
									break;
								case "[Prefix]" : // MySpell prefix rules
								case "[Suffix]" : // MySpell suffix rules

									// split line by white space
									MatchCollection partMatches = _spaceRegx.Matches(tempLine);
									
									// if 3 parts, then new rule  
									if (partMatches.Count == 3)
									{
										currentRule = new AffixRule();
									
										// part 1 = affix key
										currentRule.Name = partMatches[0].Value;
										// part 2 = combine flag
										if (partMatches[1].Value == "Y") currentRule.AllowCombine = true;
										// part 3 = entry count

										if (currentSection == "[Prefix]")
										{
											// add to prefix collection
											this.PrefixRules.Add(currentRule.Name, currentRule);
										}
										else 
										{
											// add to suffix collection
											this.SuffixRules.Add(currentRule.Name, currentRule);
										}
									}
									//if 4 parts, then entry for current rule
									else if (partMatches.Count == 4)
									{
										// part 1 = affix key
										if (currentRule.Name == partMatches[0].Value)
										{
											AffixEntry entry = new AffixEntry();

											// part 2 = strip char
											if (partMatches[1].Value != "0") entry.StripCharacters = partMatches[1].Value;
											// part 3 = add chars
											entry.AddCharacters = partMatches[2].Value;
											// part 4 = conditions
											AffixUtility.EncodeConditions(partMatches[3].Value, entry);

											currentRule.AffixEntries.Add(entry);
										}
									}	
									
									break;
								case "[Phonetic]" : // ASpell phonetic rules
									break;
								case "[Words]" : // dictionary word list
									// splits word into its parts
									string[] parts = tempLine.Split('/');
									Word tempWord = new Word();
									// part 1 = base word
									tempWord.Value = parts[0];
									// part 2 = affix keys
									if (parts.Length >= 2) tempWord.AffixKeys = parts[1];
									// part 3 = phonetic code
									if (parts.Length >= 3) tempWord.PhoneticCode = parts[2];
									this.BaseWords.Add(tempWord.Value, tempWord);

									break;
							} // currentSection swith
						break;
					} //tempLine switch
						
				} 
			}
		}

#region Public IDictionary Members

		/// <summary>
		///     Adds a word to the user list
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to add
		///     </para>
		/// </param>
		/// <param name="value" type="NetSpell.SpellChecker.Dictionary.Word">
		///     <para>
		///         The word object to add
		///     </para>
		/// </param>
		/// <remarks>
		///		This method is only affects the user word list
		/// </remarks>
		public void Add(string word, Word value)
		{
			_UserWords.Add(word, value);
		}

		/// <summary>
		///     Clears the user list of words
		/// </summary>
		/// <remarks>
		///		This method is only affects the user word list
		/// </remarks>
		public void Clear()
		{
			_UserWords.Clear();
		}

		/// <summary>
		///     Searches all contained word lists for word
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to search for
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if word is found
		/// </returns>
		public bool Contains(string word)
		{
			// Step 1 Search UserWords
			if (_UserWords.Contains(word)) return true;

			// Step 2 Search BaseWords
			if (_BaseWords.Contains(word)) return true;

			// Step 3 Remove Affix, Search BaseWords
			foreach(AffixRule rule in SuffixRules.Values)
			{	
				foreach(AffixEntry entry in rule.AffixEntries)
				{
					int tempLength = word.Length - entry.AddCharacters.Length;
					if ((tempLength > 0)  &&  (tempLength + entry.StripCharacters.Length >= entry.ConditionCount))
					{
						// word with out affix
						string tempWord = word.Substring(0, tempLength);
						// add back strip chars
						tempWord += entry.StripCharacters;
						// check that this is valid
						int passCount = 0;
						for (int i = 0;  i < entry.ConditionCount; i++) 
						{
							int charCode = (int)tempWord[tempWord.Length - (entry.ConditionCount - i)];
							// TODO: fix when more then one cond
							if ((entry.Condition[charCode] & (1 << i)) == 1)
							{
								passCount++;
							}
						}
						if (passCount == entry.ConditionCount)
						{
							if (_BaseWords.Contains(tempWord)) return true;
						}
					}
				}
			}
			// Step 4 Remove Prefix, Search BaseWords

			return false;
		}

		public IDictionaryEnumerator GetEnumerator()
		{
			// TODO:  Add WordDictionary.GetEnumerator implementation
			return null;
		}

		/// <summary>
		///     Removes a word from the user list
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to remove
		///     </para>
		/// </param>
		/// <remarks>
		///		This method is only affects the user word list
		/// </remarks>
		public void Remove(string word)
		{
			_UserWords.Remove(word);
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public bool IsFixedSize
		{
			get {return false;}
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public bool IsReadOnly
		{
			get {return false;}
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public ICollection Keys
		{
			get
			{
				// TODO:  Add WordDictionary.Keys getter implementation
				return null;
			}
		}

		/// <summary>
		///     Searchs for word and returns a word object
		/// </summary>
		/// <value>
		///     <para>
		///         Word to search for
		///     </para>
		/// </value>
		public Word this[string key]
		{
			get
			{
				// TODO:  Add WordDictionary.this getter implementation
				return null;
			}
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public ICollection Values
		{
			get
			{
				// TODO:  Add WordDictionary.Values getter implementation
				return null;
			}
		}

#endregion

#region Public ICollection Members

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public void CopyTo(Array array, int index)
		{
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		/// <remarks>
		///     Always returns 0
		/// </remarks>
		public int Count
		{
			get {return 0;}
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public bool IsSynchronized
		{
			get {return false;}
		}

		/// <summary>
		///     Not Implemented!
		/// </summary>
		public object SyncRoot
		{
			get {return null;}
		}

#endregion

#region IEnumerable Members

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			// TODO:  Add WordDictionary.System.Collections.IEnumerable.GetEnumerator implementation
			return null;
		}

#endregion

#region IDictionary Members

		void System.Collections.IDictionary.Add(object key, object value)
		{
			this.Add((string)key, (Word)value);
		}

		bool System.Collections.IDictionary.Contains(object key)
		{
			return this.Contains((string)key);
		}

		void System.Collections.IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		object System.Collections.IDictionary.this[object key]
		{
			get {return this[(string)key];}
			set {}
		}

#endregion

	}

}
