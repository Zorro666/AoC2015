using System;
using System.Collections.Generic;

/*

--- Day 24: It Hangs in the Balance ---

It's Christmas Eve, and Santa is loading up the sleigh for this year's deliveries. 
However, there's one small problem: he can't get the sleigh to balance. 
If it isn't balanced, he can't defy physics, and nobody gets presents this year.

No pressure.

Santa has provided you a list of the weights of every package he needs to fit on the sleigh. 
The packages need to be split into three groups of exactly the same weight, and every package has to fit. 
The first group goes in the passenger compartment of the sleigh, and the second and third go in containers on either side. 
Only when all three groups weigh exactly the same amount will the sleigh be able to fly. 
Defying physics has rules, you know!

Of course, that's not the only problem. 
The first group - the one going in the passenger compartment - needs as few packages as possible so that Santa has some legroom left over. 
It doesn't matter how many packages are in either of the other two groups, so long as all of the groups weigh the same.

Furthermore, Santa tells you, if there are multiple ways to arrange the packages such that the fewest possible are in the first group, you need to choose the way where the first group has the smallest quantum entanglement to reduce the chance of any "complications".
The quantum entanglement of a group of packages is the product of their weights, that is, the value you get when you multiply their weights together. 
Only consider quantum entanglement if the first group has the fewest possible number of packages in it and all groups weigh the same amount.

For example, suppose you have ten packages with weights 1 through 5 and 7 through 11. 
For this situation, some of the unique first groups, their quantum entanglements, and a way to divide the remaining packages are as follows:

Group 1;             Group 2; Group 3
11 9       (QE= 99); 10 8 2;  7 5 4 3 1
10 9 1     (QE= 90); 11 7 2;  8 5 4 3
10 8 2     (QE=160); 11 9;    7 5 4 3 1
10 7 3     (QE=210); 11 9;    8 5 4 2 1
10 5 4 1   (QE=200); 11 9;    8 7 3 2
10 5 3 2   (QE=300); 11 9;    8 7 4 1
10 4 3 2 1 (QE=240); 11 9;    8 7 5
9 8 3      (QE=216); 11 7 2;  10 5 4 1
9 7 4      (QE=252); 11 8 1;  10 5 3 2
9 5 4 2    (QE=360); 11 8 1;  10 7 3
8 7 5      (QE=280); 11 9;    10 4 3 2 1
8 5 4 3    (QE=480); 11 9;    10 7 2 1
7 5 4 3 1  (QE=420); 11 9;    10 8 2

Of these, although 10 9 1 has the smallest quantum entanglement (90), the configuration with only two packages, 11 9, in the passenger compartment gives Santa the most legroom and wins. 
In this situation, the quantum entanglement for the ideal configuration is therefore 99. 
Had there been two configurations with only two packages in the first group, the one with the smaller quantum entanglement would be chosen.

What is the quantum entanglement of the first group of packages in the ideal configuration?

Your puzzle answer was 10723906903.

--- Part Two ---

That's weird... the sleigh still isn't balancing.

"Ho ho ho", Santa muses to himself. "I forgot the trunk".

Balance the sleigh again, but this time, separate the packages into four groups instead of three. 
The other constraints still apply.

Given the example packages above, this would be some of the new unique first groups, their quantum entanglements, and one way to divide the remaining packages:

11 4    (QE=44); 10 5;   9 3 2 1; 8 7
10 5    (QE=50); 11 4;   9 3 2 1; 8 7
9 5 1   (QE=45); 11 4;   10 3 2;  8 7
9 4 2   (QE=72); 11 3 1; 10 5;    8 7
9 3 2 1 (QE=54); 11 4;   10 5;    8 7
8 7     (QE=56); 11 4;   10 5;    9 3 2 1
Of these, there are three arrangements that put the minimum (two) number of packages in the first group: 11 4, 10 5, and 8 7. 
Of these, 11 4 has the lowest quantum entanglement, and so it is selected.

Now, what is the quantum entanglement of the first group of packages in the ideal configuration?

*/

