using NUnit.Framework;

namespace Day11
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("hijklmmn", false)]
        [TestCase("abbceffg", false)]
        [TestCase("abbcegjk", false)]
        [TestCase("abcdffaa", true)]
        [TestCase("ghjaabcc", true)]
        public void IsValidPassword(string password, bool expectedValid)
        {
            Assert.That(Program.IsValidPassword(password.ToCharArray()), Is.EqualTo(expectedValid));
        }

        [Test]
        [TestCase("abcdefgh", "abcdffaa")]
        [TestCase("ghijklmn", "ghjaabcc")]
        public void NextValidPassword(string password, string expectedPassword)
        {
            Assert.That(Program.NextValidPassword(password), Is.EqualTo(expectedPassword));
        }
    }
}
