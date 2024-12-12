using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week2
{
    internal class Day12
    {
        private static List<string> input = File.ReadAllLines("inputs/day12t.txt").ToList();
        private static List<List<int[]>> plots = new();
        private static List<int[]> locChecked = new();

        public static string Answer()
        {
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    int[] locCur = [row, col];
                    if (ListContainsIntArr(locChecked, locCur)) { continue; }
                    plots.Add(new());
                    MapPlot(locCur);
                }
            }

            return $"Day 10: {Primary()} AND {Secondary()}";
        }

        public static long Primary()
        {
            long total = 0;

            foreach (List<int[]> plot in plots)
            {
                // area * perimeter
                int area = plot.Count;
                int perimeter = Perimeter(plot);
                total += area * perimeter;
                // Console.WriteLine($"{area} * {perimeter} += {total}");
            }

            return total;
        }

        public static long Secondary()
        {
            long total = 0;

            foreach(List<int[]> plot in plots)
            {
                int area = plot.Count;
                int sides = Sides(plot);
                total += area * sides;
                Console.WriteLine($"{area} * {sides} += {total}");
            }

            return total;
        }

        private static void MapPlot(int[] loc)
        {
            // if already seen before then return
            // if not then check each adjacent for matching char
            // call MapPlot on new adjacent locations
            if (ListContainsIntArr(plots[plots.Count - 1], loc)) { return; }
            plots[plots.Count - 1].Add([loc[0], loc[1]]);
            locChecked.Add([loc[0], loc[1]]);
            char cur = input[loc[0]][loc[1]];
            //up
            if (loc[0] > 0 && input[loc[0] - 1][loc[1]] == cur)
            {
                MapPlot([loc[0] - 1, loc[1]]);
            }
            //down
            if (loc[0] < input.Count-1 && input[loc[0] + 1][loc[1]] == cur)
            {
                MapPlot([loc[0] + 1, loc[1]]);
            }
            //left
            if (loc[1] > 0 && input[loc[0]][loc[1] - 1] == cur)
            {
                MapPlot([loc[0], loc[1] - 1]);
            }
            //right
            if (loc[1] < input[0].Length-1 && input[loc[0]][loc[1] + 1] == cur)
            {
                MapPlot([loc[0], loc[1] + 1]);
            }
        }

        private static int Perimeter(List<int[]> plot)
        {
            int total = 0;

            char cur = input[plot[0][0]][plot[0][1]];
            foreach (int[] loc in plot)
            {
                //up
                if (loc[0] == 0 || input[loc[0] - 1][loc[1]] != cur)
                {
                    total++;
                }
                //down
                if (loc[0] == input.Count - 1 || input[loc[0] + 1][loc[1]] != cur)
                {
                    total++;
                }
                //left
                if (loc[1] == 0 || input[loc[0]][loc[1] - 1] != cur)
                {
                    total++;
                }
                //right
                if (loc[1] == input[0].Length - 1 || input[loc[0]][loc[1] + 1] != cur)
                {
                    total++;
                }
            }

            return total;
        }

        private static int Sides(List<int[]> plot)
        {
            int total = 0;
            char cur = input[plot[0][0]][plot[0][1]];
            List<int[]> upChecked = new();
            List<int[]> downChecked = new();
            List<int[]> leftChecked = new();
            List<int[]> rightChecked = new();

            foreach (int[] loc in plot)
            {
                //up
                if ((loc[0] == 0 || input[loc[0] - 1][loc[1]] != cur) && !ListContainsIntArr(upChecked, loc))
                {
                    SideCheckUD(ref upChecked, loc);
                    total++;
                }
                //down
                if ((loc[0] == input.Count - 1 || input[loc[0] + 1][loc[1]] != cur) && !ListContainsIntArr(downChecked, loc))
                {
                    SideCheckUD(ref downChecked, loc);
                    total++;
                }
                //left
                if ((loc[1] == 0 || input[loc[0]][loc[1] - 1] != cur) && !ListContainsIntArr(leftChecked, loc))
                {
                    SideCheckLR(ref leftChecked, loc);
                    total++;
                }
                //right
                if ((loc[1] == input[0].Length - 1 || input[loc[0]][loc[1] + 1] != cur) && !ListContainsIntArr(rightChecked, loc))
                {
                    SideCheckLR(ref rightChecked, loc);
                    total++;
                }
            }

            return total;
        }

        private static void SideCheckUD(ref List<int[]> check, int[] loc) // side check for up and down sides
        {
            int[] temp = [loc[0], loc[1]];
            char cur = input[loc[0]][loc[1]];
            while (temp[1] >= 0 && input[temp[0]][temp[1]] == cur &&
                (temp[0] == 0 || temp[0] == input.Count - 1 || input[temp[0] - 1][temp[1]] != cur || input[temp[0] + 1][temp[1]] != cur)) // move left
            {
                check.Add([temp[0], temp[1]]);
                temp[1]--;
            }
            temp = [loc[0], loc[1] + 1];
            while (temp[1] < input[0].Length && input[temp[0]][temp[1]] == cur &&
                (temp[0] == 0 || temp[0] == input.Count - 1 || input[temp[0] - 1][temp[1]] != cur || input[temp[0] + 1][temp[1]] != cur)) // move right
            {
                check.Add([temp[0], temp[1]]);
                temp[1]++;
            }
        }

        private static void SideCheckLR(ref List<int[]> check, int[] loc) // side check for left and right sides
        {
            int[] temp = [loc[0], loc[1]];
            char cur = input[loc[0]][loc[1]];
            while (temp[0] >= 0 && input[temp[0]][temp[1]] == cur &&
                (temp[1] == 0 || temp[1] == input[0].Length - 1 || input[temp[0]][temp[1] - 1] != cur || input[temp[0]][temp[1] + 1] != cur)) // move up
            {
                check.Add([temp[0], temp[1]]);
                temp[0]--;
            }
            temp = [loc[0] + 1, loc[1]];
            while (temp[0] < input.Count && input[temp[0]][temp[1]] == cur &&
                (temp[1] == 0 || temp[1] == input[0].Length - 1 || input[temp[0]][temp[1] - 1] != cur || input[temp[0]][temp[1] + 1] != cur)) // move down
            {
                check.Add([temp[0], temp[1]]);
                temp[0]++;
            }
        }

        public static bool ListContainsIntArr(List<int[]> list, int[] arr)
        {
            bool contains = false;
            foreach (int[] ints in list)
            {
                if (ints[0] == arr[0] && ints[1] == arr[1])
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
    }
}
