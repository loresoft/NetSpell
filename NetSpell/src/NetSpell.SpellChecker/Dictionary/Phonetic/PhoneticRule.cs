using System;

namespace NetSpell.SpellChecker.Dictionary.Phonetic
{
	/// <summary>
	/// Summary description for Rule.
	/// </summary>
	public class PhoneticRule
	{

		private bool _BeginningOnly;
		private int _ConsumeCount;
		private bool _EndOnly;
		private string _MatchCharacters;
		private int _Priority;
		private string _ReplaceString;
		private string _SearchString;

		public PhoneticRule()
		{
		}

		public bool BeginningOnly
		{
			get {return _BeginningOnly;}
			set {_BeginningOnly = value;}
		}

		public int ConsumeCount
		{
			get {return _ConsumeCount;}
			set {_ConsumeCount = value;}
		}

		public bool EndOnly
		{
			get {return _EndOnly;}
			set {_EndOnly = value;}
		}

		public string MatchCharacters
		{
			get {return _MatchCharacters;}
			set {_MatchCharacters = value;}
		}

		public int Priority
		{
			get {return _Priority;}
			set {_Priority = value;}
		}

		public string ReplaceString
		{
			get {return _ReplaceString;}
			set {_ReplaceString = value;}
		}

		public string SearchString
		{
			get {return _SearchString;}
			set {_SearchString = value;}
		}

	}
}
