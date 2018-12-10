using AdventOfCode._2018;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            new Day08().Solve();
            Console.WriteLine($"Day08 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day07().Solve();
            //Console.WriteLine($"Day07 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day06().Solve();
            //Console.WriteLine($"Day06 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day05().Solve();
            //Console.WriteLine($"Day05 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day04().Solve();
            //Console.WriteLine($"Day04 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day03().Solve();
            //Console.WriteLine($"Day03 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day02().Solve();
            //Console.WriteLine($"Day02 solved in {stopwatch.ElapsedMilliseconds}ms");

            //new Day01().Solve();
            //Console.WriteLine($"Day01 solved in {stopwatch.ElapsedMilliseconds}ms");

            Console.ReadLine();
        }


        
    }




}
