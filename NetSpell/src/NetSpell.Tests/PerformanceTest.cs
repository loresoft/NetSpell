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
			_SpellChecker.MaxSuggestions = 0;
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
			int totalFirst = 0;
			int totalTopFive = 0;
			int totalTopTen = 0;
			int totalTopTwentyFive = 0;

			Console.WriteLine("Misspelled\tCorrect\tPosition\tCount");

			// read line by line
			while (sr.Peek() >= 0) 
			{
				string tempLine = sr.ReadLine().Trim();
				if (tempLine.Length > 0)
				{
					string[] parts = tempLine.Split();
					string misSpelled = parts[0];
					string correctSpelled = parts[1];
					if (parts.Length > 2) correctSpelled += " " + parts[2];

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
								Console.WriteLine("{0}\t{1}\t{2}\t{3}", 
									misSpelled, correctSpelled, position.ToString(), _SpellChecker.Suggestions.Count.ToString());
								found = true;

								totalFound++;

								if (position == 1) totalFirst++;
								else if (position <= 5) totalTopFive++;
								else if (position <= 10) totalTopTen++;
								else if (position <= 25) totalTopTwentyFive++;

								break;
							}
						}

						if (!found)
						{
							if (_SpellChecker.Suggestions.Count > 0)
							{
								Console.WriteLine("{0}\t{1}\t{2}\t{3}", 
									misSpelled, correctSpelled, "0", _SpellChecker.Suggestions.Count.ToString());
							}
							else
							{
								Console.WriteLine("{0}\t{1}\t{2}\t{3}", 
									misSpelled, correctSpelled, "-1", "-1");
							}

						}
					}
					else
					{
						Console.WriteLine("{0}\t{1}\t{2}\t{3}", 
							misSpelled, correctSpelled, "1", "1");
					}
				}
			}


			totalTopFive += totalFirst;
			totalTopTen += totalTopFive;
			totalTopTwentyFive += totalTopTen;

			Console.WriteLine("Total Tested\t{0}", totalChecked);
			Console.WriteLine("Total Found\t{0}\t{1}%", totalFound, ((float)totalFound / (float)totalChecked * 100f));
			Console.WriteLine("First Suggestions\t{0}\t{1}%", totalFirst, ((float)totalFirst / (float)totalChecked * 100f));
			Console.WriteLine("Top 5 Suggestions\t{0}\t{1}%", totalTopFive, ((float)totalTopFive / (float)totalChecked * 100f));
			Console.WriteLine("Top 10 Suggestions\t{0}\t{1}%", totalTopTen, ((float)totalTopTen / (float)totalChecked * 100f));
			Console.WriteLine("Top 25 Suggestions\t{0}\t{1}%", totalTopTwentyFive, ((float)totalTopTwentyFive / (float)totalChecked * 100f));

			sr.Close();
			fs.Close();


		}
	}
}
