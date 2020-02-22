using NUnit.Framework;

namespace Day02
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("2x3x4", 58)]
        [TestCase("1x1x10", 43)]
        public void ComputeWrappingPaper(string line, long expected)
        {
            var lines = new string[] { line };
            Program.ParseInput(lines);
            Assert.That(Program.ComputeWrappingPaper(), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("2x3x4", 34)]
        [TestCase("1x1x10", 14)]
        public void ComputeRibbon(string line, long expected)
        {
            var lines = new string[] { line };
            Program.ParseInput(lines);
            Assert.That(Program.ComputeRibbon(), Is.EqualTo(expected));
        }
    }
}
