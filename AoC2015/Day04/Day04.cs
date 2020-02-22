using System;
using System.Security.Cryptography;
using System.Text;

/*

--- Day 4: The Ideal Stocking Stuffer ---

Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

For example:

If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....
Your puzzle input is yzbqklnj.

*/

namespace Day04
{
    class Program
    {
        Program(bool part1)
        {
            if (part1)
            {
                var result1 = FindFiveZeroHash("yzbqklnj");
                long expected = 282749;
                Console.WriteLine($"Day04 : Result1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = 0;
                long expected = 2639;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static long FindFiveZeroHash(string secretKey)
        {
            long result = 1;
            using (var md5 = MD5.Create())
            {
                while (result < 10 * 1000 * 1000)
                {
                    var totalKey = $"{secretKey}{result}";
                    byte[] inputBytes = Encoding.ASCII.GetBytes(totalKey);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // First five nibbles must be '0'
                    if ((hashBytes[0] == 0) && (hashBytes[1] == 0) && ((hashBytes[2] & 0xF0) == 0))
                    {
                        if ((hashBytes[2] & 0xF) != 0)
                        {
                            return result;
                        }
                    }
                    ++result;
                }
            }
            throw new InvalidProgramException($"No match found {result}");
        }

        public static void Run()
        {
            Console.WriteLine("Day04 : Start");
            _ = new Program(true);
            _ = new Program(false);
            Console.WriteLine("Day04 : End");
        }
    }
}