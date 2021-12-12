using System;
using System.Linq;

namespace Day7
{
    class Solution
    {
        public int GetFuelPart1(int[] input)
        {
            int minFuel = Int32.MaxValue;

            for (int i = input.Min(); i < input.Max(); i++)
            {
                int currFuel = 0;
                for (int n = 0; n < input.Length; n++)
                    currFuel += Math.Abs(input[n] - i);

                if (currFuel < minFuel)
                    minFuel = currFuel;
            }

            return minFuel;
        }

        public int GetFuelPart2(int[] input)
        {
            int minFuel = Int32.MaxValue;

            for (int i = input.Min(); i < input.Max(); i++)
            {
                int currFuel = 0;
                for (int n = 0; n < input.Length; n++)
                    currFuel += (1 + Math.Abs(input[n] - i)) * Math.Abs(input[n] - i) / 2;

                if (currFuel < minFuel)
                    minFuel = currFuel;
            }

            return minFuel;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] input = System.IO.File
                .ReadAllText("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day07\\day7.txt")
                .Split(",")
                .Select(x => Int32.Parse(x)).ToArray();

            Console.WriteLine(new Solution().GetFuelPart1(input));
            Console.WriteLine(new Solution().GetFuelPart2(input));
        }
    }
}