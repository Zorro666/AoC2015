using System;
using System.Collections.Generic;

/*

--- Day 19: Medicine for Rudolph ---

Rudolph the Red-Nosed Reindeer is sick! His nose isn't shining very brightly, and he needs medicine.

Red-Nosed Reindeer biology isn't similar to regular reindeer biology; Rudolph is going to need custom-made medicine. Unfortunately, Red-Nosed Reindeer chemistry isn't similar to regular reindeer chemistry, either.

The North Pole is equipped with a Red-Nosed Reindeer nuclear fusion/fission plant, capable of constructing any Red-Nosed Reindeer molecule you need. It works by starting with some input molecule and then doing a series of replacements, one per step, until it has the right molecule.

However, the machine has to be calibrated before it can be used. Calibration involves determining the number of molecules that can be generated in one step from a given starting point.

For example, imagine a simpler machine that supports only the following replacements:

H => HO
H => OH
O => HH

Given the replacements above and starting with HOH, the following molecules could be generated:

HOOH (via H => HO on the first H).
HOHO (via H => HO on the second H).
OHOH (via H => OH on the first H).
HOOH (via H => OH on the second H).
HHHH (via O => HH).
So, in the example above, there are 4 distinct molecules (not five, because HOOH appears twice) after one replacement from HOH. 
Santa's favorite molecule, HOHOHO, can become 7 distinct molecules (over nine replacements: six from H, and three from O).

The machine replaces without regard for the surrounding characters. 
For example, given the string H2O, the transition H => OO would result in OO2O.

Your puzzle input describes all of the possible replacements and, at the bottom, the medicine molecule for which you need to calibrate the machine. 
How many distinct molecules can be created after all the different ways you can do one replacement on the medicine molecule?

Your puzzle answer was 509.

--- Part Two ---

Now that the machine is calibrated, you're ready to begin molecule fabrication.

Molecule fabrication always begins with just a single electron, e, and applying replacements one at a time, just like the ones during calibration.

For example, suppose you have the following replacements:

e => H
e => O
H => HO
H => OH
O => HH
If you'd like to make HOH, you start with e, and then make the following replacements:

e => O to get O
O => HH to get HH
H => OH (on the second H) to get HOH
So, you could make HOH after 3 steps. Santa's favorite molecule, HOHOHO, can be made in 6 steps.

How long will it take to make the medicine? 
Given the available replacements and the medicine molecule in your puzzle input, what is the fewest number of steps to go from e to the medicine molecule?

*/

namespace Day19
{
    class Program
    {
        struct Rule
        {
            public string from;
            public string to;
        };

        static Rule[] sRules;
        static string sSource;
        static Dictionary<string, int> sOutputs;
        static Random sRandom = new Random();

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);
            if (part1)
            {
                var result1 = UniqueMolecules();
                Console.WriteLine($"Day19 : Result1 {result1}");
                var expected = 518;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = GenerateCompound();
                Console.WriteLine($"Day19 : Result2 {result2}");
                var expected = 200;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseInput(string[] lines)
        {
            var ruleCount = lines.Length - 2;
            sRules = new Rule[ruleCount];
            sOutputs = new Dictionary<string, int>(1024);

            for (var r = 0; r < ruleCount; ++r)
            {
                ref Rule rule = ref sRules[r];
                var line = lines[r];
                var tokens = line.Split(' ');
                if (tokens.Length != 3)
                {
                    throw new InvalidProgramException($"Invalid rule line '{line}' expected 3 tokens {tokens.Length}");
                }
                if (tokens[1] != "=>")
                {
                    throw new InvalidProgramException($"Invalid rule line '{line}' expected second token to be '=>' {tokens[1]}");
                }
                rule.from = tokens[0];
                rule.to = tokens[2];
            }

            if (lines[ruleCount].Length != 0)
            {
                throw new InvalidProgramException($"Invalid blank rule line '{lines[ruleCount]}' expected empty line {lines[ruleCount].Length}");
            }

            sSource = lines[ruleCount + 1];
            if (sSource.Length == 0)
            {
                throw new InvalidProgramException($"Invalid source line '{sSource}' expected non-empty line");
            }
        }

        public static int UniqueMolecules()
        {
            return CountUniqueMolecules(sSource);
        }

        static int CountUniqueMolecules(string source)
        {
            foreach (var rule in sRules)
            {
                GenerateOutputsFromRule(source, rule);
            }
            return sOutputs.Count;
        }

        static bool GenerateOutputsFromRule(string source, Rule rule)
        {
            var from = rule.from;
            var to = rule.to;
            var match = source.IndexOf(from, 0);
            var startCount = sOutputs.Count;
            do
            {
                if (match >= 0)
                {
                    var output = source.Substring(0, match);
                    output += to;
                    output += source.Substring(match + from.Length);
                    sOutputs[output] = 1;
                    match += from.Length;
                    match = source.IndexOf(from, match);
                }
            }
            while (match >= 0);
            return sOutputs.Count > startCount;
        }

        static bool RuleOutputMatches(Rule rule, string source)
        {
            var from = rule.to;
            var match = source.IndexOf(from, 0);
            return (match >= 0);
        }

        static string ReverseApplyRule(Rule rule, string source)
        {
            var from = rule.to;
            var to = rule.from;
            var match = source.IndexOf(from, 0);
            var output = source.Substring(0, match);
            output += to;
            output += source.Substring(match + from.Length);
            return output;
        }

        public static int GenerateCompound()
        {
            var target = "e";
            var start = sSource;
            var steps = 0;
            var current = start;
            var tries = 0;
            var restarts = 0;
            while (current != target)
            {
                var i = sRandom.Next(sRules.Length);
                var rule = sRules[i];
                ++tries;
                if (RuleOutputMatches(rule, current))
                {
                    ++steps;
                    var reduction = ReverseApplyRule(rule, current);
                    if (reduction == target)
                    {
                        var stepsCount = steps;
                        //Console.WriteLine($"Found it {stepsCount}");
                        return stepsCount;
                    }
                    if (reduction != current)
                    {
                        tries = 0;
                        current = reduction;
                    }
                }
                if (tries >= 1000)
                {
                    ++restarts;
                    if (restarts < 1000)
                    {
                        //Console.WriteLine($"No matches after {tries} tries. Restarting.");
                        tries = 0;
                        current = sSource;
                        steps = 0;
                    }
                    else
                    {
                        Console.WriteLine($"No matches after {restarts} restarts. Quitting.");
                        return -1;
                    }
                }
            }
            return -1;
        }

        public static void Run()
        {
            Console.WriteLine("Day19 : Start");
            _ = new Program("Day19/input.txt", true);
            _ = new Program("Day19/input.txt", false);
            Console.WriteLine("Day19 : End");
        }
    }
}
