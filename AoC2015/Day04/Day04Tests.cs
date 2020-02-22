using NUnit.Framework;

namespace Day04
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("abcdef", 609043)]
        [TestCase("pqrstuv", 1048970)]
        public void FindFiveZeroHash(string secretKey, long expected)
        {
            Assert.That(Program.FindFiveZeroHash(secretKey), Is.EqualTo(expected));
        }
    }
}
