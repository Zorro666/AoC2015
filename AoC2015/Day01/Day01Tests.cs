using NUnit.Framework;

namespace Day01
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("()", 0)]
        [TestCase("(())", 0)]
        [TestCase("()()", 0)]
        [TestCase("(((", 3)]
        [TestCase("(()(()(", 3)]
        [TestCase("))(((((", 3)]
        [TestCase("())", -1)]
        [TestCase("))(", -1)]
        [TestCase(")))", -3)]
        [TestCase(")())())", -3)]
        [TestCase("", 0)]
        [TestCase("()(()))))(((())))()", -3)]
        [TestCase("Jake", 0)]
        public void WhichFloor(string directions, int expected)
        {
            Assert.That(Program.WhichFloor(directions), Is.EqualTo(expected));
        }

        [TestCase(")", -1, 1)]
        [TestCase("()())", -1, 5)]
        [TestCase("()())", +1, 1)]
        [TestCase("()())", 0, 2)]
        public void WhichIndex(string directions, int targetFloor, int expected)
        {
            Assert.That(Program.WhichIndex(directions, targetFloor), Is.EqualTo(expected));
        }
    }
}
