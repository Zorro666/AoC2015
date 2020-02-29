using System;

/*

--- Day 11: Corporate Policy ---

Santa's previous password expired, and he needs help choosing a new one.

To help him remember his new password after the old one expires, Santa has devised a method of coming up with a password based on the previous one. Corporate policy dictates that passwords must be exactly eight lowercase letters (for security reasons), so he finds his new password by incrementing his old password string repeatedly until it is valid.

Incrementing is just like counting with numbers: xx, xy, xz, ya, yb, and so on. Increase the rightmost letter one step; if it was z, it wraps around to a, and repeat with the next letter to the left until one doesn't wrap around.

Unfortunately for Santa, a new Security-Elf recently started, and he has imposed some additional password requirements:

Passwords must include one increasing straight of at least three letters, like abc, bcd, cde, and so on, up to xyz. They cannot skip letters; abd doesn't count.
Passwords may not contain the letters i, o, or l, as these letters can be mistaken for other characters and are therefore confusing.
Passwords must contain at least two different, non-overlapping pairs of letters, like aa, bb, or zz.
For example:

hijklmmn meets the first requirement (because it contains the straight hij) but fails the second requirement requirement (because it contains i and l).
abbceffg meets the third requirement (because it repeats bb and ff) but fails the first requirement.
abbcegjk fails the third requirement, because it only has one double letter (bb).
The next password after abcdefgh is abcdffaa.
The next password after ghijklmn is ghjaabcc, because you eventually skip all the passwords that start with ghi..., since i is not allowed.
Given Santa's current password (your puzzle input), what should his next password be?

Your puzzle input is cqjxjnds.

--- Part Two ---

Santa's password expired again. What's the next one?

*/

namespace Day11
{
    class Program
    {
        private Program(string inputFile, bool part1)
        {
            string input = "cqjxjnds";
            if (part1)
            {
                var result1 = NextValidPassword(input);
                Console.WriteLine($"Day11 : Result1 {result1}");
                string expected = "cqjxxyzz";
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = NextValidPassword(NextValidPassword(input));
                Console.WriteLine($"Day11 : Result2 {result2}");
                string expected = "cqkaabcc";
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static bool IsValidPassword(char[] password)
        {
            char prevPair = '\0';
            char prevPrevChar = '\0';
            char prevChar = '\0';
            bool foundTwoPairs = false;
            bool foundStraight = false;
            foreach (var c in password)
            {
                if ((c == prevChar + 1) && (prevChar == prevPrevChar + 1))
                {
                    foundStraight = true;
                }
                if (c == prevChar)
                {
                    if (c != prevPair)
                    {
                        if (prevPair != '\0')
                        {
                            foundTwoPairs = true;
                        }
                        prevPair = c;
                    }
                }
                if (c == 'i')
                {
                    return false;
                }
                if (c == 'o')
                {
                    return false;
                }
                if (c == 'l')
                {
                    return false;
                }
                prevPrevChar = prevChar;
                prevChar = c;
            }
            if (!foundTwoPairs)
            {
                return false;
            }
            if (!foundStraight)
            {
                return false;
            }
            return true;
        }

        // 8-characters
        public static string NextValidPassword(string password)
        {
            int count = 0;
            char[] newPassword = password.ToCharArray();
            do
            {
                int increment = 1;
                for (int index = 7; index >= 0; --index)
                {
                    char c = newPassword[index];
                    c = (char)(c + increment);
                    increment = 0;
                    if (c > 'z')
                    {
                        c = 'a';
                        increment = 1;
                    }
                    newPassword[index] = c;
                    if (increment == 0)
                    {
                        break;
                    }
                }
            }
            while (!IsValidPassword(newPassword));
            return new string(newPassword);
        }

        public static void Run()
        {
            Console.WriteLine("Day11 : Start");
            _ = new Program("Day11/input.txt", true);
            _ = new Program("Day11/input.txt", false);
            Console.WriteLine("Day11 : End");
        }
    }
}

