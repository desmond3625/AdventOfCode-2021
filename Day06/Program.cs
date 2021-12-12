using System;
using System.Linq;

namespace Day6
{
    class Solution
    {
        public ulong AddDays(int daysNumber, int[] fishAges)
        {
            ulong[] countByAge = new ulong[9];

            for (int i = 0; i < fishAges.Length; i++)
                countByAge[fishAges[i]]++;

            for (int d = 0; d < daysNumber; d++)
            {
                ulong deadFish = countByAge[0];

                for (int i = 0; i < countByAge.Length - 1; i++)
                    countByAge[i] = countByAge[i + 1];

                countByAge[8] = deadFish;
                countByAge[6] += deadFish;
            }

            ulong fishCount = 0;
            for (int i = 0; i <= 8; i++)
                fishCount += countByAge[i];

            return fishCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] fishAges = System.IO.File
                .ReadAllText("C:\\UJ\\AdventOfCode\\AdventOfCode\\Day6\\day6.txt")
                .Split(",")
                .Select(x=>Int32.Parse(x))
                .ToArray();

            Console.WriteLine(new Solution().AddDays(256,fishAges));
        }
    }
}