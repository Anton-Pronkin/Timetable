using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimetableParser.PairParsers
{
    public static class PartParseHelper
    {
        public static IEnumerable<TResultType> GetEntries<TResultType>(string text, (string Pattern, TResultType Value)[] substitutions)
        {
            foreach (var substitution in substitutions)
            {
                var match = Regex.Match(text, substitution.Pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    yield return substitution.Value;
                }
            }
        }

        public static IEnumerable<Match> GetEntries(string text, string[] substitutions, RegexOptions options = RegexOptions.IgnoreCase)
        {
            foreach (var substitution in substitutions)
            {
                var match = Regex.Match(text, substitution, options);
                if (match.Success)
                {
                    yield return match;
                }
            }
        }

        public static string RemoveEntries(string text, params string[] substitutions)
        {
            return substitutions.Aggregate(text, (result, substitution) =>
            {
                var pattern = $@"[\s\.\:]*{substitution}[\s\.\:]*";
                return Regex.Replace(result, pattern, " ", RegexOptions.IgnoreCase);
            });
        }
    }
}