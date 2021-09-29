using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetData();
            var regex = new Regex(@"[\w\-\.]+@(?<domain>[\w\-]+\.[\w\-\.]+)",  RegexOptions.Compiled);

            var matches = regex.Matches(data);
            
            var counter = new Counter();
            foreach (Match match in matches)
            {
                var domain = match.Groups["domain"].Value;
                counter.Register(domain);
            }
            
            counter.Display();
        }

        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
            
        }
    }
}