// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Design;

using NetSpell.SpellChecker.Forms;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Affix;
using NetSpell.SpellChecker.Dictionary.Phonetic;

namespace NetSpell.SpellChecker
{
	/// <summary>
	///		The Spelling class encapsulates the functions necessary to check
	///		the spelling of inputted text.
	/// </summary>
	[ToolboxBitmap(typeof(NetSpell.SpellChecker.Spelling), "Spelling.bmp")]
	public class Spelling : System.ComponentModel.Component
	{


#region Global Regex
		// Regex are class scope and compiled to improve performance on reuse
		private Regex _digitRegex = new Regex(@"^\d", RegexOptions.Compiled);
		private Regex _htmlRegex = new Regex("<(\"[^\"]*\"|'[^']*'|[^'\">])*>", RegexOptions.Compiled);
		private MatchCollection _htmlTags;
		private Regex _letterRegex = new Regex(@"\D", RegexOptions.Compiled);
		private Regex _upperRegex = new Regex(@"[^A-Z]", RegexOptions.Compiled);
		private Regex _wordEx = new Regex(@"\b[A-Za-z0-9_'À-ÿ]+\b", RegexOptions.Compiled);
		private MatchCollection _words;
#endregion

#region private variables
		private System.ComponentModel.Container components = null;
#endregion

#region Events
		/// <summary>
		///     This event is fired when word is detected two times in a row
		/// </summary>
		public event DoubledWordEventHandler DoubledWord;

		/// <summary>
		///     This event is fired when the spell checker reaches the end of
		///     the text in the Text property
		/// </summary>
		public event EndOfTextEventHandler EndOfText;

		/// <summary>
		///     This event is fired when the spell checker finds a word that 
		///     is not in the dictionaries
		/// </summary>
		public event MisspelledWordEventHandler MisspelledWord;

		/// <summary>
		///     This represents the delegate method prototype that
		///     event receivers must implement
		/// </summary>
		public delegate void DoubledWordEventHandler(object sender, SpellingEventArgs args);

		/// <summary>
		///     This represents the delegate method prototype that
		///     event receivers must implement
		/// </summary>
		public delegate void EndOfTextEventHandler(object sender, System.EventArgs args);

		/// <summary>
		///     This represents the delegate method prototype that
		///     event receivers must implement
		/// </summary>
		public delegate void MisspelledWordEventHandler(object sender, SpellingEventArgs args);

		/// <summary>
		///     This is the method that is responsible for notifying
		///     receivers that the event occurred
		/// </summary>
		protected virtual void OnDoubledWord(SpellingEventArgs e)
		{
			if (DoubledWord != null)
			{
				DoubledWord(this, e);
			}
		}

		/// <summary>
		///     This is the method that is responsible for notifying
		///     receivers that the event occurred
		/// </summary>
		protected virtual void OnEndOfText(System.EventArgs e)
		{
			if (EndOfText != null)
			{
				EndOfText(this, e);
			}
		}

		/// <summary>
		///     This is the method that is responsible for notifying
		///     receivers that the event occurred
		/// </summary>
		protected virtual void OnMisspelledWord(SpellingEventArgs e)
		{
			if (MisspelledWord != null)
			{
				MisspelledWord(this, e);
			}
		}

#endregion

#region Constructors
		/// <summary>
		///     Initializes a new instance of the SpellCheck class
		/// </summary>
		public Spelling()
		{
			_spellingForm = new SpellingForm(this);
			InitializeComponent();
		}


		/// <summary>
		///     Required for Windows.Forms Class Composition Designer support
		/// </summary>
		public Spelling(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			_spellingForm = new SpellingForm(this);
			InitializeComponent();
		}

#endregion

#region private methods

		/// <summary>
		///     Calculates the words from the Text property
		/// </summary>
		private void CalculateWords()
		{
			// splits the text into words
			_words = _wordEx.Matches(_Text.ToString());
			_WordCount = _words.Count; // set word count
		}

