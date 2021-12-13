using System;
using System.Collections.Generic;
using System.Linq;

namespace Day13
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Solution
    {
        private char[,] _paper;
        private List<Tuple<char, int>> _commands;
        public int Width { get; set; }
        public int Height { get; set; }

        public void ParseInput(string[] input)
        {
            List<Point> points = new List<Point>();
            int i = 0;
            string line;
            
            while (!String.IsNullOrEmpty(line = input[i++]))
            {
                string[] coords = line.Split(",");
                points.Add(new Point(Int32.Parse(coords[1]), Int32.Parse(coords[0])));
            }
            
            _commands = new List<Tuple<char, int>>();

            while (i != input.Length)
            {
                string[] param = input[i++].Substring(11).Split("=");
                _commands.Add(Tuple.Create(param[0][0], Int32.Parse(param[1])));
            }

            SetPaper(points);
        }

        private void SetPaper(List<Point> points)
        {
            Height = points.Select(x => x.X).Max() + 1;
            Width = points.Select(x => x.Y).Max() + 1;

            _paper = new char[Height,Width];
            
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    _paper[i, j] = '.';
            }

            foreach (var p in points)
                _paper[p.X, p.Y] = '#';
        }

        private void PrintPaper()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    Console.Write(_paper[i,j]);

                Console.WriteLine();
            }
        }

        private void Fold(char dir, int breakpoint)
        {
            if (dir == 'y')
            {
                for (int i = breakpoint; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (_paper[i, j] == '#')
                            _paper[i - 2 * (i - breakpoint), j] = '#';
                    }
                }
                Height /= 2;
            }
            
            if (dir == 'x')
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = breakpoint; j < Width; j++)
                    {
                        if (_paper[i, j] == '#')
                            _paper[i, j-2 * (j - breakpoint)] = '#';
                    }
                }
                Width /= 2;
            }
        }

        private int CountDots()
        {
            int dotsCount = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                    if (_paper[i, j] == '#')
                        dotsCount++;
            }

            return dotsCount;
        }

        public int Part1()
        {
            Fold(_commands[0].Item1, _commands[0].Item2);
            return CountDots();
        }
        
        public void Part2()
        {
            foreach (var c in _commands)
                Fold(c.Item1, c.Item2);
            
            PrintPaper();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day13\\day13.txt");
            
            Solution s1 = new Solution();
            s1.ParseInput(input);
            Console.WriteLine(s1.Part1());

            Solution s2 = new Solution();
            s2.ParseInput(input);
            s2.Part2();
        }
    }
}