using System;

namespace NetSpell.SpellChecker.Phonetic
{
	/// <summary>
	///     A collection that stores 'PhoneticRule' objects.
	/// </summary>
	[Serializable()]
	public class PhoneticRuleCollection : System.Collections.CollectionBase 
	{
    
		/// <summary>
		///     Initializes a new instance of 'PhoneticRuleCollection'.
		/// </summary>
		public PhoneticRuleCollection() 
		{
		}
    
		/// <summary>
		///     Initializes a new instance of 'PhoneticRuleCollection' based on an already existing instance.
		/// </summary>
		/// <param name='Value'>
		///     A 'PhoneticRuleCollection' from which the contents is copied
		/// </param>
		public PhoneticRuleCollection(PhoneticRuleCollection Value) 
		{
			this.AddRange(Value);
		}
    
		/// <summary>
		///     Initializes a new instance of 'PhoneticRuleCollection' with an array of 'PhoneticRule' objects.
		/// </summary>
		/// <param name='Value'>
		///     An array of 'PhoneticRule' objects with which to initialize the collection
		/// </param>
		public PhoneticRuleCollection(PhoneticRule[] Value) 
		{
			this.AddRange(Value);
		}
    
		/// <summary>
		///     Represents the 'PhoneticRule' item at the specified index position.
		/// </summary>
		/// <param name='Index'>
		///     The zero-based index of the entry to locate in the collection.
		/// </param>
		/// <value>
		///     The entry at the specified index of the collection.
		/// </value>
		public PhoneticRule this[int Index] 
		{
			get 
			{
				return ((PhoneticRule)(List[Index]));
			}
			set 
			{
				List[Index] = value;
			}
		}
    
		/// <summary>
		///     Adds a 'PhoneticRule' item with the specified value to the 'PhoneticRuleCollection'
		/// </summary>
		/// <param name='Value'>
		///     The 'PhoneticRule' to add.
		/// </param>
		/// <returns>
		///     The index at which the new element was inserted.
		/// </returns>
		public int Add(PhoneticRule Value) 
		{
			return List.Add(Value);
		}
    
		/// <summary>
		///     Copies the elements of an array at the end of this instance of 'PhoneticRuleCollection'.
		/// </summary>
		/// <param name='Value'>
		///     An array of 'PhoneticRule' objects to add to the collection.
		/// </param>
		public void AddRange(PhoneticRule[] Value) 
		{
			for (int Counter = 0; (Counter < Value.Length); Counter = (Counter + 1)) 
			{
				this.Add(Value[Counter]);
			}
		}
    
		/// <summary>
		///     Adds the contents of another 'PhoneticRuleCollection' at the end of this instance.
		/// </summary>
		/// <param name='Value'>
		///     A 'PhoneticRuleCollection' containing the objects to add to the collection.
		/// </param>
		public void AddRange(PhoneticRuleCollection Value) 
		{
			for (int Counter = 0; (Counter < Value.Count); Counter = (Counter + 1)) 
			{
				this.Add(Value[Counter]);
			}
		}
    
		/// <summary>
		///     Gets a value indicating whether the 'PhoneticRuleCollection' contains the specified value.
		/// </summary>
		/// <param name='Value'>
		///     The item to locate.
		/// </param>
		/// <returns>
		///     True if the item exists in the collection; false otherwise.
		/// </returns>
		public bool Contains(PhoneticRule Value) 
		{
			return List.Contains(Value);
		}
    
		/// <summary>
		///     Copies the 'PhoneticRuleCollection' values to a one-dimensional System.Array
		///     instance starting at the specified array index.
		/// </summary>
		/// <param name='Array'>
		///     The one-dimensional System.Array that represents the copy destination.
		/// </param>
		/// <param name='Index'>
		///     The index in the array where copying begins.
		/// </param>
		public void CopyTo(PhoneticRule[] Array, int Index) 
		{
			List.CopyTo(Array, Index);
		}
    
		/// <summary>
		///     Returns the index of a 'PhoneticRule' object in the collection.
		/// </summary>
		/// <param name='Value'>
		///     The 'PhoneticRule' object whose index will be retrieved.
		/// </param>
		/// <returns>
		///     If found, the index of the value; otherwise, -1.
		/// </returns>
		public int IndexOf(PhoneticRule Value) 
		{
			return List.IndexOf(Value);
		}
    
		/// <summary>
		///     Inserts an existing 'PhoneticRule' into the collection at the specified index.
		/// </summary>
		/// <param name='Index'>
		///     The zero-based index where the new item should be inserted.
		/// </param>
		/// <param name='Value'>
		///     The item to insert.
		/// </param>
		public void Insert(int Index, PhoneticRule Value) 
		{
			List.Insert(Index, Value);
		}
    
		/// <summary>
		///     Returns an enumerator that can be used to iterate through
		///     the 'PhoneticRuleCollection'.
		/// </summary>
		public new PhoneticRuleEnumerator GetEnumerator() 
		{
			return new PhoneticRuleEnumerator(this);
		}
    
		/// <summary>
		///     Removes a specific item from the 'PhoneticRuleCollection'.
		/// </summary>
		/// <param name='Value'>
		///     The item to remove from the 'PhoneticRuleCollection'.
		/// </param>
		public void Remove(PhoneticRule Value) 
		{
			List.Remove(Value);
		}
	}

	/// <summary>
	///     A strongly typed enumerator for 'PhoneticRuleCollection'
	/// </summary>
	public class PhoneticRuleEnumerator : object, System.Collections.IEnumerator 
	{
    
		private System.Collections.IEnumerator Base;
    
		private System.Collections.IEnumerable Local;
    
		/// <summary>
		///     Enumerator constructor
		/// </summary>
		public PhoneticRuleEnumerator(PhoneticRuleCollection Mappings) 
		{
			this.Local = ((System.Collections.IEnumerable)(Mappings));
			this.Base = Local.GetEnumerator();
		}
    
		/// <summary>
		///     Gets the current element from the collection (strongly typed)
		/// </summary>
		public PhoneticRule Current 
		{
			get 
			{
				return ((PhoneticRule)(Base.Current));
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
