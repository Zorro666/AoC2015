using NUnit.Framework;

namespace Day12
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(@"[1,2,3]", 6)]
        [TestCase(@"{""a"":2,""b"":4}", 6)]
        [TestCase(@"[[[3]]]", 3)]
        [TestCase(@"{""a"":{""b"":4},""c"":-1}", 3)]
        [TestCase(@"{""a"":[-1,1]}", 0)]
        [TestCase(@"[-1,{""a"":1}]", 0)]
        [TestCase(@"[]", 0)]
        [TestCase(@"{}", 0)]
        [TestCase(@"[1,{""c"":""red"",""b"":2},3]", 6)]
        [TestCase(@"{""d"":""red"",""e"":[1,2,3,4],""f"":5}", 15)]
        [TestCase(@"[1,""red"",5]", 6)]
        public void SumIncludeRed(string json, int expectedSum)
        {
            var lines = new string[] { json };
            Program.ParseLines(lines, false);
            Assert.That(Program.GetSum(), Is.EqualTo(expectedSum));
        }

        [Test]
        [TestCase(@"[1,2,3]", 6)]
        [TestCase(@"{""a"":2,""b"":4}", 6)]
        [TestCase(@"[[[3]]]", 3)]
        [TestCase(@"{""a"":{""b"":4},""c"":-1}", 3)]
        [TestCase(@"{""a"":[-1,1]}", 0)]
        [TestCase(@"[-1,{""a"":1}]", 0)]
        [TestCase(@"[]", 0)]
        [TestCase(@"{}", 0)]
        [TestCase(@"[1,{""c"":""red"",""b"":2},3]", 4)]
        [TestCase(@"{""d"":""red"",""e"":[1,2,3,4],""f"":5}", 0)]
        [TestCase(@"[1,""red"",5]", 6)]
        [TestCase(@"{""b"":[""red"",5]}", 5)]
        [TestCase(@"{""c"":""violet"",""a"":8,""b"":[""red"",{""a"":37},""green"",84,""yellow"",""green"",[24,45,""blue"",""blue"",56,""yellow""],""orange""]}", 254)]
        public void SumIgnoreRed(string json, int expectedSum)
        {
            var lines = new string[] { json };
            Program.ParseLines(lines, true);
            Assert.That(Program.GetSum(), Is.EqualTo(expectedSum));
        }
    }
}