		/// <summary>
		///     Determines if the string should be spell checked
		/// </summary>
		/// <param name="characters" type="string">
		///     <para>
		///         The Characters to check
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if the string should be spell checked
		/// </returns>
		private bool CheckString(string characters)
		{
			if(!_upperRegex.IsMatch(characters) && _IgnoreAllCapsWords)
			{
				return false;
			}
			if(_digitRegex.IsMatch(characters) && _IgnoreWordsWithDigits)
			{
				return false;
			}
			if(!_letterRegex.IsMatch(characters))
			{
				return false;
			}
			if(_IgnoreHtml)
			{
				int startIndex = _words[_WordIndex].Index;
				
				foreach (Match item in _htmlTags) 
				{
					if (startIndex >= item.Index && startIndex <= item.Index + item.Length - 1)
					{
						return false;
					}
				}
			}
			return true;
		}
		/// <summary>
		///     Calculates the position of html tags in the Text property
		/// </summary>
		private void MarkHtml()
		{
			// splits the text into words
			_htmlTags = _htmlRegex.Matches(_Text.ToString());
		}

		/// <summary>
		///     Resets the public properties
		/// </summary>
		private void Reset()
		{
			_WordIndex = 0; // reset word index
			_CurrentWord = "";
			_ReplacementWord = "";
			_IgnoreList.Clear();
			_ReplaceList.Clear();
			_Suggestions.Clear();
		}

#endregion

#region ISpell Near Miss Suggetion methods

		/// <summary>
		///		swap out each char one by one and try all the tryme
		///		chars in its place to see if that makes a good word
		/// </summary>
		private void BadChar(ref ArrayList tempSuggestion)
		{
			for (int i = 0; i < _CurrentWord.Length; i++)
			{
				StringBuilder tempWord = new StringBuilder(_CurrentWord);
				char[] tryme = this.Dictionary.TryCharacters.ToCharArray();
				for (int x = 0; x < tryme.Length; x++)
				{
					tempWord[i] = tryme[x];
					if (this.TestWord(tempWord.ToString())) 
					{
						Word ws = new Word();
						ws.Value = tempWord.ToString().ToLower();
						ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
						tempSuggestion.Add(ws);
					}
				}			 
			}
		}

		/// <summary>
		///     try omitting one char of word at a time
		/// </summary>
		private void ExtraChar(ref ArrayList tempSuggestion)
		{
			if (_CurrentWord.Length > 1) 
			{
				for (int i = 0; i < _CurrentWord.Length; i++)
				{
					StringBuilder tempWord = new StringBuilder(_CurrentWord);
					tempWord.Remove(i, 1);

					if (this.TestWord(tempWord.ToString())) 
					{
						Word ws = new Word();
						ws.Value = tempWord.ToString().ToLower();
						ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
						tempSuggestion.Add(ws);
					}
								 
				}
			}
		}

		/// <summary>
		///     try inserting a tryme character before every letter
		/// </summary>
		private void ForgotChar(ref ArrayList tempSuggestion)
		{
			char[] tryme = this.Dictionary.TryCharacters.ToCharArray();
				
			for (int i = 0; i <= _CurrentWord.Length; i++)
			{
				for (int x = 0; x < tryme.Length; x++)
				{
					StringBuilder tempWord = new StringBuilder(_CurrentWord);
				
					tempWord.Insert(i, tryme[x]);
					if (this.TestWord(tempWord.ToString())) 
					{
						Word ws = new Word();
						ws.Value = tempWord.ToString().ToLower();
						ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
						tempSuggestion.Add(ws);
					}
				}			 
			}
		}

		/// <summary>
		///     suggestions for a typical fault of spelling, that
		///		differs with more, than 1 letter from the right form.
		/// </summary>
		private void ReplaceChars(ref ArrayList tempSuggestion)
		{
			ArrayList replacementChars = this.Dictionary.ReplaceCharacters;
			for (int i = 0; i < replacementChars.Count; i++)
			{
				int split = ((string)replacementChars[i]).IndexOf(' ');
				string key = ((string)replacementChars[i]).Substring(0, split);
				string replacement = ((string)replacementChars[i]).Substring(split+1);

				int pos = _CurrentWord.IndexOf(key);
				while (pos > -1)
				{
					string tempWord = _CurrentWord.Substring(0, pos);
					tempWord += replacement;
					tempWord += _CurrentWord.Substring(pos + key.Length);

					if (this.TestWord(tempWord))
					{
						Word ws = new Word();
						ws.Value = tempWord.ToString().ToLower();
						ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
						tempSuggestion.Add(ws);
					}
					pos = _CurrentWord.IndexOf(key, pos+1);
				}
			}
		}

