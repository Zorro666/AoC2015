using System;

/*
--- Day 15: Science for Hungry People ---

Today, you set out on the task of perfecting your milk-dunking cookie recipe. 
All you have to do is find the right balance of ingredients.

Your recipe leaves room for exactly 100 teaspoons of ingredients. 
You make a list of the remaining ingredients you could use to finish the recipe (your puzzle input) and their properties per teaspoon:

capacity (how well it helps the cookie absorb milk)
durability (how well it keeps the cookie intact when full of milk)
flavor (how tasty it makes the cookie)
texture (how it improves the feel of the cookie)
calories (how many calories it adds to the cookie)
You can only measure ingredients in whole-teaspoon amounts accurately, and you have to be accurate so you can reproduce your results in the future. The total score of a cookie can be found by adding up each of the properties (negative totals become 0) and then multiplying together everything except calories.

For instance, suppose you have these two ingredients:

Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
Then, choosing to use 44 teaspoons of butterscotch and 56 teaspoons of cinnamon (because the amounts of each ingredient must add up to 100) would result in a cookie with the following properties:

A capacity of 44*-1 + 56*2 = 68
A durability of 44*-2 + 56*3 = 80
A flavor of 44*6 + 56*-2 = 152
A texture of 44*3 + 56*-1 = 76
Multiplying these together (68 * 80 * 152 * 76, ignoring calories for now) results in a total score of 62842880, which happens to be the best score possible given these ingredients. 
If any properties had produced a negative total, it would have instead become zero, causing the whole score to multiply to zero.

Given the ingredients in your kitchen and their properties, what is the total score of the highest-scoring cookie you can make?

Your puzzle answer was 21367368.

--- Part Two ---

Your cookie recipe becomes wildly popular! 
Someone asks if you can make another recipe that has exactly 500 calories per cookie (so they can use it as a meal replacement). Keep the rest of your award-winning process the same (100 teaspoons, same ingredients, same scoring system).

For example, given the ingredients above, if you had instead selected 40 teaspoons of butterscotch and 60 teaspoons of cinnamon (which still adds to 100), the total calorie count would be 40*8 + 60*3 = 500. 
The total score would go down, though: only 57600000, the best you can do in such trying circumstances.

Given the ingredients in your kitchen and their properties, what is the total score of the highest-scoring cookie you can make with a calorie total of 500?

*/

namespace Day15
{
    class Program
    {
        struct Ingredient
        {
            public string name;
            public int capacity;
            public int durability;
            public int flavor;
            public int texture;
            public int calories;
        };

        static Ingredient[] sIngredients;
        static int[] sTeaspoons;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);

