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

			_SpellChecker.Text = "this is is a test";
			_SpellChecker.SpellCheck();
			Assert.AreEqual(2, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("is", _SpellChecker.CurrentWord, "Incorrect CurrentWord");

			_SpellChecker.DeleteWord();
			Assert.AreEqual("this is a test", _SpellChecker.Text, "Incorrect Text");
			
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
			
			Assertion.AssertEquals("Incorrect WordOffset", 0, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "ths", _SpellChecker.CurrentWord);

			_SpellChecker.ReplacementWord = "this";
			_SpellChecker.ReplaceWord();
			Assertion.AssertEquals("Incorrect Text", "this is an errr tst", _SpellChecker.Text);
			
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




	}
}
