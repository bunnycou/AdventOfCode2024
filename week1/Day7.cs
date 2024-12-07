using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.week1
{
    internal class Day7
    {
        private static List<string> input = File.ReadAllLines("inputs/day7.txt").ToList();

        public static string Answer()
        {
            return $"Day 7: {Primary()} AND {Secondary()}";
        }

        private static long Primary()
        {
            long total = 0;

            foreach (string s in input)
            {
                string[] strings = s.Split(": ");
                long target = Int64.Parse(strings[0]);
                List<int> nums = new();
                List<int> ops = new();
                foreach (string s2 in strings[1].Split(' '))
                {
                    nums.Add(Int32.Parse(s2));
                }
                for (int i = 0; i < nums.Count-1; i++)
                {
                    ops.Add(0);
                }

                bool targetHit = false;

                if (LongOperation(nums, ops) == target) // first test all add before incrememnting in while loop
                {
                    targetHit = true;
                }

                while (ops.Contains(0) && !targetHit)
                {
                    BinaryUp(ref ops);

                    if (LongOperation(nums, ops) == target)
                    {
                        targetHit = true;
                        break;
                    }
                }

                if (targetHit)
                {
                    total += target;
                }
            }

            return total;
        }

        private static long Secondary()
        {
            long total = 0;

            foreach (string s in input)
            {
                string[] strings = s.Split(": ");
                long target = Int64.Parse(strings[0]);
                List<int> nums = new();
                List<int> ops = new();
                foreach (string s2 in strings[1].Split(' '))
                {
                    nums.Add(Int32.Parse(s2));
                }
                for (int i = 0; i < nums.Count - 1; i++)
                {
                    ops.Add(0);
                }

                bool targetHit = false;

                if (LongOperation(nums, ops) == target) // first test all add before incrememnting in while loop
                {
                    targetHit = true;
                }

                while (ops.FindAll(l => l == 2).Count != ops.Count && !targetHit)
                {
                    TrinaryUp(ref ops);

                    if (LongOperation(nums, ops) == target)
                    {
                        targetHit = true;
                        break;
                    }
                }

                if (targetHit)
                {
                    total += target;
                }
            }

            return total;
        }

        private static long LongOperation(List<int> nums, List<int> ops)
        {
            if (nums.Count-1 != ops.Count)
            {
                return 0;
            }

            long total = nums[0];

            for (int i = 0; i < ops.Count; i++)
            {
                string oper = IntToOper(ops[i]);
                int num = nums[i + 1];
                if (oper == "+")
                {
                    total += num;
                } else if (oper == "*")
                {
                    total *= num;
                } else if (oper == "||")
                {
                    total = Int64.Parse($"{total}{num}");
                }
            }

            return total;
        }

        private static string IntToOper(int num)
        {
            if (num == 0)
            {
                return "+";
            }
            else if (num == 1)
            {
                return "*";
            } else if (num == 2)
            {
                return "||";
            }
            else { return "err"; }
        }

        public static void BinaryUp(ref List<int> list)
        {
            if (!list.Contains(0)) {
                return;
            }
            for (int i = list.Count-1; i >= 0; i--)
            {
                if (list[i] < 1)
                {
                    list[i] = 1;
                    break;
                } else
                {
                    list[i] = 0;
                }
            }
        }

        public static void TrinaryUp(ref List<int> list)
        {
            if (list.FindAll(l => l == 2).Count == list.Count)
            {
                return;
            }
            for (int i = list.Count-1; i >= 0; i--)
            {
                if (list[i] < 2)
                {
                    list[i]++;
                    break;
                } else
                {
                    list[i] = 0;
                }
            }
        }
    }
}
