using System;

namespace Entities
{
    public class PairTime
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public PairTime(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public PairTime(DateTime start, int hour = 2) : this(start, start.AddMinutes((45 + 45 + 5) * (hour / 2)))
        {
        }

        public override string ToString()
        {
            return $"{Start:hh:mm}-{End:hh:mm}";
        }
    }
}