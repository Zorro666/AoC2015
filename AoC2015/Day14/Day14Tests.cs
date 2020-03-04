using System;
using System.Collections;
using NUnit.Framework;

namespace Day14
{
    [TestFixture]
    public class Tests
    {
        static readonly string[] reindeerRaceTest = {
"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
"Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
        };

        public static IEnumerable DistanceTests => new[]
        {
            new TestCaseData(reindeerRaceTest, "Comet", 1).SetName("Distance Comet 1 = 14").Returns(14),
            new TestCaseData(reindeerRaceTest, "Dancer", 1).SetName("Distance Dancer 1 = 16").Returns(16),
            new TestCaseData(reindeerRaceTest, "Comet", 10).SetName("Distance Comet 10 = 140").Returns(140),
            new TestCaseData(reindeerRaceTest, "Dancer", 10).SetName("Distance Dancer 10 = 160").Returns(160),
            new TestCaseData(reindeerRaceTest, "Comet", 11).SetName("Distance Comet 11 = 140").Returns(140),
            new TestCaseData(reindeerRaceTest, "Dancer", 11).SetName("Distance Dancer 11 = 176").Returns(176),
            new TestCaseData(reindeerRaceTest, "Comet", 12).SetName("Distance Comet 12 = 140").Returns(140),
            new TestCaseData(reindeerRaceTest, "Dancer", 12).SetName("Distance Dancer 12 = 176").Returns(176),
            new TestCaseData(reindeerRaceTest, "Comet", 1000).SetName("Distance Comet 1000 = 1120").Returns(1120),
            new TestCaseData(reindeerRaceTest, "Dancer", 1000).SetName("Distance Dancer 1000 = 1056").Returns(1056)
        };

        [Test]
        [TestCaseSource("DistanceTests")]
        public int ReindeerDistance(string[] lines, string reindeer, int time)
        {
            Program.ParseInput(lines);
            return Program.Distance(reindeer, time);
        }

        public static IEnumerable WinningDistanceTests => new[]
        {
            new TestCaseData(reindeerRaceTest, 1000).SetName("WinningDistance 1000 = 1120").Returns(1120)
        };

        [Test]
        [TestCaseSource("WinningDistanceTests")]
        public int WinningDistance(string[] lines, int time)
        {
            Program.ParseInput(lines);
            return Program.WinningDistance(time);
        }

        public static IEnumerable PointsTests => new[]
        {
            new TestCaseData(reindeerRaceTest, "Comet", 1).SetName("Points Comet 1 = 0").Returns(0),
            new TestCaseData(reindeerRaceTest, "Dancer", 1).SetName("Points Dancer 1 = 1").Returns(1),
            new TestCaseData(reindeerRaceTest, "Comet", 140).SetName("Points Comet 140 = 1").Returns(1),
            new TestCaseData(reindeerRaceTest, "Dancer", 140).SetName("Points Dancer 140 = 139").Returns(139),
            new TestCaseData(reindeerRaceTest, "Comet", 1000).SetName("Points Comet 1000 = 312").Returns(312),
            new TestCaseData(reindeerRaceTest, "Dancer", 1000).SetName("Points Dancer 1000 = 689").Returns(689)
        };

        [Test]
        [TestCaseSource("PointsTests")]
        public int Points(string[] lines, string reindeer, int time)
        {
            Program.ParseInput(lines);
            return Program.Points(reindeer, time);
        }

        public static IEnumerable WinningPointsTests => new[]
        {
            new TestCaseData(reindeerRaceTest, 1000).SetName("WinningPoints 1000 = 689").Returns(689)
        };

        [Test]
        [TestCaseSource("WinningPointsTests")]
        public int WinningPoints(string[] lines, int time)
        {
            Program.ParseInput(lines);
            return Program.WinningPoints(time);
        }
    }
}
