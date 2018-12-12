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
            new Day12().Solve();
            //new Day11().Solve();
            //new Day10().Solve();
            //new Day09().Solve(); // TODO
            //new Day08().Solve();
            //new Day07().Solve();
            //new Day06().Solve();
            //new Day05().Solve();
            //new Day04().Solve();
            //new Day03().Solve();
            //new Day02().Solve();
            //new Day01().Solve();

            Console.WriteLine($"Got it solved in {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }


        
    }




}
