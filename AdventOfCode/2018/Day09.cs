using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2018
{
    public class Day09
    {
        public LinkedList<int> game = new LinkedList<int>() { };

        public void Solve()
        {
            string[] inputdata = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int players = int.Parse(inputdata[0]);
            int marbleMaxWorth = int.Parse(inputdata[6]);

            game.AddFirst(0);
            LinkedListNode<int> currentNode = game.Find(0);
            Dictionary<int, long> scoreboard = Enumerable.Range(0, players).ToDictionary(key => key, val => 0L);

            for (int i = 1; i <= marbleMaxWorth; i++)
            {
                if (i % 23 == 0)
                {
                    int playerId = i % players;

                    //scoreboard[playerId] += i;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    currentNode = currentNode.Previous ?? game.Last;
                    int valueToRemove = currentNode.Value;
                    currentNode = currentNode.Next ?? game.First;
                    scoreboard[playerId] += i + valueToRemove;
                    game.Remove(valueToRemove);
                }
                else
                {
                    // skip 1 node and add after this
                    currentNode = currentNode.Next ?? game.First;
                    game.AddAfter(currentNode, i);
                    currentNode = currentNode.Next;
                }
            }
            long highScore = scoreboard.Max(kvp => kvp.Value);

            Console.WriteLine($"Day09 part1 answer: {highScore}");

            //Day09 part1 answer: 3516007333
            //Got it solved in 1927135ms (32 minutes!!)
        }

        public string input = @"411 players; last marble is worth 7105800 points";
        public string testinput2 = @"9 players; last marble is worth 25 points"; // : high score is 8317
        public string testinput = @"10 players; last marble is worth 1618 points"; // : high score is 8317
    }
}