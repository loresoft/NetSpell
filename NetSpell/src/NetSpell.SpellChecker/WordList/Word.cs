using System;

namespace NetSpell.SpellChecker.WordList
{
	/// <summary>
	/// Summary description for WordItem.
	/// </summary>
	public class Word
	{
		private string _AffixKeys;
		private string _SoundCode;
		private string _Text;

		public Word()
		{
		}

		public Word(string text)
		{
			this.Text = text;
		}

		public Word(string text, string affixKeys)
		{
			this.Text = text;
			this.AffixKeys = affixKeys;
		}

		public Word(string text, string affixKeys, string soundCode)
		{
			this.Text = text;
			this.AffixKeys = affixKeys;
			this.SoundCode = soundCode;
		}

		public string AffixKeys
		{
			get {return _AffixKeys;}
			set {_AffixKeys = value;}
		}

		public string SoundCode
		{
			get {return _SoundCode;}
			set {_SoundCode = value;}
		}

		public string Text
		{
			get {return _Text;}
			set {_Text = value;}
		}

	}
}
