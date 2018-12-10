using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2018
{
    internal class Day07
    {
        public void Solve()
        {
            var routes = new List<(string from, string to)>();
            var routes2 = new List<(string from, string to)>();
            var allPoints = new HashSet<string>();

            // parse input
            foreach (string line in input.Split(Environment.NewLine))
            {
                string[] words = line.Split(' ');
                string from = words[1];
                string to = words[7];
                routes.Add((from, to));
                routes2.Add((from, to));
                allPoints.Add(from);
                allPoints.Add(to);
            }

            // Part 1
            var sortedPoints = allPoints.OrderBy(x => x).ToList();
            var sb = new StringBuilder();
            while (sortedPoints.Count > 0)
            {
                // get the first character that has ZERO dependencies
                string curChar = sortedPoints.Where(r => routes.Count(d => d.to == r) == 0).First();

                sb.Append(curChar);
                sortedPoints.Remove(curChar);

                // remove this dependency for all steps that needed this one
                routes.RemoveAll(r => r.from == curChar);
            }

            Console.WriteLine($"Day07 part1 answer:{sb.ToString()}");

            // Part 2
            const string DUMMY = "DUMMY";
            var workers = new List<(string id, int timeLeft)>(5);
            for (int i = 0; i < 5; i++) { workers.Add((id: DUMMY, timeLeft: 0)); }
            sortedPoints = allPoints.OrderBy(x => x).ToList();
            int timeOffset = 60;
            int totalTime = 0;
            
            while (sortedPoints.Count > 0)
            {
                // decrease time for current worker
                for(int i =0; i < workers.Count; i++)
                {
                    if (workers[i].id == DUMMY) { continue; } // skip empty entries

                    var worker = workers[i];
                    if (worker.timeLeft == 0) {
                        
                        sortedPoints.Remove(worker.id);
                        routes2.RemoveAll(r => r.from == worker.id);
                        worker.id = DUMMY;
                    }
                    else { worker.timeLeft--; }

                    // update worker
                    workers[i] = worker;
                }
                


                // add new work IF available
                string inProgress = string.Concat(workers.Where(kvp => kvp.id != DUMMY).Select(x => x.id));
                for (int i = 0; i < workers.Count;i++) {
                    if (workers[i].id != DUMMY) { continue; } // only fill empty holes

                    // get the first character that has ZERO dependencies
                    string curChar = sortedPoints
                                .Where(
                                    r => routes2.Count(d => d.to == r) == 0 
                                    && !workers.Where(kvp=>kvp.id != DUMMY)
                                    .Select(kvp=>kvp.id).ToHashSet().Contains(r) )
                                .FirstOrDefault();
                    if (curChar == null) { continue; }

                    //sb.Append(curChar);
                    workers[i] = (id: curChar, curChar[0] - 65 + timeOffset);
                }
                totalTime++;
            }
            Console.WriteLine($"Day07 part2 answer:{totalTime-1}");
        }

        public string testinput = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

        public string input = @"Step J must be finished before step K can begin.
Step N must be finished before step X can begin.
Step S must be finished before step G can begin.
Step T must be finished before step R can begin.
Step H must be finished before step L can begin.
Step V must be finished before step W can begin.
Step G must be finished before step U can begin.
Step K must be finished before step A can begin.
Step D must be finished before step Z can begin.
Step C must be finished before step E can begin.
Step X must be finished before step P can begin.
Step Y must be finished before step U can begin.
Step R must be finished before step O can begin.
Step W must be finished before step U can begin.
Step O must be finished before step Q can begin.
Step A must be finished before step P can begin.
Step B must be finished before step E can begin.
Step F must be finished before step E can begin.
Step Q must be finished before step U can begin.
Step M must be finished before step E can begin.
Step P must be finished before step U can begin.
Step L must be finished before step Z can begin.
Step Z must be finished before step U can begin.
Step U must be finished before step E can begin.
Step I must be finished before step E can begin.
Step H must be finished before step G can begin.
Step X must be finished before step I can begin.
Step K must be finished before step X can begin.
Step Z must be finished before step I can begin.
Step S must be finished before step M can begin.
Step L must be finished before step U can begin.
Step A must be finished before step M can begin.
Step W must be finished before step A can begin.
Step N must be finished before step A can begin.
Step S must be finished before step E can begin.
Step W must be finished before step Q can begin.
Step J must be finished before step L can begin.
Step Q must be finished before step L can begin.
Step M must be finished before step U can begin.
Step H must be finished before step E can begin.
Step D must be finished before step E can begin.
Step V must be finished before step P can begin.
Step Q must be finished before step M can begin.
Step X must be finished before step W can begin.
Step K must be finished before step I can begin.
Step T must be finished before step H can begin.
Step Y must be finished before step L can begin.
Step G must be finished before step O can begin.
Step M must be finished before step Z can begin.
Step F must be finished before step Z can begin.
Step Q must be finished before step E can begin.
Step H must be finished before step C can begin.
Step Q must be finished before step P can begin.
Step D must be finished before step U can begin.
Step Z must be finished before step E can begin.
Step O must be finished before step M can begin.
Step L must be finished before step I can begin.
Step J must be finished before step A can begin.
Step Q must be finished before step Z can begin.
Step P must be finished before step I can begin.
Step K must be finished before step O can begin.
Step R must be finished before step E can begin.
Step W must be finished before step F can begin.
Step D must be finished before step Q can begin.
Step R must be finished before step U can begin.
Step W must be finished before step P can begin.
Step S must be finished before step Z can begin.
Step T must be finished before step P can begin.
Step B must be finished before step Q can begin.
Step S must be finished before step T can begin.
Step R must be finished before step A can begin.
Step K must be finished before step R can begin.
Step N must be finished before step G can begin.
Step C must be finished before step W can begin.
Step T must be finished before step A can begin.
Step B must be finished before step Z can begin.
Step C must be finished before step P can begin.
Step D must be finished before step P can begin.
Step B must be finished before step P can begin.
Step F must be finished before step U can begin.
Step V must be finished before step X can begin.
Step K must be finished before step W can begin.
Step Y must be finished before step I can begin.
Step C must be finished before step B can begin.
Step X must be finished before step L can begin.
Step X must be finished before step M can begin.
Step H must be finished before step P can begin.
Step S must be finished before step F can begin.
Step J must be finished before step Y can begin.
Step Y must be finished before step Z can begin.
Step B must be finished before step I can begin.
Step S must be finished before step C can begin.
Step K must be finished before step E can begin.
Step N must be finished before step Q can begin.
Step A must be finished before step Z can begin.
Step J must be finished before step I can begin.
Step Y must be finished before step O can begin.
Step Y must be finished before step F can begin.
Step S must be finished before step U can begin.
Step D must be finished before step W can begin.
Step V must be finished before step D can begin.";
    }
}