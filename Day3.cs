using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal static class Day3 // what the hell...
    {
        private static List<string> input = File.ReadAllLines("inputs/day3.txt").ToList();

        public static string Answer()
        {
            return $"Day 3: {Primary()} AND {Secondary()}";
        }

        private static int Primary() // regex practice
        {
            int total = 0;

            string pattern = "mul\\((\\d{1,3}),(\\d{1,3})\\)";

            foreach (string s in input)
            {
                MatchCollection matches = Regex.Matches(s, pattern);

                foreach (Match match in matches)
                {
                    int num1 = Int32.Parse(match.Groups[1].Value);
                    int num2 = Int32.Parse(match.Groups[2].Value);

                    total += (num1 * num2);
                }
            }

            return total;
        }

        private static int Secondary() // parsing practice
        {
            char[] nums = "1234567890".ToCharArray();

            int total = 0;

            bool mulEnable = true;

            foreach(string s in input)
            {
                for (int c = 0; c < s.Length; c++)
                {
                    if (SubstringExists(s, c, "do()"))
                    {
                        mulEnable = true;
                    } 
                    else if (SubstringExists(s, c, "don't()"))
                    {
                        mulEnable = false;
                    } 
                    else if (mulEnable)
                    {
                        if (SubstringExists(s, c, "mul("))
                        {
                            List<List<char>> numbers =
                            [
                                new List<char>(),// initialize number 0
                                new List<char>(),// initalize number 1
                            ];
                            int num = 0;
                            bool isValid = true;
                            bool finished = false;
                            while (isValid)
                            {
                                char current = s[c + "mul(".Length];

                                if (nums.Contains(current)) // if it is a number
                                {
                                    numbers[num].Add(current); c++;
                                }
                                else if (current == ',') // if we have comma seperator
                                { 
                                    num++; c++; 
                                    if (num > 1) // if this was an extra comma seperator
                                    { isValid = false; } 
                                } 
                                else if (current == ')') // if we have parentheses terminator
                                { 
                                    finished = true; isValid = false; 
                                }
                                else { isValid = false;} // not number or part of mul(x,y) statement
                            }
                            if (finished) // found two numbers and complete mul statement
                            {
                                int num1 = CharNum(numbers[0]);
                                int num2 = CharNum(numbers[1]);

                                total += num1 * num2;
                            }
                        }
                    }
                }
            }

            return total;
        }

        public static string Substring(string s, int start, int length)
        {
            if (start+length > s.Length)
            {
                length = s.Length - start;
            }

            string ret = "";
            for (int i = start; i < start+length; i++)
            {
                ret += s[i];
            }
            return ret;
        }

        private static bool SubstringExists(string s, int index, string match)
        {
            return Substring(s, index, match.Length) == match;
        }

        private static int CharNum(List<char> chars)
        {
            List<int> ints = new();
            foreach (char c in chars)
            {
                ints.Add(Int32.Parse(c.ToString()));
            }

            int ret = 0;

            int j = 0;
            for (int i = ints.Count-1; i >= 0; i--)
            {
                ret += ints[i] * (int)Math.Pow(10, j++);
            }

            return ret;
        }
    }
}
