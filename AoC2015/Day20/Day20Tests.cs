using NUnit.Framework;

namespace Day20
{
    [TestFixture]
    class Tests
    {
        [Test]
        [TestCase(1, 10)]
        [TestCase(2, 30)]
        [TestCase(3, 40)]
        [TestCase(4, 70)]
        [TestCase(5, 60)]
        [TestCase(6, 120)]
        [TestCase(7, 80)]
        [TestCase(8, 150)]
        [TestCase(9, 130)]
        public void PresentsDelivered(int house, int expectedCount)
        {
            Assert.That(Program.PresentsDelivered(house), Is.EqualTo(expectedCount));
        }
    }
}

