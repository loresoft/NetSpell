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
	/// Summary description for EventTest.
	/// </summary>
	[TestFixture]
	public class EventTest
	{

		WordDictionary _dictionary = new WordDictionary();
		SpellingEventArgs _lastSpellingEvent;
		ReplaceWordEventArgs _lastReplaceEvent;
		EventNames _lastEvent = EventNames.None; 

		public enum EventNames
		{
			None,
			DeletedWord,
			DoubledWord,
			EndOfText,
			IgnoredWord,
			MisspelledWord,
			ReplacedWord,
		};

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
			
			_SpellChecker.DeletedWord += new NetSpell.SpellChecker.Spelling.DeletedWordEventHandler(SpellChecker_DeletedWord);
			_SpellChecker.DoubledWord +=new NetSpell.SpellChecker.Spelling.DoubledWordEventHandler(SpellChecker_DoubledWord);
			_SpellChecker.EndOfText +=new NetSpell.SpellChecker.Spelling.EndOfTextEventHandler(SpellChecker_EndOfText);
			_SpellChecker.IgnoredWord +=new NetSpell.SpellChecker.Spelling.IgnoredWordEventHandler(SpellChecker_IgnoredWord);
			_SpellChecker.MisspelledWord +=new NetSpell.SpellChecker.Spelling.MisspelledWordEventHandler(SpellChecker_MisspelledWord);
			_SpellChecker.ReplacedWord +=new NetSpell.SpellChecker.Spelling.ReplacedWordEventHandler(SpellChecker_ReplacedWord);

			return _SpellChecker;
		}

		private void ResetEvents()
		{
			// reset event data
			_lastSpellingEvent = null;
			_lastReplaceEvent = null;
			_lastEvent = EventNames.None;
		}
		private void SpellChecker_DeletedWord(object sender, SpellingEventArgs e)
		{
			_lastSpellingEvent = e;
			_lastEvent = EventNames.DeletedWord;
		}

		private void SpellChecker_DoubledWord(object sender, SpellingEventArgs e)
		{
			_lastSpellingEvent = e;
			_lastEvent = EventNames.DoubledWord;
		}

		private void SpellChecker_EndOfText(object sender, EventArgs e)
		{
			_lastEvent = EventNames.EndOfText;
		}

		private void SpellChecker_IgnoredWord(object sender, SpellingEventArgs e)
		{
			_lastSpellingEvent = e;
			_lastEvent = EventNames.IgnoredWord;
		}

		private void SpellChecker_MisspelledWord(object sender, SpellingEventArgs e)
		{
			_lastSpellingEvent = e;
			_lastEvent = EventNames.MisspelledWord;
		}

		private void SpellChecker_ReplacedWord(object sender, ReplaceWordEventArgs e)
		{
			_lastReplaceEvent = e;
			_lastEvent = EventNames.ReplacedWord;
		}

		[Test]
		public void TestEvents()
		{
			Spelling _SpellChecker = NewSpellChecker();

			_SpellChecker.Text = "ths is is a tst.";
			
			ResetEvents();
			_SpellChecker.SpellCheck();
			//spelling check
			Assert.AreEqual(0, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("ths", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			// event check
			Assert.AreEqual(EventNames.MisspelledWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastSpellingEvent, "Event not fired");
			Assert.AreEqual(0, _lastSpellingEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(0, _lastSpellingEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("ths", _lastSpellingEvent.Word, "Incorrect Event Word");
			
			ResetEvents();
			_SpellChecker.ReplaceWord("this");
			//spelling check
			Assert.AreEqual("this is is a tst.", _SpellChecker.Text, "Incorrect Text");
			// event check
			Assert.AreEqual(EventNames.ReplacedWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastReplaceEvent, "Null Event object fired");
			Assert.AreEqual(0, _lastReplaceEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(0, _lastReplaceEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("ths", _lastReplaceEvent.Word, "Incorrect Event Word");
			Assert.AreEqual("this", _lastReplaceEvent.ReplacementWord, "Incorrect Event Replacement Word");
			
			ResetEvents();
			_SpellChecker.SpellCheck();
			//spelling check
			Assert.AreEqual(2, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("is", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			// event check
			Assert.AreEqual(EventNames.DoubledWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastSpellingEvent, "Null Event object fired");
			Assert.AreEqual(2, _lastSpellingEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(8, _lastSpellingEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("is", _lastSpellingEvent.Word, "Incorrect Event Word");
			
			ResetEvents();
			_SpellChecker.DeleteWord();
			//spelling check
			Assert.AreEqual("this is a tst.", _SpellChecker.Text, "Incorrect Text");
			// event check
			Assert.AreEqual(EventNames.DeletedWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastSpellingEvent, "Null Event object fired");
			Assert.AreEqual(2, _lastSpellingEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(8, _lastSpellingEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("is ", _lastSpellingEvent.Word, "Incorrect Event Word");
		
			ResetEvents();
			_SpellChecker.SpellCheck();
			//spelling check
			Assert.AreEqual(3, _SpellChecker.WordIndex, "Incorrect WordOffset");
			Assert.AreEqual("tst", _SpellChecker.CurrentWord, "Incorrect CurrentWord");
			// event check
			Assert.AreEqual(EventNames.MisspelledWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastSpellingEvent, "Null Event object fired");
			Assert.AreEqual(3, _lastSpellingEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(10, _lastSpellingEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("tst", _lastSpellingEvent.Word, "Incorrect Event Word");
			
			ResetEvents();
			_SpellChecker.IgnoreWord();
			//spelling check
			Assert.AreEqual("this is a tst.", _SpellChecker.Text, "Incorrect Text");
			// event check
			Assert.AreEqual(EventNames.IgnoredWord, _lastEvent, "Incorrect Event");
			Assert.IsNotNull(_lastSpellingEvent, "Null Event object fired");
			Assert.AreEqual(3, _lastSpellingEvent.WordIndex, "Incorrect Event Word Index");
			Assert.AreEqual(10, _lastSpellingEvent.TextIndex, "Incorrect Event Text Index");
			Assert.AreEqual("tst", _lastSpellingEvent.Word, "Incorrect Event Word");
		
			ResetEvents();
			_SpellChecker.SpellCheck();
			// event check
			Assert.AreEqual(EventNames.EndOfText, _lastEvent, "Incorrect Event");
			
		}
	}
}
