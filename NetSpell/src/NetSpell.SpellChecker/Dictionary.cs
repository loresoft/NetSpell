using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

using NetSpell.SpellChecker.Affix;
using NetSpell.SpellChecker.Phonetic;

namespace NetSpell.SpellChecker
{
	/// <summary>
	/// Summary description for Dictionary.
	/// </summary>
	public class Dictionary
	{

		private string _AffixFile = "";
		private AffixRuleCollection _AffixRules = new AffixRuleCollection();
		private Hashtable _BaseWords = new Hashtable();
		private bool _Initialized = false;
		private string _PhoneticFile = "";
		private PhoneticRuleCollection _PhoneticRules = new PhoneticRuleCollection();
		private ArrayList _ReplaceCharacters = new ArrayList();
		private string _TryCharacters = "";
		private string _WordListFile = "";
		// the following are used to split a line by space
		private Regex _wordRegx = new Regex(@"[^\s]+", RegexOptions.Compiled);
			
		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		public Dictionary()
		{
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="wordFile" type="string">
		///     <para>
		///         The file to use as the word list
		///     </para>
		/// </param>
		/// <param name="affixFile" type="string">
		///     <para>
		///         The file containing the affix rules
		///     </para>
		/// </param>
		/// <param name="phoneticFile" type="string">
		///     <para>
		///         The file containing the phonetic rules
		///     </para>
		/// </param>
		public Dictionary(string wordFile, string affixFile, string phoneticFile)
		{
			this.WordListFile = wordFile;
			this.AffixFile = affixFile;
			this.PhoneticFile = phoneticFile;
			this.Initialize();
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="wordFile" type="string">
		///     <para>
		///         The file to use as the word list
		///     </para>
		/// </param>
		/// <param name="affixFile" type="string">
		///     <para>
		///         The file containing the affix rules
		///     </para>
		/// </param>
		public Dictionary(string wordFile, string affixFile)
		{
			this.WordListFile = wordFile;
			this.AffixFile = affixFile;
			this.Initialize();
		}

		/// <summary>
		///     Expands an affix compressed word
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to expand
		///     </para>
		/// </param>
		/// <returns>
		///     An array of words expanded from the compressed word
		/// </returns>
		public ArrayList ExpandWord(string word)
		{
			ArrayList expandedList = new ArrayList();
			ArrayList prefixWords = new ArrayList();
			string suffixKeys = "";

			if(word.IndexOf('/') > 0)
			{
				string baseWord = word.Substring(0, word.IndexOf('/'));

				expandedList.Add(baseWord);
				char[] keys = word.Substring(word.IndexOf('/') + 1).ToCharArray();
				foreach (char key in keys)
				{
					
					AffixRule rule = this.AffixRules[key.ToString()];
					
					if(rule.AllowPrefix && !rule.IsPrefix)
					{
						// saving suffix keys incase we have prefix and suffix keys
						suffixKeys += key.ToString();
					}

					foreach (AffixEntry entry in rule.Entries)
					{
						if(rule.IsPrefix)
						{
							// temp list to apply suffix to
							prefixWords.Add(entry.AddCharacters + baseWord); 
						}
						else 
						{
							if(entry.Condition.Length > 0)
							{
								if (entry.ConditionRegex.IsMatch(baseWord))
								{
									expandedList.Add(baseWord.Substring(0, baseWord.Length - entry.StripCharacters.Length) + entry.AddCharacters);
								}
							}
							else
							{
								expandedList.Add(baseWord.Substring(0, baseWord.Length - entry.StripCharacters.Length) + entry.AddCharacters);
							}
						}
					}// each entry
					
				} // each char
			}
			else
			{
				expandedList.Add(word);
			}

			// add suffix to prefixed words
			if(prefixWords.Count > 0 && suffixKeys.Length > 0)
			{
				foreach (string tempWord in prefixWords)
				{
					expandedList.AddRange(this.ExpandWord(string.Format("{0}/{1}", tempWord, suffixKeys)));
				}
			}
			else if(prefixWords.Count > 0)
			{
				expandedList.AddRange(prefixWords);
			}

			return expandedList;
		}

		/// <summary>
		///     Loads the files needed for the dictonary
		/// </summary>
		public void Initialize()
		{
			this.LoadWords();
			this.LoadAffix();
			this.LoadPhonetic();
			this.Initialized = true;

		}

		/// <summary>
		///     Uses the PhoneticRules to generate a code for how a word sounds
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to generate the code for
		///     </para>
		/// </param>
		/// <returns>
		///     The code for how the word sounds
		/// </returns>
		public string WordSound(string word)
		{
			string wordSound = "";



			return wordSound;
		}

		/// <summary>
		///     Loads and parses the affix file
		/// </summary>
		internal void LoadAffix()
		{
			MatchCollection wordMatches;

			if (_AffixFile.Length > 0) 
			{
				this.AffixRules.Clear();

				FileStream fs = new FileStream(_AffixFile, FileMode.Open, FileAccess.Read, FileShare.Read);
				StreamReader sr = new StreamReader(fs, Encoding.ASCII);
				string tempLine = "";

				while (sr.Peek() >= 0) 
				{
					tempLine = sr.ReadLine();
					if(tempLine.Length > 3)
					{
						switch(tempLine.Substring(0, 3))
						{
							case "TRY" :
								this.TryCharacters = tempLine.Substring(3).Trim();
								break;
							case "PFX" :
							case "SFX" :
								wordMatches = _wordRegx.Matches(tempLine);
								if(wordMatches.Count == 4)
								{
									string ruleKey = wordMatches[1].Value;
									string allowPrefix = wordMatches[2].Value;
									int entryCount = int.Parse(wordMatches[3].Value);

									string[] entryText = new string[entryCount];

									for (int i = 0; i < entryCount; i++)
									{
										entryText[i] = sr.ReadLine();
									}

									AffixRule newRule = new AffixRule(entryText);
									if(allowPrefix == "Y") 
									{
										newRule.AllowPrefix = true;
									}

									if(wordMatches[0].Value == "PFX")
									{
										newRule.IsPrefix = true;
									}

									this.AffixRules.Add(ruleKey, newRule);
								}
								break;

							case "REP" :
								wordMatches = _wordRegx.Matches(tempLine);
								if(wordMatches.Count == 3)
								{
									this.ReplaceCharacters.Add(string.Format("{0} {1}", wordMatches[1].Value,  wordMatches[2].Value));
								}
								break;
						} // switch
					} // line length
				} // peek

				sr.Close();
				fs.Close();
			}

		}
		
		/// <summary>
		///     loads and parses the phonetic file
		/// </summary>
		internal void LoadPhonetic()
		{
			
		}

		/// <summary>
		///     Loads the word list file
		/// </summary>
		internal void LoadWords()
		{
			
			FileStream fs = new FileStream(_WordListFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			StreamReader sr = new StreamReader(fs, Encoding.ASCII);
			string tempLine = "";
			
			this.BaseWords.Clear();

			while (sr.Peek() >= 0) 
			{
				tempLine = sr.ReadLine();
				string[] parts = tempLine.Split('/');
				Word word = null;

				switch (parts.Length)
				{
					case 3:
						word = new Word(parts[0], parts[1], parts[2]);
						break;
					case 2:
						word = new Word(parts[0], parts[1]);
						break;
					case 1:
						word = new Word(parts[0]);
						break;
				}
				if(word != null)
				{
					this.BaseWords.Add(parts[0], word);
				}
			}
		}


		/// <summary>
		///     The file name for the affix file
		/// </summary>
		public string AffixFile
		{
			get {return _AffixFile;}
			set {_AffixFile = value;}
		}


		/// <summary>
		///     A collection of affix rules
		/// </summary>
		public AffixRuleCollection AffixRules
		{
			get {return _AffixRules;}
			set {_AffixRules = value;}
		}


		/// <summary>
		///     The base words for the dictionary
		/// </summary>
		public Hashtable BaseWords
		{
			get {return _BaseWords;}
			set {_BaseWords = value;}
		}

		/// <summary>
		///     True if the dictionary files are loaded
		/// </summary>
		public bool Initialized
		{
			get {return _Initialized;}
			set {_Initialized = value;}
		}


		/// <summary>
		///     The file name for the phonetic rules
		/// </summary>
		public string PhoneticFile
		{
			get {return _PhoneticFile;}
			set {_PhoneticFile = value;}
		}


		/// <summary>
		///     A collection of phonetic rules
		/// </summary>
		public PhoneticRuleCollection PhoneticRules
		{
			get {return _PhoneticRules;}
			set {_PhoneticRules = value;}
		}


		/// <summary>
		///     The characters to interchange when trying to generate suggestions
		/// </summary>
		public ArrayList ReplaceCharacters
		{
			get {return _ReplaceCharacters;}
			set {_ReplaceCharacters = value;}
		}


		/// <summary>
		///     The characters to use when swapping characters to generate suggestions
		/// </summary>
		public string TryCharacters
		{
			get {return _TryCharacters;}
			set {_TryCharacters = value;}
		}


		/// <summary>
		///     The file that contains the base word list
		/// </summary>
		public string WordListFile
		{
			get {return _WordListFile;}
			set {_WordListFile = value;}
		}
	}
}
