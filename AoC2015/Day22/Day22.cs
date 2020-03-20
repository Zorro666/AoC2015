using System;

/*

--- Day 22: Wizard Simulator 20XX ---

Little Henry Case decides that defeating bosses with swords and stuff is boring. 
Now he's playing the game with a wizard. 
Of course, he gets stuck on another boss and needs your help again.

In this version, combat still proceeds with the player and the boss taking alternating turns. 
The player still goes first. 
Now, however, you don't get any equipment; instead, you must choose one of your spells to cast. 
The first character at or below 0 hit points loses.

Since you're a wizard, you don't get to wear armor, and you can't attack normally. 
However, since you do magic damage, your opponent's armor is ignored, and so the boss effectively has zero armor as well. 
As before, if armor (from a spell, in this case) would reduce damage below 1, it becomes 1 instead - that is, the boss' attacks always deal at least 1 damage.

On each of your turns, you must select one of your spells to cast. 
If you cannot afford to cast any spell, you lose. Spells cost mana; you start with 500 mana, but have no maximum limit. 
You must have enough mana to cast a spell, and its cost is immediately deducted when you cast it. 
Your spells are Magic Missile, Drain, Shield, Poison, and Recharge.

Magic Missile costs 53 mana. It instantly does 4 damage.
Drain costs 73 mana. It instantly does 2 damage and heals you for 2 hit points.
Shield costs 113 mana. It starts an effect that lasts for 6 turns. While it is active, your armor is increased by 7.
Poison costs 173 mana. It starts an effect that lasts for 6 turns. At the start of each turn while it is active, it deals the boss 3 damage.
Recharge costs 229 mana. It starts an effect that lasts for 5 turns. At the start of each turn while it is active, it gives you 101 new mana.
Effects all work the same way. 
Effects apply at the start of both the player's turns and the boss' turns. 
Effects are created with a timer (the number of turns they last); at the start of each turn, after they apply any effect they have, their timer is decreased by one. 
If this decreases the timer to zero, the effect ends. 
You cannot cast a spell that would start an effect which is already active. 
However, effects can be started on the same turn they end.

For example, suppose the player has 10 hit points and 250 mana, and that the boss has 13 hit points and 8 damage:

-- Player turn --
- Player has 10 hit points, 0 armor, 250 mana
- Boss has 13 hit points
Player casts Poison.

-- Boss turn --
- Player has 10 hit points, 0 armor, 77 mana
- Boss has 13 hit points
Poison deals 3 damage; its timer is now 5.
Boss attacks for 8 damage.

-- Player turn --
- Player has 2 hit points, 0 armor, 77 mana
- Boss has 10 hit points
Poison deals 3 damage; its timer is now 4.
Player casts Magic Missile, dealing 4 damage.

-- Boss turn --
- Player has 2 hit points, 0 armor, 24 mana
- Boss has 3 hit points
Poison deals 3 damage. This kills the boss, and the player wins.
Now, suppose the same initial conditions, except that the boss has 14 hit points instead:

-- Player turn --
- Player has 10 hit points, 0 armor, 250 mana
- Boss has 14 hit points
Player casts Recharge.

-- Boss turn --
- Player has 10 hit points, 0 armor, 21 mana
- Boss has 14 hit points
Recharge provides 101 mana; its timer is now 4.
Boss attacks for 8 damage!

-- Player turn --
- Player has 2 hit points, 0 armor, 122 mana
- Boss has 14 hit points
Recharge provides 101 mana; its timer is now 3.
Player casts Shield, increasing armor by 7.

-- Boss turn --
- Player has 2 hit points, 7 armor, 110 mana
- Boss has 14 hit points
Shield's timer is now 5.
Recharge provides 101 mana; its timer is now 2.
Boss attacks for 8 - 7 = 1 damage!

-- Player turn --
- Player has 1 hit point, 7 armor, 211 mana
- Boss has 14 hit points
Shield's timer is now 4.
Recharge provides 101 mana; its timer is now 1.
Player casts Drain, dealing 2 damage, and healing 2 hit points.

-- Boss turn --
- Player has 3 hit points, 7 armor, 239 mana
- Boss has 12 hit points
Shield's timer is now 3.
Recharge provides 101 mana; its timer is now 0.
Recharge wears off.
Boss attacks for 8 - 7 = 1 damage!

-- Player turn --
- Player has 2 hit points, 7 armor, 340 mana
- Boss has 12 hit points
Shield's timer is now 2.
Player casts Poison.

-- Boss turn --
- Player has 2 hit points, 7 armor, 167 mana
- Boss has 12 hit points
Shield's timer is now 1.
Poison deals 3 damage; its timer is now 5.
Boss attacks for 8 - 7 = 1 damage!

-- Player turn --
- Player has 1 hit point, 7 armor, 167 mana
- Boss has 9 hit points
Shield's timer is now 0.
Shield wears off, decreasing armor by 7.
Poison deals 3 damage; its timer is now 4.
Player casts Magic Missile, dealing 4 damage.

-- Boss turn --
- Player has 1 hit point, 0 armor, 114 mana
- Boss has 2 hit points
Poison deals 3 damage. This kills the boss, and the player wins.

You start with 50 hit points and 500 mana points. 
The boss's actual stats are in your puzzle input. 

What is the least amount of mana you can spend and still win the fight? (Do not include mana recharge effects as "spending" negative mana.)

--- Part Two ---

On the next run through the game, you increase the difficulty to hard.

At the start of each player turn (before any other effects apply), you lose 1 hit point. 
If this brings you to or below 0 hit points, you lose.

With the same starting stats for you and the boss, what is the least amount of mana you can spend and still win the fight?

*/

