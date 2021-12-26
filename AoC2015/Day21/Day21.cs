using System;

/*

--- Day 21: RPG Simulator 20XX ---

Little Henry Case got a new video game for Christmas. It's an RPG, and he's stuck on a boss. He needs to know what equipment to buy at the shop. He hands you the controller.

In this game, the player (you) and the enemy (the boss) take turns attacking. The player always goes first. Each attack reduces the opponent's hit points by at least 1. The first character at or below 0 hit points loses.

Damage dealt by an attacker each turn is equal to the attacker's damage score minus the defender's armor score. An attacker always does at least 1 damage. So, if the attacker has a damage score of 8, and the defender has an armor score of 3, the defender loses 5 hit points. If the defender had an armor score of 300, the defender would still lose 1 hit point.

Your damage score and armor score both start at zero. They can be increased by buying items in exchange for gold. You start with no items and have as much gold as you need. Your total damage or armor is equal to the sum of those stats from all of your items. You have 100 hit points.

Here is what the item shop is selling:

Weapons:    Cost  Damage  Armor
Dagger        8     4       0
Shortsword   10     5       0
Warhammer    25     6       0
Longsword    40     7       0
Greataxe     74     8       0

Armor:      Cost  Damage  Armor
Leather      13     0       1
Chainmail    31     0       2
Splintmail   53     0       3
Bandedmail   75     0       4
Platemail   102     0       5

Rings:      Cost  Damage  Armor
Damage +1    25     1       0
Damage +2    50     2       0
Damage +3   100     3       0
Defense +1   20     0       1
Defense +2   40     0       2
Defense +3   80     0       3
You must buy exactly one weapon; no dual-wielding. Armor is optional, but you can't use more than one. You can buy 0-2 rings (at most one for each hand). You must use any items you buy. The shop only has one of each item, so you can't buy, for example, two rings of Damage +3.

For example, suppose you have 8 hit points, 5 damage, and 5 armor, and that the boss has 12 hit points, 7 damage, and 2 armor:

The player deals 5-2 = 3 damage; the boss goes down to 9 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 6 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 6 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 4 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 3 hit points.
The boss deals 7-5 = 2 damage; the player goes down to 2 hit points.
The player deals 5-2 = 3 damage; the boss goes down to 0 hit points.
In this scenario, the player wins! (Barely.)

You have 100 hit points. The boss's actual stats are in your puzzle input. What is the least amount of gold you can spend and still win the fight?

--- Part Two ---

Turns out the shopkeeper is working with the boss, and can persuade you to buy whatever items he wants. 
The other rules still apply, and he still only has one of each item.

What is the most amount of gold you can spend and still lose the fight?

*/

namespace Day21
{
    class Program
    {
        struct Item
        {
            public Item(int _cost, int _attack, int _armour)
            {
                cost = _cost;
                attack = _attack;
                armour = _armour;
            }
            public int cost;
            public int attack;
            public int armour;
        };

        static Item[] sWeapons;
        static Item[] sArmour;
        static Item[] sRings;

        static int sPlayerHP;
        static int sPlayerAttack;
        static int sPlayerArmour;
        static int sBossHP;
        static int sBossAttack;
        static int sBossArmour;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);

            if (part1)
            {
                var result1 = SmallestGoldToWin();
                Console.WriteLine($"Day21 : Result1 {result1}");
                var expected = 91;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = MostGoldToLose();
                Console.WriteLine($"Day21 : Result2 {result2}");
                var expected = 158;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        static void ParseInput(string[] lines)
        {
            sPlayerHP = 100;
            sPlayerAttack = 0;
            sPlayerArmour = 0;
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
                    sBossHP = value;
                }
                else if (field == "Damage")
                {
                    sBossAttack = value;
                }
                else if (field == "Armor")
                {
                    sBossArmour = value;
                }
                else
                {
                    throw new InvalidProgramException($"Unexpected field {field} line '{line}'");
                }
            }

            /*
            Weapons:    Cost Damage  Armor
            Dagger        8     4       0
            Shortsword   10     5       0
            Warhammer    25     6       0
            Longsword    40     7       0
            Greataxe     74     8       0
            */
            sWeapons = new Item[5];
            sWeapons[0] = new Item(8, 4, 0);
            sWeapons[1] = new Item(10, 5, 0);
            sWeapons[2] = new Item(25, 6, 0);
            sWeapons[3] = new Item(40, 7, 0);
            sWeapons[4] = new Item(74, 8, 0);

            /*
            Armor: Cost Damage  Armor
            Leather      13     0       1
            Chainmail    31     0       2
            Splintmail   53     0       3
            Bandedmail   75     0       4
            Platemail   102     0       5
            */
            sArmour = new Item[6];
            sArmour[0] = new Item(0, 0, 0);
            sArmour[1] = new Item(13, 0, 1);
            sArmour[2] = new Item(31, 0, 2);
            sArmour[3] = new Item(53, 0, 3);
            sArmour[4] = new Item(75, 0, 4);
            sArmour[5] = new Item(102, 0, 5);

