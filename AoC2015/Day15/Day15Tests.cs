using System.Collections.Generic;
using NUnit.Framework;

namespace Day15
{
    [TestFixture]
    public class Tests
    {
        private static readonly string[] ingredientsA = {
"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8",
"Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3"
        };

        public static IEnumerable<TestCaseData> HighestScoreTests => new[]
        {
            new TestCaseData(ingredientsA, 62842880).SetName("HighestScore = 62842880"),
        };

        [Test]
        [TestCaseSource("HighestScoreTests")]
        public void HighestScore(string[] ingredients, int expectedScore)
        {
            Program.ParseInput(ingredients);
            Assert.That(Program.HighestScore(), Is.EqualTo(expectedScore));
        }

        public static IEnumerable<TestCaseData> HighestScoreCalorieTests => new[]
        {
            new TestCaseData(ingredientsA, 500, 57600000).SetName("HighestScoreCalorie 500 = 57600000"),
        };

        [Test]
        [TestCaseSource("HighestScoreCalorieTests")]
        public void HighestScoreCalorie(string[] ingredients, int calorieTarget, int expectedScore)
        {
            Program.ParseInput(ingredients);
            Assert.That(Program.HighestScoreCalorie(500), Is.EqualTo(expectedScore));
        }

    }
}
