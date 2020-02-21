using System;
using System.IO;

/*

--- Day 2: I Was Told There Would Be No Math ---

The elves are running low on wrapping paper, and so they need to submit an order for more. They have a list of the dimensions (length l, width w, and height h) of each present, and only want to order exactly as much as they need.

Fortunately, every present is a box (a perfect right rectangular prism), which makes calculating the required wrapping paper for each gift a little easier: find the surface area of the box, which is 2*l*w + 2*w*h + 2*h*l. The elves also need a little extra paper for each present: the area of the smallest side.

For example:

A present with dimensions 2x3x4 requires 2*6 + 2*12 + 2*8 = 52 square feet of wrapping paper plus 6 square feet of slack, for a total of 58 square feet.
A present with dimensions 1x1x10 requires 2*1 + 2*10 + 2*10 = 42 square feet of wrapping paper plus 1 square foot of slack, for a total of 43 square feet.
All numbers in the elves' list are in feet. How many total square feet of wrapping paper should they order?

*/

namespace Day02
{
    class Program
    {
        private Program(string inputFile, bool part1)
        {
            var inputs = ReadInput(inputFile);
            if (part1)
            {
                var result1 = -666;
                Console.WriteLine($"Day02 : Result1 {result1}");
            }
            else
            {
                var result2 = -666;
                Console.WriteLine($"Day02 : Result2 {result2}");
                return;
            }
        }

        private string[] ReadInput(string inputFile)
        {
            var source = File.ReadAllLines(inputFile);
            return source;
        }

        static private int[] ConvertSourceStringToInts(string source)
        {
            var sourceElements = source.Split(',');
            var data = new int[sourceElements.Length];
            var index = 0;
            foreach (var element in sourceElements)
            {
                data[index] = Int32.Parse(element);
                index++;
            }
            return data;
        }

        static private string ConvertIntsToResultString(int[] data)
        {
            var sourceElements = new string[data.Length];
            int index = 0;
            foreach (var element in data)
            {
                sourceElements[index] = element.ToString();
                index++;
            }
            var result = String.Join(',', sourceElements);
            return result;
        }

        static public string RunProgram(string source, int address1, int address2)
        {
            var data = ConvertSourceStringToInts(source);
            // fix 1202 alarm before running the program, 
            // replace position 1 with the value 12 
            // replace position 2 with the value 2.
            if (address1 >= 0)
            {
                data[1] = address1;
                if (address2 < 0)
                {
                    throw new ArgumentException("Address2 must be < 0 if address1 is < 0", "address2");
                }
                data[2] = address2;
            }
            int pc = 0;
            var opcode = data[pc++];
            while (opcode != 99)
            {
                if (pc >= data.Length)
                {
                    throw new InvalidDataException($"Invalid pc:{pc}");
                }
                var param1Index = data[pc++];
                var param2Index = data[pc++];
                var outputIndex = data[pc++];
                var param1 = data[param1Index];
                var param2 = data[param2Index];
                int output;
                if (opcode == 1)
                {
                    output = param1 + param2;
                }
                else if (opcode == 2)
                {
                    output = param1 * param2;
                }
                else
                {
                    throw new InvalidDataException($"Unknown opcode:{opcode}");
                }
                data[outputIndex] = output;
                opcode = data[pc++];
            };

            var result = ConvertIntsToResultString(data);
            return result;
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
