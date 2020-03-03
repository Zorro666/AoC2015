using System;
using System.Collections;
using NUnit.Framework;

namespace Day14
{
    [TestFixture]
    public class Tests
    {
        static readonly string[] inputA = {
"10 ORE => 10 A",
"1 ORE => 1 B",
"7 A, 1 B => 1 C",
"7 A, 1 C => 1 D",
"7 A, 1 D => 1 E",
"7 A, 1 E => 1 FUEL"
        };
        public static IEnumerable ParseInputCases => new[]
        {
            new TestCaseData(inputA, "A", "ORE").SetName("ParseInput.A A = 1 ORE").Returns(1.0),
        };

        [Test]
        [TestCaseSource("ParseInputCases")]
        public double ParseInput(string[] lines, string compoundOutput, string compoundInput)
        {
            //Program.ParseInput(lines);
            //return Program.CompoundCost(compoundOutput, compoundInput);
            return -666.0;
        }
    }
}
