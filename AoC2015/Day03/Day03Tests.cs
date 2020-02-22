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
        public void HowManyHouses(string moves, long expected)
        {
            Assert.That(Program.HowManyHousesVisited(moves), Is.EqualTo(expected));
        }
    }
}
