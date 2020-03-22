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

        public override string ToString()
        {
            return $"{Start:hh:mm}-{End:hh:mm}";
        }
    }
}