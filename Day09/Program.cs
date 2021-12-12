using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09
{
    class Solution
    {
        private int[,] _map;
        public int Width { get; set; }
        public int Height { get; set; }
        private List<HashSet<Tuple<int, int>>> _basinList;

        public Solution()
        {
            _basinList = new List<HashSet<Tuple<int, int>>>();
        }

        public int CountBasins()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    if (_map[i, j] != -1 && _map[i, j] != 9)
                    {
                        var basin = new HashSet<Tuple<int, int>>();
                        _basinList.Add(basin);
                        FindBasin(i, j, basin);
                    }
            }

            _basinList = _basinList.OrderByDescending(x => x.Count).ToList();

            return _basinList[0].Count * _basinList[1].Count * _basinList[2].Count;
        }

        public void FindBasin(int x, int y, HashSet<Tuple<int, int>> basin)
        {
            if (x >= Width || x < 0 || y >= Height || y < 0 || _map[x, y] == 9)
                return;

            if (basin.Contains(Tuple.Create(x, y)))
                return;

            basin.Add(Tuple.Create(x, y));
            _map[x, y] = -1;

            FindBasin(x + 1, y, basin);
            FindBasin(x, y + 1, basin);
            FindBasin(x - 1, y, basin);
            FindBasin(x, y - 1, basin);
        }

        public void ParseMap(string[] input)
        {
            Width = input.Length;
            Height = input[0].Length;
            _map = new int[Width, Height];
            
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    _map[i, j] = Int32.Parse(input[i][j].ToString());
            }
        }

        public bool IsLowPoint(int x, int y)
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>
                {Tuple.Create(0, 1), Tuple.Create(0, -1), Tuple.Create(1, 0), Tuple.Create(-1, 0)};

            int val = _map[x, y];

            foreach (var point in list)
            {
                int i = point.Item1;
                int j = point.Item2;

                if (x + i >= 0 && y + j >= 0 && x + i < Width && y + j < Height)
                    if (_map[x + i, y + j] <= val)
                        return false;
            }

            return true;
        }

        public int CountLowPoints()
        {
            int riskLevelSum = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    if (IsLowPoint(i, j))
                        riskLevelSum += 1 + _map[i, j];
            }

            return riskLevelSum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day09\\day9.txt");

            Solution s = new Solution();
            s.ParseMap(input);
            Console.WriteLine(s.CountLowPoints());
            Console.WriteLine(s.CountBasins());
        }
    }
}