// Copyright (c) 2003, Paul Welter
// All rights reserved.

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Diagnostics;

using NUnit.Framework;
using NetSpell.SpellChecker;
using NetSpell.SpellChecker.Dictionary;
using NetSpell.SpellChecker.Dictionary.Phonetic;
using NetSpell.SpellChecker.Dictionary.Affix;

namespace NetSpell.Tests
{
	/// <summary>
	/// Summary description for PerformanceTest.
	/// </summary>
	[TestFixture]
	public class PerformanceTest
	{
		Spelling _SpellChecker = new Spelling();
		PerformanceTimer _timer = new PerformanceTimer();

		public PerformanceTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		[SetUp]
		public void SetUp()
		{
			_SpellChecker.Dictionary.DictionaryFolder = @"..\..\..\Dictionaries";
			_SpellChecker.Dictionary.Initialize();
			
			_SpellChecker.ShowDialog = false;
			_SpellChecker.MaxSuggestions = 50;
		}

		[Test]
		public void SuggestionRank()
		{
			string invalidFile = @"..\..\..\Dictionaries\Test\SuggestionTest.txt";
			
			// open file
			FileStream fs = new FileStream(invalidFile, FileMode.Open, FileAccess.Read, FileShare.Read);
			StreamReader sr = new StreamReader(fs, Encoding.UTF7);

			int totalFound = 0;
			int totalChecked = 0;
			int totalTopFive = 0;
			int totalTopTen = 0;
			int totalTopTwentyFive = 0;

			// read line by line
			while (sr.Peek() >= 0) 
			{
				string tempLine = sr.ReadLine().Trim();
				if (tempLine.Length > 0)
				{
					string[] parts = tempLine.Split();
					string misSpelled = parts[0];
					string correctSpelled = parts[1];
					bool found = false;

					if(_SpellChecker.SpellCheck(misSpelled))
					{
						totalChecked++;
						_SpellChecker.Suggest();
						int position = 0;
						foreach(string suggestion in _SpellChecker.Suggestions)
						{
							position++;
							if(suggestion.ToLower() == correctSpelled.ToLower())
							{
								Console.WriteLine("{0} found correctly spelled as {1} in position {2}", 
									misSpelled, suggestion, position.ToString());
								found = true;

								totalFound++;

								if (position <= 5) totalTopFive++;
								else if (position <= 10) totalTopTen++;
								else if (position <= 25) totalTopTwentyFive++;

								break;
							}
						}

						if (!found)
						{
							Console.WriteLine("{0} not found in suggestions. {1} suggestions generated.", 
								misSpelled, _SpellChecker.Suggestions.Count.ToString());

						}
					}
				}
			}

			Console.WriteLine("{0} words tested", totalChecked);
			Console.WriteLine("{0} words found", totalFound);
			Console.WriteLine("{0} words found in top 5", totalTopFive);
			Console.WriteLine("{0} words found in top 10", totalTopTen);
			Console.WriteLine("{0} words found in top 25", totalTopTwentyFive);

			sr.Close();
			fs.Close();


		}
	}
}