		/// <summary>
		///     try swapping adjacent chars one by one
		/// </summary>
		private void SwapChar(ref ArrayList tempSuggestion)
		{
			for (int i = 0; i < _CurrentWord.Length - 1; i++)
			{
				StringBuilder tempWord = new StringBuilder(_CurrentWord);
				
				char swap = tempWord[i];
				tempWord[i] = tempWord[i+1];
				tempWord[i+1] = swap;

				if (this.TestWord(tempWord.ToString())) 
				{
					
					Word ws = new Word();
					ws.Value = tempWord.ToString().ToLower();
					ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
					tempSuggestion.Add(ws);
				}	 
			}
		}
		
		/// <summary>
		///     split the string into two pieces after every char
		///		if both pieces are good words make them a suggestion
		/// </summary>
		private void TwoWords(ref ArrayList tempSuggestion)
		{
			for (int i = 1; i < _CurrentWord.Length - 1; i++)
			{
				string firstWord = _CurrentWord.Substring(0,i);
				string secondWord = _CurrentWord.Substring(i);
				
				if (this.TestWord(firstWord) && this.TestWord(secondWord)) 
				{
					string tempWord = firstWord + " " + secondWord;
					
					Word ws = new Word();
					ws.Value = tempWord.ToString().ToLower();
					ws.EditDistance = this.EditDistance(_CurrentWord, tempWord.ToString());
				
					tempSuggestion.Add(ws);
				}	 
			}
		}

#endregion

#region public methods

		/// <summary>
		///     Deletes the CurrentWord from the Text Property
		/// </summary>
		public void DeleteWord()
		{
			int index = _words[_WordIndex].Index;
			int length = _words[_WordIndex].Length;

			_Text.Remove(index, length);
			this.CalculateWords();
		}

		/// <summary>
		///     Calculates the minimum number of change, inserts or deletes
		///     required to change firstWord into secondWord
		/// </summary>
		/// <param name="source" type="string">
		///     <para>
		///         The first word to calculate
		///     </para>
		/// </param>
		/// <param name="target" type="string">
		///     <para>
		///         The second word to calculate
		///     </para>
		/// </param>
		/// <param name="positionPriority" type="bool">
		///     <para>
		///         set to true if the first and last char should have priority
		///     </para>
		/// </param>
		/// <returns>
		///     The number of edits to make firstWord equal secondWord
		/// </returns>
		public int EditDistance(string source, string target, bool positionPriority)
		{
		
			// i.e. 2-D array
			Array matrix = Array.CreateInstance(typeof(int), source.Length+1, target.Length+1);

			// boundary conditions
			matrix.SetValue(0, 0, 0); 

			for(int j=1; j <= target.Length; j++)
			{
				// boundary conditions
				int val = (int)matrix.GetValue(0,j-1);
				matrix.SetValue(val+1, 0, j);
			}

			// outer loop
			for(int i=1; i <= source.Length; i++)                            
			{ 
				// boundary conditions
				int val = (int)matrix.GetValue(i-1, 0);
				matrix.SetValue(val+1, i, 0); 

				// inner loop
				for(int j=1; j <= target.Length; j++)                         
				{ 
					int diag = (int)matrix.GetValue(i-1, j-1);

					if(source.Substring(i-1, 1) != target.Substring(j-1, 1)) 
					{
						diag++;
					}

					int deletion = (int)matrix.GetValue(i-1, j);
					int insertion = (int)matrix.GetValue(i, j-1);
					int match = Math.Min(deletion+1, insertion+1);		
					matrix.SetValue(Math.Min(diag, match), i, j);
				}//for j
			}//for i

			int dist = (int)matrix.GetValue(source.Length, target.Length);

			// extra edit on first and last chars
			if (positionPriority)
			{
				if (source[0] != target[0]) dist++;
				if (source[source.Length-1] != target[target.Length-1]) dist++;
			}
			return dist;
		}
		
		/// <summary>
		///     Calculates the minimum number of change, inserts or deletes
		///     required to change firstWord into secondWord
		/// </summary>
		/// <param name="source" type="string">
		///     <para>
		///         The first word to calculate
		///     </para>
		/// </param>
		/// <param name="target" type="string">
		///     <para>
		///         The second word to calculate
		///     </para>
		/// </param>
		/// <returns>
		///     The number of edits to make firstWord equal secondWord
		/// </returns>
		/// <remarks>
		///		This method automaticly gives priority to matching the first and last char
		/// </remarks>
		public int EditDistance(string source, string target)
		{
			return this.EditDistance(source, target, true);
		}

