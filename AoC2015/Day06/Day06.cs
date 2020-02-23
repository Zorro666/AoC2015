using System;

/*

--- Day 6: Probably a Fire Hazard ---

Because your neighbors keep defeating you in the holiday house decorating contest year after year, you've decided to deploy one million lights in a 1000x1000 grid.

Furthermore, because you've been especially nice this year, Santa has mailed you instructions on how to display the ideal lighting configuration.

Lights in your grid are numbered from 0 to 999 in each direction; the lights at each corner are at 0,0, 0,999, 999,999, and 999,0. The instructions include whether to turn on, turn off, or toggle various inclusive ranges given as coordinate pairs. Each coordinate pair represents opposite corners of a rectangle, inclusive; a coordinate pair like 0,0 through 2,2 therefore refers to 9 lights in a 3x3 square. The lights all start turned off.

To defeat your neighbors this year, all you have to do is set up your lights by doing the instructions Santa sent you in order.

For example:

turn on 0,0 through 999,999 would turn on (or leave on) every light.
toggle 0,0 through 999,0 would toggle the first line of 1000 lights, turning off the ones that were on, and turning on the ones that were off.
turn off 499,499 through 500,500 would turn off (or leave off) the middle four lights.
After following the instructions, how many lights are lit?

--- Part Two ---

You just finish implementing your winning light pattern when you realize you mistranslated Santa's message from Ancient Nordic Elvish.

The light grid you bought actually has individual brightness controls; each light can have a brightness of zero or more. The lights all start at zero.

The phrase turn on actually means that you should increase the brightness of those lights by 1.

The phrase turn off actually means that you should decrease the brightness of those lights by 1, to a minimum of zero.

The phrase toggle actually means that you should increase the brightness of those lights by 2.

What is the total brightness of all lights combined after following Santa's instructions?

For example:

turn on 0,0 through 0,0 would increase the total brightness by 1.
toggle 0,0 through 999,999 would increase the total brightness by 2000000.

*/

namespace Day06
{
    class Program
    {
        static readonly int GRID_SIZE = 1000;
        private Program(string inputFile, bool part1)
        {
            var instructions = AoC2015.Program.ReadLines(inputFile);
            if (part1)
            {
                var result1 = HowManyLights(instructions, false);
                long expected = 543903;
                Console.WriteLine($"Day06 : Result1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = HowManyLights(instructions, true);
                long expected = 14687245;
                Console.WriteLine($"Day06 : Result2 {result2}");
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static long HowManyLights(string[] instructions, bool useBrightness)
        {
            var lights = new int[GRID_SIZE, GRID_SIZE];
            for (var y = 0; y < GRID_SIZE; ++y)
            {
                for (var x = 0; x < GRID_SIZE; ++x)
                {
                    lights[x, y] = 0;
                }
            }

            foreach (var line in instructions)
            {
                // turn <on>/<off> <x1>,<y1> through <x2,y2>
                // toggle <x1>,<y1> through <x2,y2>
                var tokens = line.Split(' ');
                var rangeStart = -1;
                var command = tokens[0];
                int op = -2;
                if (command == "turn")
                {
                    rangeStart = 2;
                    var operation = tokens[1];
                    if (operation == "on")
                    {
                        op = 1;
                    }
                    else if (operation == "off")
                    {
                        op = 0;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Unknown operation {operation}");
                    }
                }
                else if (command == "toggle")
                {
                    rangeStart = 1;
                    op = 2;
                }
                else
                {
                    throw new InvalidOperationException($"Unknown command {command}");

                }
                var xy1Tokens = tokens[rangeStart].Split(',');
                var x1 = int.Parse(xy1Tokens[0]);
                var y1 = int.Parse(xy1Tokens[1]);
                if (tokens[rangeStart + 1] != "through")
                {
                    throw new InvalidOperationException($"Failed xy range parsign {line} expected 'through' got {tokens[rangeStart + 1]}");
                }
                var xy2Tokens = tokens[rangeStart + 2].Split(',');
                var x2 = int.Parse(xy2Tokens[0]);
                var y2 = int.Parse(xy2Tokens[1]);
                for (var y = y1; y <= y2; ++y)
                {
                    for (var x = x1; x <= x2; ++x)
                    {
                        if (op == 0)
                        {
                            if (useBrightness)
                            {
                                lights[x, y] = lights[x, y] - 1;
                                if (lights[x, y] < 0)
                                {
                                    lights[x, y] = 0;
                                }
                            }
                            else
                            {
                                lights[x, y] = 0;
                            }
                        }
                        else if (op == 1)
                        {
                            if (useBrightness)
                            {
                                lights[x, y] = lights[x, y] + 1;
                            }
                            else
                            {
                                lights[x, y] = 1;
                            }
                        }
                        else if (op == 2)
                        {
                            if (useBrightness)
                            {
                                lights[x, y] = lights[x, y] + 2;
                            }
                            else
                            {
                                if (lights[x, y] == 0)
                                {
                                    lights[x, y] = 1;
                                }
                                else
                                {
                                    lights[x, y] = 0;
                                }
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Invalid op {op}");
                        }
                    }
                }
            }
            var brightness = 0;
            for (var y = 0; y < GRID_SIZE; ++y)
            {
                for (var x = 0; x < GRID_SIZE; ++x)
                {
                    brightness += lights[x, y];
                }
            }
            return brightness;
        }

        public static void Run()
        {
            Console.WriteLine("Day06 : Start");
            _ = new Program("Day06/input.txt", true);
            _ = new Program("Day06/input.txt", false);
            Console.WriteLine("Day06 : End");
        }
    }
}
