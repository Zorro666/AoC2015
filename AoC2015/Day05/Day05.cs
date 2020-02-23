using System;
using System.Collections.Generic;

/*

--- Day 5: Doesn't He Have Intern-Elves For This? ---

Santa needs help figuring out which strings in his text file are naughty or nice.

A nice string is one with all of the following properties:

It contains at least three vowels (aeiou only), like aei, xazegov, or aeiouaeiouaeiou.
It contains at least one letter that appears twice in a row, like xx, abcdde (dd), or aabbccdd (aa, bb, cc, or dd).
It does not contain the strings ab, cd, pq, or xy, even if they are part of one of the other requirements.
For example:

ugknbfddgicrmopn is nice because it has at least three vowels (u...i...o...), a double letter (...dd...), and none of the disallowed substrings.
aaa is nice because it has at least three vowels and a double letter, even though the letters used by different rules overlap.
jchzalrnumimnmhp is naughty because it has no double letter.
haegwjzuvuyypxyu is naughty because it contains the string xy.
dvszwmarrgswjxmb is naughty because it contains only one vowel.

How many strings are nice?

--- Part Two ---

Realizing the error of his ways, Santa has switched to a better model of determining whether a string is naughty or nice. None of the old rules apply, as they are all clearly ridiculous.

Now, a nice string is one with all of the following properties:

It contains a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
It contains at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
For example:

qjhvhtzxzqqjkmpb is nice because is has a pair that appears twice (qj) and a letter that repeats with exactly one letter between them (zxz).
xxyxx is nice because it has a pair that appears twice and a letter that repeats with one between, even though the letters used by each rule overlap.
uurcxstgmygtbstg is naughty because it has a pair (tg) but no repeat with a single letter between them.
ieodomkazucvgmuy is naughty because it has a repeating letter with one between (odo), but no pair that appears twice.
How many strings are nice under these new rules?

*/

namespace Day05
{
    class Program
    {
        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            if (part1)
            {
                var result1 = 0;
                foreach (var line in lines)
                {
                    if (IsNice(line))
                    {
                        ++result1;
                    }
                }
                long expected = 236;
                Console.WriteLine($"Day05 : Result1 {result1}");
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = 0;
                foreach (var line in lines)
                {
                    if (IsNicePart2(line))
                    {
                        ++result2;
                    }
                }
                long expected = 51;
                Console.WriteLine($"Day05 : Result2 {result2}");
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        // at least three vowels (aeiou)
        // at least one letter twice in a row
        // does not contain the strings ab, cd, pq, or xy
        public static bool IsNice(string text)
        {
            var vowelCount = 0;
            char prevLetter = '\0';
            bool doubleLetter = false;

            foreach (var c in text)
            {
                switch (c)
                {
                    case 'a':
                        ++vowelCount;
                        break;
                    case 'e':
                        ++vowelCount;
                        break;
                    case 'i':
                        ++vowelCount;
                        break;
                    case 'o':
                        ++vowelCount;
                        break;
                    case 'u':
                        ++vowelCount;
                        break;
                }

                if (prevLetter == c)
                {
                    doubleLetter = true;
                }

                if ((prevLetter == 'a') && (c == 'b'))
                {
                    return false;
                }
                if ((prevLetter == 'c') && (c == 'd'))
                {
                    return false;
                }
                if ((prevLetter == 'p') && (c == 'q'))
                {
                    return false;
                }
                if ((prevLetter == 'x') && (c == 'y'))
                {
                    return false;
                }
                prevLetter = c;
            }
            if (doubleLetter == false)
            {
                return false;
            }
            return vowelCount >= 3;
        }

        // a pair of any two letters that appears at least twice in the string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like aaa (aa, but it overlaps).
        // at least one letter which repeats with exactly one letter between them, like xyx, abcdefeghi (efe), or even aaa.
        public static bool IsNicePart2(string text)
        {
            char prevPrevLetter = '\0';
            char prevLetter = '\0';
            bool repeatWithSingleGap = false;
            bool pairAtLeastTwice = false;
            var letterPairIndex = new Dictionary<(char c1, char c2), int>(text.Length);

            int index = 0;
            foreach (var c in text)
            {
                if (prevPrevLetter == c)
                {
                    repeatWithSingleGap = true;
                }
                if (prevLetter != '\0')
                {
                    var repeat = (prevLetter, c);
                    if (letterPairIndex.TryGetValue(repeat, out int prevIndex))
                    {
                        if (index > prevIndex + 1)
                        {
                            pairAtLeastTwice = true;
                        }
                    }
                    else
                    {
                        letterPairIndex[repeat] = index;
                    }
                }

                prevPrevLetter = prevLetter;
                prevLetter = c;
                ++index;
            }
            if (repeatWithSingleGap == false)
            {
                return false;
            }
            if (pairAtLeastTwice == false)
            {
                return false;
            }
            return true;
        }

        public static void Run()
        {
            Console.WriteLine("Day05 : Start");
            _ = new Program("Day05/input.txt", true);
            _ = new Program("Day05/input.txt", false);
            Console.WriteLine("Day05 : End");
        }
    }
}
