using System;
using System.Collections.Generic;

/*

--- Day 17: No Such Thing as Too Much ---

The elves bought too much eggnog again - 150 liters this time. To fit it all into your refrigerator, you'll need to move it into smaller containers. You take an inventory of the capacities of the available containers.

For example, suppose you have containers of size 20, 15, 10, 5, and 5 liters. If you need to store 25 liters, there are four ways to do it:

15 and 10
20 and 5 (the first 5)
20 and 5 (the second 5)
15, 5, and 5

Filling all containers entirely, how many different combinations of containers can exactly fit all 150 liters of eggnog?

Your puzzle answer was 1304.

--- Part Two ---

While playing with all the containers in the kitchen, another load of eggnog arrives! The shipping and receiving department is requesting as many containers as you can spare.

Find the minimum number of containers that can exactly fit all 150 liters of eggnog. 
How many different ways can you fill that number of containers and still hold exactly 150 litres?

In the example above, the minimum number of containers was two. There were three ways to use that many containers, and so the answer there would be 3.

*/

namespace Day17
{
    class Program
    {
        static int[] sContainers;
        static int sContainerCount;
        static List<int> sMatches;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            if (part1)
            {
                var result1 = CountCombinations(lines, 150);
                Console.WriteLine($"Day17 Result1:{result1}");
                var expected = 1304;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = CountCombinationsMin(lines, 150);
                Console.WriteLine($"Day17 Result2:{result2}");
                var expected = 18;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static int CountCombinations(string[] lines, int target)
        {
            sMatches = new List<int>();
            sContainerCount = lines.Length;
            sContainers = new int[sContainerCount];
            for (var i = 0; i < sContainerCount; ++i)
            {
                sContainers[i] = int.Parse(lines[i]);
            }
            int currentTotal = 0;
            var usedContainers = new bool[sContainerCount];
            CountMatchTarget(0, target, ref usedContainers, ref currentTotal);
            return sMatches.Count;
        }

        public static int CountCombinationsMin(string[] lines, int target)
        {
            sMatches = new List<int>();
            sContainerCount = lines.Length;
            sContainers = new int[sContainerCount];
            for (var i = 0; i < sContainerCount; ++i)
            {
                sContainers[i] = int.Parse(lines[i]);
            }
            int currentTotal = 0;
            var usedContainers = new bool[sContainerCount];
            CountMatchTarget(0, target, ref usedContainers, ref currentTotal);
            var minContainersUsed = int.MaxValue;
            foreach (var match in sMatches)
            {
                minContainersUsed = Math.Min(minContainersUsed, match);
            }
            var count = 0;
            foreach (var match in sMatches)
            {
                if (minContainersUsed == match)
                {
                    ++count;
                }
            }
            return count;
        }

        static void CountMatchTarget(int start, int target, ref bool[] usedContainers, ref int currentTotal)
        {
            for (var i = start; i < sContainerCount; ++i)
            {
                if (usedContainers[i] == false)
                {
                    usedContainers[i] = true;
                    currentTotal += sContainers[i];
                    if (currentTotal == target)
                    {
                        var numContainersUsed = 0;
                        for (var c = 0; c < sContainerCount; ++c)
                        {
                            if (usedContainers[c])
                            {
                                ++numContainersUsed;
                            }
                        }
                        sMatches.Add(numContainersUsed);
                    }
                    else if (currentTotal < target)
                    {
                        CountMatchTarget(i + 1, target, ref usedContainers, ref currentTotal);
                    }
                    currentTotal -= sContainers[i];
                    usedContainers[i] = false;
                }
            }
        }

        public static void Run()
        {
            Console.WriteLine("Day17 : Start");
            _ = new Program("Day17/input.txt", true);
            _ = new Program("Day17/input.txt", false);
            Console.WriteLine("Day17 : End");
        }
    }
}
