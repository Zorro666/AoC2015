using System.Collections.Generic;
using NUnit.Framework;

namespace Day07
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] testCircuit = new string[] {
"123 -> x",
"456 -> y",
"x AND y -> d",
"x OR y -> e",
"x LSHIFT 2 -> f",
"y RSHIFT 2 -> g",
"NOT x -> h",
"NOT y -> ri"
    };

        public static IEnumerable<TestCaseData> TestCircuitTests => new[]
        {
            new TestCaseData(testCircuit, "d", 72).SetName("TestCircuit d 72"),
            new TestCaseData(testCircuit, "e", 507).SetName("TestCircuit e 507"),
            new TestCaseData(testCircuit, "f", 492).SetName("TestCircuit f 492"),
            new TestCaseData(testCircuit, "g", 114).SetName("TestCircuit g 114"),
            new TestCaseData(testCircuit, "h", 65412).SetName("TestCircuit h 65412"),
            new TestCaseData(testCircuit, "ri", 65079).SetName("TestCircuit ri 65079"),
            new TestCaseData(testCircuit, "x", 123).SetName("TestCircuit x 123"),
            new TestCaseData(testCircuit, "y", 456).SetName("TestCircuit y 456")
        };

        [Test]
        [TestCaseSource("TestCircuitTests")]
        public void TestCircuit(string[] instructions, string wire, long expected)
        {
            Program.CreateCircuit(instructions);
            Assert.That(Program.GetWire(wire), Is.EqualTo(expected));
        }
    }
}
