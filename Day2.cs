namespace AdventOfCode2024
{
    internal static class Day2
    {
        private static List<string> input = File.ReadAllLines("inputs/day2.txt").ToList();

        public static string Answer()
        {
            return $"Day 2: {Primary()} AND {Secondary()}";
        }

        public static int Primary() // number of safe reports
        {
            // reports need to be all increasing or all decreasing and change by 1-3, same is unstable

            int total = 0;

            foreach (string report in input)
            {
                if (ReportIsSafe(report))
                {
                    total++;
                }
            }

            return total;
        }

        private static int Secondary() // number of safe reports if we remove one report level
        {
            int total = 0;

            foreach (string reportRaw in input)
            {
                if (ReportIsSafe(reportRaw))
                {
                    total++;
                } else
                {
                    bool safe = false;
                    List<int> report = new();
                    foreach (string item in reportRaw.Split(" "))
                    {
                        report.Add(Int32.Parse(item));
                    }

                    for (int i = 0; i < report.Count; i++) // skip i element
                    {
                        List<int> reportSub = new();
                        for (int j = 0; j < report.Count; j++) // remake list without i element
                        {
                            if (j != i)
                            {
                                reportSub.Add(report[j]);
                            }
                        }
                        if (ReportIsSafe(reportSub))
                        {
                            safe = true;
                            break;
                        }
                    }

                    if (safe) total++;
                }
            }

            return total;
        }

        private static bool ReportIsSafe(string reportRaw)
        {
            List<int> report = new();
            foreach (string item in reportRaw.Split(" "))
            {
                report.Add(Int32.Parse(item));
            }

            return ReportIsSafe(report);
        }

        private static bool ReportIsSafe(List<int> report) // master function for determining if report is safe
        {
            // reports need to be all increasing or all decreasing and change by 1-3, same is unstable
            bool safe = true;
            if (!ReportRateConsistent(report)) { safe = false; } // check direction is consistent
            // check values vary by 1-3 and are not same
            for (int i = 1; i < report.Count; i++)
            {
                int diff = Math.Abs(report[i] - report[i - 1]);
                if (diff == 0) { safe = false; break; } // 0 change
                if (diff > 3) { safe = false; break; } // change greater than 3
                // else it is 1-3 and it is safe
            }

            return safe;
        }

        private static bool ReportRateConsistent(List<int> report) // check if report is only increasing or only decreasing
        {
            if (report[0] < report[1]) // increasing
            {
                List<int> reportSort = Day1.MergeSort(report); // reuse sorting from Day1
                return report.SequenceEqual(reportSort); // if original report is the same as new sort then it is consistent increasing
            } else if (report[0] > report[1]) // decreasing
            {
                List<int> reportSort = MergeSortReverse(report); // reuse sorting from Day1
                return report.SequenceEqual(reportSort); // if original report is the same as new sort then it is consistent decreasing
            } else // there are matching nums which indicates unsafe report
            {
                return false;
            }
        }

        public static List<int> MergeSortReverse(List<int> list) // reverse the list from Day1 MergeSort for descending
        {
            List<int> listSort = Day1.MergeSort(list);

            List<int> ret = new();
            for (int i = listSort.Count - 1; i >= 0; i--)
            {
                ret.Add(listSort[i]);
            }
            return ret;
        }
    }
}