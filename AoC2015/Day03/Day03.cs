using System;
using System.Collections.Generic;

/*

--- Day 3: Perfectly Spherical Houses in a Vacuum ---

Santa is delivering presents to an infinite two-dimensional grid of houses.

He begins by delivering a present to the house at his starting location, and then an elf at the North Pole calls him via radio and tells him where to move next.Moves are always exactly one house to the north (^), south(v), east(>), or west(<). After each move, he delivers another present to the house at his new location.

However, the elf back at the north pole has had a little too much eggnog, and so his directions are a little off, and Santa ends up visiting some houses more than once.How many houses receive at least one present?

For example:

> delivers presents to 2 houses: one at the starting location, and one to the east.
^>v< delivers presents to 4 houses in a square, including twice to the house at his starting/ending location.
^v^v^v^v^v delivers a bunch of presents to some very lucky children at only 2 houses.

--- Part Two ---

The next year, to speed up the process, Santa creates a robot version of himself, Robo-Santa, to deliver presents with him.

Santa and Robo-Santa start at the same location (delivering two presents to the same starting house), then take turns moving based on instructions from the elf, who is eggnoggedly reading from the same script as the previous year.

This year, how many houses receive at least one present?

For example:

^v delivers presents to 3 houses, because Santa goes north, and then Robo-Santa goes south.
^>v< now delivers presents to 3 houses, and Santa and Robo-Santa end up back where they started.
^v^v^v^v^v now delivers presents to 11 houses, with Santa going one direction and Robo-Santa going the other.

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
                var result1 = HowManyHousesVisited(moves, false);
                long expected = 2565;
                Console.WriteLine($"Day03 : Result1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = HowManyHousesVisited(moves, true);
                long expected = 2639;
                Console.WriteLine($"Day03 : Result2 {result2}");
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static long HowManyHousesVisited(string moves, bool roboSanta)
        {
            int santaX = 0;
            int santaY = 0;
            int roboSantaX = 0;
            int roboSantaY = 0;
            var houses = new Dictionary<(int x, int y), int>(1024);
            int x = santaX;
            int y = santaY;
            houses.TryGetValue((x, y), out int count);
            count += 1;
            houses[(x, y)] = count;

            bool moveSanta = true;
            foreach (var c in moves)
            {
                x = moveSanta ? santaX : roboSantaX;
                y = moveSanta ? santaY : roboSantaY;
                switch (c)
                {
                    case '^':
                        y -= 1;
                        break;
                    case 'v':
                        y += 1;
                        break;
                    case '<':
                        x -= 1;
                        break;
                    case '>':
                        x += 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown move {c}");
                }
                houses.TryGetValue((x, y), out count);
                count += 1;
                houses[(x, y)] = count;
                if (moveSanta)
                {
                    santaX = x;
                    santaY = y;
                }
                else
                {
                    roboSantaX = x;
                    roboSantaY = y;
                }
                moveSanta ^= true;
                if (!roboSanta)
                {
                    moveSanta = true;
                }
            }
            return houses.Count;
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
