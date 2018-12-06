using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    internal class Day06
    {
        public void Solve()
        {

            // process input 
            int coordinateId = 1;
            var infinites = new HashSet<int>();
            var areas = new Dictionary<int, int>(); // (coordinateId, area)
            var gridSizeEstimates = (horizontal : 0, vertical: 0);
            var coordinates = new Dictionary<int, (int X, int Y)>();

            foreach (string line in testinput.Split(Environment.NewLine))
            {
                int[] coordinate = Array.ConvertAll(line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                areas.Add(coordinateId, 0);
                coordinates.Add(coordinateId, (coordinate[0], coordinate[1]));
                if (gridSizeEstimates.horizontal < coordinate[0]) { gridSizeEstimates.horizontal = coordinate[0]; }
                if (gridSizeEstimates.vertical < coordinate[1]) { gridSizeEstimates.vertical = coordinate[1]; }
                coordinateId++;
            }

            // initialise Grid dynamically based on the size of input
            int offset = (gridSizeEstimates.horizontal > gridSizeEstimates.vertical ? gridSizeEstimates.horizontal : gridSizeEstimates.vertical);
            int[][] grid = new int[gridSizeEstimates.horizontal + offset][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[gridSizeEstimates.vertical + offset]; }
            offset = offset / 2;

            // fill in all coordinates
            foreach (var coordinate in coordinates) { grid[coordinate.Value.X + offset][coordinate.Value.Y + offset] = coordinate.Key; }

            bool gridChanged = true;
            while (gridChanged)
            {
                var processedCoordinates = new HashSet<(int X, int Y)>();
                for (int i = 0; i < grid.Length; i++) {
                    for (int j = 0; j < grid[0].Length; j++) {
                        if (grid[i][j] < 1) { continue; } // not yet visited, or multiple dots
                        int curCoorId = grid[i][j];
                        if (i > 0 && grid[i - 1][j] != curCoorId && grid[i - 1][j] >= 0) {
                            if (grid[i - 1][j] == 0) { grid[i - 1][j] = curCoorId; areas[curCoorId]++; processedCoordinates.Add((i - 1, j)); }
                            if (grid[i - 1][j] > 0 && processedCoordinates.Contains((i - 1, j))){ areas[grid[i - 1][j]]--; grid[i=1][j]= -1; }
                        }
                        if (i < grid.Length-1 && grid[i + 1][j] != curCoorId && grid[i + 1][j] >= 0)
                        {
                            if (grid[i + 1][j] == 0) { grid[i + 1][j] = curCoorId; areas[curCoorId]++; processedCoordinates.Add((i + 1, j)); }
                            if (grid[i + 1][j] > 0 && processedCoordinates.Contains((i + 1, j))) { areas[grid[i + 1][j]]--; grid[i + 1][j] = -1; }
                        }
                        if (j > 0 && grid[i][j - 1] != curCoorId && grid[i][j - 1] >= 0)
                        {
                            if (grid[i][j - 1] == 0) { grid[i ][j - 1] = curCoorId; areas[curCoorId]++; processedCoordinates.Add((i, j - 1)); }
                            if (grid[i][j - 1] > 0 && processedCoordinates.Contains((i, j - 1))) { areas[grid[i][j - 1]]--; grid[i][j - 1] = -1; }
                        }
                        if (j > grid[0].Length - 1 && grid[i][j + 1] != curCoorId && grid[i][j + 1] >= 0)
                        {
                            if (grid[i][j + 1] == 0) { grid[i][j + 1] = curCoorId; areas[curCoorId]++; processedCoordinates.Add((i, j + 1)); }
                            if (grid[i][j + 1] > 0 && processedCoordinates.Contains((i, j + 1))) { areas[grid[i][j + 1]]--; grid[i][j + 1] = -1; }
                        }

                    }
                }
                gridChanged = processedCoordinates.Count > 0;
            }
            for (int i = 0; i < grid.Length; i++) { infinites.Add(grid[i][0]); infinites.Add(grid[i][grid[0].Length - 1]); }
            for (int i = 0; i < grid[0].Length; i++) { infinites.Add(grid[0][i]); infinites.Add(grid[0][grid.Length - 1]); }

            Console.WriteLine($"Day06 part1 answer: { areas.Where(kvp => !infinites.Contains(kvp.Key)).Max(kvp => kvp.Value) }");
            Console.WriteLine($"Day06 part2 answer: ");
        }

        public string testinput = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";

        public string input = @"195, 221
132, 132
333, 192
75, 354
162, 227
150, 108
46, 40
209, 92
153, 341
83, 128
256, 295
311, 114
310, 237
99, 240
180, 337
332, 176
212, 183
84, 61
275, 341
155, 89
169, 208
105, 78
151, 318
92, 74
146, 303
184, 224
285, 348
138, 163
216, 61
277, 270
130, 155
297, 102
197, 217
72, 276
299, 89
357, 234
136, 342
346, 221
110, 188
82, 183
271, 210
46, 198
240, 286
128, 95
111, 309
108, 54
258, 305
241, 157
117, 162
96, 301";
    }
}