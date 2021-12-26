using System;
using System.Collections.Generic;

/*

--- Day 9: All in a Single Night ---

Every year, Santa manages to deliver all of his presents in a single night.

This year, however, he has some new locations to visit; his elves have provided him the distances between every pair of locations. He can start and end at any two (different) locations he wants, but he must visit each location exactly once. What is the shortest distance he can travel to achieve this?

For example, given the following distances:

London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141
The possible routes are therefore:

Dublin -> London -> Belfast = 982
London -> Dublin -> Belfast = 605
London -> Belfast -> Dublin = 659
Dublin -> Belfast -> London = 659
Belfast -> Dublin -> London = 605
Belfast -> London -> Dublin = 982
The shortest of these is London -> Dublin -> Belfast = 605, and so the answer is 605 in this example.

What is the distance of the shortest route?

Your puzzle answer was 207.

--- Part Two ---

The next year, just to show off, Santa decides to take the route with the longest distance instead.

He can still start and end at any two (different) locations he wants, and he still must visit each location exactly once.

For example, given the distances above, the longest route would be 982 via (for example) Dublin -> London -> Belfast.

What is the distance of the longest route?

*/

namespace Day09
{
    class Program
    {
        struct Route
        {
            public string from;
            public string to;
            public int distance;
        };

        static List<string> sLocations;
        static Route[] sRoutes;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseRoutes(lines);
            if (part1)
            {
                var result1 = ShortestRoute();
                Console.WriteLine($"Day09 : Result1 {result1}");
                int expected = 141;
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = LongestRoute();
                Console.WriteLine($"Day09 : Result2 {result2}");
                int expected = 736;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseRoutes(string[] routes)
        {
            sLocations = new List<string>(routes.Length);
            sRoutes = new Route[routes.Length * 2];
            int routeCount = 0;
            foreach (var line in routes)
            {
                Route route;
                var tokens = line.Split(" ");
                if (tokens.Length != 5)
                {
                    throw new InvalidProgramException($"Invalid route line {line} {tokens.Length} != 5");
                }
                route.from = tokens[0];
                if (tokens[1] != "to")
                {
                    throw new InvalidProgramException($"Invalid route line {line} {tokens[1]} != 'to'");
                }
                route.to = tokens[2];
                if (tokens[3] != "=")
                {
                    throw new InvalidProgramException($"Invalid route line {line} {tokens[3]} != '='");
                }
                route.distance = int.Parse(tokens[4]);
                sRoutes[routeCount] = route;
                ++routeCount;
                Route reverseRoute;
                reverseRoute.from = route.to;
                reverseRoute.to = route.from;
                reverseRoute.distance = route.distance;
                sRoutes[routeCount] = reverseRoute;
                ++routeCount;
                if (!sLocations.Contains(route.from))
                {
                    sLocations.Add(route.from);
                }
                if (!sLocations.Contains(route.to))
                {
                    sLocations.Add(route.to);
                }
            }
        }

        public static int ShortestRoute()
        {
            int minDistance = int.MaxValue;
            foreach (var start in sLocations)
            {
                var totalDistance = 0;
                var visitedLocations = new List<string>();
                visitedLocations.Add(start);
                ShortestDistance(ref visitedLocations, start, ref totalDistance, ref minDistance);
            }
            return minDistance;
        }

        public static void ShortestDistance(ref List<string> visitedLocations, string startingLocation, ref int totalDistance, ref int minDistance)
        {
            foreach (var destination in sLocations)
            {
                if (visitedLocations.Contains(destination))
                {
                    continue;
                }
                int routeDistance = 0;
                foreach (var route in sRoutes)
                {
                    if ((route.from == startingLocation) && (route.to == destination))
                    {
                        routeDistance = route.distance;
                        break;
                    }
                }
                if (routeDistance > 0)
                {
                    totalDistance += routeDistance;
                    visitedLocations.Add(destination);
                    if (visitedLocations.Count == sLocations.Count)
                    {
                        if (totalDistance < minDistance)
                        {
                            minDistance = totalDistance;
                        }
                    }
                    else
                    {
                        if (totalDistance < minDistance)
                        {
                            ShortestDistance(ref visitedLocations, destination, ref totalDistance, ref minDistance);
                        }
                    }
                    visitedLocations.Remove(destination);
                    totalDistance -= routeDistance;
                }
            }
        }

        public static int LongestRoute()
        {
            int maxDistance = int.MinValue;
            foreach (var start in sLocations)
            {
                var totalDistance = 0;
                var visitedLocations = new List<string>();
                visitedLocations.Add(start);
                LongestDistance(ref visitedLocations, start, ref totalDistance, ref maxDistance);
            }
            return maxDistance;
        }

        public static void LongestDistance(ref List<string> visitedLocations, string startingLocation, ref int totalDistance, ref int maxDistance)
        {
            foreach (var destination in sLocations)
            {
                if (visitedLocations.Contains(destination))
                {
                    continue;
                }
                int routeDistance = 0;
                foreach (var route in sRoutes)
                {
                    if ((route.from == startingLocation) && (route.to == destination))
                    {
                        routeDistance = route.distance;
                        break;
                    }
                }
                if (routeDistance > 0)
                {
                    totalDistance += routeDistance;
                    visitedLocations.Add(destination);
                    if (visitedLocations.Count == sLocations.Count)
                    {
                        if (totalDistance > maxDistance)
                        {
                            maxDistance = totalDistance;
                        }
                    }
                    else
                    {
                        LongestDistance(ref visitedLocations, destination, ref totalDistance, ref maxDistance);
                    }
                    visitedLocations.Remove(destination);
                    totalDistance -= routeDistance;
                }
            }
        }

        public static void Run()
        {
            Console.WriteLine("Day09 : Start");
            _ = new Program("Day09/input.txt", true);
            _ = new Program("Day09/input.txt", false);
            Console.WriteLine("Day09 : End");
        }
    }
}
