using System;

/*

--- Day 2: I Was Told There Would Be No Math ---

The elves are running low on wrapping paper, and so they need to submit an order for more. They have a list of the dimensions (length l, width w, and height h) of each present, and only want to order exactly as much as they need.

Fortunately, every present is a box (a perfect right rectangular prism), which makes calculating the required wrapping paper for each gift a little easier: find the surface area of the box, which is 2*l*w + 2*w*h + 2*h*l. The elves also need a little extra paper for each present: the area of the smallest side.

For example:

A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
All numbers in the elves' list are in feet. How many total square feet of wrapping paper should they order?

--- Part Two ---

The elves are also running low on ribbon. Ribbon is all the same width, so they only have to worry about the length they need to order, which they would again like to be exact.

The ribbon required to wrap a present is the shortest distance around its sides, or the smallest perimeter of any one face. Each present also requires a bow made out of ribbon as well; the feet of ribbon required for the perfect bow is equal to the cubic feet of volume of the present. Don't ask how they tie the bow, though; they'll never tell.

For example:

A present with dimensions 2x3x4 requires 2+2+3+3 = 10 feet of ribbon to wrap the present plus 2*3*4 = 24 feet of ribbon for the bow, for a total of 34 feet.
A present with dimensions 1x1x10 requires 1+1+1+1 = 4 feet of ribbon to wrap the present plus 1*1*10 = 10 feet of ribbon for the bow, for a total of 14 feet.

How many total feet of ribbon should they order?
*/

namespace Day02
{
    class Program
    {
        static int sDimensionsCount = 0;
        static (long l, long w, long h)[] sDimensions;

        private Program(string inputFile, bool part1)
        {
            var inputs = AoC2015.Program.ReadLines(inputFile);
            ParseInput(inputs);
            if (part1)
            {
                var result1 = ComputeWrappingPaper();
                long expected = 1588178;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
                Console.WriteLine($"Day02 : Result1 {result1}");
            }
            else
            {
                var result2 = ComputeRibbon();
                Console.WriteLine($"Day02 : Result2 {result2}");
                long expected = 3783758;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
                return;
            }
        }

        public static void ParseInput(string[] source)
        {
            int i = 0;
            sDimensions = new (long l, long w, long h)[source.Length];
            sDimensionsCount = 0;
            foreach (var line in source)
            {
                var tokens = line.Split('x');
                if (tokens.Length != 3)
                {
                    throw new InvalidProgramException($"Invalid line {line} does not have 3 tokens {tokens.Length}");

                }
                sDimensions[i].l = long.Parse(tokens[0]);
                sDimensions[i].w = long.Parse(tokens[1]);
                sDimensions[i].h = long.Parse(tokens[2]);
                ++i;
            }
            sDimensionsCount = i;
        }

        public static long ComputeRibbon()
        {
            long total = 0;
            for (var i = 0; i < sDimensionsCount; ++i)
            {
                var dimension = sDimensions[i];

                var l = dimension.l;
                var w = dimension.w;
                var h = dimension.h;
                var minPerimeter = 2 * (l + w);
                minPerimeter = Math.Min(minPerimeter, 2 * (w + h));
                minPerimeter = Math.Min(minPerimeter, 2 * (h + l));
                total += minPerimeter;
                // + bow = l * w * h
                total += l * w * h;
            }
            return total;
        }

        public static long ComputeWrappingPaper()
        {
            long total = 0;
            for (var i = 0; i < sDimensionsCount; ++i)
            {
                var dimension = sDimensions[i];

                // 2*l*w + 2*w*h + 2*h*l. 
                // + the area of the smallest side.
                var l = dimension.l;
                var w = dimension.w;
                var h = dimension.h;
                total += 2 * (l * w + w * h + h * l);
                var minArea = l * w;
                minArea = Math.Min(minArea, w * h);
                minArea = Math.Min(minArea, h * l);
                total += minArea;
            }
            return total;
        }

        public static void Run()
        {
            Console.WriteLine("Day02 : Start");
            _ = new Program("Day02/input.txt", true);
            _ = new Program("Day02/input.txt", false);
            Console.WriteLine("Day02 : End");
        }
    }
}
