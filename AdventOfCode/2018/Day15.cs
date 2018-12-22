using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    internal class Day15
    {
        public void Solve()
        {
            string[] lines = testinput.Split(Environment.NewLine);

            char[][] grid = new char[lines.Length][];
            var players = new List<(char playertype, int X, int Y, int HP, bool Dead)>();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                grid[i] = new char[line.Length];
                for (int j = 0; j < line.Length; j++)
                {
                    var c = line[j];
                    if (c == '#' || c == '.') { grid[i][j] = c; }
                    else
                    {
                        grid[i][j] = '.';
                        players.Add((c, i, j, 200, false));
                    }
                }
            }

            // game on!
            int turns = 1;
            bool gameInProgress = true;
#pragma warning restore S1155 // "Any()" should be used to test for emptiness
            while (gameInProgress)
            {
                // sort players top-bottom & left to right
                players = players.OrderBy(x => x.X).ThenBy(y => y.Y).ToList();

                // print players
                foreach (var pl in players) { Console.WriteLine($"Type: {pl.playertype} - {pl.HP}, on (X: {pl.X}, Y: {pl.Y})"); }

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Dead) { continue; }
                    var player = players[i];                    
                    bool isElf = player.playertype == 'E';
                    var enemies = players.Where(x => x.playertype != player.playertype);

                    // bubble untill reachable player
                    var queue = new Queue<(int X, int Y)>();
                    var tilesDone = new HashSet<(int X, int Y)>();
                    queue.Enqueue((player.X, player.Y));
                    tilesDone.Add((player.X, player.Y));
                    bool playerFound = false;
                    while (queue.Count > 0)
                    {
                        var square = queue.Dequeue();
                        // exit if this square has already been visited
                        if (!tilesDone.Contains((square.X, square.Y))){ continue; }

                        // check neighbours if it contains a player 
                        




                        // can't move through walls -> early exit
                        if (grid[square.X][square.Y - 1] == '#') { continue; }
                        if (grid[square.X][square.Y + 1] == '#') { continue; }
                        if (grid[square.X - 1][square.Y] == '#') { continue; }
                        if (grid[square.X + 1][square.Y] == '#') { continue; }


                        // check surroundings and add
                        if (grid[square.X][square.Y - 1] == '.') { queue.Enqueue((square.X, square.Y - 1)); }
                        if (grid[square.X][square.Y + 1] == '.') { queue.Enqueue((square.X, square.Y + 1)); }
                        if (grid[square.X - 1][square.Y] == '.') { queue.Enqueue((square.X - 1, square.Y)); }
                        if (grid[square.X + 1][square.Y] == '.') { queue.Enqueue((square.X + 1, square.Y)); }
                        // is there a player on this square?

                        tilesDone.Add((square.X, square.Y));
                    }
                }

                // update meta

                players = players.Where(x => !x.Dead).ToList();
                gameInProgress = players.Any(x => x.playertype == 'E') && players.Any(x => x.playertype == 'G');
                turns++;

                // debug kill switch
                if (turns == 2) { gameInProgress = false; }
            }

            // print function - grid
            foreach (var gridLine in grid) { Console.WriteLine(new string(gridLine)); }

            // print function - players
            foreach (var player in players) { Console.WriteLine($"Type: {player.playertype} - {player.HP}, on (X: {player.X}, Y: {player.Y})"); }
        }

        public string testinput = @"#######
#.G...#
#...EG#
#.#.#G#
#..G#E#
#.....#
#######";

        public string input = @"################################
#######################.########
######################....######
#######################.....####
##################..##......####
###################.##.....#####
###################.....G..#####
##################.....G...#####
############.....GG.G...#..#####
##############...##....##.######
############...#..G............#
###########......E.............#
###########...#####..E........##
#...#######..#######.......#####
#..#..G....G#########.........##
#..#....G...#########..#....####
##.....G....#########.E......###
#####G.....G#########..E.....###
#####.......#########....#.....#
#####G#G....G#######.......#..E#
###.....G.....#####....#.#######
###......G.....G.G.......#######
###..................#..########
#####...................########
#####..............#...#########
####......G........#.E.#E..#####
####.###.........E...#E...######
####..##........#...##.....#####
########.#......######.....#####
########...E....#######....#####
#########...##..########...#####
################################";
    }
}