namespace Day22
{
    class Program
    {
        static readonly int sSpellCount = 5;
        static readonly string[] sSpells = { "Magic Missile", "Drain", "Shield", "Poison", "Recharge" };
        static int sInitialPlayerHP;
        static int sInitialPlayerMana;
        static int sInitialBossAttack;
        static int sInitialBossHP;

        static int sPlayerHP;
        static int sPlayerArmour;
        static int sPlayerMana;
        static int sBossAttack;
        static int sBossHP;
        static int sPlayerDamagePerTurn;

        static int sShieldTimer;
        static int sPoisonTimer;
        static int sRechargeTimer;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);

            if (part1)
            {
                var result1 = LeastManaToWin(0);
                var expected = 1824;
                Console.WriteLine($"Day22: Result1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = LeastManaToWin(1);
                Console.WriteLine($"Day22: Result2 {result2}");
                var expected = 1937;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        static void ParseInput(string[] lines)
        {
            var initialBossHP = 0;
            var initialBossAttack = 0;
            foreach (var line in lines)
            {
                var tokens = line.Split(':');
                if (tokens.Length != 2)
                {
                    throw new InvalidProgramException($"Unexpected number of tokens {tokens.Length} expected 2 line '{line}'");

                }
                var field = tokens[0];
                var value = int.Parse(tokens[1]);
                if (field == "Hit Points")
                {
                    initialBossHP = value;
                }
                else if (field == "Damage")
                {
                    initialBossAttack = value;
                }
                else
                {
                    throw new InvalidProgramException($"Unexpected field {field} line '{line}'");
                }
            }
            if (initialBossHP == 0)
            {
                throw new InvalidProgramException($"Failed to find initial boss HP");
            }
            if (initialBossAttack == 0)
            {
                throw new InvalidProgramException($"Failed to find initial boss Attack");
            }
            InitGame(50, 500, initialBossAttack, initialBossHP);
        }

        public static void InitGame(int initialPlayerHP, int initialPlayerMana, int bossAttack, int initialBossHP)
        {
            sInitialPlayerHP = initialPlayerHP;
            sInitialPlayerMana = initialPlayerMana;
            sInitialBossAttack = bossAttack;
            sInitialBossHP = initialBossHP;
            sPlayerDamagePerTurn = 0;
            StartGame();
        }

        static void StartGame()
        {
            sPlayerHP = sInitialPlayerHP;
            sPlayerArmour = 0;
            sPlayerMana = sInitialPlayerMana;
            sBossAttack = sInitialBossAttack;
            sBossHP = sInitialBossHP;

            sShieldTimer = 0;
            sPoisonTimer = 0;
            sRechargeTimer = 0;
        }

        static void PlayerTurn(string playerSpell)
        {
            if (playerSpell == "Magic Missile")
            {
                if (sPlayerMana < 53)
                {
                    throw new InvalidProgramException($"Not enough mana to cast spell {playerSpell} Mana:{sPlayerMana}");
                }
                sPlayerMana -= 53;
                sBossHP -= 4;
            }
            else if (playerSpell == "Drain")
            {
                if (sPlayerMana < 73)
                {
                    throw new InvalidProgramException($"Not enough mana to cast spell {playerSpell} Mana:{sPlayerMana}");
                }
                sPlayerMana -= 73;
                sBossHP -= 2;
                sPlayerHP += 2;
            }
            else if (playerSpell == "Shield")
            {
                if (sPlayerMana < 113)
                {
                    throw new InvalidProgramException($"Not enough mana to cast spell {playerSpell} Mana:{sPlayerMana}");
                }
                sPlayerMana -= 113;
                if (sShieldTimer != 0)
                {
                    throw new InvalidProgramException($"Shield timer is not 0 {sShieldTimer}");
                }
                sShieldTimer = 6;
            }
            else if (playerSpell == "Poison")
            {
                if (sPlayerMana < 173)
                {
                    throw new InvalidProgramException($"Not enough mana to cast spell {playerSpell} Mana:{sPlayerMana}");
                }
                sPlayerMana -= 173;
                if (sPoisonTimer != 0)
                {
                    throw new InvalidProgramException($"Poison timer is not 0 {sPoisonTimer}");
                }
                sPoisonTimer = 6;
            }
            else if (playerSpell == "Recharge")
            {
                if (sPlayerMana < 229)
                {
                    throw new InvalidProgramException($"Not enough mana to cast spell {playerSpell} Mana:{sPlayerMana}");
                }
                sPlayerMana -= 229;
                if (sRechargeTimer != 0)
                {
                    throw new InvalidProgramException($"Recharge timer is not 0 {sRechargeTimer}");
                }
                sRechargeTimer = 5;
            }
            //Console.WriteLine($"Player casts '{playerSpell}' Mana:{sPlayerMana}");
        }

        static void BossTurn()
        {
            if (sBossHP > 0)
            {
                var bossDamage = sBossAttack - sPlayerArmour;
                bossDamage = Math.Max(1, bossDamage);
                sPlayerHP -= bossDamage;
            }
        }

        static void ApplyEffects()
        {
            sPlayerArmour = 0;
            if (sShieldTimer > 0)
            {
                sPlayerArmour = 7;
                --sShieldTimer;
            }
            if (sPoisonTimer > 0)
            {
                sBossHP -= 3;
                --sPoisonTimer;
            }
            if (sRechargeTimer > 0)
            {
                sPlayerMana += 101;
                --sRechargeTimer;
            }
        }

        public static void RunTurn(string playerSpell)
        {
            ApplyEffects();
            sPlayerHP -= sPlayerDamagePerTurn;
            if (sPlayerHP == 0)
            {
                return;
            }
            PlayerTurn(playerSpell);
            sBossHP = Math.Max(0, sBossHP);
            if (sBossHP == 0)
            {
                return;
            }

            ApplyEffects();
            BossTurn();
            sBossHP = Math.Max(0, sBossHP);
            sPlayerHP = Math.Max(0, sPlayerHP);
            if (sPlayerHP == 0)
            {
                return;
            }
            if (sBossHP == 0)
            {
                return;
            }
        }

        public static int GetPlayerHP()
        {
            return sPlayerHP;
        }

        public static int GetPlayerArmour()
        {
            return sPlayerArmour;
        }

        public static int GetPlayerMana()
        {
            return sPlayerMana;
        }

        public static int GetBossHP()
        {
            return sBossHP;
        }

        static bool ValidSpell(int spellIndex)
        {
            var playerMana = sPlayerMana;
            if (sRechargeTimer > 0)
            {
                playerMana += 101;
            }
            if ((spellIndex == 0) && (playerMana >= 53))
            {
                return true;
            }
            if ((spellIndex == 1) && (playerMana >= 73))
            {
                return true;
            }
            if ((spellIndex == 2) && (playerMana >= 113))
            {
                if (sShieldTimer <= 1)
                {
                    return true;
                }
                return false;
            }
            if ((spellIndex == 3) && (playerMana >= 173))
            {
                if (sPoisonTimer <= 1)
                {
                    return true;
                }
                return false;
            }
            if ((spellIndex == 4) && (playerMana >= 229))
            {
                if (sRechargeTimer <= 1)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        static int NextSpell(byte[] spellCombination, int spellDepth)
        {
            int spellIndex = spellCombination[spellDepth];
            if (ValidSpell(spellIndex))
            {
                return spellIndex;
            }
            return -1;
        }

        static int LeastManaToWin(int playerDamagePerTurn)
        {
            sPlayerDamagePerTurn = playerDamagePerTurn;
            var leastMana = int.MaxValue;

            var spellCombinationDepth = 12 + 1 * sPlayerDamagePerTurn;
            byte[] spellCombination = new byte[spellCombinationDepth];
            long maxNumGames = 1;
            for (var c = 0; c < spellCombinationDepth; ++c)
            {
                maxNumGames *= sSpellCount;
                spellCombination[c] = 0;
            }
            for (long g = 0; g < maxNumGames; ++g)
            {
                if ((g % 200000000) == 0)
                {
                    Console.WriteLine($"New Game {g + 1}/{maxNumGames} LeastMana:{leastMana}");
                }
                var manaCost = RunATestGame(leastMana, spellCombination);
                if (manaCost < leastMana)
                {
                    Console.WriteLine($"New Least Mana {manaCost} {g + 1}/{maxNumGames}");
                }
                leastMana = Math.Min(leastMana, manaCost);
                byte add = 1;
                for (var c = 0; c < spellCombinationDepth; ++c)
                {
                    spellCombination[c] += add;
                    add = 0;
                    if (spellCombination[c] >= sSpellCount)
                    {
                        spellCombination[c] = 0;
                        add = 1;
                    }
                    if (add == 0)
                    {
                        break;
                    }
                }
            }
            return leastMana;
        }

        static int ManaCost(int playerSpell)
        {
            int manaCost;
            if (playerSpell == 0)
            {
                manaCost = 53;
            }
            else if (playerSpell == 1)
            {
                manaCost = 73;
            }
            else if (playerSpell == 2)
            {
                manaCost = 113;
            }
            else if (playerSpell == 3)
            {
                manaCost = 173;
            }
            else if (playerSpell == 4)
            {
                manaCost = 229;
            }
            else
            {
                throw new InvalidProgramException($"Unknown playerSpell '{playerSpell}'");
            }
            return manaCost;
        }

        static int RunATestGame(int minManaCost, byte[] spellCombination)
        {
            var spellDepth = 0;
            var manaCost = 0;
            StartGame();
            do
            {
                var playerSpell = NextSpell(spellCombination, spellDepth);
                if (playerSpell == -1)
                {
                    return int.MaxValue;
                }
                manaCost += ManaCost(playerSpell);
                if (manaCost > minManaCost)
                {
                    return int.MaxValue;
                }
                var spell = sSpells[playerSpell];
                RunTurn(spell);
                ++spellDepth;
                if (spellDepth >= spellCombination.Length)
                {
                    break;
                }
            } while ((sBossHP > 0) && (sPlayerHP > 0));
            if (sBossHP != 0)
            {
                return int.MaxValue;
            }
            return manaCost;
        }

        public static void Run()
        {

            _ = new Program("Day22/input.txt", true);
            _ = new Program("Day22/input.txt", false);
            Console.WriteLine("Day22 : End");
        }
    }
}
