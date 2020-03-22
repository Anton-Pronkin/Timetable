using System;
using System.Linq;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class PairTypeParser
    {
        private static readonly (string Pattern, PairType Type)[] Substitutions = 
        {
            ( @"\bлек\b", PairType.Lecture ),
            ( @"\bлекция\b", PairType.Lecture ),
            ( @"\bупр\b", PairType.Exercise ),
            ( @"\bлаб\b", PairType.Laboratory )
        };

        private static readonly string[] Patterns = Substitutions.Select(s => s.Pattern).ToArray();

        public static PairType Parse(string content, out string reducedText)
        {
            PairType type = GetType(content);
            reducedText = RemoveType(content);

            return type;
        }

        private static PairType GetType(string content)
        {
            var types = PartParseHelper.GetEntries(content, Substitutions).ToList();
            if (types.Count != 1)
            {
                throw new Exception("Невозможно однозначно определить тип пары.");
            }

            return types.Single();
        }

        private static string RemoveType(string content)
        {
            return PartParseHelper.RemoveEntries(content, Patterns);
        }
    }
}