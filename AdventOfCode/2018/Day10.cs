﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2018
{
    internal class Day10
    {
        public void Solve()
        {
            var positions = new Dictionary<int, (int X, int Y)>();
            var velocities = new Dictionary<int, (int X, int Y)>();
            var minimumDimension = (Horizontal: int.MaxValue, Vertical: int.MaxValue);

            // -- parse input
            int id = 0;
            foreach (string line in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var filterInput = line.Replace("position=", string.Empty).Replace("velocity=", string.Empty);
                int[] startData = Array.ConvertAll(filterInput.Split(new char[] { '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                positions.Add(id, (X: startData[1], Y: startData[0]));
                velocities.Add(id, (X: startData[3], Y: startData[2]));
                id++;
            }

            // part 01
            bool smallestBox = false;
            int seconds = 0;
            while (!smallestBox)
            {
                // update for next round
                var newPositions = new Dictionary<int, (int X, int Y)>();
                foreach (var kvp in velocities)
                {
                    var updatedPosition = positions[kvp.Key];
                    updatedPosition.X += kvp.Value.X;
                    updatedPosition.Y += kvp.Value.Y;
                    newPositions.Add(kvp.Key, updatedPosition);
                }
                int curHorizontal = newPositions.Max(kvp => kvp.Value.X) - newPositions.Min(kvp => kvp.Value.X);
                int curVertical = newPositions.Max(kvp => kvp.Value.Y) - newPositions.Min(kvp => kvp.Value.Y);
                if (curHorizontal <= minimumDimension.Horizontal && curVertical <= minimumDimension.Vertical)
                {
                    minimumDimension = (Horizontal: curHorizontal, Vertical: curVertical);
                    positions = newPositions;
                    seconds++;
                }
                else
                {
                    smallestBox = true;
                }
                
            }
            // print result
            var listOfPoints = positions.Values.ToList();
            for (int i = listOfPoints.Min(x => x.X); i <= listOfPoints.Max(x => x.X); i++)
            {
                var sb = new StringBuilder();
                for (int j = positions.Min(kvp => kvp.Value.Y); j <= positions.Max(kvp => kvp.Value.Y); j++)
                {
                    sb.Append((listOfPoints.Count(x => x.X == i && x.Y == j) > 0) ? '#' : ' ');
                }
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine($"Dimension {minimumDimension.Horizontal} x {minimumDimension.Vertical} -> left-top{listOfPoints.Min(x => x.X)}");
            Console.WriteLine($"Day10 part2 answer:{seconds}");
        }

        public string testinput = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        public string input = @"position=< 51781,  41361> velocity=<-5, -4>
position=< 31129, -41131> velocity=<-3,  4>
position=<-20450,  31057> velocity=< 2, -3>
position=< 31145,  10425> velocity=<-3, -1>
position=< 10497,  41360> velocity=<-1, -4>
position=< 31097, -41136> velocity=<-3,  4>
position=< 51779, -20512> velocity=<-5,  2>
position=< 20827,  20741> velocity=<-2, -2>
position=< 51745,  10431> velocity=<-5, -1>
position=< 10531,  41360> velocity=<-1, -4>
position=< 31105,  10433> velocity=<-3, -1>
position=<-51343, -41134> velocity=< 5,  4>
position=< 31124,  51676> velocity=<-3, -5>
position=< 41465, -51440> velocity=<-4,  5>
position=<-51347,  20738> velocity=< 5, -2>
position=< 20817, -10200> velocity=<-2,  1>
position=<-51367,  20742> velocity=< 5, -2>
position=<-20431, -10197> velocity=< 2,  1>
position=<-30749,  20740> velocity=< 3, -2>
position=< 31107,  10428> velocity=<-3, -1>
position=< 31153, -41128> velocity=<-3,  4>
position=< 20797,  41366> velocity=<-2, -4>
position=<-51354,  41362> velocity=< 5, -4>
position=<-20463, -51445> velocity=< 2,  5>
position=<-20415, -51439> velocity=< 2,  5>
position=< 41454, -41131> velocity=<-4,  4>
position=< 41418, -30822> velocity=<-4,  3>
position=< 51721,  10426> velocity=<-5, -1>
position=< 51722, -51439> velocity=<-5,  5>
position=< 31149, -41129> velocity=<-3,  4>
position=<-51398, -51439> velocity=< 5,  5>
position=<-41087, -41133> velocity=< 4,  4>
position=< 20793,  51680> velocity=<-2, -5>
position=< 41460,  31053> velocity=<-4, -3>
position=< 31145, -41128> velocity=<-3,  4>
position=<-51338, -30822> velocity=< 5,  3>
position=< 10501,  10424> velocity=<-1, -1>
position=<-10111, -30818> velocity=< 1,  3>
position=<-30735,  51679> velocity=< 3, -5>
position=< 41409,  41361> velocity=<-4, -4>
position=< 41442, -10196> velocity=<-4,  1>
position=<-10111, -41129> velocity=< 1,  4>
position=< 51782, -20504> velocity=<-5,  2>
position=< 10485, -41133> velocity=<-1,  4>
position=<-30726, -30818> velocity=< 3,  3>
position=< 20796,  20741> velocity=<-2, -2>
position=<-30722, -41127> velocity=< 3,  4>
position=< 10505, -30821> velocity=<-1,  3>
position=< 41465,  10431> velocity=<-4, -1>
position=< 41413, -10191> velocity=<-4,  1>
position=< 31116,  31057> velocity=<-3, -3>
position=<-51350,  10427> velocity=< 5, -1>
position=< 10521,  51673> velocity=<-1, -5>
position=< 51753, -30822> velocity=<-5,  3>
position=< 10518,  51676> velocity=<-1, -5>
position=<-20402, -20506> velocity=< 2,  2>
position=<-51346, -41127> velocity=< 5,  4>
position=<-10131, -10200> velocity=< 1,  1>
position=<-41063, -20506> velocity=< 4,  2>
position=<-20421, -51448> velocity=< 2,  5>
position=<-10108,  51677> velocity=< 1, -5>
position=< 31148,  51676> velocity=<-3, -5>
position=< 20825,  20741> velocity=<-2, -2>
position=< 10475,  31057> velocity=<-1, -3>
position=<-30775,  10429> velocity=< 3, -1>
position=<-30742,  31053> velocity=< 3, -3>
position=< 20830, -10195> velocity=<-2,  1>
position=< 41420, -41132> velocity=<-4,  4>
position=< 20817,  41368> velocity=<-2, -4>
position=<-51357,  10424> velocity=< 5, -1>
position=<-30767,  31056> velocity=< 3, -3>
position=<-10118, -20507> velocity=< 1,  2>
position=< 51782, -20503> velocity=<-5,  2>
position=< 51726,  41369> velocity=<-5, -4>
position=<-41078, -51446> velocity=< 4,  5>
position=<-20454,  31051> velocity=< 2, -3>
position=<-30735,  31052> velocity=< 3, -3>
position=<-41078,  10430> velocity=< 4, -1>
position=<-30735, -10197> velocity=< 3,  1>
position=< 41444,  20738> velocity=<-4, -2>
position=<-41087,  20742> velocity=< 4, -2>
position=<-30739, -10192> velocity=< 3,  1>
position=< 41413, -30815> velocity=<-4,  3>
position=< 31147,  10428> velocity=<-3, -1>
position=<-30743, -51445> velocity=< 3,  5>
position=<-20463, -41133> velocity=< 2,  4>
position=< 41434,  41364> velocity=<-4, -4>
position=< 51741,  10427> velocity=<-5, -1>
position=<-10140,  41365> velocity=< 1, -4>
position=< 10526, -51440> velocity=<-1,  5>
position=< 51766,  20745> velocity=<-5, -2>
position=<-41069,  31057> velocity=< 4, -3>
position=< 51774,  51672> velocity=<-5, -5>
position=< 20788,  31057> velocity=<-2, -3>
position=< 31117, -10197> velocity=<-3,  1>
position=<-30755, -41128> velocity=< 3,  4>
position=<-10099, -30817> velocity=< 1,  3>
position=<-20443,  41367> velocity=< 2, -4>
position=<-41052,  20743> velocity=< 4, -2>
position=<-41031,  41369> velocity=< 4, -4>
position=< 51782,  41364> velocity=<-5, -4>
position=< 51780, -30824> velocity=<-5,  3>
position=< 41411, -51439> velocity=<-4,  5>
position=< 51747,  51672> velocity=<-5, -5>
position=<-51390,  51679> velocity=< 5, -5>
position=< 10534, -10191> velocity=<-1,  1>
position=<-51338, -41128> velocity=< 5,  4>
position=< 10529,  51678> velocity=<-1, -5>
position=< 31121, -41136> velocity=<-3,  4>
position=< 51777,  20740> velocity=<-5, -2>
position=<-10098, -20511> velocity=< 1,  2>
position=< 51721,  31057> velocity=<-5, -3>
position=< 51733,  51678> velocity=<-5, -5>
position=<-10091, -20507> velocity=< 1,  2>
position=< 41433, -20506> velocity=<-4,  2>
position=< 41457, -30816> velocity=<-4,  3>
position=< 10498,  51672> velocity=<-1, -5>
position=<-30717, -41131> velocity=< 3,  4>
position=<-20402, -41128> velocity=< 2,  4>
position=< 31137, -20504> velocity=<-3,  2>
position=< 41467,  41365> velocity=<-4, -4>
position=<-20418,  51674> velocity=< 2, -5>
position=< 10534, -10191> velocity=<-1,  1>
position=< 31149, -20506> velocity=<-3,  2>
position=<-41031, -41134> velocity=< 4,  4>
position=< 20821,  51673> velocity=<-2, -5>
position=< 10481, -51448> velocity=<-1,  5>
position=< 31147,  31052> velocity=<-3, -3>
position=< 31134, -10191> velocity=<-3,  1>
position=<-20426,  20745> velocity=< 2, -2>
position=< 10515,  20741> velocity=<-1, -2>
position=< 10501, -30824> velocity=<-1,  3>
position=<-20410,  31049> velocity=< 2, -3>
position=<-51347, -10197> velocity=< 5,  1>
position=<-41043,  20737> velocity=< 4, -2>
position=<-20403, -51443> velocity=< 2,  5>
position=<-41026,  31053> velocity=< 4, -3>
position=< 10518,  10433> velocity=<-1, -1>
position=< 41434, -41132> velocity=<-4,  4>
position=< 20812, -30824> velocity=<-2,  3>
position=< 51756, -10198> velocity=<-5,  1>
position=< 51721,  20737> velocity=<-5, -2>
position=< 10507,  41366> velocity=<-1, -4>
position=< 51755, -20506> velocity=<-5,  2>
position=< 51777,  41363> velocity=<-5, -4>
position=< 51741,  41363> velocity=<-5, -4>
position=<-10127,  31057> velocity=< 1, -3>
position=< 51761,  31057> velocity=<-5, -3>
position=<-51388,  20740> velocity=< 5, -2>
position=<-41087, -10196> velocity=< 4,  1>
position=<-20460, -41127> velocity=< 2,  4>
position=<-30747, -51444> velocity=< 3,  5>
position=< 20805,  51678> velocity=<-2, -5>
position=<-30759,  51680> velocity=< 3, -5>
position=< 31140, -30824> velocity=<-3,  3>
position=< 20841,  41368> velocity=<-2, -4>
position=< 10532,  41365> velocity=<-1, -4>
position=<-30735, -41132> velocity=< 3,  4>
position=<-20411,  51678> velocity=< 2, -5>
position=<-10119, -20511> velocity=< 1,  2>
position=<-20439,  31052> velocity=< 2, -3>
position=< 20794,  41363> velocity=<-2, -4>
position=<-20436,  20736> velocity=< 2, -2>
position=< 51769, -51448> velocity=<-5,  5>
position=<-20422,  20741> velocity=< 2, -2>
position=<-51375,  31048> velocity=< 5, -3>
position=< 20846, -10193> velocity=<-2,  1>
position=< 31154, -51447> velocity=<-3,  5>
position=<-41087, -20511> velocity=< 4,  2>
position=<-41063,  20741> velocity=< 4, -2>
position=<-10134,  20745> velocity=< 1, -2>
position=< 31129,  20743> velocity=<-3, -2>
position=< 31156, -30824> velocity=<-3,  3>
position=< 31142,  31055> velocity=<-3, -3>
position=< 41417,  41360> velocity=<-4, -4>
position=<-20450, -20511> velocity=< 2,  2>
position=< 51770, -30822> velocity=<-5,  3>
position=< 41410, -30815> velocity=<-4,  3>
position=< 41451,  10429> velocity=<-4, -1>
position=<-20431, -41136> velocity=< 2,  4>
position=<-51371,  20736> velocity=< 5, -2>
position=< 20809, -20510> velocity=<-2,  2>
position=< 10507, -41133> velocity=<-1,  4>
position=<-20418,  31051> velocity=< 2, -3>
position=<-20431, -20508> velocity=< 2,  2>
position=<-20429, -30818> velocity=< 2,  3>
position=< 20817, -20512> velocity=<-2,  2>
position=<-20421,  41360> velocity=< 2, -4>
position=<-20439,  10426> velocity=< 2, -1>
position=<-10102, -30817> velocity=< 1,  3>
position=< 20826, -20511> velocity=<-2,  2>
position=< 31117, -30819> velocity=<-3,  3>
position=<-10090,  41365> velocity=< 1, -4>
position=<-30751, -51442> velocity=< 3,  5>
position=<-30730,  31057> velocity=< 3, -3>
position=<-30751,  10427> velocity=< 3, -1>
position=<-20414,  10426> velocity=< 2, -1>
position=<-30765, -51443> velocity=< 3,  5>
position=< 31155,  31048> velocity=<-3, -3>
position=< 20813,  10428> velocity=<-2, -1>
position=< 31123,  51676> velocity=<-3, -5>
position=< 51734,  51672> velocity=<-5, -5>
position=< 31137,  41365> velocity=<-3, -4>
position=< 51737, -20505> velocity=<-5,  2>
position=<-41087,  41360> velocity=< 4, -4>
position=< 41417,  41368> velocity=<-4, -4>
position=< 31129,  20740> velocity=<-3, -2>
position=< 51745,  51681> velocity=<-5, -5>
position=< 20834, -51445> velocity=<-2,  5>
position=< 51745, -30816> velocity=<-5,  3>
position=<-41044, -30819> velocity=< 4,  3>
position=<-10124,  20740> velocity=< 1, -2>
position=<-30743, -20511> velocity=< 3,  2>
position=< 20812,  10428> velocity=<-2, -1>
position=<-41087,  31056> velocity=< 4, -3>
position=< 41468, -20512> velocity=<-4,  2>
position=< 51771, -10195> velocity=<-5,  1>
position=< 20801,  10432> velocity=<-2, -1>
position=<-41079, -10200> velocity=< 4,  1>
position=<-10140, -30820> velocity=< 1,  3>
position=<-30756,  10424> velocity=< 3, -1>
position=< 41409,  41366> velocity=<-4, -4>
position=<-10127,  31053> velocity=< 1, -3>
position=<-30751,  10425> velocity=< 3, -1>
position=<-51359, -41134> velocity=< 5,  4>
position=< 41462, -51440> velocity=<-4,  5>
position=<-30751, -30817> velocity=< 3,  3>
position=< 10497, -41128> velocity=<-1,  4>
position=<-20402, -30821> velocity=< 2,  3>
position=< 51782, -30822> velocity=<-5,  3>
position=<-20439, -30823> velocity=< 2,  3>
position=<-20406,  51677> velocity=< 2, -5>
position=< 31133,  10432> velocity=<-3, -1>
position=<-41087, -41134> velocity=< 4,  4>
position=<-20404,  51677> velocity=< 2, -5>
position=<-20422, -20507> velocity=< 2,  2>
position=<-41043, -30823> velocity=< 4,  3>
position=< 20785, -30817> velocity=<-2,  3>
position=<-51387, -10193> velocity=< 5,  1>
position=< 10513, -41133> velocity=<-1,  4>
position=<-10095,  41365> velocity=< 1, -4>
position=<-51379, -51440> velocity=< 5,  5>
position=< 31146, -30818> velocity=<-3,  3>
position=< 31106, -30818> velocity=<-3,  3>
position=< 31130,  41364> velocity=<-3, -4>
position=<-51379, -20510> velocity=< 5,  2>
position=<-30739,  10425> velocity=< 3, -1>
position=<-20443,  41364> velocity=< 2, -4>
position=< 20793,  51673> velocity=<-2, -5>
position=<-51343, -20506> velocity=< 5,  2>
position=<-20407,  51679> velocity=< 2, -5>
position=< 31106, -20510> velocity=<-3,  2>
position=<-10098, -51448> velocity=< 1,  5>
position=<-41052, -51441> velocity=< 4,  5>
position=<-20402, -20507> velocity=< 2,  2>
position=< 31129,  51677> velocity=<-3, -5>
position=< 20834,  41363> velocity=<-2, -4>
position=<-41075, -41134> velocity=< 4,  4>
position=< 10478,  41369> velocity=<-1, -4>
position=<-41083,  31057> velocity=< 4, -3>
position=< 10518, -30816> velocity=<-1,  3>
position=< 31129,  51678> velocity=<-3, -5>
position=< 51741, -30823> velocity=<-5,  3>
position=< 41429,  10425> velocity=<-4, -1>
position=<-20439, -30822> velocity=< 2,  3>
position=< 20806, -41136> velocity=<-2,  4>
position=<-41050, -10191> velocity=< 4,  1>
position=< 51737, -41129> velocity=<-5,  4>
position=< 41433, -51439> velocity=<-4,  5>
position=< 51756, -20510> velocity=<-5,  2>
position=< 51766,  51679> velocity=<-5, -5>
position=<-30723,  10427> velocity=< 3, -1>
position=< 41470,  31051> velocity=<-4, -3>
position=< 20829, -20507> velocity=<-2,  2>
position=< 41460,  31053> velocity=<-4, -3>
position=<-51359, -51442> velocity=< 5,  5>
position=< 31113, -41128> velocity=<-3,  4>
position=<-20415, -20504> velocity=< 2,  2>
position=<-20438,  41364> velocity=< 2, -4>
position=<-51363,  10432> velocity=< 5, -1>
position=<-30714,  10431> velocity=< 3, -1>
position=<-20402,  20743> velocity=< 2, -2>
position=<-20450,  51680> velocity=< 2, -5>
position=< 51732, -30819> velocity=<-5,  3>
position=<-51370, -41136> velocity=< 5,  4>
position=< 10485,  51675> velocity=<-1, -5>
position=< 20829,  51673> velocity=<-2, -5>
position=< 10524,  31053> velocity=<-1, -3>
position=< 10513,  31052> velocity=<-1, -3>
position=<-30756,  20745> velocity=< 3, -2>
position=< 20817,  20745> velocity=<-2, -2>
position=< 41465, -30817> velocity=<-4,  3>
position=< 20809,  41367> velocity=<-2, -4>
position=< 31116,  31057> velocity=<-3, -3>
position=<-20426,  20736> velocity=< 2, -2>
position=<-30738, -41136> velocity=< 3,  4>
position=<-41078, -41130> velocity=< 4,  4>
position=<-30751, -10192> velocity=< 3,  1>
position=< 31146,  41362> velocity=<-3, -4>
position=<-30730, -51442> velocity=< 3,  5>";
    }
}