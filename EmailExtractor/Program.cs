using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = new CommandParser();
            commands.ParseArgs(args);
            
            var noTld = commands.Exists("--notld");
            var caseInsensitive = commands.Exists("--caseInsensitive");
            var httpUrl = commands.GetFlagValue("--url");
            
            var data = httpUrl is null ? GetData() : GetUrlData(httpUrl);
            
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
            
            counter.Display(keyColumnName: "Domain");
            Console.WriteLine("");
            counter.DisplayAppearingDomains(89, "Domain");
        }

        static string GetUrlData(string url)
        {
            var client = new WebClient();
            return client.DownloadString(url);
        }
        
        static string GetData()
        {
            return File.ReadAllText("./data/sample.txt");
        }
    }
}