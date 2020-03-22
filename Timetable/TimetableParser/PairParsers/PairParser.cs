using Entities;

namespace TimetableParser.PairParsers
{
    public static class PairParser
    {
        public static Pair Parse(DirtyPair pair)
        {
            var group = GroupParser.Parse(pair.Group);
            var dayOfWeek = DayOfWeekParser.Parse(pair.DayOfWeek);
            var time = TimeRangeParser.Parse(pair.Time);

            var content = pair.Content;
            var type = PairTypeParser.Parse(content, out content);
            var teacher = TeacherParser.Parse(content, out content);
            var room = RoomParser.Parse(content, out content);
            var subject = content;

            return new Pair
            {
                Group = group,
                Teacher = teacher,
                DayOfWeek = dayOfWeek,
                Room = room,
                Type = type,
                TimeRange = time,
                Subject = subject,
                OriginalText = pair.Content
            };
        }
    }
}