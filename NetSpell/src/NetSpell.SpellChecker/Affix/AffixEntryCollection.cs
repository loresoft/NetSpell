using System;

namespace NetSpell.SpellChecker.Affix
{
	
	/// <summary>
	///     A collection that stores 'AffixEntry' objects.
	/// </summary>
	[Serializable()]
	public class AffixEntryCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'AffixEntryCollection'.
		/// </summary>
		public AffixEntryCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'AffixEntryCollection' based on an already existing instance.
		/// </summary>
		/// <param name='Value'>
		///     A 'AffixEntryCollection' from which the contents is copied
		/// </param>
		public AffixEntryCollection(AffixEntryCollection Value) 
		{
			this.AddRange(Value);
		}
    
		/// <summary>
		///     Initializes a new instance of 'AffixEntryCollection' with an array of 'AffixEntry' objects.
		/// </summary>
		/// <param name='Value'>
		///     An array of 'AffixEntry' objects with which to initialize the collection
		/// </param>
		public AffixEntryCollection(AffixEntry[] Value) 
		{
			this.AddRange(Value);
		}
    
		/// <summary>
		///     Represents the 'AffixEntry' item at the specified index position.
		/// </summary>
		/// <param name='Index'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public AffixEntry this[int Index] 
		{
			get 
			{
				return ((AffixEntry)(List[Index]));
			}
			set 
			{
				List[Index] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'AffixEntry' item with the specified value to the 'AffixEntryCollection'
		/// </summary>
		/// <param name='Value'>
		///     The 'AffixEntry' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(AffixEntry Value) 
		{
			return List.Add(Value);
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'AffixEntryCollection'.
		/// </summary>
		/// <param name='Value'>
		///     An array of 'AffixEntry' objects to add to the collection.
		/// </param>
		public void AddRange(AffixEntry[] Value) 
		{
			for (int Counter = 0; (Counter < Value.Length); Counter = (Counter + 1)) 
			{
				this.Add(Value[Counter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'AffixEntryCollection' at the end of this instance.
		/// </summary>
		/// <param name='Value'>
		///     A 'AffixEntryCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(AffixEntryCollection Value) 
		{
			for (int Counter = 0; (Counter < Value.Count); Counter = (Counter + 1)) 
			{
				this.Add(Value[Counter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'AffixEntryCollection' contains the specified value.
		/// </summary>
		/// <param name='Value'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(AffixEntry Value) 
		{
			return List.Contains(Value);
		}
    
		/// <summary>
		///     Copies the 'AffixEntryCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='Array'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='Index'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(AffixEntry[] Array, int Index) 
		{
			List.CopyTo(Array, Index);
		}
    
		/// <summary>
		///     Returns the index of a 'AffixEntry' object in the collection.
		/// </summary>
		/// <param name='Value'>
		///     The 'AffixEntry' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(AffixEntry Value) 
		{
			return List.IndexOf(Value);
		}
    
		/// <summary>
		///     Inserts an existing 'AffixEntry' into the collection at the specified index.
		/// </summary>
		/// <param name='Index'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='Value'>
		///     The item to insert.
		/// </param>
		public void Insert(int Index, AffixEntry Value) 
		{
			List.Insert(Index, Value);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'AffixEntryCollection'.
		/// </summary>
		public new AffixEntryEnumerator GetEnumerator() 
		{
			return new AffixEntryEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'AffixEntryCollection'.
		/// </summary>
		/// <param name='Value'>
		///     The item to remove from the 'AffixEntryCollection'.
		/// </param>
		public void Remove(AffixEntry Value) 
		{
			List.Remove(Value);
		}
	}

	/// <summary>
	///     A strongly typed enumerator for 'AffixEntryCollection'
	/// </summary>
	public class AffixEntryEnumerator : object, System.Collections.IEnumerator 
	{
    
		private System.Collections.IEnumerator Base;
    
		private System.Collections.IEnumerable Local;
    
		/// <summary>
		///     Enumerator constructor
		/// </summary>
		public AffixEntryEnumerator(AffixEntryCollection Mappings) 
		{
			this.Local = ((System.Collections.IEnumerable)(Mappings));
			this.Base = Local.GetEnumerator();
		}
    
		/// <summary>
		///     Gets the current element from the collection (strongly typed)
		/// </summary>
		public AffixEntry Current 
		{
			get 
			{
				return ((AffixEntry)(Base.Current));
			}
		}
    
		/// <summary>
		///     Gets the current element from the collection
		/// </summary>
		object System.Collections.IEnumerator.Current 
		{
			get 
			{
				return Base.Current;
			}
		}
    
		/// <summary>
		///     Advances the enumerator to the next element of the collection
		/// </summary>
		public bool MoveNext() 
		{
			return Base.MoveNext();
		}
    
		/// <summary>
		///     Advances the enumerator to the next element of the collection
		/// </summary>
		bool System.Collections.IEnumerator.MoveNext() 
		{
			return Base.MoveNext();
		}
    
		/// <summary>
		///     Sets the enumerator to the first element in the collection
		/// </summary>
		public void Reset() 
		{
			Base.Reset();
		}
    
		/// <summary>
		///     Sets the enumerator to the first element in the collection
		/// </summary>
		void System.Collections.IEnumerator.Reset() 
		{
			Base.Reset();
		}
	}

}
