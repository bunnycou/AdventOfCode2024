using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week2
{
    internal class Day10
    {
        private static List<string> input = File.ReadAllLines("inputs/day10.txt").ToList();

        public static string Answer()
        {
            return $"Day 10: {Primary()} AND {Secondary()}";
        }

        private static int Primary()
        {
            int total = 0;
            Dictionary<int[], int> TrailheadScores = new();
            int current = 0;
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    int index = int.Parse(input[row][col].ToString());
                    if (index == current)
                    {
                        if (current == 0)
                        {
                            TrailheadScores.Add([row, col], traceAll9s([row, col]).Count);
                        }
                    }
                }
            }

            foreach (int value in TrailheadScores.Values)
            {
                total += value;
            }

            return total;
        }

        private static int Secondary()
        {
            int total = 0;

            Dictionary<int[], int> TrailheadScores = new();
            int current = 0;
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    int index = int.Parse(input[row][col].ToString());
                    if (index == current)
                    {
                        if (current == 0)
                        {
                            TrailheadScores.Add([row, col], traceAllPaths([row, col]));
                        }
                    }
                }
            }

            foreach (int value in TrailheadScores.Values)
            {
                total += value;
            }

            return total;
        }

        private static Dictionary<string, bool> ValidDirections(int row, int col) // returns dictionary saying if UP DOWN LEFT RIGHT are valid paths
        {
            Dictionary<string, bool> ret = new();
            int current = int.Parse(input[row][col].ToString());
            if (row > 0)
            {
                int up = int.Parse(input[row - 1][col].ToString());
                if (up == current+1)
                {
                    ret.Add("UP", true);
                }
            }
            if (!ret.ContainsKey("UP"))
            {
                ret.Add("UP", false);
            }

            if (row < input.Count-1)
            {
                int down = int.Parse(input[row + 1][col].ToString());
                if (down == current + 1)
                {
                    ret.Add("DOWN", true);
                }
            }
            if (!ret.ContainsKey("DOWN"))
            {
                ret.Add("DOWN", false);
            }

            if (col > 0)
            {
                int left = int.Parse(input[row][col - 1].ToString());
                if (left == current + 1)
                {
                    ret.Add("LEFT", true);
                }
            }
            if (!ret.ContainsKey("LEFT"))
            {
                ret.Add("LEFT", false);
            }

            if (col < input[0].Length-1)
            {
                int right = int.Parse(input[row][col + 1].ToString());
                if (right == current + 1)
                {
                    ret.Add("RIGHT", true);
                }
            }
            if (!ret.ContainsKey("RIGHT"))
            {
                ret.Add("RIGHT", false);
            }

            return ret;
        }

        private static List<int[]> traceAll9s(int[] start) // find amount of unique reachable nines, this is trailhead count for part 1
        {
            List<int[]> locNine = new();

            int current = int.Parse(input[start[0]][start[1]].ToString());

            if (current == 9)
            {
                locNine.Add([start[0], start[1]]);
                return locNine;
            }

            Dictionary<string, bool> directions = ValidDirections(start[0], start[1]);
            if (!directions.Values.ToList().Contains(true))
            {
                return locNine; // dead end path
            }
            if (directions["UP"])
            {
                List<int[]> tracedPaths = traceAll9s([start[0] - 1, start[1]]);
                AddNewLocs(ref locNine, ref tracedPaths);
            }
            if (directions["DOWN"])
            {
                List<int[]> tracedPaths = traceAll9s([start[0] + 1, start[1]]);
                AddNewLocs(ref locNine, ref tracedPaths);
            }
            if (directions["LEFT"])
            {
                List<int[]> tracedPaths = traceAll9s([start[0], start[1] - 1]);
                AddNewLocs(ref locNine, ref tracedPaths);
            }
            if (directions["RIGHT"])
            {
                List<int[]> tracedPaths = traceAll9s([start[0], start[1] + 1]);
                AddNewLocs(ref locNine, ref tracedPaths);
            }

            return locNine;
        }

        private static void AddNewLocs(ref List<int[]> main, ref List<int[]> sub)
        {
            foreach (int[] subloc in sub)
            {
                bool contains = false;
                foreach (int[] mainloc in main)
                {
                    if (mainloc[0] == subloc[0] && mainloc[1] == subloc[1])
                    {
                        contains = true;
                    }
                }
                if (!contains)
                {
                    main.Add([subloc[0], subloc[1]]);
                }
            }
        }

        private static int traceAllPaths(int[] start)
        {
            int totalPaths = 0;

            int current = int.Parse(input[start[0]][start[1]].ToString());

            if (current == 9)
            {
                return ++totalPaths;
            }

            Dictionary<string, bool> directions = ValidDirections(start[0], start[1]);
            if (!directions.Values.ToList().Contains(true))
            {
                return 0; // dead end path, dont increase path count
            }
            if (directions["UP"])
            {
                totalPaths += traceAllPaths([start[0] - 1, start[1]]);
            }
            if (directions["DOWN"])
            {
                totalPaths += traceAllPaths([start[0] + 1, start[1]]);
            }
            if (directions["LEFT"])
            {
                totalPaths += traceAllPaths([start[0], start[1] - 1]);
            }
            if (directions["RIGHT"])
            {
                totalPaths += traceAllPaths([start[0], start[1] + 1]);
            }

            return totalPaths;
        }
    }
}
