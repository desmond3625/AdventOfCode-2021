using System;

namespace Day2
{
    class Submarine
    {
        public int HorizontalPosition { get; private set; }
        public int Depth { get; private set; }
        public int Aim { get; private set; }

        public void Forward(int x)
        {
            HorizontalPosition += x;
            Depth += Aim * x;
        }

        public void DownPart1(int x)
        {
             Depth += x;
        }
        
        public void DownPart2(int x)
        {
            Aim += x;
        }

        public void UpPart1(int x)
        {
            Depth -= Math.Min(x, Depth);
        }
        
        public void UpPart2(int x)
        {
            Aim -= x;
        }

        public void MovePart1(string cmd, int value)
        {
            switch (cmd)
            {
                case "forward":
                    Forward(value);
                    break;
                case "down":
                    DownPart1(value);
                    break;
                case "up":
                    UpPart1(value);
                    break;
            } 
        }
        
        public void MovePart2(string cmd, int value)
        {
            switch (cmd)
            {
                case "forward":
                    Forward(value);
                    break;
                case "down":
                    DownPart2(value);
                    break;
                case "up":
                    UpPart2(value);
                    break;
            } 
        }

        public int GetResult()
        {
            return HorizontalPosition * Depth;
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            string[] lines = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day02\\day2.txt");
            
            Submarine submarine1 = new Submarine();
            Submarine submarine2 = new Submarine();
            
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                String cmd = line.Substring(0, line.IndexOf(" "));
                int value = Int32.Parse(line.Substring(line.IndexOf(" ") + 1,
                    line.Length - line.IndexOf(" ") - 1));

               submarine1.MovePart1(cmd, value);
               submarine2.MovePart2(cmd, value);
            }
            
            Console.WriteLine(submarine1.GetResult());
            Console.WriteLine(submarine2.GetResult());
        }
    }
}