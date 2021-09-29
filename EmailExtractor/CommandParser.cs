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
            _regex = new Regex(@"(?<flag>--[\w\-]+)(?: (?<value>[\w\-]+))?", RegexOptions.Compiled);
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
            var cliArgs = string.Concat(args);
            var matches = _regex.Matches(cliArgs);
            foreach (Match match in matches)
            {
                var flag = match.Groups["flag"].Value;

                var valueGroup = match.Groups["value"];
                var value = valueGroup.Success ? valueGroup.Value : null;

                _values[flag] = value;
            }
        }
    }
}