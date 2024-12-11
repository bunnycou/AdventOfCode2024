using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week2
{
    internal class Day11
    {
        private static string input = File.ReadAllText("inputs/day11.txt");
        private static Dictionary<long, long> stones = new();

        public static string Answer()
        {
            foreach (string s in input.Split(" "))
            {
                stones.Add(long.Parse(s), 1);
            }

            return $"Day 11: {Primary()} AND {Secondary()}";
        }

        private static long Primary()
        {
            long total = 0;

            for (int i = 0; i < 25; i++)
            {
                BlinkStones();
            }

            foreach (long value in stones.Values)
            {
                total += value;
            }

            return total;
        }

        private static long Secondary()
        {
            long total = 0;

            for (int i = 0; i < 50; i++) // blink another 50 for 75 total
            {
                BlinkStones();
            }

            foreach (long value in stones.Values)
            {
                total += value;
            }

            return total;
        }

        private static void BlinkStones()
        {
            List<long> stoneKeys = stones.Keys.ToList();
            Dictionary<long, long> newStones = new();
            foreach (long key in stoneKeys)
            {
                long numberOfStones = stones[key];
                if (key == 0)
                {
                    CreateOrAdd(ref newStones, 1, numberOfStones);
                    //CreateOrAdd(ref stones, 1, stones[0]);
                    //stones[0] = 0;
                }
                else if (key.ToString().Length % 2 == 0) // even digits
                {
                    long[] newNums = TwoStones(key);

                    CreateOrAdd(ref newStones, newNums[0], numberOfStones);
                    CreateOrAdd(ref newStones, newNums[1], numberOfStones);
                    //CreateOrAdd(ref stones, newNums[0], stones[key]);
                    //CreateOrAdd(ref stones, newNums[1], stones[key]);
                    //stones[key] = 0;
                }
                else
                {
                    newStones.Add(key * 2024, numberOfStones);
                    //CreateOrAdd(ref stones, key * 2024, stones[key]);
                    //stones[key] = 0;
                }
            }
            stones = newStones;
        }

        private static long[] TwoStones(long num)
        {
            string nums = num.ToString();
            long firstHalf = long.Parse(nums.Substring(0, nums.Length / 2));
            long secondHalf = long.Parse(nums.Substring(nums.Length / 2));
            return [firstHalf, secondHalf];
        }

        private static void CreateOrAdd(ref Dictionary<long, long> dict, long key, long val)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] += val;
            } else
            {
                dict.Add(key, val);
            }
        }
    }
}
