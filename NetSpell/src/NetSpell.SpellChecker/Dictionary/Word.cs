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
		///     The affix keys that can be applied to this base word
		/// </summary>
		public string AffixKeys
		{
			get {return _AffixKeys;}
			set {_AffixKeys = value;}
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
		///     Sorts a collection of words by EditDistance
		/// </summary>
		/// <remarks>
		///		The compare sorts in desc order, largest EditDistance first
		/// </remarks>
		public int CompareTo(object obj)
		{
			int result = this.EditDistance.CompareTo(((Word)obj).EditDistance);
			return result * -1; // sorts desc order
		}

	}
}
