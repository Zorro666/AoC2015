using NUnit.Framework;

namespace Day03
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(">", 2)]
        [TestCase("^>v<", 4)]
        [TestCase("^v^v^v^v^v", 2)]
        public void HowManyHousesNoRoboSanta(string moves, long expected)
        {
            Assert.That(Program.HowManyHousesVisited(moves, false), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("^v", 3)]
        [TestCase("^>v<", 3)]
        [TestCase("^v^v^v^v^v", 11)]
        public void HowManyHousesRoboSanta(string moves, long expected)
        {
            Assert.That(Program.HowManyHousesVisited(moves, true), Is.EqualTo(expected));
        }
    }
}
