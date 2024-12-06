using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week1
{
    internal static class Day1
    {
        private static List<string> input = File.ReadAllLines("inputs/day1.txt").ToList();

        public static string Answer()
        {
            return $"Day 1: {Primary()} AND {Secondary()}";
        }

        private static int Primary()
        {
            List<int> left = new();
            List<int> right = new();

            foreach (string line in input)
            {
                string[] nums = line.Split("   ");
                left.Add(int.Parse(nums[0]));
                right.Add(int.Parse(nums[1]));
            }

            left = MergeSort(left);
            right = MergeSort(right);

            int ret = 0;

            for (int i = 0; i < left.Count; i++)
            {
                ret += Math.Abs(left[i] - right[i]);
            }

            return ret;
        }
        private static int Secondary()
        {
            List<int> left = new();
            List<int> right = new();

            foreach (string line in input)
            {
                string[] nums = line.Split("   ");
                left.Add(int.Parse(nums[0]));
                right.Add(int.Parse(nums[1]));
            }

            Dictionary<int, int> rightDict = BucketSortish(right);

            int ret = 0;
            foreach (int i in left)
            {
                if (rightDict.TryGetValue(i, out int value)) { ret += i * value; }
            }
            return ret;
        }
        public static List<int> MergeSort(List<int> list)
        {
            // return if single element
            if (list.Count == 1) { return list; }

            // split list in two
            int mid = list.Count / 2; // is floor
            List<int> left = new();
            List<int> right = new();
            for (int i = 0; i < list.Count; i++)
            {
                if (i < mid) { left.Add(list[i]); } else { right.Add(list[i]); }
            }

            // recursive
            left = MergeSort(left); right = MergeSort(right);

            // merge
            List<int> ret = new();
            int j = 0; int k = 0;
            while (ret.Count < list.Count)
            {
                if (j >= left.Count)
                {
                    ret.Add(right[k++]);
                }
                else if (k >= right.Count)
                {
                    ret.Add(left[j++]);
                }
                else
                {
                    if (left[j] < right[k]) { ret.Add(left[j++]); }
                    else { ret.Add(right[k++]); }
                }
            }
            return ret;

        }

        private static Dictionary<int, int> BucketSortish(List<int> list) // int: number of times it appears
        {
            Dictionary<int, int> ret = new();
            foreach (int i in list)
            {
                if (ret.ContainsKey(i)) { ret[i]++; } else { ret[i] = 1; }
            }
            return ret;
        }
    }
}
