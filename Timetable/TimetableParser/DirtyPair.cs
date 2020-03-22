using System;
using Entities;

namespace TimetableParser
{
    public class DirtyPair
    {
        public string Group { get; }
        public string DayOfWeek { get; }
        public string Time { get; }
        public string Content { get; }

        public DirtyPair(string group, string time, string dayOfWeek, string content)
        {
            Group = group;
            Time = time;
            DayOfWeek = dayOfWeek;
            Content = content;
        }
    }
}