            /*
            Rings: Cost Damage  Armor
            Damage + 1    25     1       0
            Damage + 2    50     2       0
            Damage + 3   100     3       0
            Defense + 1   20     0       1
            Defense + 2   40     0       2
            Defense + 3   80     0       3
            */
            sRings = new Item[7];
            sRings[0] = new Item(0, 0, 0);
            sRings[1] = new Item(25, 1, 0);
            sRings[2] = new Item(50, 2, 0);
            sRings[3] = new Item(100, 3, 0);
            sRings[4] = new Item(20, 0, 1);
            sRings[5] = new Item(40, 0, 2);
            sRings[6] = new Item(80, 0, 3);
        }

        static int MostGoldToLose()
        {
            var maxGold = int.MinValue;
            for (var w = 0; w < sWeapons.Length; ++w)
            {
                Item weapon = sWeapons[w];
                for (var a = 0; a < sArmour.Length; ++a)
                {
                    Item armour = sArmour[a];
                    for (var r1 = 0; r1 < sRings.Length; ++r1)
                    {
                        Item ring1 = sRings[r1];
                        for (var r2 = 0; r2 < sRings.Length; ++r2)
                        {
                            if ((r2 > 0) && (r2 == r1))
                            {
                                continue;
                            }
                            Item ring2 = sRings[r2];
                            for (var r3 = 0; r3 < sRings.Length; ++r3)
                            {
                                if ((r3 > 0) && (r3 == r2))
                                {
                                    continue;
                                }
                                if ((r3 > 0) && (r3 == r1))
                                {
                                    continue;
                                }
                                Item ring3 = sRings[r3];
                                sPlayerAttack = 0;
                                sPlayerArmour = 0;
                                sPlayerAttack += weapon.attack;
                                sPlayerArmour += weapon.armour;
                                sPlayerAttack += armour.attack;
                                sPlayerArmour += armour.armour;
                                sPlayerAttack += ring1.attack;
                                sPlayerArmour += ring1.armour;
                                sPlayerAttack += ring2.attack;
                                sPlayerArmour += ring2.armour;
                                sPlayerAttack += ring3.attack;
                                sPlayerArmour += ring3.armour;
                                if (!PlayerWins())
                                {
                                    var gold = 0;
                                    gold += weapon.cost;
                                    gold += armour.cost;
                                    gold += ring1.cost;
                                    gold += ring2.cost;
                                    gold += ring3.cost;
                                    if (gold > maxGold)
                                    {
                                        //Console.WriteLine($"Cost:{gold} Weapon:{w} Armour:{a} Rings:{r1} {r2} {r3}");
                                    }
                                    maxGold = Math.Max(maxGold, gold);
                                }
                            }
                        }
                    }
                }
            }
            return maxGold;
        }

        static int SmallestGoldToWin()
        {
            var minGold = int.MaxValue;
            for (var w = 0; w < sWeapons.Length; ++w)
            {
                Item weapon = sWeapons[w];
                for (var a = 0; a < sArmour.Length; ++a)
                {
                    Item armour = sArmour[a];
                    for (var r1 = 0; r1 < sRings.Length; ++r1)
                    {
                        Item ring1 = sRings[r1];
                        for (var r2 = 0; r2 < sRings.Length; ++r2)
                        {
                            if ((r2 > 0) && (r2 == r1))
                            {
                                continue;
                            }
                            Item ring2 = sRings[r2];
                            for (var r3 = 0; r3 < sRings.Length; ++r3)
                            {
                                if ((r3 > 0) && (r3 == r2))
                                {
                                    continue;
                                }
                                if ((r3 > 0) && (r3 == r1))
                                {
                                    continue;
                                }
                                Item ring3 = sRings[r3];
                                sPlayerAttack = 0;
                                sPlayerArmour = 0;
                                sPlayerAttack += weapon.attack;
                                sPlayerArmour += weapon.armour;
                                sPlayerAttack += armour.attack;
                                sPlayerArmour += armour.armour;
                                sPlayerAttack += ring1.attack;
                                sPlayerArmour += ring1.armour;
                                sPlayerAttack += ring2.attack;
                                sPlayerArmour += ring2.armour;
                                sPlayerAttack += ring3.attack;
                                sPlayerArmour += ring3.armour;
                                if (PlayerWins())
                                {
                                    var gold = 0;
                                    gold += weapon.cost;
                                    gold += armour.cost;
                                    gold += ring1.cost;
                                    gold += ring2.cost;
                                    gold += ring3.cost;
                                    if (gold < minGold)
                                    {
                                        //Console.WriteLine($"Cost:{gold} Weapon:{w} Armour:{a} Rings:{r1} {r2} {r3}");
                                    }
                                    minGold = Math.Min(minGold, gold);
                                }
                            }
                        }
                    }
                }
            }
            return minGold;
        }

        static int Damage(int attack, int armour)
        {
            var damage = attack - armour;
            damage = Math.Max(1, damage);
            return damage;
        }

        static bool PlayerWins()
        {
            sPlayerHP = 100;
            sBossHP = 100;
            return PlayerWins(sPlayerHP, sPlayerAttack, sPlayerArmour, sBossHP, sBossAttack, sBossArmour);
        }

        public static bool PlayerWins(int playerHP, int playerAttack, int playerArmour, int bossHP, int bossAttack, int bossArmour)
        {
            do
            {
                bossHP -= BossDamage(playerAttack, playerArmour, bossAttack, bossArmour);
                if (bossHP > 0)
                {
                    playerHP -= PlayerDamage(playerAttack, playerArmour, bossAttack, bossArmour);
                }
            } while ((bossHP > 0) && (playerHP > 0));

            if (playerHP > 0)
            {
                return true;
            }
            if (bossHP > 0)
            {
                return false;
            }
            return false;
        }

        public static int BossDamage(int playerAttack, int playerArmour, int bossAttack, int bossArmour)
        {
            return Damage(playerAttack, bossArmour);
        }

        public static int PlayerDamage(int playerAttack, int playerArmour, int bossAttack, int bossArmour)
        {
            return Damage(bossAttack, playerArmour);
        }

        public static void Run()
        {
            Console.WriteLine("Day21 : Start");
            _ = new Program("Day21/input.txt", true);
            _ = new Program("Day21/input.txt", false);
            Console.WriteLine("Day21 : End");
        }
    }
}
