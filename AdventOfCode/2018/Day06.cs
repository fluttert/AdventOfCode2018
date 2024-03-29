﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    internal class Day06
    {
        public void Solve()
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var points = new List<(int X, int Y)>();
            var area = new Dictionary<int, int>(); // area matrix

            // add all points
            for (int i = 0; i < lines.Length; i++)
            {
                int[] coordinates = Array.ConvertAll(lines[i].Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                points.Add((X: coordinates[0], Y: coordinates[1]));
                area.Add(i, 0);
            }

            // determine for each point on the grid which is the nearest point
            int horizontal = points.Max(kvp => kvp.X), vertical = points.Max(kvp => kvp.Y);
            var edges = new HashSet<int>();
            int regionTilesLessThen10K = 0;
            for (int i = 0; i <= horizontal; i++)
            {
                for (int j = 0; j <= vertical; j++)
                {
                    int minimumDistance = int.MaxValue;
                    int minimumId = -1;
                    int sumDistance = 0;
                    for (int k = 0; k < points.Count; k++)
                    {
                        int distance = Math.Abs(i - points[k].X) + Math.Abs(j - points[k].Y);
                        sumDistance += distance;
                        if (distance > minimumDistance) { continue; }
                        if (distance == minimumDistance && minimumId != -1) { area[minimumId]--; minimumId = -1; continue; }
                        if (distance < minimumDistance)
                        {
                            if (minimumId != -1) { area[minimumId]--; }
                            minimumId = k;
                            minimumDistance = distance;
                            area[k]++;
                        }
                    }
                    // is edge?
                    if (minimumId != -1 && (i == 0 || j == 0 || i == horizontal - 1 || j == vertical - 1)) { edges.Add(minimumId); }
                    if (sumDistance < 10000) { regionTilesLessThen10K++; }
                }
            }

            Console.WriteLine($"Day06 Answer Part 1 is {area.Where(kvp => !edges.Contains(kvp.Key)).Max(x => x.Value)}");
            Console.WriteLine($"Day06 Answer Part 2 is {regionTilesLessThen10K}");
        }

        public void SolvePart01Naive()
        {
            // process input
            int coordinateId = 1;
            int offset = 0;
            var infinites = new HashSet<int>();
            var areas = new Dictionary<int, int>(); // (coordinateId, area)
            var minimumGridSize = (horizontal: 0, vertical: 0);
            var coordinates = new Dictionary<int, (int X, int Y)>();
            int gridHorizontal = int.MaxValue, gridVertical = int.MaxValue;

            foreach (string line in input.Split(Environment.NewLine))
            {
                int[] coordinate = Array.ConvertAll(line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                areas.Add(coordinateId, 0);
                coordinates.Add(coordinateId, (coordinate[0], coordinate[1]));
                if (minimumGridSize.horizontal < coordinate[0]) { minimumGridSize.horizontal = coordinate[0]; }
                if (minimumGridSize.vertical < coordinate[1]) { minimumGridSize.vertical = coordinate[1]; }
                coordinateId++;
            }
            gridHorizontal = 2 * offset + minimumGridSize.horizontal;
            gridVertical = 2 * offset + minimumGridSize.vertical;

            // initialise Grid dynamically based on the size of input
            var valGrid = new Dictionary<(int X, int Y), int>(); // shortest distance, from any point
            var idGrid = new Dictionary<(int X, int Y), HashSet<int>>(); // all points that have this shorted distance
            var queue = new Queue<(int X, int Y, int Id, int Distance)>();
            var edgeIds = new HashSet<int>();
            foreach (var coor in coordinates) { queue.Enqueue((X: coor.Value.X + offset, Y: coor.Value.Y + offset, Id: coor.Key, Distance: 0)); }
            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                point.Distance++; // increase distance

                // check grid if they are initialised
                if (!valGrid.ContainsKey((point.X, point.Y))) { valGrid.Add((point.X, point.Y), int.MaxValue); }
                if (!idGrid.ContainsKey((point.X, point.Y))) { idGrid.Add((point.X, point.Y), new HashSet<int>()); }

                // case 1: another point is closer by OR it is already  claimed by this ID
                if (valGrid[(point.X, point.Y)] < point.Distance || idGrid[(point.X, point.Y)].Contains(point.Id)) { continue; }

                // case 2: claim this tile
                valGrid[(point.X, point.Y)] = point.Distance;
                idGrid[(point.X, point.Y)].Add(point.Id);

                // propagate and enqueue neighbours
                if (point.X > 0) { queue.Enqueue((X: point.X - 1, Y: point.Y, Id: point.Id, Distance: point.Distance)); }
                if (point.X < gridHorizontal - 1) { queue.Enqueue((X: point.X + 1, Y: point.Y, Id: point.Id, Distance: point.Distance)); }
                if (point.Y > 0) { queue.Enqueue((X: point.X, Y: point.Y - 1, Id: point.Id, Distance: point.Distance)); }
                if (point.Y < gridVertical - 1) { queue.Enqueue((X: point.X, Y: point.Y + 1, Id: point.Id, Distance: point.Distance)); }
            }

            for (int i = 0; i < gridHorizontal - 1; i++)
            {
                if (idGrid[(i, 0)].Count == 1) { edgeIds.Add(idGrid[(i, 0)].First()); }
                if (idGrid[(i, gridVertical - 1)].Count == 1) { edgeIds.Add(idGrid[(i, gridVertical - 1)].First()); }
            }

            for (int i = 0; i < gridVertical - 1; i++)
            {
                if (idGrid[(0, i)].Count == 1) { edgeIds.Add(idGrid[(0, i)].First()); }
                if (idGrid[(gridHorizontal - 1, i)].Count == 1) { edgeIds.Add(idGrid[(gridHorizontal - 1, i)].First()); }
            }

            // count the areas
            foreach (var kvp in idGrid)
            {
                if (kvp.Value.Count == 1 && !edgeIds.Contains(kvp.Value.First()))
                {
                    areas[kvp.Value.First()]++;
                }
            }

            Console.WriteLine($"Day06 part1 answer: { areas.Max(kvp => kvp.Value) }");
            // not 7976
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