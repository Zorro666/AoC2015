using System;

/*

--- Day 18: Like a GIF For Your Yard ---

After the million lights incident, the fire code has gotten stricter: now, at most ten thousand lights are allowed. You arrange them in a 100x100 grid.

Never one to let you down, Santa again mails you instructions on the ideal lighting configuration. With so few lights, he says, you'll have to resort to animation.

Start by setting your lights to the included initial configuration (your puzzle input). A # means "on", and a . means "off".

Then, animate your grid in steps, where each step decides the next configuration based on the current one. Each light's next state (either on or off) depends on its current state and the current states of the eight lights adjacent to it (including diagonals). Lights on the edge of the grid might have fewer than eight neighbors; the missing ones always count as "off".

For example, in a simplified 6x6 grid, the light marked A has the neighbors numbered 1 through 8, and the light marked B, which is on an edge, only has the neighbors marked 1 through 5:

1B5...
234...
......
..123.
..8A4.
..765.
The state a light should have next is based on its current state (on or off) plus the number of neighbors that are on:

A light which is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
A light which is off turns on if exactly 3 neighbors are on, and stays off otherwise.
All of the lights update simultaneously; they all consider the same current state before moving to the next.

Here's a few steps from an example configuration of another 6x6 grid:

Initial state:
.#.#.#
...##.
#....#
..#...
#.#..#
####..

After 1 step:
..##..
..##.#
...##.
......
#.....
#.##..

After 2 steps:
..###.
......
..###.
......
.#....
.#....

After 3 steps:
...#..
......
...#..
..##..
......
......

After 4 steps:
......
......
..##..
..##..
......
......
After 4 steps, this example has four lights on.

In your grid of 100x100 lights, given your initial configuration, how many lights are on after 100 steps?

Your puzzle answer was 814.

--- Part Two ---

You flip the instructions over; Santa goes on to point out that this is all just an implementation of Conway's Game of Life. 
At least, it was, until you notice that something's wrong with the grid of lights you bought: four lights, one in each corner, are stuck on and can't be turned off. 
The example above will actually run like this:

Initial state:
##.#.#
...##.
#....#
..#...
#.#..#
####.#

After 1 step:
#.##.#
####.#
...##.
......
#...#.
#.####

After 2 steps:
#..#.#
#....#
.#.##.
...##.
.#..##
##.###

After 3 steps:
#...##
####.#
..##.#
......
##....
####.#

After 4 steps:
#.####
#....#
...#..
.##...
#.....
#.#..#

After 5 steps:
##.###
.##..#
.##...
.##...
#.#...
##...#
After 5 steps, this example now has 17 lights on.

In your grid of 100x100 lights, given your initial configuration, but with the four corners always in the on state, how many lights are on after 100 steps?

*/

