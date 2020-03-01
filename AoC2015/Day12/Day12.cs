using System;
using System.Collections.Generic;
using System.Text.Json;

/*

--- Day 12: JSAbacusFramework.io ---

Santa's Accounting-Elves need help balancing the books after a recent order. Unfortunately, their accounting software uses a peculiar storage format. That's where you come in.

They have a JSON document which contains a variety of things: arrays ([1,2,3]), objects ({"a":1, "b":2}), numbers, and strings. Your first job is to simply find all of the numbers throughout the document and add them together.

For example:

[1,2,3] and {"a":2,"b":4} both have a sum of 6.
[[[3]]] and {"a":{"b":4},"c":-1} both have a sum of 3.
{"a":[-1,1]} and [-1,{"a":1}] both have a sum of 0.
[] and {} both have a sum of 0.
You will not encounter any strings containing numbers.

What is the sum of all numbers in the document?

Your puzzle answer was 119433.

--- Part Two ---

Uh oh - the Accounting-Elves have realized that they double-counted everything red.

Ignore any object (and all of its children) which has any property with the value "red". Do this only for objects ({...}), not arrays ([...]).

[1,2,3] still has a sum of 6.
[1,{"c":"red","b":2},3] now has a sum of 4, because the middle object is ignored.
{"d":"red","e":[1,2,3,4],"f":5} now has a sum of 0, because the entire structure is ignored.
[1,"red",5] has a sum of 6, because "red" in an array has no effect.

*/

namespace Day12
{
    class Program
    {
        static List<int> sValues;
        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);

            if (part1)
            {
                ParseLines(lines, false);
                var result1 = GetSum();
                Console.WriteLine($"Day12 : Part1 {result1}");
                var expected = 119433;
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                ParseLines(lines, true);
                var result2 = GetSum();
                Console.WriteLine($"Day12 : Part2 {result2}");
                var expected = 68466;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseLines(string[] lines, bool ignoreRed)
        {
            sValues = new List<int>();
            string fullLine = "";
            foreach (var line in lines)
            {
                fullLine += line;
            }
            using (JsonDocument document = JsonDocument.Parse(fullLine))
            {
                var root = document.RootElement;
                CountIntegers(root, ignoreRed);
            }
        }

        static void CountIntegers(JsonElement element, bool ignoreRed)
        {
            if (element.ValueKind == JsonValueKind.Number)
            {
                var value = element.GetInt32();
                sValues.Add(value);
            }
            else if (element.ValueKind == JsonValueKind.Object)
            {
                bool hasRed = false;
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    if ((property.Value.ValueKind == JsonValueKind.String) && (property.Value.GetString() == "red"))
                    {
                        //Console.WriteLine($"Found red {property.Name}");
                        hasRed = true;
                    }
                }
                if (!hasRed || !ignoreRed)
                {
                    foreach (JsonProperty property in element.EnumerateObject())
                    {
                        JsonElement subElement = property.Value;
                        CountIntegers(subElement, ignoreRed);
                    }
                }
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement subElement in element.EnumerateArray())
                {
                    CountIntegers(subElement, ignoreRed);
                }
            }
        }

        public static int GetSum()
        {
            int sum = 0;
            foreach (var value in sValues)
            {
                sum += value;
            }
            return sum;
        }

        public static void Run()
        {
            Console.WriteLine("Day12 : Start");
            _ = new Program("Day12/input.txt", true);
            _ = new Program("Day12/input.txt", false);
            Console.WriteLine("Day12 : End");
        }
    }
}
