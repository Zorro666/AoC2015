using System;
using System.Collections.Generic;

/*

--- Day 16: Aunt Sue ---

Your Aunt Sue has given you a wonderful gift, and you'd like to send her a thank you card. However, there's a small problem: she signed it "From, Aunt Sue".

You have 500 Aunts named "Sue".

So, to avoid sending the card to the wrong person, you need to figure out which Aunt Sue (which you conveniently number 1 to 500, for sanity) gave you the gift. You open the present and, as luck would have it, good ol' Aunt Sue got you a My First Crime Scene Analysis Machine! 
Just what you wanted. Or needed, as the case may be.

The My First Crime Scene Analysis Machine (MFCSAM for short) can detect a few specific compounds in a given sample, as well as how many distinct kinds of those compounds there are. 
According to the instructions, these are what the MFCSAM can detect:

children, by human DNA age analysis.
cats. It doesn't differentiate individual breeds.
Several seemingly random breeds of dog: samoyeds, pomeranians, akitas, and vizslas.
goldfish. No other kinds of fish.
trees, all in one group.
cars, presumably by exhaust or gasoline or something.
perfumes, which is handy, since many of your Aunts Sue wear a few kinds.
In fact, many of your Aunts Sue have many of these. You put the wrapping from the gift into the MFCSAM. 
It beeps inquisitively at you a few times and then prints out a message on ticker tape:

children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1
You make a list of the things you can remember about each Aunt Sue. 
Things missing from your list aren't zero - you simply don't remember the value.

What is the number of the Sue that got you the gift?

Your puzzle answer was 103.

--- Part Two ---

As you're about to send the thank you note, something in the MFCSAM's instructions catches your eye. 
Apparently, it has an outdated retroencabulator, and so the output from the machine isn't exact values - 
some of them indicate ranges.

In particular, 
the cats and trees readings indicates that there are greater than that many 
(due to the unpredictable nuclear decay of cat dander and tree pollen), 
while the pomeranians and goldfish readings indicate that there are fewer than that many 
(due to the modial interaction of magnetoreluctance).

What is the number of the real Aunt Sue?

*/

namespace Day16
{
    class Program
    {
        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);

            if (part1)
            {
                var result1 = FindSue(lines, true);
                Console.WriteLine($"Day16 Result1:{result1}");
                var expected = 103;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = FindSue(lines, false);
                Console.WriteLine($"Day16 Result2:{result2}");
                var expected = 405;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static int FindSue(string[] lines, bool exactMatch)
        {
            Dictionary<string, int> knownFacts = new Dictionary<string, int>();
            knownFacts["children"] = 3;
            knownFacts["cats"] = 7;
            knownFacts["samoyeds"] = 2;
            knownFacts["pomeranians"] = 3;
            knownFacts["akitas"] = 0;
            knownFacts["vizslas"] = 0;
            knownFacts["goldfish"] = 5;
            knownFacts["trees"] = 3;
            knownFacts["cars"] = 2;
            knownFacts["perfumes"] = 1; ;
            foreach (var line in lines)
            {
                // Sue 499: vizslas: 8, perfumes: 1, akitas: 3
                var tokens = line.Split(' ');
                if (tokens[0] != "Sue")
                {
                    throw new InvalidProgramException($"Invalid line '{line}' does not start with 'Sue'");
                }
                var sue = int.Parse(tokens[1].TrimEnd(':'));
                bool badSue = false;
                for (var t = 2; t < tokens.Length; t += 2)
                {
                    var property = tokens[t].TrimEnd(':');
                    var value = int.Parse(tokens[t + 1].TrimEnd(','));
                    if (exactMatch)
                    {
                        if (knownFacts[property] != value)
                        {
                            badSue = true;
                        }
                    }
                    else
                    {
                        // the cats and trees readings indicates that there are greater than that many 
                        if ((property == "cats") || (property == "trees"))
                        {
                            if (knownFacts[property] >= value)
                            {
                                badSue = true;
                            }
                        }
                        // the pomeranians and goldfish readings indicate that there are fewer than that many 
                        else if ((property == "pomeranians") || (property == "goldfish"))
                        {
                            if (knownFacts[property] <= value)
                            {
                                badSue = true;
                            }
                        }
                        else
                        {
                            if (knownFacts[property] != value)
                            {
                                badSue = true;
                            }
                        }
                    }
                }
                if (badSue == false)
                {
                    Console.WriteLine($"Good Sue {sue} '{line}'");
                    return sue;
                }
            }
            return -1;
        }

        public static void Run()
        {
            Console.WriteLine("Day16 : Start");
            _ = new Program("Day16/input.txt", true);
            _ = new Program("Day16/input.txt", false);
            Console.WriteLine("Day16 : End");
        }
    }
}
