using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2024.week1;

namespace AdventOfCode2024.week2
{
    internal class Day8
    {
        private static List<string> input = File.ReadAllLines("inputs/day8.txt").ToList();

        public static string Answer()
        {
            return $"Day 8: {Primary()} AND {Secondary()}";
        }

        private static int Primary()
        {
            int rowMax = input.Count;
            int colMax = input[0].Length;

            List<int[]> antinodeLocs = new();
            for (int row1 = 0; row1 < rowMax; row1++)
            {
                for (int col1 = 0;  col1 < colMax; col1++)
                {
                    char main = input[row1][col1];
                    if (main != '.') // new node
                    {
                        bool firstColLoop = true;
                        for (int row2 = row1; row2 < rowMax; row2++)
                        {
                            for (int col2 = 0; col2 < colMax; col2++)
                            {
                                if (firstColLoop) { col2 = col1 + 1; firstColLoop = false; if (col2 >= colMax) { continue; } }
                                char sub = input[row2][col2];
                                if (sub == main) // matching node, calculate antinodes, add if inbounds
                                {
                                    int rowDist = row2 - row1;
                                    int colDist = col2 - col1;
                                    int[] beforeMain = [row1 - rowDist, col1 - colDist];
                                    int[] afterSub = [row2 + rowDist, col2 + colDist];

                                    if (Day6.InBounds(beforeMain, rowMax, colMax) && antinodeLocs.FindIndex(l => l.SequenceEqual(beforeMain)) == -1)
                                    {
                                        antinodeLocs.Add([beforeMain[0], beforeMain[1]]);
                                    }
                                    if (Day6.InBounds(afterSub, rowMax, colMax) && antinodeLocs.FindIndex(l => l.SequenceEqual(afterSub)) == -1)
                                    {
                                        antinodeLocs.Add([afterSub[0], afterSub[1]]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return antinodeLocs.Count;
        }

        private static int Secondary()
        {
            int rowMax = input.Count;
            int colMax = input[0].Length;

            List<int[]> antinodeLocs = new();

            for (int row1 = 0; row1 < rowMax; row1++)
            {
                for (int col1 = 0; col1 < colMax; col1++)
                {
                    char main = input[row1][col1];
                    if (main != '.') // new node
                    {
                        bool firstColLoop = true;
                        for (int row2 = row1; row2 < rowMax; row2++)
                        {
                            for (int col2 = 0; col2 < colMax; col2++)
                            {
                                if (firstColLoop) { col2 = col1 + 1; firstColLoop = false; if (col2 >= colMax) { continue; } }
                                char sub = input[row2][col2];
                                if (sub == main) // matching node, calculate antinodes, add if inbounds
                                {
                                    int rowDist = row2 - row1;
                                    int colDist = col2 - col1;
                                    int[] testpos = [row1, col1];
                                    while (Day6.InBounds(testpos, rowMax, colMax)) // move backwards
                                    {
                                        if (antinodeLocs.FindIndex(l => l.SequenceEqual(testpos)) == -1)
                                        {
                                            antinodeLocs.Add([testpos[0], testpos[1]]);
                                        }

                                        testpos = [testpos[0] - rowDist, testpos[1] - colDist];
                                    }
                                    testpos = [row2, col2];
                                    while (Day6.InBounds(testpos, rowMax, colMax)) // move forwards
                                    {
                                        if (antinodeLocs.FindIndex(l => l.SequenceEqual(testpos)) == -1)
                                        {
                                            antinodeLocs.Add([testpos[0], testpos[1]]);
                                        }

                                        testpos = [testpos[0] + rowDist, testpos[1] + colDist];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return antinodeLocs.Count;
        }
    }
}