		/// <summary>
		///     Ignores all instances of the CurrentWord in the Text Property
		/// </summary>
		public void IgnoreAllWord()
		{
			// Add current word to ignore list
			_IgnoreList.Add(_CurrentWord);
			this.IgnoreWord();
		}

		/// <summary>
		///     Ignores the instances of the CurrentWord in the Text Property
		/// </summary>
		/// <remarks>
		///		Must call SpellCheck after call this method to resume
		///		spell checking
		/// </remarks>
		public void IgnoreWord()
		{
			// increment Word Index to skip over this word
			_WordIndex++;
		}

		/// <summary>
		///     Replaces all instances of the CurrentWord in the Text Property
		/// </summary>
		public void ReplaceAllWord()
		{
			if(!_ReplaceList.ContainsKey(_CurrentWord)) 
			{
				_ReplaceList.Add(_CurrentWord, _ReplacementWord);
			}
			this.ReplaceWord();
		}

		/// <summary>
		///     Replaces all instances of the CurrentWord in the Text Property
		/// </summary>
		/// <param name="replacementWord" type="string">
		///     <para>
		///         The word to replace the CurrentWord with
		///     </para>
		/// </param>
		public void ReplaceAllWord(string replacementWord)
		{
			this.ReplacementWord = replacementWord;
			this.ReplaceAllWord();
		}


		/// <summary>
		///     Replaces the instances of the CurrentWord in the Text Property
		/// </summary>
		public void ReplaceWord()
		{

			int index = _words[_WordIndex].Index;
			int length = _words[_WordIndex].Length;
			
			_Text.Remove(index, length);
			if (_ReplacementWord.Length > 0) 
			{
				// if first letter upper case, match case for replacement word
				if (char.IsUpper(_words[_WordIndex].ToString(), 0))
				{
					_ReplacementWord = _ReplacementWord.Substring(0,1).ToUpper() 
						+ _ReplacementWord.Substring(1);
				}
				_Text.Insert(index, _ReplacementWord);
			}
			else if (index > 0) 
			{
				if (_Text.ToString().Substring(index-1, 2) == "  ")
				{
					//removing double space
					_Text.Remove(index, 1);
				}
			}
			this.CalculateWords();
		}

		/// <summary>
		///     Replaces the instances of the CurrentWord in the Text Property
		/// </summary>
		/// <param name="replacementWord" type="string">
		///     <para>
		///         The word to replace the CurrentWord with
		///     </para>
		/// </param>
		public void ReplaceWord(string replacementWord)
		{
			this.ReplacementWord = replacementWord;
			this.ReplaceWord();
		}

		/// <summary>
		///     Spell checks the words in the <see cref="Text"/> property starting
		///     at the <see cref="WordIndex"/> position
		/// </summary>
		/// <returns>
		///     Returns true if there is a word found in the text 
		///     that is not in the dictionaries
		/// </returns>
		/// <seealso cref="CurrentWord"/>
		/// <seealso cref="WordIndex"/>
		/// <seealso cref="Dictionaries"/>
		public bool SpellCheck()
		{
			if (!this.Dictionary.Initialized)
			{
				this.Dictionary.Initialize();
			}

			string currentWord = "";
			bool misspelledWord = false;
            
			if(_words != null) 
			{
				for (int i = _WordIndex; i < _words.Count; i++) 
				{
					currentWord = _words[i].Value.ToString();
					_CurrentWord = currentWord;		// setting the current word
					_WordIndex = i;					// saving the current word index 

					if(CheckString(currentWord)) 
					{
						if(!TestWord(currentWord)) 
						{
							if(_ReplaceList.ContainsKey(currentWord)) 
							{
								this.ReplacementWord = _ReplaceList[currentWord].ToString();
								this.ReplaceWord();
							}
							else if(!_IgnoreList.Contains(currentWord))
							{
								misspelledWord = true;
								this.OnMisspelledWord(new SpellingEventArgs(currentWord, i, _words[i].Index));		//raise event
								break;
							}
						}
						else if(i > 0) 
						{
							if(_words[i-1].Value.ToString() == currentWord) 
							{
								misspelledWord = true;
								this.OnDoubledWord(new SpellingEventArgs(currentWord, i, _words[i].Index));		//raise event
								break;
							}
						}
					}
				} // for

				if(_WordIndex >= _words.Count-1 && !misspelledWord) 
				{
					OnEndOfText(System.EventArgs.Empty);	//raise event
				}
			} // not words null

			return misspelledWord;

		} // SpellCheck

