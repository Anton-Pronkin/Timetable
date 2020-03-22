using System;
using System.Linq;
using System.Text.RegularExpressions;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class RoomParser
    {
        private static readonly string Pattern = @"([aа]?\s*\.?\s*\d+(би)?[0-9\- \/]*к?[0-9\- \/\.]*(би)?[0-9\- \/]*[а-я]?[ \.]*к?[0-9\- \/\.]*){2,}";

        public static Room Parse(string content, out string reducedText)
        {
            Room room = GetRoom(content);
            reducedText = RemoveRoom(content);

            return room;
        }

        private static Room GetRoom(string content)
        {
            var rooms = PartParseHelper.GetEntries(content, new [] { Pattern }).Select(ParseRoom).ToList();
            if (rooms.Count != 1)
            {
                throw new Exception("Невозможно однозначно определить аудиторию.");
            }

            return rooms.Single();
        }

        private static Room ParseRoom(Match match)
        {
            var text = Regex.Replace(match.Value, @"\W", string.Empty);
                
            var number = Regex.Match(text, @"\d+\-?(\d|[а-д])?").Value;
            var isLab = Regex.IsMatch(text, @"к2", RegexOptions.IgnoreCase);
            var isBi = Regex.IsMatch(text, @"би", RegexOptions.IgnoreCase);

            return new Room(number, isLab, isBi); // TODO
        }

        private static string RemoveRoom(string content)
        {
            return PartParseHelper.RemoveEntries(content, Pattern);
        }
    }
}