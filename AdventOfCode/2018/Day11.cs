using System;

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
            int largestPower = int.MinValue;
            var topLeft = (X: 0, Y: 0);
            for (int i = 0; i < gridSize - 2; i++)
            { // COLUMN
                for (int j = 0; j < gridSize - 2; j++)
                { // ROW
                    int totalPower = grid[j][i] + grid[j + 1][i] + grid[j + 2][i];
                    totalPower += grid[j][i + 1] + grid[j + 1][i + 1] + grid[j + 2][i + 1];
                    totalPower += grid[j][i + 2] + grid[j + 2][i + 1] + grid[j + 2][i + 2];

                    if (largestPower < totalPower)
                    {
                        largestPower = totalPower;
                        topLeft = (X: j, Y: i);
                    }
                    //sb.Append($"({j},{i}) ");
                }
                //Console.WriteLine(sb.ToString());
            }

            Console.WriteLine($"Day11 part1 answer: ({topLeft.X},{topLeft.Y})");
        }

        public string input = "5235";
    }
}