using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var noTld = args.Contains("--notld");
            var caseInsensitive = args.Contains("--caseInsensitive");
            
            var data = GetData();
            var regex = new Regex(
                @"(?:[a-z0-9]+[\.\-_]?[a-z0-9]+)+@(?<domain>[a-z0-9\-]+)(?<tld>(\.[a-z]{2,})+)", 
                RegexOptions.Compiled | RegexOptions.IgnoreCase
                );

            var matches = regex.Matches(data);
            
            var counter = new Counter();
            foreach (Match match in matches)
            {
                var domain = match.Groups["domain"].Value;
                
                if (!noTld)
                {
                    var tld = match.Groups["tld"].Value;
                    domain = $"{domain}{tld}";
                }

                if (caseInsensitive)
                    domain = domain.ToLower();
                
                counter.Register(domain);
            }
            
            counter.Display();
            Console.WriteLine("");
            counter.DisplayAppearingDomains(89);
        }

        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
            
        }
    }
}