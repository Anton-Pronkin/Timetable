using System;
using System.Text.RegularExpressions;

namespace TimetableParser.PairParsers
{
    public static class DayOfWeekParser
    {
        public static DayOfWeek Parse(string dayOfWeek)
        {
            switch (Normalize(dayOfWeek))
            {
                case "понедельник":
                    return DayOfWeek.Monday;
                case "вторник":
                    return DayOfWeek.Tuesday;
                case "среда":
                    return DayOfWeek.Wednesday;
                case "четверг":
                    return DayOfWeek.Thursday;
                case "пятница":
                    return DayOfWeek.Friday;
                case "суббота":
                    return DayOfWeek.Saturday;
                case "воскресенье":
                    return DayOfWeek.Sunday;
                default:
                    throw new Exception($"Некоректный день недели \"{dayOfWeek}\".");
            }
        }

        private static string Normalize(string dayOfWeek)
        {
            return Regex.Replace(dayOfWeek, @"\W", String.Empty, RegexOptions.IgnoreCase).ToLower();
        }
    }
}