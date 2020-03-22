using System;
using System.Collections.Generic;
using Entities;
using NUnit.Framework;
using TimetableParser.PairParsers;

namespace TimetableParser.Tests
{
    [TestFixture]
    public class GroupParserTest
    {
        [Test]
        [TestCaseSource(nameof(Parse_NegativeTestCases))]
        [TestCaseSource(nameof(Parse_PositiveTestCases))]
        public Group Parse(string group)
        {
            return GroupParser.Parse(group);
        }

        public static IEnumerable<TestCaseData> Parse_NegativeTestCases()
        {
            // Основные исключения
            yield return new TestCaseData(null)
                .SetName("Исключение при передаче null.")
                .Throws(typeof(ArgumentNullException));

            yield return new TestCaseData("")
                .SetName("Исключение при передачи пустой строки.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData(new string(' ', 1))
                .SetName("Исключение при длине строки равной 1")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData(new string(' ', 2))
                .SetName("Исключение при длине строки равной 2.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData(new string(' ', 6))
                .SetName("Исключение при длине строки равной 6.")
                .Throws(typeof(ArgumentOutOfRangeException));

            // Группа из трех смиволов
            yield return new TestCaseData("а23")
                .SetName("Исключение при длине строки равной 3 и неверном 1 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("1а3")
                .SetName("Исключение при длине строки равной 3 и неверном 2 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("12а")
                .SetName("Исключение при длине строки равной 3 и неверном 3 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            // Группа из четырех смиволов
            yield return new TestCaseData("а234")
                .SetName("Исключение при длине строки равной 4 и неверном 1 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("1а34")
                .SetName("Исключение при длине строки равной 4 и неверном 2 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("12а4")
                .SetName("Исключение при длине строки равной 4 и неверном 3 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("123а")
                .SetName("Исключение при длине строки равной 4 и неверном 4 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            // Группа из пяти смиволов
            yield return new TestCaseData("а2345")
                .SetName("Исключение при длине строки равной 5 и неверном 1 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("1а345")
                .SetName("Исключение при длине строки равной 5 и неверном 2 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("12а45")
                .SetName("Исключение при длине строки равной 5 и неверном 3 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("123а5")
                .SetName("Исключение при длине строки равной 5 и неверном 4 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));

            yield return new TestCaseData("1234а")
                .SetName("Исключение при длине строки равной 5 и неверном 5 символе.")
                .Throws(typeof(ArgumentOutOfRangeException));
        }

        public static IEnumerable<TestCaseData> Parse_PositiveTestCases()
        {
            yield return new TestCaseData("123").Returns(Group.Create(1, 2, 3));
            yield return new TestCaseData("2345").Returns(Group.Create(2, 3, 45));
            yield return new TestCaseData("234М").Returns(Group.Create(2, 3, 4, true));
            yield return new TestCaseData("234м").Returns(Group.Create(2, 3, 4, true));
            yield return new TestCaseData("3456М").Returns(Group.Create(3, 4, 56, true));
            yield return new TestCaseData("3456м").Returns(Group.Create(3, 4, 56, true));
        }
    }
}
