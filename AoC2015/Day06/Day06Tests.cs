using NUnit.Framework;

namespace Day06
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("turn on 0,0 through 999,999", 1000 * 1000)]
        [TestCase("toggle 0,0 through 999,0", 1000)]
        [TestCase("toggle off 499,499 through 500,500", 0)]
        [TestCase("toggle on 499,499 through 500,500", 4)]
        public void HowManyLights(string line, long expected)
        {
            var instructions = new string[] { line };
            Assert.That(Program.HowManyLights(instructions), Is.EqualTo(expected));
        }
    }
}
