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
            int offset = 5;
            var infinites = new HashSet<int>();
            var areas = new Dictionary<int, int>(); // (coordinateId, area)
            var minimumGridSize = (horizontal: 0, vertical: 0);
            var coordinates = new Dictionary<int, (int X, int Y)>();

            foreach (string line in testinput.Split(Environment.NewLine))
            {
                int[] coordinate = Array.ConvertAll(line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                areas.Add(coordinateId, 0);
                coordinates.Add(coordinateId, (coordinate[0], coordinate[1]));
                if (minimumGridSize.horizontal < coordinate[0]) { minimumGridSize.horizontal = coordinate[0]; }
                if (minimumGridSize.vertical < coordinate[1]) { minimumGridSize.vertical = coordinate[1]; }
                coordinateId++;
            }

            // initialise Grid dynamically based on the size of input
            var valGrid = new Dictionary<(int X, int Y), int>(); // shortest distance, from any point
            var idGrid = new Dictionary<(int X, int Y), HashSet<int>>(); // all points that have this shorted distance
            var queue = new Queue<(int Id, int X, int Y, int Distance)>();
            foreach (var coor in coordinates) { queue.Enqueue((coor.Key, coor.Value.X + offset, coor.Value.Y+offset, 0)); }
            while (queue.Count > 0) {
                var point = queue.Dequeue();
                if (valGrid[(point.X, point.Y)] > point.Distance) { continue; }

            }



            
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