using AdventOfCode2024.week1;
using AdventOfCode2024.week2;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main()
        {
            // week1 //
            //Console.WriteLine(Day1.Answer());
            //Console.WriteLine(Day2.Answer());
            //Console.WriteLine(Day3.Answer());
            //Console.WriteLine(Day4.Answer());
            //Console.WriteLine(Day5.Answer());
            //Console.WriteLine(Day6.Answer()); // takes ~5-10 seconds
            //Console.WriteLine(Day7.Answer());
            // week 2 //
            Console.WriteLine(Day8.Answer());
            Console.WriteLine(Day9.Answer()); // takes ~3-5 seconds
            // week 3 //
            // week 4 //
        }

        public static void ConsoleWriteList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    Console.Write(list[i] + "\n");
                } else
                {
                    Console.Write(list[i] + ", ");
                }
            }
        }
    }
}
