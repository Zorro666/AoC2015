using NUnit.Framework;

namespace Day10
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("1", "11")]
        [TestCase("11", "21")]
        [TestCase("21", "1211")]
        [TestCase("1211", "111221")]
        [TestCase("111221", "312211")]
        public void LookSeeTests(string source, string expectedReply)
        {
            Assert.That(Program.LookSee(source), Is.EqualTo(expectedReply));
        }
    }
}