            if (part1)
            {
                var result1 = HighestScore();
                Console.WriteLine($"Day15 Result1:{result1}");
                var expected = 21367368;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                var result2 = HighestScoreCalorie(500);
                Console.WriteLine($"Day15 Result2:{result2}");
                var expected = 1766400;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void ParseInput(string[] ingredients)
        {
            sIngredients = new Ingredient[ingredients.Length];
            sTeaspoons = new int[ingredients.Length];
            int i = 0;
            foreach (var line in ingredients)
            {
                var tokens = line.Split(' ');
                if (tokens.Length != 11)
                {
                    throw new InvalidProgramException($"Invalid ingredient line '{line}' expected 11 tokens {tokens.Length}");
                }
                if ((tokens[1] != "capacity") ||
                    (tokens[3] != "durability") ||
                    (tokens[5] != "flavor") ||
                    (tokens[7] != "texture") ||
                    (tokens[9] != "calories"))
                {
                    throw new InvalidProgramException($"Invalid ingredient line '{line}'");
                }
                ref Ingredient ingredient = ref sIngredients[i];
                ingredient.name = tokens[0].TrimEnd(':');
                ingredient.capacity = int.Parse(tokens[2].TrimEnd(','));
                ingredient.durability = int.Parse(tokens[4].TrimEnd(','));
                ingredient.flavor = int.Parse(tokens[6].TrimEnd(','));
                ingredient.texture = int.Parse(tokens[8].TrimEnd(','));
                ingredient.calories = int.Parse(tokens[10]);
                sTeaspoons[i] = 0;
                ++i;
            }
        }

        static int ComputeCalorie()
        {
            var ingredientCount = sIngredients.Length;
            var totalCalorie = 0;
            for (var i = 0; i < ingredientCount; ++i)
            {
                ref Ingredient ingredient = ref sIngredients[i];
                int teaspoonAmount = sTeaspoons[i];
                totalCalorie += teaspoonAmount * ingredient.calories;
            }
            totalCalorie = Math.Max(totalCalorie, 0);
            return totalCalorie;
        }

        static int ComputeScore()
        {
            var ingredientCount = sIngredients.Length;
            var totalCapacity = 0;
            var totalDurability = 0;
            var totalFlavor = 0;
            var totalTexture = 0;
            for (var i = 0; i < ingredientCount; ++i)
            {
                ref Ingredient ingredient = ref sIngredients[i];
                int teaspoonAmount = sTeaspoons[i];
                totalCapacity += teaspoonAmount * ingredient.capacity;
                totalDurability += teaspoonAmount * ingredient.durability;
                totalFlavor += teaspoonAmount * ingredient.flavor;
                totalTexture += teaspoonAmount * ingredient.texture;
            }
            totalCapacity = Math.Max(totalCapacity, 0);
            totalDurability = Math.Max(totalDurability, 0);
            totalFlavor = Math.Max(totalFlavor, 0);
            totalTexture = Math.Max(totalTexture, 0);
            var score = totalCapacity * totalDurability * totalFlavor * totalTexture;
            return score;
        }

        public static int HighestScore()
        {
            var maxScore = int.MinValue;
            var ingredientCount = sTeaspoons.Length;
            var finalIngredient = ingredientCount - 1;
            while (sTeaspoons[finalIngredient] != 100)
            {
                int carry = 1;
                var total = 0;
                for (var i = 0; i < finalIngredient; ++i)
                {
                    sTeaspoons[i] += carry;
                    if (sTeaspoons[i] > 100)
                    {
                        sTeaspoons[i] = 0;
                        carry = 1;
                    }
                    else
                    {
                        carry = 0;
                    }
                    total += sTeaspoons[i];
                }
                if (total > 100)
                {
                    continue;
                }
                var remainder = 100 - total;
                sTeaspoons[finalIngredient] = remainder;
                var score = ComputeScore();
                maxScore = Math.Max(maxScore, score);
            }
            return maxScore;
        }

        public static int HighestScoreCalorie(int calorieTarget)
        {
            var maxScore = int.MinValue;
            var ingredientCount = sTeaspoons.Length;
            var finalIngredient = ingredientCount - 1;
            while (sTeaspoons[finalIngredient] != 100)
            {
                int carry = 1;
                var total = 0;
                for (var i = 0; i < finalIngredient; ++i)
                {
                    sTeaspoons[i] += carry;
                    if (sTeaspoons[i] > 100)
                    {
                        sTeaspoons[i] = 0;
                        carry = 1;
                    }
                    else
                    {
                        carry = 0;
                    }
                    total += sTeaspoons[i];
                }
                if (total > 100)
                {
                    continue;
                }
                var remainder = 100 - total;
                sTeaspoons[finalIngredient] = remainder;
                if (ComputeCalorie() == calorieTarget)
                {
                    var score = ComputeScore();
                    maxScore = Math.Max(maxScore, score);
                }
            }
            return maxScore;
        }

        public static void Run()
        {
            Console.WriteLine("Day15 : Start");
            _ = new Program("Day15/input.txt", true);
            _ = new Program("Day15/input.txt", false);
            Console.WriteLine("Day15 : End");
        }
    }
}
