﻿using AdventOfCode._2018;
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

            new Day01().Solve();
            Console.WriteLine($"Day01 solved in {stopwatch.ElapsedMilliseconds}ms");


            Console.ReadLine();
        }


        
    }




}
