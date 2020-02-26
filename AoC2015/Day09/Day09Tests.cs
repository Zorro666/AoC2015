using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Day09
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] testRoute = new string[] {
"London to Dublin = 464",
"London to Belfast = 518",
"Dublin to Belfast = 141"
        };

        public static IEnumerable<TestCaseData> TestRouteCases => new[]
        {
            new TestCaseData(testRoute, 605).SetName("TestRoute 605"),
        };

        [Test]
        [TestCaseSource("TestRouteCases")]
        public void ShortestRoute(string[] routes, int expectedShortestRoute)
        {
            Program.ParseRoutes(routes);
            Assert.That(Program.ShortestRoute(), Is.EqualTo(expectedShortestRoute));
        }
    }
}
