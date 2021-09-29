using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var withTld = args.Length > 0 & args[0] != "--notld";
            var data = GetData();
            var regex = new Regex(
                @"(?<email>(?<name>(?:[a-z0-9]+[\.\-_]?[a-z0-9]+)+)@(?<domain>[a-z0-9\-]+)(?<tld>(\.[a-z]{2,})+))", 
                RegexOptions.Compiled | RegexOptions.IgnoreCase
                );

            var matches = regex.Matches(data);
            
            var counter = new Counter();
            foreach (Match match in matches)
            {
                var domain = match.Groups["domain"].Value;
                
                if (withTld)
                {
                    var tld = match.Groups["tld"].Value;
                    domain = $"{domain}{tld}";
                }
                
                counter.Register(domain);
            }
            
            counter.Display(10);
            Console.WriteLine("");
            counter.DisplayAppearingDomains(89);
        }

        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
            
        }
    }
}