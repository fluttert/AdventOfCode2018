using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    internal class Day12
    {
        public void Solve()
        {
            // parse input
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var initialLine = lines[0].Replace("initial state: ", string.Empty);
            var patterns = new Dictionary<string, char>();
            
            char[] state = new char[initialLine.Length * 3]; // we not expect to grow further then three times the input
            for (int i = 0; i < state.Length; i++) { state[i] = '.'; };
            for (int i = 0; i < initialLine.Length; i++) { state[initialLine.Length + i] = initialLine[i]; }
            for (int i = 1; i < lines.Length; i++) {
                string[] pattern = lines[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                patterns.Add(pattern[0], pattern[1][0]);
            }

            // iterations
            string currentState = new string(state);
            int totalPots = 0;
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j< state.Length; j++) { state[j] = '.'; };
                for (int j = 0; j < currentState.Length - 4; j++) {
                    var curPattern = currentState.Substring(j, 5);
                    if (patterns.ContainsKey(curPattern)) {
                        state[j + 2] = patterns[curPattern];
                    }
                }
                currentState = new string(state);
                Console.WriteLine($"{i} - {currentState}");
            }
            for (int i = 0; i < currentState.Length; i++) {
                if (currentState[i] == '#') { totalPots += (i - initialLine.Length); }
            }
            Console.WriteLine($"Day12 part1 answer: {totalPots}");
        }

        public string testinput = @"initial state: #..#.#..##......###...###

...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";

        public string input = @"initial state: ##.#...#.#.#....###.#.#....##.#...##.##.###..#.##.###..####.#..##..#.##..#.......####.#.#..#....##.#

#.#.# => #
..#.# => .
.#.## => #
.##.. => .
##... => #
##..# => #
#.##. => #
.#..# => #
.#### => .
....# => .
#.... => .
#.### => .
###.# => #
.#.#. => .
#...# => .
.#... => #
##.#. => #
#..## => #
..##. => .
####. => #
.###. => .
##### => .
#.#.. => .
...#. => .
..#.. => .
###.. => #
#..#. => .
.##.# => .
..... => .
##.## => #
..### => #
...## => #";
    }
}