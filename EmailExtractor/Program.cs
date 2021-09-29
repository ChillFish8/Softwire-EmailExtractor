using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var data = GetData();
            var regex = new Regex(@"\w+@softwire\.com",  RegexOptions.Compiled);

            var matches = regex.Matches(data);
            Console.WriteLine(matches.Count);
        }
        
        

        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
            
        }
    }
}