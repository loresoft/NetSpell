using System;
using System.Collections;

namespace NetSpell.SpellChecker.WordList
{
	/// <summary>
	///     A dictionary collection that stores 'Word' objects.
	/// </summary>	
	public class WordCollection : IDictionary, ICollection, IEnumerable, ICloneable
	{
		/// <summary>
		///     Internal Hashtable
		/// </summary>
		protected Hashtable innerHash;
		
		#region "Constructors"
		
		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		public  WordCollection()
		{
			innerHash = new Hashtable();
		}
		
		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="original" type="WordCollection">
		///     <para>
		///         A 'WordCollection' from which the contents is copied
		///     </para>
		/// </param>
		public WordCollection(WordCollection original)
		{
			innerHash = new Hashtable (original.innerHash);
		}
		
		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="dictionary" type="System.Collections.IDictionary">
		///     <para>
		///         The IDictionary to copy to a new 'WordCollection'.
		///     </para>
		/// </param>
		public WordCollection(IDictionary dictionary)
		{
			innerHash = new Hashtable (dictionary);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="capacity" type="int">
		///     <para>
		///         The approximate number of elements that the 'WordCollection' can initially contain.
		///     </para>
		/// </param>
		public WordCollection(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="dictionary" type="System.Collections.IDictionary">
		///     <para>
		///         The IDictionary to copy to a new 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="loadFactor" type="float">
		///     <para>
		///         A number in the range from 0.1 through 1.0 indicating the maximum ratio of elements to buckets.
		///     </para>
		/// </param>
		public WordCollection(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="codeProvider" type="System.Collections.IHashCodeProvider">
		///     <para>
		///         The IHashCodeProvider that supplies the hash codes for all keys in the 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="comparer" type="System.Collections.IComparer">
		///     <para>
		///         The IComparer to use to determine whether two keys are equal.
		///     </para>
		/// </param>
		public WordCollection(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (codeProvider, comparer);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="capacity" type="int">
		///     <para>
		///         The approximate number of elements that the 'WordCollection' can initially contain.
		///     </para>
		/// </param>
		/// <param name="loadFactor" type="int">
		///     <para>
		///         A number in the range from 0.1 through 1.0 indicating the maximum ratio of elements to buckets.
		///     </para>
		/// </param>
		public WordCollection(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}


		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="dictionary" type="System.Collections.IDictionary">
		///     <para>
		///         The IDictionary to copy to a new 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="codeProvider" type="System.Collections.IHashCodeProvider">
		///     <para>
		///         The IHashCodeProvider that supplies the hash codes for all keys in the 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="comparer" type="System.Collections.IComparer">
		///     <para>
		///         The IComparer to use to determine whether two keys are equal.
		///     </para>
		/// </param>
		public WordCollection(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, codeProvider, comparer);
		}
		
		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="capacity" type="int">
		///     <para>
		///         The approximate number of elements that the 'WordCollection' can initially contain.
		///     </para>
		/// </param>
		/// <param name="codeProvider" type="System.Collections.IHashCodeProvider">
		///     <para>
		///         The IHashCodeProvider that supplies the hash codes for all keys in the 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="comparer" type="System.Collections.IComparer">
		///     <para>
		///         The IComparer to use to determine whether two keys are equal.
		///     </para>
		/// </param>		
		public WordCollection(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, codeProvider, comparer);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="dictionary" type="System.Collections.IDictionary">
		///     <para>
		///         The IDictionary to copy to a new 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="loadFactor" type="float">
		///     <para>
		///         A number in the range from 0.1 through 1.0 indicating the maximum ratio of elements to buckets.
		///     </para>
		/// </param>
		/// <param name="codeProvider" type="System.Collections.IHashCodeProvider">
		///     <para>
		///         The IHashCodeProvider that supplies the hash codes for all keys in the 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="comparer" type="System.Collections.IComparer">
		///     <para>
		///         The IComparer to use to determine whether two keys are equal.
		///     </para>
		/// </param>
		public WordCollection(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, loadFactor, codeProvider, comparer);
		}

		/// <summary>
		///     Initializes a new instance of 'WordCollection'.
		/// </summary>
		/// <param name="capacity" type="int">
		///     <para>
		///         The approximate number of elements that the 'WordCollection' can initially contain. 
		///     </para>
		/// </param>
		/// <param name="loadFactor" type="float">
		///     <para>
		///         A number in the range from 0.1 through 1.0 indicating the maximum ratio of elements to buckets.
		///     </para>
		/// </param>
		/// <param name="codeProvider" type="System.Collections.IHashCodeProvider">
		///     <para>
		///         The IHashCodeProvider that supplies the hash codes for all keys in the 'WordCollection'.
		///     </para>
		/// </param>
		/// <param name="comparer" type="System.Collections.IComparer">
		///     <para>
		///         The IComparer to use to determine whether two keys are equal. 
		///     </para>
		/// </param>
		public WordCollection(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, loadFactor, codeProvider, comparer);
		}

		
		#endregion

		#region Implementation of IDictionary
		
		/// <summary>
		///     Returns an enumerator that can be used to iterate through the 'WordCollection'.
		/// </summary>
		public WordCollectionEnumerator GetEnumerator()
		{
			return new WordCollectionEnumerator(this);
		}
        
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new WordCollectionEnumerator(this);
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Removes the element with the specified key from the WordCollection.
		/// </summary>
		/// <param name="key" type="string">
		///     <para>
		///         The key of the element to remove
		///     </para>
		/// </param>
		public void Remove(string key)
		{
			innerHash.Remove (key);
		}
		void IDictionary.Remove(object key)
		{
			Remove ((string)key);
		}

		/// <summary>
		///     Determines whether the WordCollection contains an element with the specified key.
		/// </summary>
		/// <param name="key" type="string">
		///     <para>
		///         The key to locate in the WordCollection.
		///     </para>
		/// </param>
		/// <returns>
		///     true if the WordCollection contains an element with the key; otherwise, false.
		/// </returns>
		public bool Contains(string key)
		{
			return innerHash.Contains(key);
		}
		bool IDictionary.Contains(object key)
		{
			return Contains((string)key);
		}

		/// <summary>
		///     removes all elements from the WordCollection.
		/// </summary>
		public void Clear()
		{
			innerHash.Clear();		
		}

		/// <summary>
		///     adds an element with the provided key and value to the WordCollection.
		/// </summary>
		/// <param name="key" type="string">
		///     <para>
		///         The string Object to use as the key of the element to add.
		///     </para>
		/// </param>
		/// <param name="value" type="Word">
		///     <para>
		///         The Word Object to use as the value of the element to add.
		///     </para>
		/// </param>
		public void Add(string key, Word value)
		{
			innerHash.Add (key, value);
		}
		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (Word)value);
		}

