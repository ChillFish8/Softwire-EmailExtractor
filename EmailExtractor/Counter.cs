using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailExtractor
{
    /// <summary>
    /// A dictionary-like collection that counts how many times a given key has been registered.
    ///
    /// This also provides interfaces to get the top N amount of keys, get the count for a given key or
    /// display the results as a table.
    /// </summary>
    class Counter
    {
        private readonly Dictionary<string, int> _values;
        
        public Counter()
        {
            _values = new Dictionary<string, int>();
        }

        /// <summary>
        /// Adds a new key to the collection.
        /// <br/><br/>
        /// If it exists already it's count will be incremented by 1 otherwise will
        /// be set as 1.
        /// </summary>
        /// <param name="key">The key to register in the collection.</param>
        public void Register(string key)
        {
            if (_values.ContainsKey(key))
                _values[key] += 1;
            else
                _values[key] = 1;
        }

        /// <summary>
        /// Gets the given amount of times the key appears in the collection.
        /// </summary>
        /// <param name="key">The target key.</param>
        /// <returns>The number of times the key appears in the collection.</returns>
        public int GetCount(string key)
        {
            return _values.ContainsKey(key) ? _values[key] : 0;
        }

        /// <summary>
        /// Gets the top n highest count items in the collection.
        ///
        /// </summary>
        /// <param name="n">The number of items to return.</param>
        /// <returns>A enumerable containing the key, count pairs.</returns>
        public IEnumerable<KeyValuePair<string, int>> GetTopN(int n)
        {
            var i = 0;
            var processed = _values
                .OrderBy(item => item.Value)
                .Reverse()
                .TakeWhile(_ => i++ < n);

            return processed;
        }

        /// <summary>
        /// Displays the collection as a formatted table.
        /// </summary>
        /// <param name="keyColumnName">A optional key column identifier to put at the header of the table.</param>
        /// <param name="countColumnName">A optional count column identifier to put at the header of the table</param>
        public void Display(string keyColumnName = "Key", string countColumnName = "Count")
        {
            var maxKeyPad = 0;
            var maxCountPad = 0;
            var maxIndexPad = _values
                .Count
                .ToString()
                .Length;

            var tableLines = new List<Tuple<string, string, string>>();
            var index = 1;
            foreach (var (key, value) in GetTopN(_values.Count))
            {
                var countValue = value.ToString();
                if (countValue.Length > maxCountPad)
                    maxCountPad = countValue.Length;

                if (key.Length > maxKeyPad)
                    maxKeyPad = key.Length;
                
                tableLines.Add(Tuple.Create(index.ToString(), key, value.ToString()));
                
                index++;
            }

            const string rowNumberTitle = "Row Number";
            
            if (rowNumberTitle.Length > maxIndexPad)
                maxIndexPad = rowNumberTitle.Length;
            if (keyColumnName.Length > maxKeyPad)
                maxKeyPad = keyColumnName.Length;
            if (countColumnName.Length > maxCountPad)
                maxCountPad = countColumnName.Length;
            
            var lineFormat = $"| {{0,-{maxIndexPad}}} | {{1,-{maxKeyPad}}} | {{2,-{maxCountPad}}} |";

            tableLines.Insert(0, Tuple.Create(rowNumberTitle, keyColumnName, countColumnName));            
            
            foreach (var (rowNumber, key, value) in tableLines)
                Console.WriteLine(lineFormat, rowNumber, key, value);
        }
    }
}