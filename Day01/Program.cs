using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01
{
    class Program
    {
        public static int CalculatePart1(List<int> depths)
        {
            int increaseCount = 0;

            for (int i = 1; i < depths.Count; i++)
            {
                if (depths[i] > depths[i - 1])
                    increaseCount++;
            }
            return increaseCount;
        }
        
        public static int CalculatePart2(List<int> depths)
        {
            int increaseCount = 0;

            for (int i = 3; i < depths.Count; i++)
            {
                if (depths[i] > depths[i - 3])
                    increaseCount++;
            }
            return increaseCount;
        }
        
        static void Main(string[] args)
        {
            List<int> depths = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day01\\day1.txt")
                .Select(x => Int32.Parse(x)).ToList();

            Console.WriteLine(CalculatePart1(depths));
            Console.WriteLine(CalculatePart2(depths));

        }
    }
}