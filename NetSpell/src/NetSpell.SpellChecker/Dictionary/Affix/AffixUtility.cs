using System;

namespace NetSpell.SpellChecker.Dictionary.Affix
{
	/// <summary>
	/// Summary description for AffixUtility.
	/// </summary>
	public class AffixUtility
	{
		public AffixUtility()
		{
		}

		/// <summary>
		///     Generates the condition character array
		/// </summary>
		/// <param name="conditionText" type="string">
		///     <para>
		///         the text form of the conditions
		///     </para>
		/// </param>
		/// <param name="entry" type="NetSpell.SpellChecker.Dictionary.Affix.AffixEntry">
		///     <para>
		///         The AffixEntry to add the condition array to
		///     </para>
		/// </param>
		public static void EncodeConditions(string conditionText, AffixEntry entry)
		{
			// clear the conditions array
			for (int i=0; i < entry.Condition.Length; i++)
			{
				entry.Condition[i] = 0;
			}

			// if no condition just return
			if (conditionText == ".") 
			{
				entry.ConditionCount = 0;
				return;
			}

			bool neg = false;  /* complement indicator */
			bool group = false;  /* group indicator */
			bool end = false;   /* end condition indicator */
			int num = 0;    /* number of conditions */
			
			char [] memberChars = new char[200];
			int numMember = 0;   /* number of member in group */

			foreach (char cond in conditionText)
			{
				// parse member group
				if (cond == '[')
				{
					group = true;  // start a group
				}
				else if (cond == '^' && group)
				{
					neg = true; // negative group
				}
				else if (cond == ']')
				{
					end = true; // end of a group
				}
				else if (group)
				{
					// add chars to group
					memberChars[numMember] = cond;
					numMember++;
				}
				else
				{
					end = true;  // no group
				}

				// set condition
				if (end)
				{
					if (group)
					{
						if (neg)
						{
							// turn all chars on
							for (int j=0; j < entry.Condition.Length; j++) 
							{
								entry.Condition[j] = entry.Condition[j] | (1 << num);
							}
							// turn off chars in member group
							for (int j=0; j < numMember; j++) 
							{
								int charCode = (int)memberChars[j];
								entry.Condition[charCode] = entry.Condition[charCode] & ~(1 << num);
							}
						}
						else 
						{
							// turn on chars in member group
							for (int j=0; j < numMember; j++) 
							{
								int charCode = (int)memberChars[j];
								entry.Condition[charCode] = entry.Condition[charCode] | (1 << num);
							}
						}
						group = false;
						neg = false;
						numMember = 0;
					} // if group
					else
					{
						if (cond == '.')
						{
							// wild card character, turn all chars on
							for (int j=0; j < entry.Condition.Length; j++) 
							{
								entry.Condition[j] = entry.Condition[j] | (1 << num);
							}
						}
						else 
						{
							// turn on char
							int charCode = (int)cond;
							entry.Condition[charCode] = entry.Condition[charCode] | (1 << num);
						}
					} // not group

					end = false;
					num++;
				} // if end
			} // foreach char

			entry.ConditionCount = num;
			return;

		}

	}
}
