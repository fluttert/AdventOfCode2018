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

            //char[] state = new char[4 + initialLine.Length * 2]; // we not expect to grow further then three times the input
            //
            var state = new List<char>() { '.', '.', '.' };
            for (int i = 0; i < initialLine.Length; i++) { state.Add(initialLine[i]); }
            state.Add('.'); state.Add('.'); state.Add('.'); state.Add('.');

            for (int i = 1; i < lines.Length; i++) {
                string[] pattern = lines[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                patterns.Add(pattern[0], pattern[1][0]);
            }

            // iterations
            string currentState = new string(state.ToArray());
            long stableFrom = 0;
            //Console.WriteLine("    "+currentState);
            int totalPotsAfter20Iterations = 0;
            for (int i = 0; i < 500; i++) {
                for (int j = 0; j < currentState.Length - 4; j++) {
                    var curPattern = currentState.Substring(j, 5);
                    if (patterns.ContainsKey(curPattern))
                    {
                        state[j + 2] = patterns[curPattern];
                        if (j == (currentState.Length - 5)) {
                            state.Add('.'); // extend the list!
                        }
                    }
                    else { state[j + 2] = '.'; }

                    
                }

                // part 02 - see when stabilised 
                var newCurrentState = new string(state.ToArray());
                if (newCurrentState.Substring(1) == currentState) {
                    // break;
                    Console.WriteLine("Stable from iteration:"+i);
                    stableFrom = i;
                    break;
                }
                currentState = new string(state.ToArray());

                // part 01 
                if (i == 19) {
                    for (int j = 0; j < currentState.Length; j++)
                    {
                        if (currentState[j] == '#') { totalPotsAfter20Iterations += (j - 3); }
                    }
                }
                //Console.WriteLine($"{i} - {currentState}"); // print em out!
            }

            // 
            long totalPotsAfter50Billion = 0;
            long indexShift = 50_000_000_000 - stableFrom;
            for (int i = 0; i < currentState.Length; i++)
            {
                if (currentState[i] == '#') { totalPotsAfter50Billion += indexShift + (i - 3); }
            }
            Console.WriteLine($"Day12 part1 answer: {totalPotsAfter20Iterations}");
            Console.WriteLine($"Day12 part2 answer: {totalPotsAfter50Billion}");


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