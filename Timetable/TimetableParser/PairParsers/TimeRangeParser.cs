using System;
using System.Text.RegularExpressions;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class TimeRangeParser
    {
        private static string Pattern = @"(\d{1,2})(\d{2})-(\d{1,2})(\d{2})";

        public static PairTime Parse(string time)
        {
            var match = Regex.Match(time, Pattern);
            if (!match.Success)
            {
                throw new Exception($"Неверный диапазон времени пары \"{time}\".");
            }

            return ParseTime(match);
        }

        private static PairTime ParseTime(Match match)
        {
            var startHour = match.Groups[1];
            var startMinutes = match.Groups[2];
            var endHour = match.Groups[3];
            var endMinutes = match.Groups[4];

            var timeFormat = "{0}:{1}";
            var startTime = DateTime.Parse(string.Format(timeFormat, startHour, startMinutes));
            var endTime = DateTime.Parse(string.Format(timeFormat, endHour, endMinutes));

            return new PairTime(startTime, endTime);
        }
    }
}