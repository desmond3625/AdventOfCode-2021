using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    class Solution
    {
        private HashSet<Tuple<int, int, int>> _litPoints = new();

        public void Part1(string[] input)
        {
            foreach (var line in input)
            {
                bool isOn = line.Split(" ")[0].Equals("on");

                string[] param = line.Split(" ")[1]
                    .Replace("x=", "")
                    .Replace("y=", "")
                    .Replace("z=", "")
                    .Split(",");

                int[] x = param[0].Split("..").Select(n => Int32.Parse(n)).ToArray();
                int[] y = param[1].Split("..").Select(n => Int32.Parse(n)).ToArray();
                int[] z = param[2].Split("..").Select(n => Int32.Parse(n)).ToArray();

                Console.WriteLine(x[0] + "  " + x[1] + " " + y[0] + " " + y[1] + " " + z[0] + " " + z[1]);

                if (x[0] < -50 || x[1] > 50 || y[0] < -50 || y[1] > 50 || z[0] < -50 || z[1] > 50)
                    continue;

                MarkCube(isOn, x, y, z);
            }

            Console.WriteLine(_litPoints.Count);
        }

        public void MarkCube(bool isOn, int[] x, int[] y, int[] z)
        {
            for (int i = x[0]; i <= x[1]; i++)
            {
                for (int j = y[0]; j <= y[1]; j++)
                {
                    for (int k = z[0]; k <= z[1]; k++)
                    {
                        if (isOn)
                            _litPoints.Add(Tuple.Create(i, j, k));
                        else
                            _litPoints.Remove(Tuple.Create(i, j, k));
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day22\\day22.txt");

            Solution s = new Solution();
            s.Part1(input);
        }
    }
}