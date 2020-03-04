using System;

/*

--- Day 14: Reindeer Olympics ---

This year is the Reindeer Olympics! Reindeer can fly at high speeds, but must rest occasionally to recover their energy. Santa would like to know which of his reindeer is fastest, and so he has them race.

Reindeer can only either be flying (always at their top speed) or resting (not moving at all), and always spend whole seconds in either state.

For example, suppose you have the following Reindeer:

Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.

After one second, Comet has gone 14 km, while Dancer has gone 16 km. After ten seconds, Comet has gone 140 km, while Dancer has gone 160 km. On the eleventh second, Comet begins resting (staying at 140 km), and Dancer continues on for a total distance of 176 km. On the 12th second, both reindeer are resting. 
They continue to rest until the 138th second, when Comet flies for another ten seconds. 
On the 174th second, Dancer flies for another 11 seconds.

In this example, after the 1000th second, both reindeer are resting, and Comet is in the lead at 1120 km (poor Dancer has only gotten 1056 km by that point). So, in this situation, Comet would win (if the race ended at 1000 seconds).

Given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, what distance has the winning reindeer traveled?

Your puzzle answer was 2640.

--- Part Two ---

Seeing how reindeer move in bursts, Santa decides he's not pleased with the old scoring system.

Instead, at the end of each second, he awards one point to the reindeer currently in the lead. (If there are multiple reindeer tied for the lead, they each get one point.) He keeps the traditional 2503 second time limit, of course, as doing otherwise would be entirely ridiculous.

Given the example reindeer from above, after the first second, Dancer is in the lead and gets one point. He stays in the lead until several seconds into Comet's second burst: after the 140th second, Comet pulls into the lead and gets his first point. Of course, since Dancer had been in the lead for the 139 seconds before that, he has accumulated 139 points by the 140th second.

After the 1000th second, Dancer has accumulated 689 points, while poor Comet, our old champion, only has 312. So, with the new scoring system, Dancer would win (if the race ended at 1000 seconds).

Again given the descriptions of each reindeer (in your puzzle input), after exactly 2503 seconds, how many points does the winning reindeer have?

*/

namespace Day14
{
    class Program
    {
        struct Rule
        {
            public string name;
            public int speed;
            public int speedTime;
            public int restTime;
        };

        static Rule[] sRules;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);
            if (part1)
            {
                var result1 = WinningDistance(2503);
                Console.WriteLine($"Day14 Result1:{result1}");
                var expected = 2640;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = -123;
                Console.WriteLine($"Day14 Result2:{result2}");
                var expected = -666;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseInput(string[] lines)
        {
            sRules = new Rule[lines.Length];
            int i = 0;
            foreach (var line in lines)
            {
                var tokens = line.Split(' ');
                if (tokens.Length != 15)
                {
                    throw new InvalidProgramException($"Invalid line '{line}' expected 15 tokens {tokens.Length}");
                }
                ref Rule rule = ref sRules[i];
                rule.name = tokens[0];
                rule.speed = int.Parse(tokens[3]);
                rule.speedTime = int.Parse(tokens[6]);
                rule.restTime = int.Parse(tokens[13]);
                ++i;
            }
        }

        public static int Distance(string reindeer, int time)
        {
            foreach (var rule in sRules)
            {
                if (rule.name == reindeer)
                {
                    return Distance(rule.speed, rule.speedTime, rule.restTime, time);
                }
            }
            return -1;
        }

        static int Distance(int speed, int speedTime, int restTime, int time)
        {
            var distance = 0;
            var cycleTime = speedTime + restTime;
            var fullCycles = time / cycleTime;
            distance += fullCycles * speedTime * speed;
            var remainderTime = time - fullCycles * cycleTime;
            remainderTime = Math.Min(remainderTime, speedTime);
            distance += remainderTime * speed;

            return distance;
        }

        public static int WinningDistance(int time)
        {
            var maxDistance = int.MinValue;
            foreach (var rule in sRules)
            {
                var distance = Distance(rule.speed, rule.speedTime, rule.restTime, time);
                maxDistance = Math.Max(maxDistance, distance);
            }
            return maxDistance;
        }

        public static void Run()
        {
            Console.WriteLine("Day14 : Start");
            _ = new Program("Day14/input.txt", true);
            _ = new Program("Day14/input.txt", false);
            Console.WriteLine("Day14 : End");
        }
    }
}
