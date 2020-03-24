using NUnit.Framework;

namespace Day25
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 1, 4)]
        [TestCase(4, 1, 7)]
        [TestCase(5, 1, 11)]
        [TestCase(6, 1, 16)]
        [TestCase(1, 2, 3)]
        [TestCase(2, 2, 5)]
        [TestCase(3, 2, 8)]
        [TestCase(4, 2, 12)]
        [TestCase(5, 2, 17)]
        [TestCase(1, 3, 6)]
        [TestCase(2, 3, 9)]
        [TestCase(3, 3, 13)]
        [TestCase(4, 3, 18)]
        [TestCase(1, 4, 10)]
        [TestCase(2, 4, 14)]
        [TestCase(3, 4, 19)]
        [TestCase(1, 5, 15)]
        [TestCase(2, 5, 20)]
        [TestCase(1, 6, 21)]
        public void TriangleNumberSimple(int row, int column, long expectedNumber)
        {
            Assert.That(Program.GetTriangleNumber(row, column), Is.EqualTo(expectedNumber));
        }

        [Test]
        [TestCase(1, 1, 20151125)]
        [TestCase(2, 1, 31916031)]
        [TestCase(3, 1, 16080970)]
        [TestCase(4, 1, 24592653)]
        [TestCase(5, 1, 77061)]
        [TestCase(6, 1, 33071741)]
        [TestCase(1, 2, 18749137)]
        [TestCase(2, 2, 21629792)]
        [TestCase(3, 2, 8057251)]
        [TestCase(4, 2, 32451966)]
        [TestCase(5, 2, 17552253)]
        [TestCase(6, 2, 6796745)]
        [TestCase(1, 3, 17289845)]
        [TestCase(2, 3, 16929656)]
        [TestCase(3, 3, 1601130)]
        [TestCase(4, 3, 21345942)]
        [TestCase(5, 3, 28094349)]
        [TestCase(6, 3, 25397450)]
        [TestCase(1, 4, 30943339)]
        [TestCase(2, 4, 7726640)]
        [TestCase(3, 4, 7981243)]
        [TestCase(4, 4, 9380097)]
        [TestCase(5, 4, 6899651)]
        [TestCase(6, 4, 24659492)]
        [TestCase(1, 5, 10071777)]
        [TestCase(2, 5, 15514188)]
        [TestCase(3, 5, 11661866)]
        [TestCase(4, 5, 10600672)]
        [TestCase(5, 5, 9250759)]
        [TestCase(6, 5, 1534922)]
        [TestCase(1, 6, 33511524)]
        [TestCase(2, 6, 4041754)]
        [TestCase(3, 6, 16474243)]
        [TestCase(4, 6, 31527494)]
        [TestCase(5, 6, 31663883)]
        [TestCase(6, 6, 27995004)]
        public void GetCode(int row, int column, long expectedNumber)
        {
            Assert.That(Program.GetCode(row, column), Is.EqualTo(expectedNumber));
        }
    }
}
