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
using System.Text;

namespace NetSpell.SpellChecker
{

	/// <summary>
	///		The double metaphone class is used to generate a code
	///		for what the word sounds like
	/// </summary>
	/// <remarks>
	///		Double Metaphone (c) 1998, 1999 by Lawrence Philips
	/// 
	///		Slightly modified by Kevin Atkinson to fix several bugs and 
	///		to allow it to give back more than 4 characters.
	///		
	///		Converted to C# by Paul Welter
	/// </remarks>
	[Serializable()]
	public class DoubleMetaphone
	{

		private StringBuilder primary = new StringBuilder();
		private StringBuilder secondary = new StringBuilder();
		private string word = "";

		/// <summary>
		///     DoubleMetaphone Default Constructor
		/// </summary>
		public DoubleMetaphone()
		{
		}

		/// <summary>
		///     DoubleMetaphone Constructor
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         word to generate the metaphone code on
		///     </para>
		/// </param>
		public DoubleMetaphone(string word)
		{
			this.word = word.ToUpper();
			this.GenerateMetaphone();
		}

		/// <summary>
		///     Generates the Primary and Secondary Metaphone codes
		/// </summary>
		public void GenerateMetaphone()
		{

			//reset codes
			primary.Length = 0;
			secondary.Length = 0;

			int current = 0;
			int length = word.Length;
			int last = word.Length - 1;//zero based index

			if(length < 1) return;

			word = word.ToUpper();
			//pad the original string so that we can index beyond the edge of the world 
			word = word.PadRight(length + 5 , ' ');
		        
			//skip these when at start of word
			if(StringAt(0, 2, new string[] {"GN", "KN", "PN", "WR", "PS"}))
				current += 1;

			//Initial "X" is pronounced "Z" e.g. "Xavier"
			if(word.Substring(0, 1) == "X")
			{
				MetaphAdd("S"); //"Z" maps to "S"
				current += 1;
			}

			//main loop
			while((primary.Length < 4 || secondary.Length < 4) && current < length)
			{
				
				switch(word.Substring(current, 1))
				{
					case "A":
					case "E":
					case "I":
					case "O":
					case "U":
					case "Y":
						//all init vowels map to "A"
						if(current == 0) 
							MetaphAdd("A"); 
						current +=1;
						break;
		                    
					case "B":

						//"-mb", e.g", "dumb", already skipped over...
						MetaphAdd("P");
						if(word.Substring(current + 1, 1) == "B") 
							current +=2;
						else 
							current +=1;
						break;
		                    
					case "Ç":
						MetaphAdd("S");
						current += 1;
						break;

					case "C":
						//various germanic
						if((current > 1)
							&& !IsVowel(current - 2) 
							&& StringAt((current - 1), 3, new string[] {"ACH"}) 
							&& ((word.Substring(current + 2, 1) != "I") && ((word.Substring(current + 2, 1) != "E") 
							|| StringAt((current - 2), 6, new string[] {"BACHER", "MACHER"})) ))
						{       
							MetaphAdd("K");
							current +=2;
							break;
						}

						//special case "caesar"
						if((current == 0) && StringAt(current, 6, new string[] {"CAESAR"}))
						{
							MetaphAdd("S");
							current +=2;
							break;
						}

						//italian "chianti"
						if(StringAt(current, 4, new string[] {"CHIA"}))
						{
							MetaphAdd("K");
							current +=2;
							break;
						}

						if(StringAt(current, 2, new string[] {"CH"}))
						{       

							//find "michael"
							if((current > 0) && StringAt(current, 4, new string[] {"CHAE"}))
							{
								MetaphAdd("K", "X");
								current +=2;
								break;
							}

							//greek roots e.g. "chemistry", "chorus"
							if((current == 0)
								&& (StringAt((current + 1), 5, new string[] {"HARAC", "HARIS"}) 
								|| StringAt((current + 1), 3, new string[] {"HOR", "HYM", "HIA", "HEM"})) 
								&& !StringAt(0, 5, new string[] {"CHORE"}))
							{
								MetaphAdd("K");
								current +=2;
								break;
							}

							//germanic, greek, or otherwise "ch" for "kh" sound
							if((StringAt(0, 4, new string[] {"VAN ", "VON "}) || StringAt(0, 3, new string[] {"SCH"}))
								// "architect but not "arch", "orchestra", "orchid"
								|| StringAt((current - 2), 6, new string[] {"ORCHES", "ARCHIT", "ORCHID"})
								|| StringAt((current + 2), 1, new string[] {"T", "S"})
								|| ((StringAt((current - 1), 1, new string[] {"A", "O", "U", "E"}) || (current == 0))
								//e.g., "wachtler", "wechsler", but not "tichner"
								&& StringAt((current + 2), 1, new string[] {"L", "R", "N", "M", "B", "H", "F", "V", "W", " "})))
							{
								MetaphAdd("K");
							}
							else
							{  

								if(current > 0)
								{
									//e.g., "McHugh"
									if(StringAt(0, 2, new string[] {"MC"})) 
										MetaphAdd("K");
									else 
										MetaphAdd("X", "K");
								}
								else 
									MetaphAdd("X");
							}
							current +=2;
							break;
						}

						//e.g, "czerny"
						if(StringAt(current, 2, new string[] {"CZ"}) && !StringAt((current - 2), 4, new string[] {"WICZ"}))
						{
							MetaphAdd("S", "X");
							current += 2;
							break;
						}

						//e.g., "focaccia"
						if(StringAt((current + 1), 3, new string[] {"CIA"}))
						{
							MetaphAdd("X");
							current += 3;
							break;
						}

						//double "C", but not if e.g. "McClellan"
						if(StringAt(current, 2, new string[] {"CC"}) && !((current == 1) && (word.Substring(0, 1) == "M")))

							//"bellocchio" but not "bacchus"
							if(StringAt((current + 2), 1, new string[] {"I", "E", "H"}) && !StringAt((current + 2), 2, new string[] {"HU"}))
							{

								//"accident", "accede" "succeed"
								if(((current == 1) && (word.Substring(current - 1, 1) == "A")) 
									|| StringAt((current - 1), 5, new string[] {"UCCEE", "UCCES"}))
									MetaphAdd("KS");

								//"bacci", "bertucci", other italian
								else 
									MetaphAdd("X");
								current += 3;
								break;
							}
							else
							{
								//Pierce"s rule
								MetaphAdd("K");
								current += 2;
								break;
							}

						if(StringAt(current, 2, new string[] {"CK", "CG", "CQ"}))
						{
							MetaphAdd("K");
							current += 2;
							break;
						}

						if(StringAt(current, 2, new string[] {"CI", "CE", "CY"}))
						{

							//italian vs. english
							if(StringAt(current, 3, new string[] {"CIO", "CIE", "CIA"})) 
								MetaphAdd("S", "X");
							else 
								MetaphAdd("S");
							current += 2;
							break;
						}

						//else
						MetaphAdd("K");
		                            
						//name sent in "mac caffrey", "mac gregor
						if(StringAt((current + 1), 2, new string[] {" C", " Q", " G"})) 
							current += 3;
						else if(StringAt((current + 1), 1, new string[] {"C", "K", "Q"}) 
								&& !StringAt((current + 1), 2, new string[] {"CE", "CI"}))
							current += 2;
						else
							current += 1;
						break;

					case "D":

						if(StringAt(current, 2, new string[] {"DG"}))
						{
							if(StringAt((current + 2), 1, new string[] {"I", "E", "Y"}))
							{
								//e.g. "edge"
								MetaphAdd("J");
								current += 3;
								break;
							}
							else
							{
								//e.g. "edgar"
								MetaphAdd("TK");
								current += 2;
								break;
							}
						}
						if(StringAt(current, 2, new string[] {"DT", "DD"}))
						{
							MetaphAdd("T");
							current += 2;
							break;
						}
		                            
						//else
						MetaphAdd("T");
						current += 1;
						break;

					case "F":

						if(word.Substring(current + 1, 1) == "F")
							current += 2;
						else
							current += 1;
						MetaphAdd("F");
						break;

					case "G":

						if(word.Substring(current + 1, 1) == "H")
						{

							if((current > 0) && !IsVowel(current - 1))
							{
								MetaphAdd("K");
								current += 2;
								break;
							}

							if(current < 3)
							{
								//"ghislane", ghiradelli
								if(current == 0)
								{ 
									if(word.Substring(current + 2, 1) == "I")
										MetaphAdd("J");
									else
										MetaphAdd("K");
									current += 2;
									break;
								}
							}

							//Parker"s rule (with some further refinements) - e.g., "hugh"
							if(((current > 1) && StringAt((current - 2), 1, new string[] {"B", "H", "D"}) )
								//e.g., "bough"
								|| ((current > 2) && StringAt((current - 3), 1, new string[] {"B", "H", "D"}) )
								//e.g., "broughton"
								|| ((current > 3) && StringAt((current - 4), 1, new string[] {"B", "H"}) ) )
							{
								current += 2;
								break;
							}
							else
							{

								//e.g., "laugh", "McLaughlin", "cough", "gough", "rough", "tough"
								if((current > 2) 
									&& (word.Substring(current - 1, 1) == "U") 
									&& StringAt((current - 3), 1, new string[] {"C", "G", "L", "R", "T"}) )
								{
									MetaphAdd("F");
								}
								else
									if((current > 0) && word.Substring(current - 1, 1) != "I")
										MetaphAdd("K");

								current += 2;
								break;
							}
						}

						if(word.Substring(current + 1, 1) == "N")
						{

							if((current == 1) && IsVowel(0) && !SlavoGermanic())
							{
								MetaphAdd("KN", "N");
							}
							//not e.g. "cagney"
							else if(!StringAt((current + 2), 2, new string[] {"EY"}) 
								&& (word.Substring(current + 1, 1) != "Y") && !SlavoGermanic())
							{
								MetaphAdd("N", "KN");
							}
							else
								MetaphAdd("KN");
							current += 2;
							break;
						}

						//"tagliaro"
						if(StringAt((current + 1), 2, new string[] {"LI"}) && !SlavoGermanic())
						{
							MetaphAdd("KL", "L");
							current += 2;
							break;
						}

						//-ges-,-gep-,-gel-, -gie- at beginning
						if((current == 0)
							&& ((word.Substring(current + 1, 1) == "Y") 
							|| StringAt((current + 1), 2, new string[] {"ES", "EP", "EB", "EL", "EY", "IB", "IL", "IN", "IE", "EI", "ER"})) )
						{
							MetaphAdd("K", "J");
							current += 2;
							break;
						}

						// -ger-,  -gy-
						if((StringAt((current + 1), 2, new string[] {"ER"}) || (word.Substring(current + 1, 1) == "Y"))
							&& !StringAt(0, 6, new string[] {"DANGER", "RANGER", "MANGER"})
							&& !StringAt((current - 1), 1, new string[] {"E", "I"}) 
							&& !StringAt((current - 1), 3, new string[] {"RGY", "OGY"}) )
						{
							MetaphAdd("K", "J");
							current += 2;
							break;
						}

						// italian e.g, "biaggi"
						if(StringAt((current + 1), 1, new string[] {"E", "I", "Y"}) || StringAt((current - 1), 4, new string[] {"AGGI", "OGGI"}))
						{

							//obvious germanic
							if((StringAt(0, 4, new string[] {"VAN ", "VON "}) || StringAt(0, 3, new string[] {"SCH"}))
								|| StringAt((current + 1), 2, new string[] {"ET"}))
								MetaphAdd("K");
							//always soft if french ending
							else if(StringAt((current + 1), 4, new string[] {"IER "}))
								MetaphAdd("J");
							else
								MetaphAdd("J", "K");
							current += 2;
							break;
						}

						if(word.Substring(current + 1, 1) == "G")
							current += 2;
						else
							current += 1;
						MetaphAdd("K");
						break;

					case "H":

						//only keep if first & before vowel or btw. 2 vowels
						if(((current == 0) || IsVowel(current - 1)) 
							&& IsVowel(current + 1))
						{
							MetaphAdd("H");
							current += 2;
						}
						else//also takes care of "HH"
							current += 1;
						break;

					case "J":

						//obvious spanish, "jose", "san jacinto"
						if(StringAt(current, 4, new string[] {"JOSE"}) || StringAt(0, 4, new string[] {"SAN "}) )
						{

							if(((current == 0) && (word.Substring(current + 4, 1) == " ")) || StringAt(0, 4, new string[] {"SAN "}) )
								MetaphAdd("H");
							else
								MetaphAdd("J", "H");
							current +=1;
							break;
						}

						if((current == 0) && !StringAt(current, 4, new string[] {"JOSE"}))
							MetaphAdd("J", "A");//Yankelovich/Jankelowicz
						//spanish pron. of e.g. "bajador"
						else if(IsVowel(current - 1) 
							&& !SlavoGermanic()
							&& ((word.Substring(current + 1, 1) == "A") || (word.Substring(current + 1, 1) == "O")))
							MetaphAdd("J", "H");
						else if(current == last)
							MetaphAdd("J", " ");
						else if(!StringAt((current + 1), 1, new string[] {"L", "T", "K", "S", "N", "M", "B", "Z"}) 
							&& !StringAt((current - 1), 1, new string[] {"S", "K", "L"}))
							MetaphAdd("J");

						if(word.Substring(current + 1, 1) == "J")//it could happen!
							current += 2;
						else
							current += 1;
						break;

					case "K":

						if(word.Substring(current + 1, 1) == "K")
							current += 2;
						else
							current += 1;
						MetaphAdd("K");
						break;

					case "L":

						if(word.Substring(current + 1, 1) == "L")
						{

							//spanish e.g. "cabrillo", "gallegos"
							if(((current == (length - 3)) 
								&& StringAt((current - 1), 4, new string[] {"ILLO", "ILLA", "ALLE"}))
								|| ((StringAt((last - 1), 2, new string[] {"AS", "OS"}) || StringAt(last, 1, new string[] {"A", "O"})) 
								&& StringAt((current - 1), 4, new string[] {"ALLE"})) )
							{
								MetaphAdd("L", " ");
								current += 2;
								break;
							}
							current += 2;
						}
						else
							current += 1;
						MetaphAdd("L");
						break;

					case "M":

						if((StringAt((current - 1), 3, new string[] {"UMB"}) 
							&& (((current + 1) == last) || StringAt((current + 2), 2, new string[] {"ER"})))
							//"dumb","thumb"
							||  (word.Substring(current + 1, 1) == "M") )
							current += 2;
						else
							current += 1;
						MetaphAdd("M");
						break;

					case "N":

						if(word.Substring(current + 1, 1) == "N")
							current += 2;
						else
							current += 1;
						MetaphAdd("N");
						break;

					case "Ñ":
						current += 1;
						MetaphAdd("N");
						break;

					case "P":

						if(word.Substring(current + 1, 1) == "H")
						{
							MetaphAdd("F");
							current += 2;
							break;
						}

						//also account for "campbell", "raspberry"
						if(StringAt((current + 1), 1, new string[] {"P", "B"}))
							current += 2;
						else
							current += 1;
						MetaphAdd("P");
						break;

					case "Q":

						if(word.Substring(current + 1, 1) == "Q")
							current += 2;
						else
							current += 1;
						MetaphAdd("K");
						break;

					case "R":

						//french e.g. "rogier", but exclude "hochmeier"
						if((current == last)
							&& !SlavoGermanic()
							&& StringAt((current - 2), 2, new string[] {"IE"}) 
							&& !StringAt((current - 4), 2, new string[] {"ME", "MA"}))
							MetaphAdd("", "R");
						else
							MetaphAdd("R");

						if(word.Substring(current + 1, 1) == "R")
							current += 2;
						else
							current += 1;
						break;

					case "S":

						//special cases "island", "isle", "carlisle", "carlysle"
						if(StringAt((current - 1), 3, new string[] {"ISL", "YSL"}))
						{
							current += 1;
							break;
						}

						//special case "sugar-"
						if((current == 0) && StringAt(current, 5, new string[] {"SUGAR"}))
						{
							MetaphAdd("X", "S");
							current += 1;
							break;
						}

						if(StringAt(current, 2, new string[] {"SH"}))
						{

							//germanic
							if(StringAt((current + 1), 4, new string[] {"HEIM", "HOEK", "HOLM", "HOLZ"}))
								MetaphAdd("S");
							else
								MetaphAdd("X");
							current += 2;
							break;
						}

						//italian & armenian
						if(StringAt(current, 3, new string[] {"SIO", "SIA"}) || StringAt(current, 4, new string[] {"SIAN"}))
						{

							if(!SlavoGermanic())
								MetaphAdd("S", "X");
							else
								MetaphAdd("S");
							current += 3;
							break;
						}

						//german & anglicisations, e.g. "smith" match "schmidt", "snider" match "schneider"
						//also, -sz- in slavic language altho in hungarian it is pronounced "s"
						if(((current == 0) 
							&& StringAt((current + 1), 1, new string[] {"M", "N", "L", "W"}))
							|| StringAt((current + 1), 1, new string[] {"Z"}))
						{
							MetaphAdd("S", "X");

							if(StringAt((current + 1), 1, new string[] {"Z"}))
								current += 2;
							else
								current += 1;
							break;
						}

						if(StringAt(current, 2, new string[] {"SC"}))
						{

							//Schlesinger's rule
							if(word.Substring(current + 2, 1) == "H")

								//dutch origin, e.g. "school", "schooner"
								if(StringAt((current + 3), 2, new string[] {"OO", "ER", "EN", "UY", "ED", "EM"}))
								{

									//"schermerhorn", "schenker"
									if(StringAt((current + 3), 2, new string[] {"ER", "EN"}))
									{
										MetaphAdd("X", "SK");
									}
									else
										MetaphAdd("SK");
									current += 3;
									break;
								}
								else
								{

									if((current == 0) && !IsVowel(3) && (word.Substring(3, 1) != "W"))
										MetaphAdd("X", "S");
									else
										MetaphAdd("X");
									current += 3;
									break;
								}

							if(StringAt((current + 2), 1, new string[] {"I", "E", "Y"}))
							{
								MetaphAdd("S");
								current += 3;
								break;
							}
							//else
							MetaphAdd("SK");
							current += 3;
							break;
						}

						//french e.g. "resnais", "artois"
						if((current == last) && StringAt((current - 2), 2, new string[] {"AI", "OI"}))
							MetaphAdd("", "S");
						else
							MetaphAdd("S");

						if(StringAt((current + 1), 1, new string[] {"S", "Z"}))
							current += 2;
						else
							current += 1;
						break;

					case "T":

						if(StringAt(current, 4, new string[] {"TION"}))
						{
							MetaphAdd("X");
							current += 3;
							break;
						}

						if(StringAt(current, 3, new string[] {"TIA", "TCH"}))
						{
							MetaphAdd("X");
							current += 3;
							break;
						}

						if(StringAt(current, 2, new string[] {"TH"}) 
							|| StringAt(current, 3, new string[] {"TTH"}))
						{

							//special case "thomas", "thames" or germanic
							if(StringAt((current + 2), 2, new string[] {"OM", "AM"}) 
								|| StringAt(0, 4, new string[] {"VAN ", "VON "}) 
								|| StringAt(0, 3, new string[] {"SCH"}))
							{
								MetaphAdd("T");
							}
							else
							{
								MetaphAdd("0", "T");
							}
							current += 2;
							break;
						}

						if(StringAt((current + 1), 1, new string[] {"T", "D"}))
							current += 2;
						else
							current += 1;
						MetaphAdd("T");
						break;

					case "V":

						if(word.Substring(current + 1, 1) == "V")
							current += 2;
						else
							current += 1;
						MetaphAdd("F");
						break;

					case "W":

						//can also be in middle of word
						if(StringAt(current, 2, new string[] {"WR"}))
						{
							MetaphAdd("R");
							current += 2;
							break;
						}

						if((current == 0) 
							&& (IsVowel(current + 1) || StringAt(current, 2, new string[] {"WH"})))
						{

							//Wasserman should match Vasserman
							if(IsVowel(current + 1))
								MetaphAdd("A", "F");
							else
								//need Uomo to match Womo
								MetaphAdd("A");
						}

						//Arnow should match Arnoff
						if(((current == last) && IsVowel(current - 1)) 
							|| StringAt((current - 1), 5, new string[] {"EWSKI", "EWSKY", "OWSKI", "OWSKY"}) 
							|| StringAt(0, 3, new string[] {"SCH"}))
						{
							MetaphAdd("", "F");
							current +=1;
							break;
						}

						//polish e.g. "filipowicz"
						if(StringAt(current, 4, new string[] {"WICZ", "WITZ"}))
						{
							MetaphAdd("TS", "FX");
							current +=4;
							break;
						}

						//else skip it
						current +=1;
						break;

					case "X":

						//french e.g. breaux
						if(!((current == last) 
							&& (StringAt((current - 3), 3, new string[] {"IAU", "EAU"}) 
							|| StringAt((current - 2), 2, new string[] {"AU", "OU"}))) )
							MetaphAdd("KS");

						if(StringAt((current + 1), 1, new string[] {"C", "X"}))
							current += 2;
						else
							current += 1;
						break;

					case "Z":

						//chinese pinyin e.g. "zhao"
						if(word.Substring(current + 1, 1) == "H")
						{
							MetaphAdd("J");
							current += 2;
							break;
						}
						else if(StringAt((current + 1), 2, new string[] {"ZO", "ZI", "ZA"}) 
							|| (SlavoGermanic() && ((current > 0) && word.Substring(current - 1, 1) != "T")))
						{
							MetaphAdd("S", "TS");
						}
						else
							MetaphAdd("S");

						if(word.Substring(current + 1, 1) == "Z")
							current += 2;
						else
							current += 1;

						break;

					default:
						current += 1;
						break;
				} // switch
			} // while
		}

