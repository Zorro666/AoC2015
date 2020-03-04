using System.Collections.Generic;
using NUnit.Framework;

namespace Day18
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] mapA = {
".#.#.#",
"...##.",
"#....#",
"..#...",
"#.#..#",
"####.."
        };

        private static readonly string[] mapA_frame1 = {
"..##..",
"..##.#",
"...##.",
"......",
"#.....",
"#.##.."
        };

        private static readonly string[] mapA_frame2 = {
"..###.",
"......",
"..###.",
"......",
".#....",
".#...."
        };

        private static readonly string[] mapA_frame3 = {
"...#..",
"......",
"...#..",
"..##..",
"......",
"......"
        };

        private static readonly string[] mapA_frame4 = {
"......",
"......",
"..##..",
"..##..",
"......",
"......"
        };

        private static readonly string[] mapB = {
"##.#.#",
"...##.",
"#....#",
"..#...",
"#.#..#",
"####.#"
        };

        private static readonly string[] mapB_frame1 = {
"#.##.#",
"####.#",
"...##.",
"......",
"#...#.",
"#.####"
        };

        private static readonly string[] mapB_frame2 = {
"#..#.#",
"#....#",
".#.##.",
"...##.",
".#..##",
"##.###"
        };

        private static readonly string[] mapB_frame3 = {
"#...##",
"####.#",
"..##.#",
"......",
"##....",
"####.#"
        };

        private static readonly string[] mapB_frame4 = {
"#.####",
"#....#",
"...#..",
".##...",
"#.....",
"#.#..#",
        };

        private static readonly string[] mapB_frame5 = {
"##.###",
".##..#",
".##...",
".##...",
"#.#...",
"##...#"
        };

        public static IEnumerable<TestCaseData> SimulateCases => new[]
        {
            new TestCaseData(mapA, 0, mapA).SetName("Simulate 0"),
            new TestCaseData(mapA, 1, mapA_frame1).SetName("Simulate 1"),
            new TestCaseData(mapA, 2, mapA_frame2).SetName("Simulate 2"),
            new TestCaseData(mapA, 3, mapA_frame3).SetName("Simulate 3"),
            new TestCaseData(mapA, 4, mapA_frame4).SetName("Simulate 4")
        };

        [Test]
        [TestCaseSource("SimulateCases")]
        public void Simulate(string[] map, int numSteps, string[] expectedState)
        {
            Program.ParseMap(map, false);
            Program.Simulate(numSteps);
            var output = Program.GetMap();
            Assert.That(output, Is.EqualTo(expectedState));
            Assert.That(output.Length, Is.EqualTo(expectedState.Length));
            for (var i = 0; i < expectedState.Length; ++i)
            {
                Assert.That(output[i], Is.EqualTo(expectedState[i]));
            }
        }

        public static IEnumerable<TestCaseData> LightsOnCountCases => new[]
        {
            new TestCaseData(mapA, 4, 4).SetName("LightsOnCount 4 = 4")
        };

        [Test]
        [TestCaseSource("LightsOnCountCases")]
        public void LightsOnCount(string[] map, int numSteps, int expectedCount)
        {
            Program.ParseMap(map, false);
            Program.Simulate(numSteps);
            Assert.That(Program.LightsOnCount(), Is.EqualTo(expectedCount));
        }

        public static IEnumerable<TestCaseData> SimulateCasesPart2 => new[]
        {
            new TestCaseData(mapA, 0, mapB).SetName("SimulatePart2 0"),
            new TestCaseData(mapA, 1, mapB_frame1).SetName("SimulatePart2 1"),
            new TestCaseData(mapA, 2, mapB_frame2).SetName("SimulatePart2 2"),
            new TestCaseData(mapA, 3, mapB_frame3).SetName("SimulatePart2 3"),
            new TestCaseData(mapA, 4, mapB_frame4).SetName("SimulatePart2 4"),
            new TestCaseData(mapA, 5, mapB_frame5).SetName("SimulatePart2 5")
        };

        [Test]
        [TestCaseSource("SimulateCasesPart2")]
        public void SimulatePart2(string[] map, int numSteps, string[] expectedState)
        {
            Program.ParseMap(map, true);
            Program.Simulate(numSteps);
            var output = Program.GetMap();
            Assert.That(output, Is.EqualTo(expectedState));
            Assert.That(output.Length, Is.EqualTo(expectedState.Length));
            for (var i = 0; i < expectedState.Length; ++i)
            {
                Assert.That(output[i], Is.EqualTo(expectedState[i]));
            }
        }
        public static IEnumerable<TestCaseData> LightsOnCountCasesPart2 => new[]
        {
            new TestCaseData(mapB, 5, 17).SetName("LightsOnCountPart2 5 = 17")
        };

        [Test]
        [TestCaseSource("LightsOnCountCasesPart2")]
        public void LightsOnCountPart2(string[] map, int numSteps, int expectedCount)
        {
            Program.ParseMap(map, true);
            Program.Simulate(numSteps);
            Assert.That(Program.LightsOnCount(), Is.EqualTo(expectedCount));
        }
    }
}
