using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Day4
    {
        private static List<string> input = File.ReadAllLines("inputs/day4.txt").ToList();

        public static string Answer()
        {
            return $"Day 4: {Primary()} AND {Secondary()}";
        }

        // horiz right
        // XMAS
        // horiz left
        // SAMX
        // vert down 
        // X
        // M
        // A
        // S
        // vert up
        // S
        // A
        // M
        // X
        // diag down right
        // X
        //  M
        //   A
        //    S
        // diag down left
        //    X
        //   M
        //  A
        // S
        // diag up right
        //    S
        //   A
        //  M
        // X
        // diag upleft
        // S
        //  A
        //   M
        //    X
        private static int Primary()
        {
            int total = 0;

            for (int row = 0; row < input.Count; row++)
            {
                for (int col = 0;  col < input[row].Length; col++)
                {
                    if (input[row][col] == 'X')
                    {
                        // right
                        if (col < input[row].Length - 3) // make sure we dont go out of bounds
                        {
                            if (Right(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // left
                        if (col > 2)
                        {
                            if (Left(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Down
                        if (row < input.Count - 3)
                        {
                            if (Down(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Up
                        if (row > 2)
                        {
                            if (Up(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Down and Right Diag
                        if (row < input.Count - 3 && col < input[row].Length - 3)
                        {
                            if (DownRight(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Down and Left Diag
                        if (row < input.Count - 3 && col > 2)
                        {
                            if (DownLeft(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Up and Right Diag
                        if (row > 2 && col < input[row].Length - 3)
                        {
                            if (UpRight(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                        // Up and Left Diag
                        if (row > 2 && col > 2)
                        {
                            if (UpLeft(input, row, col, "XMAS".Length) == "XMAS")
                            {
                                total++;
                            }
                        }
                    }
                }
            }

            return total;
        }
        private static int Secondary()
        {
            int total = 0;

            for (int row = 1; row < input.Count-1; row++)
            {
                for (int col = 1; col < input[row].Length-1; col++)
                {
                    if (input[row][col] == 'A')
                    {
                        if ((DownRight(input, row-1, col-1, "MAS".Length) == "MAS" || UpLeft(input, row+1, col+1, "MAS".Length) == "MAS") && 
                            (DownLeft(input, row-1, col+1, "MAS".Length) == "MAS" || UpRight(input, row+1, col-1, "MAS".Length) == "MAS")) 
                        {
                            total++;
                        }
                    }
                }
            }
            
            return total;
        }

        private static string Right(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row][col + i];
            }
            return ret;
        }
        private static string Left(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row][col - i];
            }
            return ret;
        }
        private static string Down(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row + i][col];
            }
            return ret;
        }
        private static string Up(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row - i][col];
            }
            return ret;
        }
        private static string DownRight(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row + i][col + i];
            }
            return ret;
        }
        private static string DownLeft(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row + i][col - i];
            }
            return ret;
        }
        private static string UpRight(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row - i][col + i];
            }
            return ret;
        }
        private static string UpLeft(List<string> input, int row, int col, int len)
        {
            string ret = "";
            for (int i = 0; i < len; i++)
            {
                ret += input[row - i][col - i];
            }
            return ret;
        }
    }
}
