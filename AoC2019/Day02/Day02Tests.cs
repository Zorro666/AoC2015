﻿using NUnit.Framework;

namespace Day02
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void ComputeFuelMatchesExamples(string source, string expected)
        {
            Assert.That(Program.RunProgram(source, -1, -1), Is.EqualTo(expected));
        }
    }
}