		/// <summary>
		///     Spell checks the words in the <see cref="Text"/> property starting
		///     at the <see cref="WordIndex"/> position. This overload takes in the
		///     WordIndex to start checking from.
		/// </summary>
		/// <param name="startWordIndex" type="int">
		///     <para>
		///         The index of the word to start checking from. 
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if there is a word found in the text 
		///     that is not in the dictionaries
		/// </returns>
		/// <seealso cref="CurrentWord"/>
		/// <seealso cref="WordIndex"/>
		/// <seealso cref="Dictionaries"/>
		public bool SpellCheck(int startWordIndex)
		{
			_WordIndex = startWordIndex;
			return SpellCheck();
		}
		
		/// <summary>
		///     Spell checks the words in the <see cref="Text"/> property starting
		///     at the <see cref="WordIndex"/> position. This overload takes in the 
		///     text to spell check
		/// </summary>
		/// <param name="text" type="string">
		///     <para>
		///         The text to spell check
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if there is a word found in the text 
		///     that is not in the dictionaries
		/// </returns>
		/// <seealso cref="CurrentWord"/>
		/// <seealso cref="WordIndex"/>
		/// <seealso cref="Dictionaries"/>
		public bool SpellCheck(string text)
		{
			this.Text = text;
			return SpellCheck();
		}

		/// <summary>
		///     Spell checks the words in the <see cref="Text"/> property starting
		///     at the <see cref="WordIndex"/> position. This overload takes in 
		///     the text to check and the WordIndex to start checking from.
		/// </summary>
		/// <param name="text" type="string">
		///     <para>
		///         The text to spell check
		///     </para>
		/// </param>
		/// <param name="startWordIndex" type="int">
		///     <para>
		///         The index of the word to start checking from
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if there is a word found in the text 
		///     that is not in the dictionaries
		/// </returns>
		/// <seealso cref="CurrentWord"/>
		/// <seealso cref="WordIndex"/>
		/// <seealso cref="Dictionaries"/>
		public bool SpellCheck(string text, int startWordIndex)
		{
			this.WordIndex = startWordIndex;
			this.Text = text;
			return SpellCheck();
		}

		/// <summary>
		///     Populates the <see cref="Suggestions"/> property with word suggestions
		///     for the <see cref="CurrentWord"/>
		/// </summary>
		/// <remarks>
		///		<see cref="TestWord"/> must have been called before calling this method
		/// </remarks>
		/// <seealso cref="CurrentWord"/>
		/// <seealso cref="Suggestions"/>
		/// <seealso cref="TestWord"/>
		public void Suggest()
		{
			if (!_Dictionary.Initialized)
			{
				_Dictionary.Initialize();
			}

			ArrayList tempSuggestion = new ArrayList();

			if ((_SuggestionMode == SuggestionEnum.PhoneticNearMiss 
				|| _SuggestionMode == SuggestionEnum.Phonetic)
				&& _Dictionary.PhoneticRules.Count > 0)
			{
				// generate phonetic code for possible root word
				Hashtable codes = new Hashtable();
				foreach (string tempWord in _Dictionary.PossibleBaseWords)
				{
					string tempCode = _Dictionary.PhoneticCode(tempWord);
					if (tempCode.Length > 0 && !codes.ContainsKey(tempCode)) 
					{
						codes.Add(tempCode, tempCode);
					}
				}
				
				if (codes.Count > 0)
				{
					// search root words for phonetic codes
					foreach (Word word in _Dictionary.BaseWords.Values)
					{
						if (codes.ContainsKey(word.PhoneticCode))
						{
							ArrayList words = _Dictionary.ExpandWord(word);
							// add expanded words
							foreach (string expandedWord in words)
							{
								Word newWord = new Word();
								newWord.Value = expandedWord;
								newWord.EditDistance = this.EditDistance(_CurrentWord, expandedWord);
								tempSuggestion.Add(newWord);
							}
						}
					}
				}
			}

			if (_SuggestionMode == SuggestionEnum.PhoneticNearMiss 
				|| _SuggestionMode == SuggestionEnum.NearMiss)
			{
				// suggestions for a typical fault of spelling, that
				// differs with more, than 1 letter from the right form.
				this.ReplaceChars(ref tempSuggestion);

				// swap out each char one by one and try all the tryme
				// chars in its place to see if that makes a good word
				this.BadChar(ref tempSuggestion);

				// try omitting one char of word at a time
				this.ExtraChar(ref tempSuggestion);

				// try inserting a tryme character before every letter
				this.ForgotChar(ref tempSuggestion);

				// split the string into two pieces after every char
				// if both pieces are good words make them a suggestion
				this.TwoWords(ref tempSuggestion);

				// try swapping adjacent chars one by one
				this.SwapChar(ref tempSuggestion);
			}

			tempSuggestion.Sort();  // sorts by edit score
			_Suggestions.Clear(); 

			for (int i = 0; i < tempSuggestion.Count; i++)
			{
				string word = ((Word)tempSuggestion[i]).Value;
				// looking for duplicates
				if (!_Suggestions.Contains(word))
				{
					// populating the suggestion list
					_Suggestions.Add(word);
				}

				if (_Suggestions.Count >= _MaxSuggestions && _MaxSuggestions > 0)
				{
					break;
				}
			}

		} // suggest

