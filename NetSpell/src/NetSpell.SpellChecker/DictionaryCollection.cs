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
using System.Collections;
using System.Runtime.Serialization;

namespace NetSpell.SpellChecker
{
#region "'DictionaryCollection' strongly typed collection class"

	/// <summary>
	///     A collection that stores 'Dictionary' objects.
	/// </summary>
	[Serializable()]
	public class DictionaryCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'DictionaryCollection'.
		/// </summary>
		public DictionaryCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'DictionaryCollection' based on an already existing instance.
		/// </summary>
		/// <param name='dicValue'>
		///     A 'DictionaryCollection' from which the contents is copied
		/// </param>
		public DictionaryCollection(DictionaryCollection dicValue) 
		{
			this.AddRange(dicValue);
		}
    
		/// <summary>
		///     Initializes a new instance of 'DictionaryCollection' with an array of 'Dictionary' objects.
		/// </summary>
		/// <param name='dicValue'>
		///     An array of 'Dictionary' objects with which to initialize the collection
		/// </param>
		public DictionaryCollection(Dictionary[] dicValue) 
		{
			this.AddRange(dicValue);
		}
    
		/// <summary>
		///     Represents the 'Dictionary' item at the specified index position.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public Dictionary this[int intIndex] 
		{
			get 
			{
				return ((Dictionary)(List[intIndex]));
			}
			set 
			{
				List[intIndex] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'Dictionary' item with the specified value to the 'DictionaryCollection'
		/// </summary>
		/// <param name='dicValue'>
		///     The 'Dictionary' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(Dictionary dicValue) 
		{
			return List.Add(dicValue);
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'DictionaryCollection'.
		/// </summary>
		/// <param name='dicValue'>
		///     An array of 'Dictionary' objects to add to the collection.
		/// </param>
		public void AddRange(Dictionary[] dicValue) 
		{
			for (int intCounter = 0; (intCounter < dicValue.Length); intCounter = (intCounter + 1)) 
			{
				this.Add(dicValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'DictionaryCollection' at the end of this instance.
		/// </summary>
		/// <param name='dicValue'>
		///     A 'DictionaryCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(DictionaryCollection dicValue) 
		{
			for (int intCounter = 0; (intCounter < dicValue.Count); intCounter = (intCounter + 1)) 
			{
				this.Add(dicValue[intCounter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'DictionaryCollection' contains the specified value.
		/// </summary>
		/// <param name='dicValue'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(Dictionary dicValue) 
		{
			return List.Contains(dicValue);
		}
    
		/// <summary>
		///     Copies the 'DictionaryCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='dicArray'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='intIndex'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(Dictionary[] dicArray, int intIndex) 
		{
			List.CopyTo(dicArray, intIndex);
		}
    
		/// <summary>
		///     Returns the index of a 'Dictionary' object in the collection.
		/// </summary>
		/// <param name='dicValue'>
		///     The 'Dictionary' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(Dictionary dicValue) 
		{
			return List.IndexOf(dicValue);
		}
    
		/// <summary>
		///     Inserts an existing 'Dictionary' into the collection at the specified index.
		/// </summary>
		/// <param name='intIndex'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='dicValue'>
		///     The item to insert.
		/// </param>
		public void Insert(int intIndex, Dictionary dicValue) 
		{
			List.Insert(intIndex, dicValue);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'DictionaryCollection'.
		/// </summary>
		public new DictionaryEnumerator GetEnumerator() 
		{
			return new DictionaryEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'DictionaryCollection'.
		/// </summary>
		/// <param name='dicValue'>
		///     The item to remove from the 'DictionaryCollection'.
		/// </param>
		public void Remove(Dictionary dicValue) 
		{
			List.Remove(dicValue);
		}
    
		/// <summary>
		///     A strongly typed enumerator for 'DictionaryCollection'
		/// </summary>
		public class DictionaryEnumerator : object, System.Collections.IEnumerator 
		{
        
			private System.Collections.IEnumerator iEnBase;
        
			private System.Collections.IEnumerable iEnLocal;
        
			/// <summary>
			///     Enumerator constructor
			/// </summary>
			public DictionaryEnumerator(DictionaryCollection dicMappings) 
			{
				this.iEnLocal = ((System.Collections.IEnumerable)(dicMappings));
				this.iEnBase = iEnLocal.GetEnumerator();
			}
        
			/// <summary>
			///     Gets the current element from the collection (strongly typed)
			/// </summary>
			public Dictionary Current 
			{
				get 
				{
					return ((Dictionary)(iEnBase.Current));
				}
			}
        
			/// <summary>
			///     Gets the current element from the collection
			/// </summary>
			object System.Collections.IEnumerator.Current 
			{
				get 
				{
					return iEnBase.Current;
				}
			}
        
			/// <summary>
			///     Advances the enumerator to the next element of the collection
			/// </summary>
			public bool MoveNext() 
			{
				return iEnBase.MoveNext();
			}
        
			/// <summary>
			///     Advances the enumerator to the next element of the collection
			/// </summary>
			bool System.Collections.IEnumerator.MoveNext() 
			{
				return iEnBase.MoveNext();
			}
        
			/// <summary>
			///     Sets the enumerator to the first element in the collection
			/// </summary>
			public void Reset() 
			{
				iEnBase.Reset();
			}
        
			/// <summary>
			///     Sets the enumerator to the first element in the collection
			/// </summary>
			void System.Collections.IEnumerator.Reset() 
			{
				iEnBase.Reset();
			}
		}
	}

#endregion //('DictionaryCollection' strongly typed collection class)
}
