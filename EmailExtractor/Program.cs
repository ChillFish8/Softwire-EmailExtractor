using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailExtractor
{
    class Counter
    {
        private Dictionary<string, int> values;
        
        public Counter()
        {
            values = new Dictionary<string, int>();
        }

        public void Register(string value)
        {
            if (values.ContainsKey(value))
                values[value] += 1;
            else
                values[value] = 1;
        }

        public int GetCount(string value)
        {
            return values.ContainsKey(value) ? values[value] : 0;
        }

        public int GetTopN(int n)
        {
            var i = 0;
            var processed = values
                .OrderBy(item => item.Value)
                .TakeWhile(v => i < n;i++);
                .ToList();
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