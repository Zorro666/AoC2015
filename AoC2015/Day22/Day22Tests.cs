using NUnit.Framework;

namespace Day22
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(10, 250, 8, 13, new string[] { "Poison" }, 1, 2, 0, 77, 10)]
        [TestCase(10, 250, 8, 13, new string[] { "Poison", "Magic Missile" }, 2, 2, 0, 24, 0)]
        [TestCase(10, 250, 8, 14, new string[] { "Recharge" }, 1, 2, 0, 122, 14)]
        [TestCase(10, 250, 8, 14, new string[] { "Recharge", "Shield" }, 2, 1, 7, 211, 14)]
        [TestCase(10, 250, 8, 14, new string[] { "Recharge", "Shield", "Drain" }, 3, 2, 7, 340, 12)]
        [TestCase(10, 250, 8, 14, new string[] { "Recharge", "Shield", "Drain", "Poison" }, 4, 1, 7, 167, 9)]
        [TestCase(10, 250, 8, 14, new string[] { "Recharge", "Shield", "Drain", "Poison", "Magic Missile" }, 5, 1, 0, 114, 0)]
        public void Fights(int initialPlayerHP, int initialPlayerMana, int bossAttack, int initialBossHP,
            string[] playerSpells, int turns,
            int expectedPlayerHP, int expectedPlayerArmour, int expectedPlayerMana, int expectedBossHP)
        {
            Program.InitGame(initialPlayerHP, initialPlayerMana, bossAttack, initialBossHP);
            for (var t = 0; t < turns; ++t)
            {
                Program.RunTurn(playerSpells[t]);
            }
            Assert.That(Program.GetPlayerHP(), Is.EqualTo(expectedPlayerHP));
            Assert.That(Program.GetPlayerArmour(), Is.EqualTo(expectedPlayerArmour));
            Assert.That(Program.GetPlayerMana(), Is.EqualTo(expectedPlayerMana));
            Assert.That(Program.GetBossHP(), Is.EqualTo(expectedBossHP));
        }
    }
}
