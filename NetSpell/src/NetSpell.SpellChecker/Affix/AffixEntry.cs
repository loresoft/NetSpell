using System;
using System.Text.RegularExpressions;

namespace NetSpell.SpellChecker.Affix
{
	/// <summary>
	///		Rule Entry for expanding base words
	/// </summary>
	public class AffixEntry
	{


		private string _AddCharacters = "";
		private string _Condition = "" ;
		private Regex _ConditionRegex = null;
		private string _StripCharacters = "";

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		public AffixEntry()
		{
		}

		/// <summary>
		///     Initializes a new instance of the class while setting its properties
		/// </summary>
		/// <param name="addChars" type="string">
		///     <para>
		///         The characters to add to the string
		///     </para>
		/// </param>
		/// <param name="stripChars" type="string">
		///     <para>
		///         The characters to remove before adding characters
		///     </para>
		/// </param>
		/// <param name="condition" type="string">
		///     <para>
		///         The condition to be met in order to add characters
		///     </para>
		/// </param>
		public AffixEntry(string addChars, string stripChars, string condition)
		{
			_AddCharacters = addChars;
			_Condition = condition;
			_StripCharacters = stripChars;
		}

		/// <summary>
		///     The characters to add to the string
		/// </summary>
		public string AddCharacters
		{
			get {return _AddCharacters;}
			set {_AddCharacters = value;}
		}

		/// <summary>
		///     The condition to be met in order to add characters
		/// </summary>
		public string Condition
		{
			get {return _Condition;}
			set {_Condition = value;}
		}


		/// <summary>
		///     Regular Expresion to evaluate the condition
		/// </summary>
		public Regex ConditionRegex
		{
			get {return _ConditionRegex;}
			set {_ConditionRegex = value;}
		}

		/// <summary>
		///     The characters to remove before adding characters
		/// </summary>
		public string StripCharacters
		{
			get {return _StripCharacters;}
			set {_StripCharacters = value;}
		}

	}
}
