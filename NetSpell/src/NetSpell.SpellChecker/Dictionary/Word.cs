using System;

namespace NetSpell.SpellChecker.Dictionary
{
	/// <summary>
	/// The Word class represents a base word in the dictionary
	/// </summary>
	public class Word  : IComparable
	{
		private string _AffixKeys = "";
		private int _EditDistance = 0;
		private string _PhoneticCode = "";
		private string _Value = "";

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		public Word()
		{
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="value" type="string">
		///     <para>
		///         The string for the base word
		///     </para>
		/// </param>
		/// <param name="editDistance" type="int">
		///     <para>
		///         The edit distance from the misspelled word
		///     </para>
		/// </param>
		public Word(string value, int editDistance)
		{
			this.Value = value;
			this.EditDistance = editDistance;
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="value" type="string">
		///     <para>
		///         The string for the base word
		///     </para>
		/// </param>
		/// <param name="affixKeys" type="string">
		///     <para>
		///         The affix keys that can be applied to this base word
		///     </para>
		/// </param>
		/// <param name="phoneticCode" type="string">
		///     <para>
		///         The phonetic code for this word
		///     </para>
		/// </param>
		public Word(string value, string affixKeys, string phoneticCode)
		{
			this.Value = value;
			this.AffixKeys = affixKeys;
			this.PhoneticCode = phoneticCode;
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="value" type="string">
		///     <para>
		///         The string for the base word
		///     </para>
		/// </param>
		/// <param name="affixKeys" type="string">
		///     <para>
		///         The affix keys that can be applied to this base word
		///     </para>
		/// </param>
		public Word(string value, string affixKeys)
		{
			this.Value = value;
			this.AffixKeys = affixKeys;
		}

		/// <summary>
		///     Initializes a new instance of the class
		/// </summary>
		/// <param name="value" type="string">
		///     <para>
		///         The string for the base word
		///     </para>
		/// </param>
		public Word(string value)
		{
			this.Value = value;
		}

		/// <summary>
		///     Sorts a collection of words by EditDistance
		/// </summary>
		/// <remarks>
		///		The compare sorts in desc order, largest EditDistance first
		/// </remarks>
		public int CompareTo(object obj)
		{
			int result = this.EditDistance.CompareTo(((Word)obj).EditDistance);
			return result; // * -1; // sorts desc order
		}


		/// <summary>
		///     The affix keys that can be applied to this base word
		/// </summary>
		public string AffixKeys
		{
			get {return _AffixKeys;}
			set {_AffixKeys = value;}
		}


		/// <summary>
		///     Used for sorting suggestions by its edit distance for 
		///     the misspelled word
		/// </summary>
		/// <remarks>
		///		Internal use only
		/// </remarks>
		public int EditDistance
		{
			get {return _EditDistance;}
			set {_EditDistance = value;}
		}

		/// <summary>
		///     The phonetic code for this word
		/// </summary>
		public string PhoneticCode
		{
			get {return _PhoneticCode;}
			set {_PhoneticCode = value;}
		}

		/// <summary>
		///     The string for the base word
		/// </summary>
		public string Value
		{
			get {return _Value;}
			set {_Value = value;}
		}

	}
}
