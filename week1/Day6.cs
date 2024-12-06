using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week1
{
    internal class Day6
    {
        private static List<string> input = File.ReadAllLines("inputs/day6.txt").ToList();

        private static int[] guardLocStart = [0, 0];

        private static List<int[]> guardLocsPrimary = new();

        public static string Answer()
        {
            FindGuard();

            return $"Day 6: {Primary()} AND {Secondary()}";
        }

        private static int Primary()
        {
            int rowMax = input.Count;
            int colMax = input[0].Length;

            //Dictionary<int, List<int>> guardLocs = new();
            List<int[]> guardLocs = new();
            int[] guardLoc = [guardLocStart[0], guardLocStart[1]];
            string direction = "N"; // N E S W

            // the general loop
            // keep going until out of bounds
            // check ahead, if it is # turn right (N->E E->S S->W W->N)
            while (InBounds(guardLoc, rowMax, colMax))
            {
                int[] newGuardLoc = [guardLoc[0], guardLoc[1]];

                if (direction == "N") { newGuardLoc[0]--; }
                else if (direction == "E") { newGuardLoc[1]++; }
                else if (direction == "S") { newGuardLoc[0]++; }
                else if (direction == "W") { newGuardLoc[1]--; }

                if (CanMoveGuard(newGuardLoc, rowMax, colMax, input)) // moves out of bounds or does not encounter wall
                {
                    bool inList = false;
                    foreach (int[] loc in guardLocs) { if (loc[0] == guardLoc[0] && loc[1] == guardLoc[1]) { inList = true; break; } }
                    if (!inList)
                    {
                        guardLocs.Add([guardLoc[0], guardLoc[1]]);
                    }

                    guardLoc = [newGuardLoc[0], newGuardLoc[1]];
                }
                else // hits wall
                {
                    direction = Turn(direction);
                }
            }

            guardLocsPrimary = guardLocs;

            return guardLocs.Count;
        }

        private static int Secondary() // track last 4 turn coordinates, if they don't change you are in a loop
        {
            int total = 0;

            int rowMax = input.Count;
            int colMax = input[0].Length;

            foreach (int[] pos in guardLocsPrimary) // try a barrel in every position the guard goes through
            {
                if (pos[0] == guardLocStart[0] && pos[1] == guardLocStart[1]) { continue; } // can't place an obstancle where they already are

                List<string> map = new();

                for (int x = 0; x < rowMax; x++) // construct new map
                {
                    map.Add("");
                    for (int y = 0; y < colMax; y++)
                    {
                        if (x == pos[0] && y == pos[1])
                        {
                            map[x] += "#";
                        }
                        else
                        {
                            map[x] += input[x][y];
                        }
                    }
                }

                int[] guardLoc = [guardLocStart[0], guardLocStart[1]];
                string direction = "N"; // N E S W
                List<int[]> turns = new();

                while (InBounds(guardLoc, rowMax, colMax)) // run through new map
                {
                    int[] newGuardLoc = [guardLoc[0], guardLoc[1]];

                    if (direction == "N") { newGuardLoc[0]--; }
                    else if (direction == "E") { newGuardLoc[1]++; }
                    else if (direction == "S") { newGuardLoc[0]++; }
                    else if (direction == "W") { newGuardLoc[1]--; }

                    if (CanMoveGuard(newGuardLoc, rowMax, colMax, map)) // moves out of bounds or does not encounter wall
                    {
                        guardLoc = [newGuardLoc[0], newGuardLoc[1]];
                    }
                    else // hits wall
                    {  
                        int inTurns = 0;
                        foreach (int[] turn in turns) { if (turn[0] == guardLoc[0] && turn[1] == guardLoc[1]) { inTurns++; } }
                        if (inTurns >= 2) // the current turn is in our past turns multiple times meaning we are looped
                        {
                            total++;
                            break;
                        }
                        turns.Add([guardLoc[0], guardLoc[1]]);
                        direction = Turn(direction);
                    }
                }
            }

            return total;
        }

        private static void FindGuard()
        {
            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0; col < input[row].Length; col++)
                {
                    if (input[row][col] == '^')
                    {
                        guardLocStart = [row, col];
                    }
                }
            }
        }

        public static bool InBounds(int[] pos, int rowMax, int colMax)
        {
            return pos[0] >= 0 && pos[0] < rowMax && pos[1] >= 0 && pos[1] < colMax;
        }

        private static bool CanMoveGuard(int[] newGuardLoc, int rowMax, int colMax, List<string> map)
        {
            if (InBounds(newGuardLoc, rowMax, colMax))
            {
                if (map[newGuardLoc[0]][newGuardLoc[1]] == '#') // turn
                {
                    return false;
                }
            }
            return true;
        }

        private static string Turn(string direction)
        {
            if (direction == "N") { return "E"; }
            else if (direction == "E") { return "S"; }
            else if (direction == "S") { return "W"; }
            else if (direction == "W") { return "N"; }
            return "N"; // this should never happen
        }
    }
}
