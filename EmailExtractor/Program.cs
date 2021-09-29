using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EmailExtractor
{
    class Counter
    {
        private readonly Dictionary<string, int> _values;
        
        public Counter()
        {
            _values = new Dictionary<string, int>();
        }

        public void Register(string value)
        {
            if (_values.ContainsKey(value))
                _values[value] += 1;
            else
                _values[value] = 1;
        }

        public int GetCount(string value)
        {
            return _values.ContainsKey(value) ? _values[value] : 0;
        }

        public IEnumerable<KeyValuePair<string, int>> GetTopN(int n)
        {
            var i = 0;
            var processed = _values
                .OrderBy(item => item.Value)
                .Reverse()
                .TakeWhile(_ => i++ < n);

            return processed;
        }

        public void Display(string keyColumnName = "Key", string countColumnName = "Count")
        {
            var maxKeyPad = _values
                .OrderBy(item => item.Key.Length)
                .Reverse()
                .First()
                .Key
                .Length
                .ToString();

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
                
                tableLines.Add(Tuple.Create(index.ToString(), key, value.ToString()));
                
                index++;
            }

            const string rowNumberTitle = "Row Number";
            if (rowNumberTitle.Length > maxIndexPad)
                maxIndexPad = rowNumberTitle.Length;
            var lineFormat = $"| {{0,-{maxIndexPad}}} | {{0,-{maxKeyPad}}} | {{0,-{maxCountPad}}} |";

            tableLines.Add(Tuple.Create(rowNumberTitle, keyColumnName, countColumnName));            

            foreach (var (rowNumber, key, value) in tableLines)
                Console.WriteLine(lineFormat, rowNumber, key, value);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}