using System;

namespace Entities
{
    public class Pair
    {
        public string Subject { get; set; }
        public Teacher Teacher { get; set; }
        public Group Group { get; set; }
        public PairType Type { get; set; }
        public Room Room { get; set; }
        public PairTime TimeRange { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public PairFrequency Frequency { get; set; }

        public string OriginalText { get; set; }

        public override string ToString()
        {
            return $"Original:  {OriginalText}\n" +
                   $"Subject:   {Subject}\n" +
                   $"Teacher:   {Teacher}\n" +
                   $"Group:     {Group}\n" +
                   $"Type:      {Type}\n" +
                   $"Room:      {Room}\n" +
                   $"Time:      {TimeRange}\n" +
                   $"Frequency: {Frequency}\n" +
                   $"DayOfWeek: {DayOfWeek}\n";
        }
    }
}
