using System;
using System.Linq;

namespace Day5
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

    class Path
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Path(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }

    class OceanFloor
    {
        private int[,] _grid;
        public int Width { get; set; }
        public int Height { get; set; }

        public OceanFloor(int width, int height)
        {
            Width = width; Height = height;
            _grid = new int[width, height];
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    _grid[i, j] = 0;
            }
        }

        public void MarkPoint(Point p)
        {
            _grid[p.Y, p.X]++;
        }

        public int GetOverlappingPoints()
        {
            int overlapsCount = 0;
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if (_grid[i, j] >= 2)
                        overlapsCount++;
                }
            }

            return overlapsCount;
        }

        public void AddPath(Path p)
        {
            Point start = p.StartPoint;
            Point end = p.EndPoint;
            int diffX = start.X - end.X;
            int diffY = start.Y - end.Y;

            if (diffX != 0 && diffY != 0 && Math.Abs(diffX) != Math.Abs(diffY))
                return;

            Point pi = start;

            MarkPoint(pi);

            while (pi.Y != end.Y || pi.X != end.X)
            {
                int shiftX = (start.X > end.X) ? -1 : (start.X == end.X) ? 0 : 1;
                int shiftY = (start.Y > end.Y) ? -1 : (start.Y == end.Y) ? 0 : 1;
                pi = new Point(pi.X + shiftX, pi.Y + shiftY);
                MarkPoint(pi);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\UJ\\AdventOfCode\\AdventOfCode\\Day5\\day5.txt");

            Path[] paths = new Path[lines.Length];

            int maxX = 0, maxY = 0;
            
            for (int i = 0; i < lines.Length; i++)
            {
                int[] coords = lines[i].Replace(" -> ", ",").Split(",")
                    .Select(x => Int32.Parse(x)).ToArray();

                maxX = Math.Max(maxX, Math.Max(coords[0], coords[2]));
                maxY = Math.Max(maxY, Math.Max(coords[1], coords[3]));
                
                paths[i] = new Path(new Point(coords[0], coords[1]), new Point(coords[2], coords[3]));
            }

            var oceanFloor = new OceanFloor(maxY + 1, maxX + 1);

            foreach (var path in paths)
                oceanFloor.AddPath(path);

            Console.WriteLine(oceanFloor.GetOverlappingPoints());
        }
    }
}