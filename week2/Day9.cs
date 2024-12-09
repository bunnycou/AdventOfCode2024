using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week2
{
    internal class Day9
    {
        private static string input = File.ReadAllText("inputs/day9.txt"); // one line input for today

        public static string Answer()
        {
            return $"Day 9: {Primary()} AND {Secondary()}";
        }

        public static long Primary()
        {
            long total = 0;

            List<string> filesys = new();

            for (int i = 0; i < input.Length; i++) // create filesystem with free blocks
            {
                int blockSize = Int32.Parse(input[i].ToString());
                if (i % 2 == 0) // even is blocks, odd is free space
                {
                    for (int j = 0; j < blockSize; j++)
                    {
                        filesys.Add($"{i / 2}");
                    }
                } else
                {
                    for (int j = 0; j < blockSize; j++)
                    {
                        filesys.Add(".");
                    }
                }
            }

            int endInd = filesys.Count - 1;
            for (int i = 0; i <= endInd; i++) // compress filesystem, moving blocks from end to free blocks
            {
                if (filesys[endInd] == ".") // update endInd to be a block to move
                {
                    for (int j = endInd - 1; j >= i; j--)
                    {
                        if (filesys[j] != ".")
                        {
                            endInd = j; break;
                        }
                    }
                }
                // when i is a free block, move, otherwise just move on to next i
                if (filesys[i] == ".")
                {
                    filesys[i] = filesys[endInd];
                    filesys[endInd] = ".";
                }
            }

            for (int i = 0; i < filesys.Count; i++)
            {
                if (filesys[i] == ".") { break; }
                total += i * Int32.Parse(filesys[i]);
            }

            return total;
        }

        public static long Secondary()
        {
            long total = 0;

            List<string> filesys = new();
            List<int> usedBlocks = new();
            List<int> freeBlocks = new();
            List<int> offset = new();

            for (int i = 0; i < input.Length; i++) // create filesystem with free blocks
            {
                offset.Add(0);
                int blockSize = Int32.Parse(input[i].ToString());
                if (i % 2 == 0) // even is blocks, odd is free space
                {
                    usedBlocks.Add(blockSize);
                    for (int j = 0; j < blockSize; j++)
                    {
                        filesys.Add($"{i / 2}");
                    }
                }
                else
                {
                    freeBlocks.Add(blockSize);
                    for (int j = 0; j < blockSize; j++)
                    {
                        filesys.Add(".");
                    }
                }
            }

            for (int i = usedBlocks.Count - 1; i > 0; i--) // compress filesystem
            {
                for (int j = 0; j < i; j++)
                {
                    if (usedBlocks[i] <= freeBlocks[j])
                    {
                        freeBlocks[j] -= usedBlocks[i];
                        int usedInd = filesys.IndexOf($"{i}");
                        int freeInd = filesys.IndexOf($"{j}") + usedBlocks[j] + offset[j];
                        offset[j] += usedBlocks[i];
                        for (int k = 0; k < usedBlocks[i]; k++)
                        {
                            filesys[usedInd+k] = ".";
                            filesys[freeInd+k] = $"{i}";
                        }
                        break;
                    }
                }
            }

            for (int i = 0; i < filesys.Count; i++)
            {
                if (filesys[i] != ".")
                {
                    total += i * Int32.Parse(filesys[i]);
                }
            }

            return total;
        }
    }
}
