using NUnit.Framework;

namespace Day06
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("turn on 0,0 through 999,999", 1000 * 1000)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        [TestCase("turn off 499,499 through 500,500", 0)]
        [TestCase("turn on 499,499 through 500,500", 4)]
        public void HowManyLights(string line, long expected)
        {
            var instructions = new string[] { line };
            Assert.That(Program.HowManyLights(instructions, false), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("turn on 0,0 through 0,0", 1)]
        [TestCase("toggle 0,0 through 999,999", 2 * 1000 * 1000)]
        [TestCase("turn off 499,499 through 500,500", 0)]
        [TestCase("turn on 499,499 through 500,500", 4)]
        public void GetBrightness(string line, long expected)
        {
            var instructions = new string[] { line };
            Assert.That(Program.HowManyLights(instructions, true), Is.EqualTo(expected));
        }
    }
}