namespace Day24
{
    class Program
    {
        static int sTotalWeight;
        static int sGroupWeight;
        static int sNumGroups;

        static int[] sWeights;
        static int[] sWeightGroups;
        static bool[] sUsedWeights;
        static long sMinQE;
        static long sNumItemsInQE;
        static List<long> sPotentialQEs;
        static List<long> sQEs;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);

            if (part1)
            {
                ParseInput(lines, 3);
                var result1 = SmallestQE();
                Console.WriteLine($"Day24: Result1 {result1}");
                var expected = 10723906903;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                ParseInput(lines, 4);
                var result2 = SmallestQE();
                Console.WriteLine($"Day24: Result2 {result2}");
                var expected = 74850409;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseInput(string[] lines, int numGroups)
        {
            sWeights = new int[lines.Length];
            sWeightGroups = new int[lines.Length];
            sUsedWeights = new bool[sWeights.Length];
            sPotentialQEs = new List<long>(sWeights.Length);
            sQEs = new List<long>(sWeights.Length);
            sTotalWeight = 0;
            sMinQE = long.MaxValue;
            sNumItemsInQE = lines.Length;
            sNumGroups = numGroups;

            ClearWeightGroups();

            for (var l = 0; l < lines.Length; ++l)
            {
                var line = lines[l];
                sWeights[l] = int.Parse(line);
                sTotalWeight += sWeights[l];
            }
            for (int i = 0; i < sWeights.Length - 1; ++i)
            {
                for (int j = i + 1; j < sWeights.Length; ++j)
                {
                    var weightI = sWeights[i];
                    var weightJ = sWeights[j];
                    if (weightJ > weightI)
                    {
                        sWeights[i] = weightJ;
                        sWeights[j] = weightI;
                    }
                }
            }

            sGroupWeight = sTotalWeight / numGroups;
            if (sGroupWeight * numGroups != sTotalWeight)
            {
                throw new InvalidProgramException($"Invalid total weight not a multiple of {numGroups} {sTotalWeight} {sGroupWeight}");
            }
        }

        public static void ComputeQEs()
        {
            FindQEs(0, sGroupWeight, 1);
        }

        public static bool IsQEValid(int weightLeft, int qeCount)
        {
            for (var w = 0; w < sWeights.Length; ++w)
            {
                if (sUsedWeights[w] == true)
                {
                    continue;
                }
                int weight = sWeights[w];
                if (weightLeft >= weight)
                {
                    var orgWeightLeft = weightLeft;
                    sUsedWeights[w] = true;
                    weightLeft -= weight;
                    if (weightLeft == 0)
                    {
                        ++qeCount;
                        if (qeCount > sNumGroups)
                        {
                            return false;
                        }
                        bool usedAllWeights = true;
                        foreach (var uw in sUsedWeights)
                        {
                            if (uw == false)
                            {
                                usedAllWeights = false;
                                break;
                            }
                        }
                        if (usedAllWeights)
                        {
                            if (qeCount == sNumGroups)
                            {
                                return true;
                            }
                            return false;
                        }
                        return IsQEValid(sGroupWeight, qeCount);
                    }
                    var result = IsQEValid(weightLeft, qeCount);
                    if (result)
                    {
                        return true;
                    }
                    weightLeft = orgWeightLeft;
                    sUsedWeights[w] = false;
                }
            }
            return false;
        }

        static void ClearWeightGroups()
        {
            for (var w = 0; w < sWeightGroups.Length; ++w)
            {
                sWeightGroups[w] = 0;
                sUsedWeights[w] = false;
            }
        }

        static void FindQEs(int start, int weightLeft, long qe)
        {
            for (var w = start; w < sWeights.Length; ++w)
            {
                if (sUsedWeights[w] == true)
                {
                    continue;
                }
                int weight = sWeights[w];
                if (weightLeft >= weight)
                {
                    var orgQE = qe;
                    var orgWeightLeft = weightLeft;
                    sUsedWeights[w] = true;
                    weightLeft -= weight;
                    qe *= weight;
                    if (weightLeft == 0)
                    {
                        if (sPotentialQEs.Contains(qe) == false)
                        {
                            sPotentialQEs.Add(qe);
                        }
                        var orgUsedWeights = new bool[sUsedWeights.Length];
                        for (var i = 0; i < sUsedWeights.Length; ++i)
                        {
                            orgUsedWeights[i] = sUsedWeights[i];
                        }
                        if (IsQEValid(sGroupWeight, 1) == true)
                        {
                            if (sQEs.Contains(qe) == false)
                            {
                                sQEs.Add(qe);
                            }
                        }
                        for (var i = 0; i < sUsedWeights.Length; ++i)
                        {
                            sUsedWeights[i] = orgUsedWeights[i];
                        }
                        sUsedWeights[w] = false;
                        return;
                    }
                    FindQEs(w + 1, weightLeft, qe);
                    weightLeft = orgWeightLeft;
                    qe = orgQE;
                    sUsedWeights[w] = false;
                }
            }
        }

        static void FindSmallestQE(int start, int weightLeft, long qe, long numItemsInQE)
        {
            for (var w = start; w < sWeights.Length; ++w)
            {
                if (sUsedWeights[w] == true)
                {
                    continue;
                }
                int weight = sWeights[w];
                if (weightLeft >= weight)
                {
                    var orgNumItemsInQE = numItemsInQE;
                    var orgQE = qe;
                    var orgWeightLeft = weightLeft;
                    sUsedWeights[w] = true;
                    weightLeft -= weight;
                    qe *= weight;
                    ++numItemsInQE;
                    if ((qe < sMinQE) || (numItemsInQE < sNumItemsInQE))
                    {
                        if (weightLeft == 0)
                        {
                            if (sPotentialQEs.Contains(qe) == false)
                            {
                                sPotentialQEs.Add(qe);
                            }
                            var orgUsedWeights = new bool[sUsedWeights.Length];
                            for (var i = 0; i < sUsedWeights.Length; ++i)
                            {
                                orgUsedWeights[i] = sUsedWeights[i];
                            }
                            if (IsQEValid(sGroupWeight, 1) == true)
                            {
                                if (sQEs.Contains(qe) == false)
                                {
                                    sQEs.Add(qe);
                                    //Console.WriteLine($"Found QE {qe}");
                                    if (numItemsInQE < sNumItemsInQE)
                                    {
                                        sMinQE = qe;
                                        sNumItemsInQE = numItemsInQE;
                                    }
                                    else if (numItemsInQE == sNumItemsInQE)
                                    {
                                        if (qe < sMinQE)
                                        {
                                            sMinQE = qe;
                                        }
                                    }
                                }
                            }
                            for (var i = 0; i < sUsedWeights.Length; ++i)
                            {
                                sUsedWeights[i] = orgUsedWeights[i];
                            }
                            sUsedWeights[w] = false;
                            return;
                        }
                        FindSmallestQE(w + 1, weightLeft, qe, numItemsInQE);
                    }
                    weightLeft = orgWeightLeft;
                    qe = orgQE;
                    numItemsInQE = orgNumItemsInQE;
                    sUsedWeights[w] = false;
                }
            }
        }

        public static long SmallestQE()
        {
            FindSmallestQE(0, sGroupWeight, 1, 0);
            return sMinQE;
        }

        public static bool QEExists(long qe)
        {
            return sQEs.Contains(qe);
        }

        public static void Run()
        {
            Console.WriteLine("Day24 : Start");
            _ = new Program("Day24/input.txt", true);
            _ = new Program("Day24/input.txt", false);
            Console.WriteLine("Day24 : End");
        }
    }
}
