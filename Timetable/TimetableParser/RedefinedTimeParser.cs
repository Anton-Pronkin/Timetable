using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entities;
using TimetableParser.PairParsers;

namespace TimetableParser
{
    public static class RedefinedTimeParser
    {
        private static readonly string[] RedefinedTimePatterns = { @"^\s*\d{3,4}", @"^\s*\d{1,2}[\.\:]\d\d" };
        private static readonly string LongPairPattern = @"4\s*-?\s*[хx]\s*час[а-я]*\s*";

        public static PairTime Parse(string content, PairTime defaultTime, out string reducedText)
        {
            PairTime time = ParseTime(content) ?? defaultTime;
            reducedText = RemoveTime(content);

            return ParseLongPair(content, ref reducedText, time); ;
        }

        private static PairTime ParseTime(string content)
        {
            var time = PartParseHelper.GetEntries(content, RedefinedTimePatterns).ToList();
            if (time.Count == 0)
            {
                return null;
            }

            if (time.Count != 1)
            {
                throw new Exception("Невозможно однозначно определить время.");
            }

            var timeParts = Regex.Split(time.Single().Value, @"\D");

            var parsedTime = DateTime.Parse($"{timeParts[0]}:{timeParts[1]}");
            return new PairTime(parsedTime);
        }

        private static PairTime ParseLongPair(string content, ref string reducedText, PairTime time)
        {
            bool hasLongPair = PartParseHelper.CheckEntry(content, LongPairPattern);
            if (!hasLongPair)
            {
                return time;
            }

            reducedText = PartParseHelper.RemoveEntries(reducedText, LongPairPattern);
            return new PairTime(time.Start, 4);
        }

        private static string RemoveTime(string content)
        {
            return PartParseHelper.RemoveEntries(content, RedefinedTimePatterns);
        }
    }
}