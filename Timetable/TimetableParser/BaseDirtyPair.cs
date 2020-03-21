namespace TimetableParser
{
    public class BaseDirtyPair
    {
        public string GroupNumber { get; }
        public string DayOfWeek { get; }
        public string Time { get; }
        public string Content { get; }

        public BaseDirtyPair(string groupNumber, string time, string dayOfWeek, string content)
        {
            GroupNumber = groupNumber;
            Time = time;
            DayOfWeek = dayOfWeek;
            Content = content;
        }
    }
}