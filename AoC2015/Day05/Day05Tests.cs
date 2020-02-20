﻿using NUnit.Framework;

namespace Day05
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("4,0,99", "4")]
        [TestCase("3,0,4,0,99", "1")]
        [TestCase("1,0,0,0,4,0,99", "2")]
        [TestCase("2,3,0,3,4,3,99", "6")]
        [TestCase("2,6,6,7,4,7,99,0", "9801")]
        [TestCase("1002,6,3,6,4,6,33", "99")]
        [TestCase("1,1,1,4,99,5,6,0,4,0,99", "30")]
        [TestCase("1,1,1,4,99,5,6,0,4,4,99", "2")]
        public void RunProgramPart1OutputMatches(string source, string expected)
        {
            Assert.That(Program.RunProgram(source, 1), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 1, "0")]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 8, "1")]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", 9, "0")]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 1, "1")]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 8, "0")]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", 9, "0")]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 1, "0")]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 8, "1")]
        [TestCase("3,3,1108,-1,8,3,4,3,99", 9, "0")]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 1, "1")]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 8, "0")]
        [TestCase("3,3,1107,-1,8,3,4,3,99", 9, "0")]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 0, "0")]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 1, "1")]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", 2, "1")]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 0, "0")]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 1, "1")]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", 2, "1")]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 5, "999")]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 8, "1000")]
        [TestCase("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", 9, "1001")]
        public void RunProgramPart2OutputMatches(string source, int input, string expected)
        {
            Assert.That(Program.RunProgram(source, input), Is.EqualTo(expected));
        }
    }
}