		/// <summary>
		///     Checks to see if the word is in the dictionary
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         The word to check
		///     </para>
		/// </param>
		/// <returns>
		///     Returns true if word is found in dictionary
		/// </returns>
		public bool TestWord(string word)
		{
			if (!this.Dictionary.Initialized)
			{
				this.Dictionary.Initialize();
			}

			if (this.Dictionary.Contains(word))
			{
				return true;
			}
			else if (this.Dictionary.Contains(word.ToLower()))
			{
				return true;
			}
			return false;
		}

#endregion

#region public properties

		private string _CurrentWord = "";
		private WordDictionary _Dictionary = new WordDictionary();
		private bool _IgnoreAllCapsWords = true;
		private bool _IgnoreHtml = true;
		private ArrayList _IgnoreList = new ArrayList();
		private bool _IgnoreWordsWithDigits = false;
		private int _MaxSuggestions = 25;
		private int _PhoneticLevel = 0;
		private Hashtable _ReplaceList = new Hashtable();
		private string _ReplacementWord = "";
		private bool _ShowDialog = true;
		private SpellingForm _spellingForm;
		private SuggestionEnum _SuggestionMode = SuggestionEnum.PhoneticNearMiss;
		private ArrayList _Suggestions = new ArrayList();
		private StringBuilder _Text = new StringBuilder();
		private int _WordCount = 0;
		private int _WordIndex = 0;


		/// <summary>
		///     The suggestion stratagy to use when generating suggestions
		/// </summary>
		public enum SuggestionEnum
		{
			/// <summary>
			///     Combines the phonetic and near miss stratagies
			/// </summary>
			PhoneticNearMiss,
			/// <summary>
			///     The phonetic stratagy generates suggestions by word sound
			/// </summary>
			/// <remarks>
			///		This techneque was developed by the open source project ASpell.net
			/// </remarks>
			Phonetic,
			/// <summary>
			///     The near miss stratagy generates suggestion by replacing, 
			///     removing, adding chars to make words
			/// </summary>
			/// <remarks>
			///     This techneque was developed by the open source spell checker ISpell
			/// </remarks>
			NearMiss
		}


		/// <summary>
		///     The current word being spell checked from the text property
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CurrentWord
		{
			get	{return _CurrentWord;}
		}

