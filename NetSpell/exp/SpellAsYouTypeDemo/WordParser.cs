using System;
using System.Text;
using System.Text.RegularExpressions;

namespace SpellAsYouTypeDemo
{
	/// <summary>
	/// Summary description for WordParser.
	/// </summary>
    public class WordParser 
    {
        private int _WordIndex;

        
        private StringBuilder _stringBuilder = new StringBuilder();
        private Regex _wordEx = new Regex(@"\b[A-Za-z0-9_'À-ÿ]+\b", RegexOptions.Compiled);
		
        public WordParser()
		{
			
		}

        public void Append(string value)
        {

        }
        public void Append(char value)
        {

        }

        public void Insert(int index, char value)
        {
    
        }

        public void Insert(int index, string value)
        {
    
        }

        public void Remove(int startIndex, int length)
        {

        }

        public string Text
        {
            get { return _stringBuilder.ToString(); }
            set 
            { 
                _stringBuilder.Length = 0; 
                _stringBuilder.Append(value); 
            }
        }

        public int Length
        {
            get { return _stringBuilder.Length; }
        }


        public int WordIndex
        {
            get { return _WordIndex; }
            set { _WordIndex = value; }
        }


    }
}
