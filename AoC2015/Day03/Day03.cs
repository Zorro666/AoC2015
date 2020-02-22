using System;
using System.IO;

/*

--- Day 3: Perfectly Spherical Houses in a Vacuum ---

Santa is delivering presents to an infinite two-dimensional grid of houses.

He begins by delivering a present to the house at his starting location, and then an elf at the North Pole calls him via radio and tells him where to move next.Moves are always exactly one house to the north (^), south(v), east(>), or west(<). After each move, he delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends up visiting some houses more than once.How many houses receive at least one present?

For example:

> delivers presents to 2 houses: one at the starting location, and one to the east.
^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.

*/

namespace Day03
{
    class Program
    {
        Program(string inputFile, bool part1)
        {
            string moves = AoC2015.Program.ReadLines(inputFile)[0];
            if (part1)
            {
                var result1 = HowManyHousesVisited(moves);
                long expected = -123;
                Console.WriteLine($"Day03 : Part1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = -666;
                long expected = -123;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
                Console.WriteLine($"Day03 : Part2 {result2}");
            }
        }

        public static long HowManyHousesVisited(string moves)
        {
            return 0;
        }

        public static void Run()
        {
            Console.WriteLine("Day03 : Start");
            _ = new Program("Day03/input.txt", true);
            _ = new Program("Day03/input.txt", false);
            Console.WriteLine("Day03 : End");
        }
    }
}
