using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Entities;

namespace TimetableParser.PairParsers
{
    public static class FrequencyParser
    {
        private const string DefinedDatePattern = @"(\d{1,2}[\.:]\d{2}[ ,]){2,}";

        private const string NumeratorPattern = @"числ\b";
        private const string DenominatorPattern = @"знам\b";

        public static PairFrequency Parse(string content, int firstWeek, out string reducedText)
        {
            if (IsDefinedData(content))
            {
                return CalculateDefinedDate(content, firstWeek, out reducedText);
            }

            return ParseFrequency(content, out reducedText);
        }

        private static PairFrequency ParseFrequency(string content, out string reducedText)
        {
            bool isNumerator = PartParseHelper.CheckEntry(content, NumeratorPattern);
            bool isDenominator = PartParseHelper.CheckEntry(content, DenominatorPattern);

            reducedText = PartParseHelper.RemoveEntries(content, NumeratorPattern, DenominatorPattern);

            if (isNumerator && isDenominator)
            {
                throw new NotImplementedException("Парсинг пары с числителем и знаменателем не поддерживается.");
            }

            if (isNumerator)
            {
                return PairFrequency.EveryNumerator;
            }

            if (isDenominator)
            {
                return PairFrequency.EveryDenominator;
            }

            return PairFrequency.EveryWeek;
        }

        private static PairFrequency CalculateDefinedDate(string content, int firstWeek, out string reducedText)
        {
            var days = PartParseHelper.GetEntries(content, new[] {DefinedDatePattern}).ToList();
            if (!days.Any())
            {
                throw new Exception("Не найдено ни одной определенной даты.");
            }

            reducedText = PartParseHelper.RemoveEntries(content, DefinedDatePattern, NumeratorPattern, DenominatorPattern);

            var firstDate = days.First().Groups[0].Value;
            var weekNumber = (GetWeekNumber(firstDate) - firstWeek) / 4;

            switch (weekNumber)
            {
                case 0:
                    return PairFrequency.Numerator1;
                case 1:
                    return PairFrequency.Denominator1;
                case 2:
                    return PairFrequency.Numerator2;
                default:
                    return PairFrequency.Denominator2;
            }
        }

        private static int GetWeekNumber(string date)
        {
            var dateParts = Regex.Split(date, @"\D");
            if (dateParts.Length < 2)
            {
                throw new Exception("Не найдено определенной даты.");
            }

            var day = int.Parse(dateParts[0]);
            var month = int.Parse(dateParts[1]);

            var pairDate = new DateTime(DateTime.Now.Year, month, day);
            return (pairDate.DayOfYear - 1) / 7;
        }

        private static bool IsDefinedData(string content)
        {
            return PartParseHelper.CheckEntry(content, DefinedDatePattern);
        }
    }
}