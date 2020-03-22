using System.Collections.Generic;
using NUnit.Framework;

namespace Day24
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] TestPackages = {
"1",
"2",
"3",
"4",
"5",
"7",
"8",
"9",
"10",
"11",
};

        public static IEnumerable<TestCaseData> TestPackageCases => new[]
        {
            new TestCaseData(TestPackages, 90).SetName("TestPackages QE 90"),
            new TestCaseData(TestPackages, 99).SetName("TestPackages QE 99"),
            new TestCaseData(TestPackages, 160).SetName("TestPackages QE 160"),
            new TestCaseData(TestPackages, 200).SetName("TestPackages QE 200"),
            new TestCaseData(TestPackages, 210).SetName("TestPackages QE 210"),
            new TestCaseData(TestPackages, 216).SetName("TestPackages QE 216"),
            new TestCaseData(TestPackages, 240).SetName("TestPackages QE 240"),
            new TestCaseData(TestPackages, 252).SetName("TestPackages QE 252"),
            new TestCaseData(TestPackages, 280).SetName("TestPackages QE 280"),
            new TestCaseData(TestPackages, 300).SetName("TestPackages QE 300"),
            new TestCaseData(TestPackages, 360).SetName("TestPackages QE 360"),
            new TestCaseData(TestPackages, 420).SetName("TestPackages QE 420"),
            new TestCaseData(TestPackages, 480).SetName("TestPackages QE 480"),
        };

        [Test]
        [TestCaseSource("TestPackageCases")]
        public void QuantumEntanglementExists(string[] packages, int expectedQE)
        {
            Program.ParseInput(packages, 3);
            Program.ComputeQEs();
            Assert.That(Program.QEExists(expectedQE), Is.True);
        }

        public static IEnumerable<TestCaseData> SmallestQECases => new[]
        {
            new TestCaseData(TestPackages, 99).SetName("SmallestQE 99"),
        };

        [Test]
        [TestCaseSource("SmallestQECases")]
        public void SmallestQE(string[] packages, int expectedQE)
        {
            Program.ParseInput(packages, 3);
            Assert.That(Program.SmallestQE(), Is.EqualTo(expectedQE));
        }

        public static IEnumerable<TestCaseData> TestPackagePart2Cases => new[]
        {
            new TestCaseData(TestPackages, 44).SetName("TestPackagesPart2 QE 44"),
            new TestCaseData(TestPackages, 45).SetName("TestPackagesPart2 QE 45"),
            new TestCaseData(TestPackages, 50).SetName("TestPackagesPart2 QE 50"),
            new TestCaseData(TestPackages, 54).SetName("TestPackagesPart2 QE 54"),
            new TestCaseData(TestPackages, 56).SetName("TestPackagesPart2 QE 56"),
            new TestCaseData(TestPackages, 72).SetName("TestPackagesPart2 QE 72"),
        };

        [Test]
        [TestCaseSource("TestPackagePart2Cases")]
        public void QuantumEntanglementExistsPart2(string[] packages, int expectedQE)
        {
            Program.ParseInput(packages, 4);
            Program.ComputeQEs();
            Assert.That(Program.QEExists(expectedQE), Is.True);
        }

        public static IEnumerable<TestCaseData> SmallestQEPart2Cases => new[]
        {
            new TestCaseData(TestPackages, 44).SetName("SmallestQEPart2 44"),
        };

        [Test]
        [TestCaseSource("SmallestQEPart2Cases")]
        public void SmallestQEPart2(string[] packages, int expectedQE)
        {
            Program.ParseInput(packages, 4);
            Assert.That(Program.SmallestQE(), Is.EqualTo(expectedQE));
        }
    }
}