		/// <summary>
		///     The WordDictionary object to use when spell checking
		/// </summary>
		[Browsable(true)]
		[CategoryAttribute("Dictionary")]
		[Description("The WordDictionary object to use when spell checking")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WordDictionary Dictionary
		{
			get {return _Dictionary;}
			set {_Dictionary = value;}
		}


		/// <summary>
		///     Ignore words with all capital letters when spell checking
		/// </summary>
		[DefaultValue(true)]
		[CategoryAttribute("Options")]
		[Description("Ignore words with all capital letters when spell checking")]
		public bool IgnoreAllCapsWords
		{
			get {return _IgnoreAllCapsWords;}
			set {_IgnoreAllCapsWords = value;}
		}

		/// <summary>
		///     Ignore html tags when spell checking
		/// </summary>
		[DefaultValue(true)]
		[CategoryAttribute("Options")]
		[Description("Ignore html tags when spell checking")]
		public bool IgnoreHtml
		{
			get {return _IgnoreHtml;}
			set {_IgnoreHtml = value;}
		}

		/// <summary>
		///     List of words to automatically ignore
		/// </summary>
		/// <remarks>
		///		When <see cref="IgnoreAllWord"/> is clicked, the <see cref="CurrentWord"/> is added to this list
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ArrayList IgnoreList
		{
			get {return _IgnoreList;}
			set {_IgnoreList = value;}
		}

		/// <summary>
		///     Ignore words with digits when spell checking
		/// </summary>
		[DefaultValue(false)]
		[CategoryAttribute("Options")]
		[Description("Ignore words with digits when spell checking")]
		public bool IgnoreWordsWithDigits
		{
			get {return _IgnoreWordsWithDigits;}
			set {_IgnoreWordsWithDigits = value;}
		}

		/// <summary>
		///     The maximum number of suggestions to generate
		/// </summary>
		[DefaultValue(25)]
		[CategoryAttribute("Options")]
		[Description("The maximum number of suggestions to generate")]
		public int MaxSuggestions
		{
			get {return _MaxSuggestions;}
			set {_MaxSuggestions = value;}
		}

		/// <summary>
		///     Determines how close the suggestion words need to sound
		/// </summary>
		[DefaultValue(2)]
		[CategoryAttribute("Options")]
		[Description("Determines how close the suggestion words need to sound")]
		public int PhoneticLevel
		{
			get {return _PhoneticLevel;}
			set {_PhoneticLevel = value;}
		}


		/// <summary>
		///     List of words and replacement values to automatically replace
		/// </summary>
		/// <remarks>
		///		When <see cref="ReplaceAllWord"/> is clicked, the <see cref="CurrentWord"/> is added to this list
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Hashtable ReplaceList
		{
			get {return _ReplaceList;}
			set {_ReplaceList = value;}
		}

		/// <summary>
		///     The word to used when replacing the misspelled word
		/// </summary>
		/// <seealso cref="ReplaceAllWord"/>
		/// <seealso cref="ReplaceWord"/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ReplacementWord
		{
			get {return _ReplacementWord;}
			set {_ReplacementWord = value;}
		}

		/// <summary>
		///     Determines if the spell checker should use its internal suggestions
		///     and options dialogs.
		/// </summary>
		[DefaultValue(true)]
		[CategoryAttribute("Options")]
		[Description("Determines if the spell checker should use its internal dialogs")]
		public bool ShowDialog
		{
			get {return _ShowDialog;}
			set 
			{
				_ShowDialog = value;
				if (_ShowDialog) _spellingForm.AttachEvents();
				else _spellingForm.DetachEvents();
			}
		}


		/// <summary>
		///     The internal spelling suggestions dialog form
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SpellingForm SpellingForm
		{
			get {return _spellingForm;}
		}

		/// <summary>
		///     The suggestion stratagy to use when generating suggestions
		/// </summary>
		[DefaultValue(SuggestionEnum.PhoneticNearMiss)]
		[CategoryAttribute("Options")]
		[Description("The suggestion stratagy to use when generating suggestions")]
		public SuggestionEnum SuggestionMode
		{
			get {return _SuggestionMode;}
			set {_SuggestionMode = value;}
		}

		/// <summary>
		///     An array of word suggestions for the correct spelling of the misspelled word
		/// </summary>
		/// <seealso cref="Suggest"/>
		/// <seealso cref="SpellCheck"/>
		/// <seealso cref="MaxSuggestions"/>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ArrayList Suggestions
		{
			get {return _Suggestions;}
		}

		/// <summary>
		///     The text to spell check
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Text
		{
			get {return _Text.ToString();}
			set 
			{
				_Text = new StringBuilder(value);
				this.CalculateWords();
				this.MarkHtml();
				this.Reset();
			}
		}


		/// <summary>
		///     The number of words being spell checked
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int WordCount
		{
			get {return _WordCount;}
		}

		/// <summary>
		///     WordIndex is the index of the current word being spell checked
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int WordIndex
		{
			get {return _WordIndex;}
			set {_WordIndex = value;}
		}

#endregion

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


	} 
}
