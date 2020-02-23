using NUnit.Framework;

namespace Day05
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("ugknbfddgicrmopn", true)]
        [TestCase("aaa", true)]
        [TestCase("ugknbfddgicrmopnab", false)]
        [TestCase("ugkncdbfddgicrmopn", false)]
        [TestCase("ugkpqnbfddgicrmopn", false)]
        [TestCase("ugkpqnbfddgicrmopxyn", false)]
        [TestCase("jchzalrnumimnmhp", false)]
        [TestCase("haegwjzuvuyypxyu", false)]
        [TestCase("dvszwmarrgswjxmb", false)]
        public void IsStringNice(string source, bool expected)
        {
            Assert.That(Program.IsNice(source), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("qjhvhtzxzqqjkmpb", true)]
        [TestCase("xxyxx", true)]
        [TestCase("abcdefeghiab", true)]
        [TestCase("aaaa", true)]
        [TestCase("aaa", false)]
        [TestCase("xyx", false)]
        [TestCase("aabcdefghifg", false)]
        [TestCase("uurcxstgmygtbstg", false)]
        [TestCase("ieodomkazucvgmuy", false)]
        public void IsStringNicePart2(string source, bool expected)
        {
            Assert.That(Program.IsNicePart2(source), Is.EqualTo(expected));
        }
    }
}
