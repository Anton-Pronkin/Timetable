using System;
using System.Text.RegularExpressions;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class PairParser
    {
        public static Pair Parse(DirtyPair pair, int firstWeek)
        {
            var content = pair.Content;

            if (IsSimplePair(content))
            {
                return ParseSimplePair(pair);
            }

            if (IsChangeTimetable(content))
            {
                throw new NotImplementedException("Парсинг пары с учетом смены расписания не поддерживается.");
            }

            return ParsePair(pair, firstWeek);
        }

        private static Pair ParsePair(DirtyPair pair, int firstWeek)
        {
            var group = GroupParser.Parse(pair.Group);
            var dayOfWeek = DayOfWeekParser.Parse(pair.DayOfWeek);
            var defaultTime = TimeRangeParser.Parse(pair.Time);

            string content = pair.Content;

            var time = RedefinedTimeParser.Parse(content, defaultTime, out content);
            var frequency = FrequencyParser.Parse(content, firstWeek, out content);
            var type = PairTypeParser.Parse(content, out content);
            var teacher = TeacherParser.Parse(content, out content);
            var room = RoomParser.Parse(content, out content);
            var subject = content.Trim(' ', '.');

            return new Pair
            {
                Group = group,
                Teacher = teacher,
                DayOfWeek = dayOfWeek,
                Room = room,
                Type = type,
                TimeRange = time,
                Subject = subject,
                Frequency = frequency,
                OriginalText = pair.Content
            };
        }

        private static bool IsSimplePair(string content)
        {
            // Пары без типа, преподавателя, аудитории ("Военная подготовка", "День НИР" и т.д.)
            return Regex.IsMatch(content, @"^[\w\s]*$");
        }

        private static Pair ParseSimplePair(DirtyPair pair)
        {
            var group = GroupParser.Parse(pair.Group);
            var dayOfWeek = DayOfWeekParser.Parse(pair.DayOfWeek);
            var time = TimeRangeParser.Parse(pair.Time);

            return new Pair
            {
                Group = group,
                DayOfWeek = dayOfWeek,
                TimeRange = time,
                Subject = pair.Content,
                OriginalText = pair.Content
            };
        }

        private static bool IsChangeTimetable(string content)
        {
            return Regex.IsMatch(content, @"\bсо см", RegexOptions.IgnoreCase);
        }
    }
}