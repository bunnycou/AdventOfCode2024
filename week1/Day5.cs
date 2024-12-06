using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace AdventOfCode2024.week1
{
    internal class Day5
    {
        private static List<string> input = File.ReadAllLines("inputs/day5.txt").ToList();
        private static Dictionary<int, List<int>> pages = new();
        private static List<int> order = new();
        private static List<string> updates = new();

        public static string Answer()
        {
            ParseInput();
            return $"Day 5: {Primary()} AND {Secondary()}";
        }

        private static void ParseInput()
        {
            bool firstHalf = true;
            foreach (var line in input)
            {
                if (firstHalf)
                {
                    if (line.Length == 0) // if we reach the break point in the input
                    {
                        firstHalf = false;
                    }
                    else // building our reference dictionary
                    {
                        string[] linesplit = line.Split('|');
                        int pageNum = int.Parse(linesplit[0]);
                        int pageAfter = int.Parse(linesplit[1]);
                        if (pages.ContainsKey(pageNum))
                        {
                            pages[pageNum].Add(pageAfter);
                        }
                        else
                        {
                            pages.Add(pageNum, new List<int>() { pageAfter });
                        }
                    }
                }
                else
                {
                    updates.Add(line);
                }
            }
        }

        public static int Primary() // find valid lines, add middle num
        {
            int total = 0;

            foreach (var line in updates)
            {
                // parse input and make sure that no after page shows up before page in list
                string[] pageraw = line.Split(",");
                List<int> pageList = new();
                foreach (string p in pageraw) { pageList.Add(int.Parse(p)); }

                bool valid = true;
                for (int i = 1; i < pageList.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (pages[pageList[i]].Contains(pageList[j]))
                        {
                            valid = false;
                        }
                    }
                }
                if (valid)
                {
                    total += pageList[pageList.Count / 2]; // add middle number
                }
            }

            return total;
        }
        private static int Secondary() // find not valid lines, order them (not numerically), add middle num
        {
            int total = 0;

            foreach (string line in updates)
            {
                string[] pageraw = line.Split(",");
                List<int> pageList = new();
                foreach (string p in pageraw) { pageList.Add(int.Parse(p)); }

                bool valid = true;

                for (int i = 1; i < pageList.Count; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (pages[pageList[i]].Contains(pageList[j]))
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid) { break; }
                }
                if (!valid)
                {
                    pageList = SortPages(pageList);
                    total += pageList[pageList.Count / 2]; // add middle number
                }
            }

            return total;
        }

        private static List<int> SortPages(List<int> original)
        {
            for (int i = 1; i < original.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (pages[original[i]].Contains(original[j])) // page needs to come later
                    {
                        original.Insert(j, original[i]);
                        original.RemoveAt(i + 1);
                        i = 1; j = -1; // restart check
                    }
                }
            }

            return original;
        }
    }
}
