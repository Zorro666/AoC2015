using System.Collections.Generic;
using NUnit.Framework;

namespace Day08
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] testLines = new string[] {
@"""""",
@"""abc""",
@"""aaa\""aaa""",
@"""\x27""",
    };

        public static IEnumerable<TestCaseData> TestCase => new[]
        {
            new TestCaseData(testLines, 23, 11).SetName("TestFile 23,11"),
        };

        [Test]
        [TestCaseSource("TestCase")]
        public void CountChars(string[] lines, int expectedFileCharacterCount, int expectedMemoryCharacterCount)
        {
            (int fileCharacterCount, int memoryCharacterCount) = Program.CountChars(lines);
            Assert.That(fileCharacterCount, Is.EqualTo(expectedFileCharacterCount));
            Assert.That(memoryCharacterCount, Is.EqualTo(expectedMemoryCharacterCount));
        }

        public static IEnumerable<TestCaseData> TestCaseEscaped => new[]
        {
            new TestCaseData(testLines, 23, 42).SetName("TestFile 23,42"),
        };

        [Test]
        [TestCaseSource("TestCaseEscaped")]
        public void CountCharsEscaped(string[] lines, int expectedFileCharacterCount, int expectedEscapedCharacterCount)
        {
            (int fileCharacterCount, int escapedCharacterCount) = Program.CountCharsEscaped(lines);
            Assert.That(fileCharacterCount, Is.EqualTo(expectedFileCharacterCount));
            Assert.That(escapedCharacterCount, Is.EqualTo(expectedEscapedCharacterCount));
        }
    }
}
