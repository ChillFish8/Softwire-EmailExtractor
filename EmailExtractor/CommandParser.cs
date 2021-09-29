using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EmailExtractor
{
    public class CommandParser
    {
        private readonly Regex _regex;
        private readonly Dictionary<string, string?> _values;

        public CommandParser()
        {
            _regex = new Regex(@"(?<flag>--[\w\-]+)(?: (?<value>(?:[\w\-]+).|(?:"".+"")))?", RegexOptions.Compiled);
            _values = new Dictionary<string, string?>();
        }

        public bool Exists(string flag)
        {
            return _values.ContainsKey(flag);
        }

        public string? GetFlagValue(string flag)
        {
            return Exists(flag) ? _values[flag] : null;
        }

        public void ParseArgs(string[] args)
        {

            string? currentFlag = null;
            var expectingFlag = true;
            foreach (var match in args)
            {
                if (!match.StartsWith("--") & expectingFlag)
                    continue;
                
                if (match.StartsWith("--"))
                {
                    if (currentFlag is not null)
                        _values[currentFlag] = null;

                    currentFlag = match;
                    expectingFlag = false;
                    continue;
                }
                
                if (currentFlag is null) 
                    continue;

                expectingFlag = true;

                _values[currentFlag] = match;
            }
        }
    }
}