using System;
using System.Windows.Forms;

namespace NetSpell.TraySpell
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	[Serializable]
	public class Settings
	{
		private string[] _Dictionaries = new string[]{"us-en.dic", "user.dic"};
		private int _HotKey = (int)Keys.S;
		private int _HotKeyModifier = (int)Win32.KeyModifiers.MOD_WIN;
		private bool _IgnoreAllCapsWords = true;
		private bool _IgnoreWordsWithDigits = false;

		public Settings()
		{
		}

		public string[] Dictionaries
		{
			get {return _Dictionaries;}
			set {_Dictionaries = value;}
		}

		public int HotKey
		{
			get {return _HotKey;}
			set {_HotKey = value;}
		}

		public int HotKeyModifier
		{
			get {return _HotKeyModifier;}
			set {_HotKeyModifier = value;}
		}

		public bool IgnoreAllCapsWords
		{
			get {return _IgnoreAllCapsWords;}
			set {_IgnoreAllCapsWords = value;}
		}

		public bool IgnoreWordsWithDigits
		{
			get {return _IgnoreWordsWithDigits;}
			set {_IgnoreWordsWithDigits = value;}
		}

	}
}
