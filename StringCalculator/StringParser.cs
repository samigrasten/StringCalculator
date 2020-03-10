using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    internal class StringParser
    {
        public StringParser(string numbers)
        {
            var match = _delimiterPatterns
                .Select(pattern => new Regex(pattern).Match(numbers))
                .FirstOrDefault(match => match.Success);

            var delimiters = ResolveDelimiters(match);
            Numbers = numbers.Substring(match?.Value.Length ?? 0)
                .Split(delimiters.ToArray(), new StringSplitOptions())
                .Where(number => !string.IsNullOrEmpty(number))
                .Select(int.Parse)
                .ToArray();
        }

        public int[] Numbers { get; }

        private string[] ResolveDelimiters(Match match)
        {
            var delimiters = 
                match?.Success ?? false
                ? match.Value
                    .Substring(2, match.Value.Length - 3)
                    .Replace("[", string.Empty)
                    .Split(']')
                    .ToList()
                : new List<string> { "," };
            delimiters.Add("\n");
            return delimiters.ToArray();
        }

        private readonly string[] _delimiterPatterns = new[]
        {
            "//\\[.*\\]\\n",
            "//.\\n",
        };
    }
}