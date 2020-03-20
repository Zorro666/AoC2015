using System.Collections.Generic;
using NUnit.Framework;

namespace Day23
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] TestProg = {
"inc a",
"jio a, +2",
"tpl a",
"inc a"
};

        public static IEnumerable<TestCaseData> TestProgCases => new[]
        {
            new TestCaseData(TestProg, 2, 0).SetName("Test Program a = 2 b = 0"),
        };

        [Test]
        [TestCaseSource("TestProgCases")]
        public void TestProgram(string[] program, int expectedA, int expectedB)
        {
            Program.ParseInput(program);
            Program.RunProgram(0, 0);
            Assert.That(Program.GetA(), Is.EqualTo(expectedA));
            Assert.That(Program.GetB(), Is.EqualTo(expectedB));
        }
    }
}
