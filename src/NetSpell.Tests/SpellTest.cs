// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Collections;
using NUnit.Framework;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Phonetic;
using NetSpell.SpellChecker.Dictionary.Affix;


namespace NetSpell.Tests
{

	/// <summary>
	///     This is the spell checker test fixture for NUnit
	/// </summary>
	[TestFixture]
	public class SpellTest
	{
		
		PerformanceTimer _timer = new PerformanceTimer();
		WordDictionary _dictionary = new WordDictionary();

		[SetUp]
		public void Setup()
		{
			_dictionary.DictionaryFolder = @"..\..\..\..\dic";
			_dictionary.Initialize();
		}

		private Spelling NewSpellChecker()
		{
			Spelling _SpellChecker = new Spelling();
			_SpellChecker.Dictionary = _dictionary;
			
			_SpellChecker.ShowDialog = false;
			return _SpellChecker;
		}

		/// <summary>
		///		NUnit Test Function for DeleteWord
		/// </summary>
		[Test]
		public void DeleteWord()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is is a tst.";
			_SpellChecker.SpellCheck();
			Assert.AreEqual(2, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("is", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			// basic delete test
			_SpellChecker.DeleteWord();
			Assert.AreEqual("this is a tst.", _SpellChecker.Text, "Incorrect Text");
						
			_SpellChecker.SpellCheck();
			Assert.AreEqual(3, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("tst", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			// before punctuation delete test
			_SpellChecker.DeleteWord();
			Assert.AreEqual("this is a.", _SpellChecker.Text, "Incorrect Text");
			
			
			_SpellChecker.Text = "Becuase people are realy bad spelers";
			_SpellChecker.SpellCheck();

			Assert.AreEqual(0, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("Becuase", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			//delete first word test
			_SpellChecker.DeleteWord();
			Assert.AreEqual("people are realy bad spelers", _SpellChecker.Text, "Incorrect Text");
			
			_SpellChecker.SpellCheck();
			Assert.AreEqual(2, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("realy", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			//delete first word test
			_SpellChecker.DeleteWord();
			Assert.AreEqual("people are bad spelers", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(3, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("spelers", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			//delete last word test
			_SpellChecker.DeleteWord();
			Assert.AreEqual("people are bad", _SpellChecker.Text, "Incorrect Text");


		}

		/// <summary>
		///		NUnit Test Function for DeleteWord
		/// </summary>
		[Test]
		public void NoText()
		{
			Spelling _SpellChecker = NewSpellChecker();
			
			Assert.AreEqual(string.Empty, _SpellChecker.CurrentWord, "Incorrect Current Word");
			
			_SpellChecker.WordIndex = 1;
			Assert.AreEqual(0, _SpellChecker.WordIndex, "Incorrect Word Index");

			Assert.AreEqual(0, _SpellChecker.WordCount, "Incorrect Word Count");

			Assert.AreEqual(0, _SpellChecker.TextIndex, "Incorrect Text Index");

			_SpellChecker.DeleteWord();
			Assert.AreEqual(string.Empty, _SpellChecker.Text, "Incorrect Text");
			
			_SpellChecker.IgnoreWord();
			Assert.AreEqual(string.Empty, _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.ReplaceWord("Test");
			Assert.AreEqual(string.Empty, _SpellChecker.Text, "Incorrect Text");

			Assert.IsFalse(_SpellChecker.SpellCheck(), "Spell Check not false");

			_SpellChecker.Suggest();
			Assert.AreEqual(0, _SpellChecker.Suggestions.Count, "Generated Suggestions with no text");
			
		}

		/// <summary>
		///		NUnit Test Function for IgnoreWord
		/// </summary>
		[Test]
		public void IgnoreWord()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is an errr tst";

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 3, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);
			_SpellChecker.IgnoreWord();

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 4, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "tst", _SpellChecker.CurrentWord);
			
		}

		/// <summary>
		///		NUnit Test Function for IgnoreWord
		/// </summary>
		[Test]
		public void IgnoreAllWord()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is a tst of a tst errr";

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 3, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "tst", _SpellChecker.CurrentWord);
			_SpellChecker.IgnoreAllWord();

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 7, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);
			
		}

		/// <summary>
		///		NUnit Test Function for ReplaceWord
		/// </summary>
		[Test]
		public void ReplaceWord()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "ths is an errr tst";

			_SpellChecker.SpellCheck();
			Assert.AreEqual(0, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("ths", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplacementWord = "this";
			_SpellChecker.ReplaceWord();
			Assert.AreEqual("this is an errr tst", _SpellChecker.Text, "Incorrect Text");
			
			//replace with empty string
			_SpellChecker.SpellCheck();
			Assert.AreEqual(3, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("errr", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("");
			Assert.AreEqual("this is an tst", _SpellChecker.Text, "Incorrect Text");

			
			_SpellChecker.Text = "Becuase people are realy bad spelers, \r\nths produc was desinged to prevent spelling errors in a text area like ths.";

			_SpellChecker.SpellCheck();
			Assert.AreEqual(0, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("Becuase", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("because");
			Assert.AreEqual("Because people are realy bad spelers, \r\nths produc was desinged to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(3, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("realy", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("really");
			Assert.AreEqual("Because people are really bad spelers, \r\nths produc was desinged to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(5, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("spelers", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("spellers");
			Assert.AreEqual("Because people are really bad spellers, \r\nths produc was desinged to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(6, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("ths", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("this");
			Assert.AreEqual("Because people are really bad spellers, \r\nthis produc was desinged to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(7, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("produc", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("product");
			Assert.AreEqual("Because people are really bad spellers, \r\nthis product was desinged to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(9, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("desinged", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("designed");
			Assert.AreEqual("Because people are really bad spellers, \r\nthis product was designed to prevent spelling errors in a text area like ths.", _SpellChecker.Text, "Incorrect Text");

			_SpellChecker.SpellCheck();
			Assert.AreEqual(19, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("ths", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			_SpellChecker.ReplaceWord("this");
			Assert.AreEqual("Because people are really bad spellers, \r\nthis product was designed to prevent spelling errors in a text area like this.", _SpellChecker.Text, "Incorrect Text");

		}

		/// <summary>
		///		NUnit Test Function for ReplaceWord
		/// </summary>
		[Test]
		public void ReplaceAllWord()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is a tst of a tst errr";
			_SpellChecker.IgnoreList.Clear();
			_SpellChecker.ReplaceList.Clear();

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 3, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "tst", _SpellChecker.CurrentWord);
			_SpellChecker.ReplaceAllWord("test");
			Assertion.AssertEquals("Incorrect Text", "this is a test of a tst errr", _SpellChecker.Text);
			
			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 7, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);
			Assertion.AssertEquals("Incorrect Text", "this is a test of a test errr", _SpellChecker.Text);
			
			
		}

		/// <summary>
		///		NUnit Test Function for SpellCheck
		/// </summary>
		[Test]
		public void SpellCheck()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is an errr tst";

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 3, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);

		}

		/// <summary>
		///		NUnit Test Function for SpellCheck
		/// </summary>
		[Test]
		public void HtmlSpellCheck()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.IgnoreHtml = true;
			_SpellChecker.Text = "<a href=\"#\">this <span id=\"txt\">is</span> an errr tst</a>";

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 9, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);

		}


		/// <summary>
		///		NUnit Test Function for Suggest
		/// </summary>
		[Test]
		public void Suggest()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "this is tst";
			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 2, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "tst", _SpellChecker.CurrentWord);

			_SpellChecker.Suggest();
			Assertion.AssertEquals("Incorrect Suggestion Count", 25, _SpellChecker.Suggestions.Count);
			Assertion.AssertEquals("Could not find 'test' in suggestions", true, _SpellChecker.Suggestions.Contains("test"));
			
		}

		/// <summary>
		///		NUnit Test Function for TestWord
		/// </summary>
		[Test]
		public void TestWord() 
		{
			Spelling _SpellChecker = NewSpellChecker();

			Assert.IsTrue(_SpellChecker.TestWord("test"), "Did not find test word");
			Assert.IsFalse(_SpellChecker.TestWord("tst"), "Found tst word and shouldn't have");

		}

		/// <summary>
		///		NUnit Test Function for WordSimilarity
		/// </summary>
		[Test]
		public void EditDistance()
		{
			Spelling _SpellChecker = NewSpellChecker();

			Assertion.AssertEquals("Incorrect EditDistance", 1, _SpellChecker.EditDistance("test", "tst"));
			Assertion.AssertEquals("Incorrect EditDistance", 2, _SpellChecker.EditDistance("test", "tes"));
			Assertion.AssertEquals("Incorrect EditDistance", 0, _SpellChecker.EditDistance("test", "test"));
			
		}

		
		[Test]
		public void GetWordIndexFromTextIndex() 
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "This is a test ";
			Assert.AreEqual(0, _SpellChecker.GetWordIndexFromTextIndex(1));
			Assert.AreEqual(0, _SpellChecker.GetWordIndexFromTextIndex(4));
			Assert.AreEqual(1, _SpellChecker.GetWordIndexFromTextIndex(5));
			Assert.AreEqual(2, _SpellChecker.GetWordIndexFromTextIndex(9));
			Assert.AreEqual(3, _SpellChecker.GetWordIndexFromTextIndex(12));
			Assert.AreEqual(3, _SpellChecker.GetWordIndexFromTextIndex(15));
			Assert.AreEqual(3, _SpellChecker.GetWordIndexFromTextIndex(20));
		}

	}
}
