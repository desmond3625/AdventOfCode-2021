using System;
using System.Collections.Generic;
using System.Drawing;

namespace Day17
{
    class Probe
    {
        public Point Position = new(0, 0);
        public int VelX { get; set; }
        public int VelY { get; set; }

        public Probe(int velocityX, int velocityY)
        {
            VelX = velocityX;
            VelY = velocityY;
        }

        public void Move()
        {
            Position.X += VelX;
            Position.Y += VelY;
            if (VelX != 0)
                VelX = VelX > 0 ? --VelX : ++VelX;
            VelY--;
        }
    }

    class Solution
    {
        private Point _targetStart;
        private Point _targetEnd;

        public void ParseInput(string input)
        {
            string[] coords = input.Replace("target area: ", "").Replace("x=", "")
                .Replace("y=", "").Split(", ");

            string[] x = coords[0].Split("..");
            string[] y = coords[1].Split("..");

            _targetStart = new Point(Int32.Parse(x[0]), Int32.Parse(y[0]));
            _targetEnd = new Point(Int32.Parse(x[1]), Int32.Parse(y[1]));
        }

        public Boolean HitTarget(Point p)
        {
            return (_targetStart.X <= p.X && p.X <= _targetEnd.X) && (_targetEnd.Y >= p.Y && p.Y >= _targetStart.Y);
        }

        public Boolean PassedTarget(Point p)
        {
            return _targetEnd.X < p.X || _targetStart.Y > p.Y;
        }

        public int MaxHeight(int y)
        {
            return y * (y + 1) / 2;
        }

        public void Launch()
        {
            HashSet<Tuple<int, int>> velocities = new HashSet<Tuple<int, int>>();
            int maxY = 0;
            
            for (int vx = 0; vx <= _targetEnd.X; vx++)
            {
                for (int vy = -1000; vy <= 1000; vy++)
                {
                    Probe p = new Probe(vx, vy);
                    while (true)
                    {
                        p.Move();
                        if (HitTarget(p.Position))
                        {
                            velocities.Add(Tuple.Create(vx, vy));
                            if (MaxHeight(vy) > maxY)
                                maxY = MaxHeight(vy);

                            break;
                        }
                        if (PassedTarget(p.Position)) break;
                    }
                }
            }
            Console.WriteLine("Max height: "+maxY);
            Console.WriteLine("Distinct velocities: "+velocities.Count);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day17\\day17.txt");

            Solution s = new Solution();
            s.ParseInput(input);
            s.Launch();
        }
    }
}