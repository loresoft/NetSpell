// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Collections;
using System.IO;
using System.Text;

using NUnit.Framework;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Phonetic;
using NetSpell.SpellChecker.Dictionary.Affix;

namespace NetSpell.Tests
{
	/// <summary>
	/// Summary description for DictionaryTest.
	/// </summary>
	[TestFixture]
	public class DictionaryTest
	{
		WordDictionary _WordDictionary = new WordDictionary();
		PerformanceTimer _timer = new PerformanceTimer();


		public DictionaryTest()
		{
		}

		[SetUp]
		public void SetUp()
		{
			_WordDictionary.DictionaryFile = @"..\..\..\Dictionaries\en_US.txt";
			_WordDictionary.Initialize();
		}

		[Test]
		public void Contains() 
		{
			string validFile = @"..\..\..\Dictionaries\Test\allwords.txt";
			string invalidFile = @"..\..\..\Dictionaries\Test\SuggestionTest.txt";
			
			// open file
			FileStream fs = new FileStream(validFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			StreamReader sr = new StreamReader(fs, Encoding.UTF7);
			
			_timer.StartTimer();

			// read line by line
			while (sr.Peek() >= 0) 
			{
				string tempLine = sr.ReadLine().Trim();
				if (tempLine.Length > 0)
				{
					if (tempLine.IndexOf(' ') > 0)
					{
						tempLine = tempLine.Substring(0, tempLine.IndexOf(' '));
					}

					if (!_WordDictionary.Contains(tempLine)) 
					{
						Assertion.Fail(string.Format("Did not find word: {0}" , tempLine));
					}
				}
			}
			float checkTime = _timer.StopTimer();

			sr.Close();
			fs.Close();

			
			// open file
			fs = new FileStream(invalidFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			sr = new StreamReader(fs, Encoding.UTF7);
			
			_timer.StartTimer();
			// read line by line
			while (sr.Peek() >= 0) 
			{
				string tempLine = sr.ReadLine().Trim();
				if (tempLine.Length > 0)
				{
					if (tempLine.IndexOf(' ') > 0)
					{
						tempLine = tempLine.Substring(0, tempLine.IndexOf(' '));
					}

					if (_WordDictionary.Contains(tempLine)) 
					{
						Assertion.Fail(string.Format("Word found that should not be: {0}" , tempLine));
					}
				}

			}
			float invalidTime = _timer.StopTimer();

			sr.Close();
			fs.Close();
		}

	}
}
