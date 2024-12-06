using AdventOfCode2024.week1;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine(Day1.Answer());
            Console.WriteLine(Day2.Answer());
            Console.WriteLine(Day3.Answer());
            Console.WriteLine(Day4.Answer());
            Console.WriteLine(Day5.Answer());
            Console.WriteLine(Day6.Answer());
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
