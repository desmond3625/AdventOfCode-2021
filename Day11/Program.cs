using System;
using System.Collections.Generic;

namespace Day11
{
    class Octopus
    {
        public int Energy { get; set; }
        public Boolean HasFlashed { get; set; }

        public void FlashAndReset()
        {
            Energy = 0;
            HasFlashed = true;
        }
    }

    class Solution
    {
        private Octopus[,] _cavern;
        public int Width { get; set; }
        public int Height { get; set; }
        public int FlashCount { get; set; }

        public void ParseMap(string[] input)
        {
            Width = input.Length;
            Height = input[0].Length;
            _cavern = new Octopus[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    _cavern[i, j] = new Octopus {Energy = Int32.Parse(input[i][j].ToString())};
            }
        }

        public void Reset()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    _cavern[i, j].HasFlashed = false;
            }
        }

        public Boolean SimultaneousFlash()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    if (_cavern[i, j].Energy != 0)
                        return false;
            }

            return true;
        }
        public void Flash(int x, int y)
        {
            _cavern[x, y].FlashAndReset();
            FlashCount++;

            List<int> diff = new List<int> {1, 0, -1};
            var neighbours = new List<Tuple<int, int>>();

            foreach (var dx in diff)
            {
                foreach (var dy in diff)
                {
                    if (dx == 0 && dy == 0) continue;

                    int newX = x + dx;
                    int newY = y + dy;

                    if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
                    {
                        neighbours.Add(Tuple.Create(newX, newY));
                        if (!_cavern[newX, newY].HasFlashed)
                            _cavern[newX, newY].Energy++;
                    }
                }
            }

            foreach (var p in neighbours)
            {
                if (_cavern[p.Item1, p.Item2].Energy >= 10)
                    Flash(p.Item1, p.Item2);
            }
        }

        public void Step()
        {
            Reset();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    if (!_cavern[i, j].HasFlashed)
                        _cavern[i, j].Energy++;
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    if (_cavern[i, j].Energy > 9 && !_cavern[i, j].HasFlashed)
                        Flash(i, j);
            }
        }

        public int StepsToSimultaneousFlash()
        {
            int stepsCount = 0;
            while (true)
            {
                Step();
                stepsCount++;
                if (SimultaneousFlash())
                    return stepsCount;
            }
        }

        public int CountFlashes(int stepsNumber)
        {
            for (int i = 0; i < stepsNumber; i++)
                Step();

            return FlashCount;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day11\\day11.txt");

            Solution part1 = new Solution();
            part1.ParseMap(input);
            Console.WriteLine(part1.CountFlashes(100));

            Solution part2 = new Solution();
            part2.ParseMap(input);
            Console.WriteLine(part2.StepsToSimultaneousFlash());
        }
    }
}