		/// <summary>
		///     Generates the Primary and Secondary Metaphone codes
		/// </summary>
		/// <param name="word" type="string">
		///     <para>
		///         word to generate the metaphone code on
		///     </para>
		/// </param>
		public void GenerateMetaphone(string word)
		{
			this.word = word.ToUpper();
			this.GenerateMetaphone();
		}

		internal bool IsVowel(int at)
		{

			if((at < 0) || (at >= word.Length)) 
				return false;

			string it = word.Substring(at, 1);

			if((it == "A") || (it == "E") 
				|| (it == "I") || (it == "O") 
				|| (it == "U") || (it == "Y") ) 
			{
				return true;
			}
			return false;
		}

		internal void MetaphAdd(string main, string alt)
		{

			if(main.Length > 0) 
				primary.Append(main);

			if(alt.Length > 0)
			{
				if(alt != " ") 
					secondary.Append(alt);
			}
			else 
			{
				if(main.Length > 0 && main != " ") 
					secondary.Append(main);
			}
		}

		internal void MetaphAdd(string main)
		{

			if(main.Length > 0) 
			{
				primary.Append(main);
				secondary.Append(main);
			}
		}

		internal bool SlavoGermanic()
		{       

			if((word.IndexOf("W") > -1) || (word.IndexOf("K") > -1) || (word.IndexOf("CZ") > -1) || (word.IndexOf("WITZ") > -1))
				return true;

			return false;
				
		}

		internal bool StringAt(int start, int length, string[] strings)
		{

			if (start < 0) 
				return false;

			foreach (string temp in strings)
			{
				if (word.Substring(start, length) == temp) 
					return true;
			}
			return false;
		}

		/// <summary>
		///		Word that the metaphone code was generated from
		/// </summary>
		public string CurrentWord
		{
			get { return word.Trim(); }
			set { word = value.ToUpper(); }
		}

		/// <summary>
		///		Primary Metaphone code
		/// </summary>
		public string PrimaryCode
		{
			get { return primary.ToString(); }
		}

		/// <summary>
		///		Secondary Metaphone code
		/// </summary>
		public string SecondaryCode
		{
			get { return secondary.ToString(); }
		}

	}
}
