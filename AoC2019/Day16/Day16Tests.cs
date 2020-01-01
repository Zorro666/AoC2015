﻿using NUnit.Framework;

namespace Day16
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("12345678", 1, ExpectedResult = "48226158")]
        [TestCase("12345678", 2, ExpectedResult = "34040438")]
        [TestCase("12345678", 3, ExpectedResult = "03415518")]
        [TestCase("12345678", 4, ExpectedResult = "01029498")]
        [TestCase("48226158", 1, ExpectedResult = "34040438")]
        [TestCase("48226158", 2, ExpectedResult = "03415518")]
        [TestCase("48226158", 3, ExpectedResult = "01029498")]
        [TestCase("34040438", 1, ExpectedResult = "03415518")]
        [TestCase("34040438", 2, ExpectedResult = "01029498")]
        [TestCase("03415518", 1, ExpectedResult = "01029498")]
        [TestCase("80871224585914546619083218645595", 100, ExpectedResult = "24176176")]
        [TestCase("19617804207202209144916044189917", 100, ExpectedResult = "73745418")]
        [TestCase("69317163492948606335995924319873", 100, ExpectedResult = "52432133")]
        public string RunFFT(string start, int numIterations)
        {
            return Program.RunFFT(start, numIterations);
        }
    }
}
