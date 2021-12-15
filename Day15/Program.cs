using System;

namespace Day15
{
    class Solution
    {
        private int[,] _map;
        public int Rows { get; set; }
        public int Cols { get; set; }


        public void ParseMap(string[] input)
        {
            Rows = input.Length;
            Cols = input[0].Length;
            _map = new int[Rows, Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    _map[i, j] = Int32.Parse(input[i][j].ToString());
            }
        }

        public int GetLowestRisk(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            int[,] riskSum = new int[rows, cols];
            
            riskSum[0, 0] = 0;
            
            for (int i = 1; i < rows; i++)
                riskSum[i, 0] = riskSum[i - 1, 0] + map[i, 0];

            for (int j = 1; j < cols; j++)
                riskSum[0, j] = riskSum[0, j - 1] + map[0, j];

            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                    riskSum[i, j] = map[i, j] + Math.Min(riskSum[i - 1, j], riskSum[i, j - 1]);
            }

            return riskSum[rows - 1, cols - 1];
        }

        public void Print(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(arr[i, j] + " ");

                Console.WriteLine();
            }
        }

        public int Part1()
        {
            return GetLowestRisk(_map);
        }

        public int Part2()
        {
            int[,] fullMap = new int[5 * Rows, 5 * Cols];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    fullMap[i, j] = _map[i, j];
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    for (int k = 1; k < 5; k++)
                        fullMap[i + k * Rows, j] =
                            fullMap[i + (k - 1) * Rows, j] < 9 ? fullMap[i + (k - 1) * Rows, j] + 1 : 1;
                }
            }

            for (int i = 0; i < 5 * Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    for (int k = 1; k < 5; k++)
                        fullMap[i, j + k * Cols] = 
                            fullMap[i, j + (k - 1) * Cols] < 9 ? fullMap[i, j + (k - 1) * Cols] + 1 : 1;
                }
            }

            return GetLowestRisk(fullMap);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day15\\day15.txt");

            Solution s = new Solution();
            s.ParseMap(input);
            Console.WriteLine(s.Part1());
            Console.WriteLine(s.Part2());
        }
    }
}