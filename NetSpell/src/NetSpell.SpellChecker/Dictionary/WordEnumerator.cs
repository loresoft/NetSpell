using System;
using System.Collections;

namespace NetSpell.SpellChecker.Dictionary
{
	/// <summary>
	/// Summary description for WordEnumerator.
	/// </summary>
	public class WordEnumerator : IDictionaryEnumerator
	{

		#region IDictionaryEnumerator Members

		public DictionaryEntry Entry
		{
			get
			{
				// TODO:  Add WordEnumerator.Entry getter implementation
				return new DictionaryEntry ();
			}
		}

		public object Key
		{
			get
			{
				// TODO:  Add WordEnumerator.Key getter implementation
				return null;
			}
		}

		public object Value
		{
			get
			{
				// TODO:  Add WordEnumerator.Value getter implementation
				return null;
			}
		}

		#endregion

		#region IEnumerator Members

		public bool MoveNext()
		{
			// TODO:  Add WordEnumerator.MoveNext implementation
			return false;
		}

		public void Reset()
		{
			// TODO:  Add WordEnumerator.Reset implementation
		}

		public object Current
		{
			get
			{
				// TODO:  Add WordEnumerator.Current getter implementation
				return null;
			}
		}

		#endregion

	}

}
