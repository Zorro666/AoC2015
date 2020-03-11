using NUnit.Framework;

namespace Day21
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(5, 5, 7, 2, 3)]
        [TestCase(5, 5, 7, 100, 1)]
        [TestCase(1, 5, 7, 2, 1)]
        public void BossDamage(int playerDamage, int playeArmour, int bossDamage, int bossArmour, int expectedDamage)
        {
            Assert.That(Program.BossDamage(playerDamage, playeArmour, bossDamage, bossArmour), Is.EqualTo(expectedDamage));
        }

        [Test]
        [TestCase(5, 5, 7, 2, 2)]
        [TestCase(5, 100, 7, 2, 1)]
        [TestCase(5, 5, 1, 2, 1)]
        public void PlayerDamage(int playerDamage, int playeArmour, int bossDamage, int bossArmour, int expectedDamage)
        {
            Assert.That(Program.PlayerDamage(playerDamage, playeArmour, bossDamage, bossArmour), Is.EqualTo(expectedDamage));
        }

        [Test]
        [TestCase(8, 5, 5, 12, 7, 2)]
        public void PlayerWins(int playerHP, int playerDamage, int playeArmour, int bossHP, int bossDamage, int bossArmour)
        {
            Assert.That(Program.PlayerWins(playerHP, playerDamage, playeArmour, bossHP, bossDamage, bossArmour), Is.True);
        }
    }
}
