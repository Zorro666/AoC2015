using System;

/*

--- Day 10: Elves Look, Elves Say ---

Today, the Elves are playing a game called look-and-say.They take turns making sequences by reading aloud the previous sequence and using that reading as the next sequence.For example, 211 is read as "one two, two ones", which becomes 1221 (1 2, 2 1s).

Look-and-say sequences are generated iteratively, using the previous value as input for the next step.For each step, take the previous value, and replace each run of digits(like 111) with the number of digits(3) followed by the digit itself(1).

For example:

1 becomes 11 (1 copy of digit 1).
11 becomes 21 (2 copies of digit 1).
21 becomes 1211 (one 2 followed by one 1).
1211 becomes 111221 (one 1, one 2, and two 1s).
111221 becomes 312211 (three 1s, two 2s, and one 1).
Starting with the digits in your puzzle input, apply this process 40 times.What is the length of the result?

Your puzzle input is 1113122113.

*/

namespace Day10
{
    class Program
    {
        private Program(bool part1)
        {
            if (part1)
            {
                var result1 = -666;
                Console.WriteLine($"Day10 : Result1 {result1}");
                int expected = 207;
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = -123;
                Console.WriteLine($"Day10 : Result2 {result2}");
                int expected = 804;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static string LookSee(string source)
        {
            string reply = source;
            return reply;
        }

        public static void Run()
        {
            Console.WriteLine("Day10 : Start");
            _ = new Program(true);
            _ = new Program(false);
            Console.WriteLine("Day10 : End");
        }
    }
}
