using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class TeacherParser
    {
        private static readonly (string Pattern, TeacherRank Rank)[] TeacherRankSubstitutions =
        {
            ( @"\bдоц\b", TeacherRank.Docent ),
            ( @"\bст?\.?п\.?\b", TeacherRank.SeniorTeacher ),
            ( @"\bпроф\b", TeacherRank.Professor ),
            ( @"\bд\.", TeacherRank.Docent ),
            ( @"\bасс\b", TeacherRank.Assistant )
        };

        private const string TeacherNamePattern = @"(?<surname>[А-Я][а-я]+)\s+(?<initials>[А-Я]\.?[А-Я]\.?\b)";
        private static readonly string TeacherPatternFormat = $@"(?<rank>{{0}})[\\.\s]*{TeacherNamePattern}";

        private static readonly string[] Patterns = TeacherRankSubstitutions.Select(s => string.Format(TeacherPatternFormat, s.Pattern)).ToArray();

        public static Teacher Parse(string content, out string reducedText)
        {
            Teacher teacher = ParseTeacher(content);
            reducedText = RemoveTeacher(content);

            return teacher;
        }

        private static Teacher ParseTeacher(string content)
        {
            var teacher = PartParseHelper.GetEntries(content, Patterns, RegexOptions.None).Select(ParseTeacher).ToList();
            if (teacher.Count != 1)
            {
                throw new Exception("Невозможно однозначно определить преподавателя.");
            }

            return teacher.Single();
        }

        private static Teacher ParseTeacher(Match match)
        {
            var rankText = match.Groups["rank"].Value;
            var rank = PartParseHelper.GetEntries(rankText, TeacherRankSubstitutions).Single();

            var surname = match.Groups["surname"].Value;
            var initials = FormatInitial(match.Groups["initials"].Value);

            return new Teacher(surname, initials, rank);
        }

        private static string FormatInitial(string value)
        {
            string initials = Regex.Replace(value, @"\W", string.Empty);
            if (initials.Length == 1)
            {
                return initials;
            }

            if (initials.Length == 2)
            {
                const string initialFormat = "{0}.{1}.";
                return string.Format(initialFormat, initials[0], initials[1]);
            }

            throw new Exception("Ошибка форматирования инициалов преподавателя.");
        }

        private static string RemoveTeacher(string content)
        {
            return PartParseHelper.RemoveEntries(content, Patterns);
        }
    }
}