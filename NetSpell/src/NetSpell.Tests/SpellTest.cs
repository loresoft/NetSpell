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
using NUnit.Framework;
using NetSpell.SpellChecker;

namespace NetSpell.Tests
{

	/// <summary>
	///     This is the spell checker test fixture for NUnit
	/// </summary>
	[TestFixture]
	public class SpellTest
	{
		bool _endOfText = false;
		Spelling _SpellChecker = new Spelling("..\\..\\..\\NetSpell.DictionaryBuild\\Dictionaries\\us-en-md.dic");
		WordEventArgs _wordEventArgs;
		
		private void DoubleWord(object sender, WordEventArgs args)
		{
			_wordEventArgs = args;
		}

		private void EndOfText(object sender, EventArgs args)
		{
			_endOfText = true;
		}

		private void MisspelledWord(object sender, WordEventArgs args)
		{
			_wordEventArgs = args;
		}

		[SetUp]
		public void AttachEvents()
		{
			_SpellChecker.ShowDialog = false;
			_SpellChecker.MisspelledWord += new Spelling.MisspelledWordEventHandler(MisspelledWord);
			_SpellChecker.DoubledWord += new Spelling.DoubledWordEventHandler(DoubleWord);
			_SpellChecker.EndOfText += new Spelling.EndOfTextEventHandler(EndOfText);
		}

		/// <summary>
		///		NUnit Test Function for DeleteWord
		/// </summary>
		[Test]
		public void DeleteWord()
		{
			_SpellChecker.Text = "this is is a test";
			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 2, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "is", _SpellChecker.CurrentWord);

			_SpellChecker.DeleteWord();
			Assertion.AssertEquals("Incorrect Text", "this is  a test", _SpellChecker.Text);
			
		}

		/// <summary>
		///		NUnit Test Function for IgnoreWord
		/// </summary>
		[Test]
		public void IgnoreWord()
		{
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
		///		NUnit Test Function
		/// </summary>
		[Test]
		public void BadChar()
		{
			_SpellChecker.Text = "this is a tst of a tst errr";

			_SpellChecker.SpellCheck();
			
			ArrayList tempSuggestion = new ArrayList();

			_SpellChecker.BadChar(ref tempSuggestion);

		}

		/// <summary>
		///		NUnit Test Function for ReplaceWord
		/// </summary>
		[Test]
		public void ReplaceWord()
		{
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
			_SpellChecker.Text = "this is a tst of a tst errr";

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
			_SpellChecker.Text = "this is an errr tst";

			_SpellChecker.SpellCheck();
			Assertion.AssertEquals("Incorrect WordOffset", 3, _SpellChecker.WordIndex);
			Assertion.AssertEquals("Incorrect CurrentWord", "errr", _SpellChecker.CurrentWord);

		}

		/// <summary>
		///		NUnit Test Function for Soundex
		/// </summary>
		[Test]
		public void Soundex()
		{
			Assertion.AssertEquals("Incorrect Soundex value", "T36", _SpellChecker.Soundex("test"));
		}

		/// <summary>
		///		NUnit Test Function for DoubleMetaphone
		/// </summary>
		[Test]
		public void DoubleMetaphoneTest()
		{
			DoubleMetaphone metaPhone = new DoubleMetaphone();

			metaPhone.GenerateMetaphone("occasionally");
			Assertion.AssertEquals("Incorrect Metaphone code", "AKSN", metaPhone.PrimaryCode);
			Assertion.AssertEquals("Incorrect Metaphone code", "AKXN", metaPhone.SecondaryCode);

			metaPhone.GenerateMetaphone("leisure");
			Assertion.AssertEquals("Incorrect Metaphone code", "LSR", metaPhone.PrimaryCode);
			Assertion.AssertEquals("Incorrect Metaphone code", "LSR", metaPhone.SecondaryCode);

			metaPhone.GenerateMetaphone("congratulations");
			Assertion.AssertEquals("Incorrect Metaphone code", "KNKR", metaPhone.PrimaryCode);
			Assertion.AssertEquals("Incorrect Metaphone code", "KNKR", metaPhone.SecondaryCode);
			
			metaPhone.GenerateMetaphone("simplicity");
			Assertion.AssertEquals("Incorrect Metaphone code", "SMPL", metaPhone.PrimaryCode);
			Assertion.AssertEquals("Incorrect Metaphone code", "SMPL", metaPhone.SecondaryCode);

		}

		/// <summary>
		///		NUnit Test Function for Suggest
		/// </summary>
		[Test]
		public void Suggest()
		{
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
			if (!_SpellChecker.TestWord("test")) 
			{
				Assertion.Fail("Did not find test word");
			}
			
			if (_SpellChecker.TestWord("tst"))
			{
				Assertion.Fail("Found tst word and shouldn't have");
			}
		}

		/// <summary>
		///		NUnit Test Function for WordSimilarity
		/// </summary>
		[Test]
		public void WordSimilarity()
		{
			Assertion.AssertEquals("Incorrect WordSimilarity score", 0.454545454545455F, _SpellChecker.WordSimilarity("test", "tst"), 0F);
			Assertion.AssertEquals("Incorrect WordSimilarity score", 1F, _SpellChecker.WordSimilarity("test", "test"), 0F);
		}
	}
}
