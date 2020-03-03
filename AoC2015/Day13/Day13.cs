using System;
using System.Collections.Generic;

/*

--- Day 13: Knights of the Dinner Table ---

In years past, the holiday feast with your family hasn't gone so well. Not everyone gets along! This year, you resolve, will be different. You're going to find the optimal seating arrangement and avoid all those awkward conversations.

You start by writing up a list of everyone invited and the amount their happiness would increase or decrease if they were to find themselves sitting next to each other person. You have a circular table that will be just big enough to fit everyone comfortably, and so each person will have exactly two neighbors.

For example, suppose you have only four attendees planned, and you calculate their potential happiness as follows:

Alice would gain 54 happiness units by sitting next to Bob.
Alice would lose 79 happiness units by sitting next to Carol.
Alice would lose 2 happiness units by sitting next to David.
Bob would gain 83 happiness units by sitting next to Alice.
Bob would lose 7 happiness units by sitting next to Carol.
Bob would lose 63 happiness units by sitting next to David.
Carol would lose 62 happiness units by sitting next to Alice.
Carol would gain 60 happiness units by sitting next to Bob.
Carol would gain 55 happiness units by sitting next to David.
David would gain 46 happiness units by sitting next to Alice.
David would lose 7 happiness units by sitting next to Bob.
David would gain 41 happiness units by sitting next to Carol.
Then, if you seat Alice next to David, Alice would lose 2 happiness units (because David talks so much), but David would gain 46 happiness units (because Alice is such a good listener), for a total change of 44.

If you continue around the table, you could then seat Bob next to Alice (Bob gains 83, Alice gains 54). Finally, seat Carol, who sits next to Bob (Carol gains 60, Bob loses 7) and David (Carol gains 55, David gains 41). The arrangement looks like this:

     +41 +46
+55   David    -2
Carol       Alice
+60    Bob    +54
     -7  +83
After trying every other seating arrangement in this hypothetical scenario, you find that this one is the most optimal, with a total change in happiness of 330.

What is the total change in happiness for the optimal seating arrangement of the actual guest list?

Your puzzle answer was 618.

--- Part Two ---

In all the commotion, you realize that you forgot to seat yourself. At this point, you're pretty apathetic toward the whole thing, and your happiness wouldn't really go up or down regardless of who you sit next to. You assume everyone else would be just as ambivalent about sitting next to you, too.

So, add yourself to the list, and give all happiness relationships that involve you a score of 0.

What is the total change in happiness for the optimal seating arrangement that actually includes yourself?

*/

namespace Day13
{
    class Program
    {
        public struct Rule
        {
            public string person;
            public string neighour;
            public int happiness;
        };
        static List<string> sNames;
        static List<Rule> sRules;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);
            if (part1)
            {
                var result1 = ComputeOptimum();
                Console.WriteLine($"Day13 : Result1 {result1}");
                var expected = 618;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 result has been broken {result1}");
                }
            }
            else
            {
                foreach (var name in sNames)
                {
                    Rule rule;
                    rule.person = "Jake";
                    rule.neighour = name;
                    rule.happiness = 0;
                    sRules.Add(rule);
                }
                sNames.Add("Jake");
                var result2 = ComputeOptimum();
                Console.WriteLine($"Day13 : Result2 {result2}");
                var expected = 601;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 result has been broken {result2}");
                }
            }
        }

        public static void ParseInput(string[] lines)
        {
            sNames = new List<string>();
            sRules = new List<Rule>(lines.Length);
            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                if (tokens.Length != 11)
                {
                    throw new InvalidProgramException($"Invalid line should have 11 tokens '{line}' {tokens.Length}");
                }
                if ((tokens[1] != "would") ||
                    (tokens[4] != "happiness") ||
                    (tokens[5] != "units") ||
                    (tokens[6] != "by") ||
                    (tokens[7] != "sitting") ||
                    (tokens[8] != "next") ||
                    (tokens[9] != "to"))
                {
                    throw new InvalidProgramException($"Invalid line '{line}'");
                }
                Rule rule;
                rule.person = tokens[0];
                rule.happiness = int.Parse(tokens[3]);
                if (tokens[2] == "gain")
                {
                    rule.happiness *= 1;
                }
                else if (tokens[2] == "lose")
                {
                    rule.happiness *= -1;
                }
                else
                {
                    throw new InvalidProgramException($"Invalid line '{line}' should be {tokens[2]} 'gain' or 'lose'");
                }
                rule.neighour = tokens[10].TrimEnd('.');
                sRules.Add(rule);
                if (!sNames.Contains(rule.person))
                {
                    sNames.Add(rule.person);
                }
            }
        }

        public static int ComputeOptimum()
        {
            int maxHappiness = int.MinValue;
            foreach (var start in sNames)
            {
                var seatingOrder = new List<string>
                {
                    start
                };
                MaxHappiness(ref seatingOrder, start, ref maxHappiness);
            }
            return maxHappiness;
        }

        public static void MaxHappiness(ref List<string> seatingOrder, string startingPerson, ref int maxHappiness)
        {
            foreach (var name in sNames)
            {
                if (seatingOrder.Contains(name))
                {
                    continue;
                }
                seatingOrder.Add(name);
                if (seatingOrder.Count == sNames.Count)
                {
                    var totalHappiness = ComputeHappiness(seatingOrder);
                    if (totalHappiness > maxHappiness)
                    {
                        maxHappiness = totalHappiness;
                    }
                }
                else
                {
                    MaxHappiness(ref seatingOrder, name, ref maxHappiness);
                }
                seatingOrder.Remove(name);
            }
        }

        static int ComputeHappiness(List<string> seatingOrder)
        {
            int happiness = 0;
            var seatCount = seatingOrder.Count;
            for (var i = 0; i < seatCount; ++i)
            {
                var leftNeigbourIndex = i - 1;
                var rightNeighbourIndex = i + 1;
                leftNeigbourIndex %= seatCount;
                if (leftNeigbourIndex < 0)
                {
                    leftNeigbourIndex += seatCount;
                }
                rightNeighbourIndex %= seatCount;
                if (rightNeighbourIndex < 0)
                {
                    rightNeighbourIndex += seatCount;
                }

                var person = seatingOrder[i];
                var leftNeighbour = seatingOrder[leftNeigbourIndex];
                var rightNeighbour = seatingOrder[rightNeighbourIndex];
                foreach (var rule in sRules)
                {
                    if (rule.person == person)
                    {
                        if (rule.neighour == leftNeighbour)
                        {
                            happiness += rule.happiness;
                        }
                        if (rule.neighour == rightNeighbour)
                        {
                            happiness += rule.happiness;
                        }
                    }
                }
            }
            return happiness;
        }

        public static void Run()
        {
            Console.WriteLine("Day13 : Start");
            _ = new Program("Day13/input.txt", true);
            _ = new Program("Day13/input.txt", false);
            Console.WriteLine("Day13 : End");
        }
    }
}
