using System.Collections.Generic;
using NUnit.Framework;

namespace Day17
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] containers = {
"20",
"15",
"10",
"5",
"5"
        };

        public static IEnumerable<TestCaseData> CountCombinationsTests => new[]
        {
            new TestCaseData(containers, 25).SetName("Combinations 25 = 4").Returns(4)
        };

        [Test]
        [TestCaseSource("CountCombinationsTests")]
        public int CountCombinations(string[] containers, int target)
        {
            return Program.CountCombinations(containers, target);
        }

        public static IEnumerable<TestCaseData> CountCombinationsMinTests => new[]
        {
            new TestCaseData(containers, 25).SetName("CombinationsMin 25 = 3").Returns(3)
        };

        [Test]
        [TestCaseSource("CountCombinationsMinTests")]
        public int CountCombinationsMin(string[] containers, int target)
        {
            return Program.CountCombinationsMin(containers, target);
        }
    }
}
