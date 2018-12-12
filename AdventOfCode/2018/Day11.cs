using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    internal class Day11
    {
        public void Solve()
        {
            // part 1
            const int gridSize = 301;
            int gridSerialNumber = int.Parse(input);
            int[][] grid = new int[gridSize][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[gridSize]; }

            // fill grid
            for (int i = 1; i < gridSize; i++)
            {
                for (int j = 1; j < gridSize; j++)
                {
                    int rackId = j + 10;
                    int powerLevel = rackId * i;
                    powerLevel += gridSerialNumber;
                    powerLevel *= rackId;
                    int hundredDigit = (powerLevel / 100) % 10;
                    powerLevel = hundredDigit - 5;

                    grid[j][i] = powerLevel;
                }
            }

            // determine largest fuel-square
            var results = new Dictionary<int, (int X, int Y, int largestPower)>();
            Parallel.For(1, gridSize, squareSize =>
            {
                results.Add(squareSize, (X: 0, Y: 0, largestPower: int.MinValue));
                for (int i = 0; i < gridSize - squareSize + 1; i++)
                { // COLUMN
                    for (int j = 0; j < gridSize - squareSize + 1; j++)
                    { // ROW
                        int totalPower = 0;
                        for (int k = 0; k < squareSize; k++)
                        {
                            for (int l = 0; l < squareSize; l++)
                            {
                                totalPower += grid[j + k][i + l];
                            }
                        }

                        if (results[squareSize].largestPower < totalPower)
                        {
                            results[squareSize] = (X: j, Y: i, totalPower);
                        }
                    }
                }
                Console.WriteLine($"Largest power for squaresize {squareSize} = {results[squareSize].largestPower})");
            });

            Console.WriteLine($"Day11 part1 answer: ({results[3].X},{results[3].Y}) - power: {results[3].largestPower} (gridsize 3)");
            var fullestSquare = results.OrderByDescending(kvp => kvp.Value.largestPower).First();
            Console.WriteLine($"Day11 part2 answer: ({fullestSquare.Value.X},{fullestSquare.Value.Y},{fullestSquare.Key}) - power: {fullestSquare.Value.largestPower}");
            // initial solution
            //for (int i = 0; i < gridSize - 2; i++)
            //{ // COLUMN
            //    for (int j = 0; j < gridSize - 2; j++)
            //    { // ROW
            //        int totalPower = grid[j][i] + grid[j + 1][i] + grid[j + 2][i];
            //        totalPower += grid[j][i + 1] + grid[j + 1][i + 1] + grid[j + 2][i + 1];
            //        totalPower += grid[j][i + 2] + grid[j + 2][i + 1] + grid[j + 2][i + 2];

            //        if (largestPower < totalPower)
            //        {
            //            largestPower = totalPower;
            //            topLeft3Square = (X: j, Y: i);
            //        }
            //        //sb.Append($"({j},{i}) ");
            //    }
            //    //Console.WriteLine(sb.ToString());
            //}
            //Console.WriteLine($"Day11 part1 answer: ({topLeft3Square.X},{topLeft3Square.Y})");
        }

        public string input = "5235";
    }
}