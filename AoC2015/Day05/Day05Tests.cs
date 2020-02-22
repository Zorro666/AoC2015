using NUnit.Framework;

namespace Day05
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("1,1,1,4,99,5,6,0,4,4,99", "2")]
        public void RunProgramPart1OutputMatches(string source, string expected)
        {
            //Assert.That(Program.RunProgram(source, 1), Is.EqualTo(expected));
        }
    }
}
