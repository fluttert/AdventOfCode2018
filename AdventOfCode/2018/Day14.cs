using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2018
{
    internal class Day14
    {
        public void Solve()
        {
            int recipesToMake = int.Parse(input);
            List<int> recipes = new List<int>() { 3, 7 };
            int elf1 = 0;
            int elf2 = 1;
            for (int i = 0; i < recipesToMake + 10; i++)
            {
                // create new recipe
                int newRecipe = recipes[elf1] + recipes[elf2];
                if (newRecipe < 10) { recipes.Add(newRecipe); }
                else { recipes.Add(newRecipe / 10); recipes.Add(newRecipe % 10); }

                // move elf
                elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;
                // Console.WriteLine(string.Join(' ', recipes) + $"  :: Elf1: {elf1} , Elf2: {elf2}");
            }
            var sb = new StringBuilder();
            for (int i = recipesToMake; i < recipesToMake + 10; i++) { sb.Append(recipes[i]); }
            Console.WriteLine($"Day14 Part 1: {sb.ToString()}");

            // reset for part 2
            recipes = new List<int>() { 3, 7 };
            elf1 = 0;
            elf2 = 1;
            bool sequenceFound = false;
            var sequence = Array.ConvertAll(input.ToCharArray(), x => x - '0');
            while (!sequenceFound) {
                int newRecipe = recipes[elf1] + recipes[elf2];
                if (newRecipe < 10) { recipes.Add(newRecipe);}
                else { recipes.Add(newRecipe / 10); recipes.Add(newRecipe % 10);  }
                elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;

                if (recipes.Count > 6) {
                    bool seq1 = true;
                    bool seq2 = true;
                    int start = recipes.Count - sequence.Length;
                    for (int i = 0; i < sequence.Length; i++) {
                        
                        if (sequence[i] != recipes[start - 1 + i]) { seq1 = false; }
                        if (sequence[i] != recipes[start + i]) { seq2 = false; }
                    }
                    if (seq1 || seq2) {
                        Console.WriteLine($"Day 14 part 2: { (seq1 ? start - 1 : start) }");
                        
                        sequenceFound = true;
                        break;
                    }
                }
            }
        }

        //public string input = @"01245";
        public string input = @"409551";
    }
}