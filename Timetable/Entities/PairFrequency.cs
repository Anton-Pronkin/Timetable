using System;

namespace Entities
{
    [Flags]
    public enum PairFrequency
    {
        Numerator1 = 0b_0001,
        Denominator1 = 0b_0010,
        Numerator2 = 0b_0100,
        Denominator2 = 0b_1000,
        EveryNumerator = Numerator1 | Numerator2,
        EveryDenominator = Denominator1 | Denominator2,
        EveryWeek = EveryNumerator | EveryDenominator
    }
}