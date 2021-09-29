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
            var regex = new Regex(@"",  RegexOptions.Compiled);

            var counter = 0;
            for (var i = 0; i < (data.Length - 13); i++)
            {
                if (data.Substring(i, 13) == "@softwire.com")
                {
                    counter += 1;
                } 
            }
            
            Console.WriteLine(counter);
        }
        
        

        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
            
        }
    }
}