		/// <summary>
		///     gets a value indicating whether the WordCollection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return innerHash.IsReadOnly;
			}
		}

		/// <summary>
		///     Gets or sets the element with the specified key.
		/// </summary>
		/// <value>
		///     <para>
		///         The key of the element to get or set.
		///     </para>
		/// </value>
		public Word this[string key]
		{
			get
			{
				return (Word) innerHash[key];
			}
			set
			{
				innerHash[key] = value;
			}
		}
		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = (Word)value;
			}
		}
        
		/// <summary>
		///     gets an ICollection containing the values in the WordCollection.
		/// </summary>
		public System.Collections.ICollection Values
		{
			get
			{
				return innerHash.Values;
			}
		}

		/// <summary>
		///     gets an ICollection containing the keys of the WordCollection.
		/// </summary>
		public System.Collections.ICollection Keys
		{
			get
			{
				return innerHash.Keys;
			}
		}

		/// <summary>
		///     gets a value indicating whether the WordCollection has a fixed size.
		/// </summary>
		public bool IsFixedSize
		{
			get
			{
				return innerHash.IsFixedSize;
			}
		}
		#endregion

		#region Implementation of ICollection

		/// <summary>
		///     copies the elements of the WordCollection to an Array, starting at a particular Array index.
		/// </summary>
		/// <param name="array" type="System.Array">
		///     <para>
		///         The one-dimensional Array that is the destination of the elements copied from WordCollection. The Array must have zero-based indexing. 
		///     </para>
		/// </param>
		/// <param name="index" type="int">
		///     <para>
		///         The zero-based index in array at which copying begins. 
		///     </para>
		/// </param>		
		public void CopyTo(System.Array array, int index)
		{
			innerHash.CopyTo (array, index);
		}

		/// <summary>
		///     Gets a value indicating whether access to the WordCollection is synchronized (thread-safe).
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return innerHash.IsSynchronized;
			}
		}

		/// <summary>
		///     Gets the number of elements contained in the WordCollection.
		/// </summary>
		public int Count
		{
			get
			{
				return innerHash.Count;
			}
		}

		/// <summary>
		///     Gets an object that can be used to synchronize access to the WordCollection.
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return innerHash.SyncRoot;
			}
		}
		#endregion

		#region Implementation of ICloneable
		
		/// <summary>
		///     Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>
		///     A new object that is a copy of this instance.
		/// </returns>
		public WordCollection Clone()
		{
			WordCollection clone = new WordCollection();
			clone.innerHash = (Hashtable) innerHash.Clone();
			
			return clone;
		}
		object ICloneable.Clone()
		{
			return Clone();
		}
		
		#endregion
		
		#region "HashTable Methods"
		
		/// <summary>
		///     Determines whether the WordCollection contains a specific key.
		/// </summary>
		/// <param name="key" type="string">
		///     <para>
		///         The key to locate in the WordCollection.
		///     </para>
		/// </param>
		/// <returns>
		///     true if the WordCollection contains an element with the specified key; otherwise, false.
		/// </returns>
		public bool ContainsKey (string key)
		{
			return innerHash.ContainsKey(key);
		}
		
		/// <summary>
		///     Determines whether the WordCollection contains a specific value.
		/// </summary>
		/// <param name="value" type="Word">
		///     <para>
		///         The value to locate in the WordCollection. The value can be a null reference (Nothing in Visual Basic).
		///     </para>
		/// </param>
		/// <returns>
		///     true if the WordCollection contains an element with the specified value; otherwise, false.
		/// </returns>
		public bool ContainsValue (Word value)
		{
			return innerHash.ContainsValue(value);
		}
		
		/// <summary>
		///     Returns a synchronized (thread-safe) wrapper for the WordCollection.
		/// </summary>
		/// <param name="nonSync" type="WordCollection">
		///     <para>
		///         The WordCollection to synchronize.
		///     </para>
		/// </param>
		public static WordCollection Synchronized(WordCollection nonSync)
		{
			WordCollection sync = new WordCollection();
			sync.innerHash = Hashtable.Synchronized(nonSync.innerHash);

			return sync;
		}
		
		#endregion

		internal Hashtable InnerHash
		{
			get
			{
				return innerHash;
			}
		}
	}
	
	/// <summary>
	///     A strongly typed enumerator for 'WordCollection'
	/// </summary>
	public class WordCollectionEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;
			
		internal WordCollectionEnumerator (WordCollection enumerable)
		{
			innerEnumerator = enumerable.InnerHash.GetEnumerator();
		}

		#region Implementation of IDictionaryEnumerator
		
		/// <summary>
		///      gets the key of the current WordCollection entry.
		/// </summary>
		public string Key
		{
			get
			{
				return (string)innerEnumerator.Key;
			}
		}
		object IDictionaryEnumerator.Key
		{
			get
			{
				return Key;
			}
		}


		/// <summary>
		///     gets the value of the current WordCollection entry.
		/// </summary>
		public Word Value
		{
			get
			{
				return (Word)innerEnumerator.Value;
			}
		}
		object IDictionaryEnumerator.Value
		{
			get
			{
				return Value;
			}
		}

		/// <summary>
		///      gets both the key and the value of the current WordCollection entry.
		/// </summary>
		public System.Collections.DictionaryEntry Entry
		{
			get
			{
				return innerEnumerator.Entry;
			}
		}

		#endregion

		#region Implementation of IEnumerator
		
		/// <summary>
		///     Sets the enumerator to the first element in the collection
		/// </summary>
		public void Reset()
		{
			innerEnumerator.Reset();
		}

		/// <summary>
		///     Advances the enumerator to the next element of the collection
		/// </summary>
		public bool MoveNext()
		{
			return innerEnumerator.MoveNext();
		}

		/// <summary>
		///     Gets the current element from the collection
		/// </summary>
		public object Current
		{
			get
			{
				return innerEnumerator.Current;
			}
		}
		#endregion
	}
}
