using System.Collections.Generic;
using NUnit.Framework;

namespace Day19
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] testInputA = {
"H => HO",
"H => OH",
"O => HH",
"",
"HOH"
        };

        private static readonly string[] testInputB = {
"H => HO",
"H => OH",
"O => HH",
"",
"HOHOHO"
        };

        private static readonly string[] testInputC = {
"e => H",
"e => O",
"H => HO",
"H => OH",
"O => HH",
"",
"HOH"
        };

        private static readonly string[] testInputD = {
"e => H",
"e => O",
"H => HO",
"H => OH",
"O => HH",
"",
"HOHOHO"
        };

        public static IEnumerable<TestCaseData> UniqueMoleculesCases => new[]
        {
            new TestCaseData(testInputA, 4).SetName("UniqueMolecules HOH 4"),
            new TestCaseData(testInputB, 7).SetName("UniqueMolecules HOHOHO 7"),
        };

        [Test]
        [TestCaseSource("UniqueMoleculesCases")]
        public void UniqueMolecules(string[] input, int expectedCount)
        {
            Program.ParseInput(input);
            Assert.That(Program.UniqueMolecules(), Is.EqualTo(expectedCount));
        }

        public static IEnumerable<TestCaseData> GenerateCompoundCases => new[]
        {
            new TestCaseData(testInputC, 3).SetName("GenerateCompound HOH 3"),
            new TestCaseData(testInputD, 6).SetName("GenerateCompound HOHOHO 6"),
        };

        [Test]
        [TestCaseSource("GenerateCompoundCases")]
        public void GenerateCompound(string[] input, int expectedCount)
        {
            Program.ParseInput(input);
            Assert.That(Program.GenerateCompound(), Is.EqualTo(expectedCount));
        }
    }
}