namespace Day18
{
    class Program
    {
        static int[,] sMap;
        static int[,] sNeighbours;
        static int sWidth;
        static int sHeight;
        static bool sCornersOn;

        Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            if (part1)
            {
                ParseMap(lines, false);
                Simulate(100);
                var result1 = LightsOnCount();
                Console.WriteLine($"Day18 : Result1 {result1}");
                var expected = 814;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                ParseMap(lines, true);
                Simulate(100);
                var result2 = LightsOnCount();
                Console.WriteLine($"Day18 : Result2 {result2}");
                var expected = 924;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseMap(string[] lines, bool cornersOn)
        {
            sHeight = lines.Length;
            sWidth = lines[0].Length;
            sMap = new int[sWidth, sHeight];
            sNeighbours = new int[sWidth, sHeight];
            sCornersOn = cornersOn;
            for (var y = 0; y < sHeight; ++y)
            {
                var line = lines[y];
                if (line.Length != sWidth)
                {
                    throw new InvalidProgramException($"Inconsistent Width[{y}] {line.Length} expected {sWidth}");
                }

                for (var x = 0; x < sWidth; ++x)
                {
                    int value;
                    char c = line[x];
                    if (c == '#')
                    {
                        value = 1;
                    }
                    else if (c == '.')
                    {
                        value = 0;
                    }
                    else
                    {
                        throw new InvalidProgramException($"Invalid character [{x},{y}] '{c}'");
                    }
                    sMap[x, y] = value;
                }
            }
            CornersOn();
        }

        static void CornersOn()
        {
            if (sCornersOn)
            {
                sMap[0, 0] = 1;
                sMap[sWidth - 1, 0] = 1;
                sMap[sWidth - 1, sHeight - 1] = 1;
                sMap[0, sHeight - 1] = 1;
            }
        }

        public static void Simulate(int numSteps)
        {
            for (var s = 0; s < numSteps; ++s)
            {
                for (var y = 0; y < sHeight; ++y)
                {
                    for (var x = 0; x < sWidth; ++x)
                    {
                        sNeighbours[x, y] = 0;
                    }
                }
                CornersOn();
                for (var y = 0; y < sHeight; ++y)
                {
                    for (var x = 0; x < sWidth; ++x)
                    {
                        var neighbourCount = 0;
                        neighbourCount += GetCell(x - 1, y - 1);
                        neighbourCount += GetCell(x + 0, y - 1);
                        neighbourCount += GetCell(x + 1, y - 1);

                        neighbourCount += GetCell(x - 1, y + 0);
                        neighbourCount += GetCell(x + 1, y + 0);

                        neighbourCount += GetCell(x - 1, y + 1);
                        neighbourCount += GetCell(x + 0, y + 1);
                        neighbourCount += GetCell(x + 1, y + 1);

                        sNeighbours[x, y] = neighbourCount;
                    }
                }
                for (var y = 0; y < sHeight; ++y)
                {
                    for (var x = 0; x < sWidth; ++x)
                    {
                        var value = GetCell(x, y);
                        var neighbourCount = sNeighbours[x, y];
                        int newValue;
                        // if light is on stays on when 2 or 3 neighbors are on, and turns off otherwise.
                        if (value == 1)
                        {
                            if ((neighbourCount == 2) || (neighbourCount == 3))
                            {
                                newValue = 1;
                            }
                            else
                            {
                                newValue = 0;
                            }
                        }
                        // if light is off turns on if exactly 3 neighbors are on, and stays off otherwise.
                        else if (value == 0)
                        {
                            if (neighbourCount == 3)
                            {
                                newValue = 1;
                            }
                            else
                            {
                                newValue = 0;
                            }
                        }
                        else
                        {
                            throw new InvalidProgramException($"Invalid map value [{x},{y}] '{value}'");
                        }
                        sMap[x, y] = newValue;
                    }
                }
                CornersOn();
            }
        }

        static int GetCell(int x, int y)
        {
            if ((x < 0) || (x >= sWidth))
            {
                return 0;
            }
            if ((y < 0) || (y >= sHeight))
            {
                return 0;
            }
            return sMap[x, y];
        }

        public static string[] GetMap()
        {
            string[] output = new string[sHeight];
            for (var y = 0; y < sHeight; ++y)
            {
                string line = "";
                for (var x = 0; x < sWidth; ++x)
                {
                    char c;
                    var value = sMap[x, y];
                    if (value == 1)
                    {
                        c = '#';
                    }
                    else if (value == 0)
                    {
                        c = '.';
                    }
                    else
                    {
                        throw new InvalidProgramException($"Invalid map value [{x},{y}] '{value}'");
                    }
                    line += c;
                }
                output[y] = line;
            }
            return output;
        }

        public static int LightsOnCount()
        {
            var count = 0;
            for (var y = 0; y < sHeight; ++y)
            {
                for (var x = 0; x < sWidth; ++x)
                {
                    count += GetCell(x, y);
                }
            }
            return count;
        }

        public static void Run()
        {
            Console.WriteLine("Day18 : Start");
            _ = new Program("Day18/input.txt", true);
            _ = new Program("Day18/input.txt", false);
            Console.WriteLine("Day18 : End");
        }
    }
}
