using System;
using System.Text.RegularExpressions;
using Group = Entities.Group;

namespace TimetableParser.PairParsers
{
    public static class GroupParser
    {
        public static Group Parse(string group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            return ParseGroup(group);
        }

        private static Group ParseGroup(string group)
        {
            var patterns = new []
            {
                @"^(\d)(\d)(\d\d)([мМmM])$",
                @"^(\d)(\d)(\d)([мМmM])$",
                @"^(\d)(\d)(\d\d)$",
                @"^(\d)(\d)(\d)$",
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(@group, pattern);
                if (match.Success)
                {
                    return ParseMatch(match);
                }
            }
            
            throw new ArgumentOutOfRangeException(nameof(group));
        }

        private static Group ParseMatch(Match match)
        {
            var year = ParseYear(match);
            var faculty = ParseFaculty(match);
            var specialty = ParseSpecialty(match);
            var isMaster = ParseMaster(match);

            return Group.Create(year, faculty, specialty, isMaster);
        }

        private static int ParseYear(Match match)
        {
            const int yearIndex = 1;
            return ParseIntGroupPart(match, yearIndex);
        }

        private static int ParseFaculty(Match match)
        {
            const int facultyIndex = 2;
            return ParseIntGroupPart(match, facultyIndex);
        }

        private static int ParseSpecialty(Match match)
        {
            const int specialtyIndex = 3;
            return ParseIntGroupPart(match, specialtyIndex);
        }

        private static bool ParseMaster(Match match)
        {
            const int masterIndex = 4;
            return match.Groups[masterIndex].Success;
        }

        private static int ParseIntGroupPart(Match match, int index)
        {
            if (match.Groups.Count >= index)
            {
                return int.Parse(match.Groups[index].Value);
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }
}