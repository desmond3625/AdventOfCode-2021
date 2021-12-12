using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        public static Dictionary<char,char> brackets = new() {{')', '('}, {']', '['}, {'}', '{'}, {'>', '<'}};
        public static Dictionary<char, char> bracketsReverse =new() {{'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}};
        public static long Part2(string[] data)
        {
            List<long> scores = new List<long>();
            var bracketVal = new Dictionary<char, int> {{')', 1}, {']', 2}, {'}', 3}, {'>', 4}};
            
            foreach (var line in data)
            {
                var stack = new Stack<char>();
                char[] seq = line.ToCharArray();
                
                Boolean isCorrupted = false;
                long score = 0;

                for (int i = 0; i < seq.Length; i++)
                {
                    if (seq[i] is '(' or '[' or '{' or '<')
                        stack.Push(seq[i]);

                    else if (seq[i] is ')' or ']' or '}' or '>')
                    {
                        brackets.TryGetValue(seq[i], out char x);
                        if (stack.Pop() != x)
                        {
                            isCorrupted = true;
                            break;
                        }
                    }
                }

                if (!isCorrupted)
                {
                    while (stack.Count != 0)
                    {
                        bracketsReverse.TryGetValue(stack.Pop(), out char c);
                        bracketVal.TryGetValue(c, out int add);
                        score = score * 5 + add;
                    }
                    scores.Add(score);
                }
            }
            return scores.OrderBy(x => x).ToList()[scores.Count / 2];
        }

        public static int Part1(string[] data)
        {
            int sum = 0;
            var bracketVal = new Dictionary<char, int> {{')', 3}, {']', 57}, {'}', 1197}, {'>', 25137}};

            foreach (var line in data)
            {
                Stack<char> stack = new Stack<char>();
                char[] seq = line.ToCharArray();

                for (int i = 0; i < seq.Length; i++)
                {
                    if (seq[i] is '(' or '[' or '{' or '<')
                        stack.Push(seq[i]);

                    else if (seq[i] is ')' or ']' or '}' or '>')
                    {
                        brackets.TryGetValue(seq[i], out char x);

                        if (stack.Pop() != x)
                        {
                            bracketVal.TryGetValue(seq[i], out int add);
                            sum += add;
                        }
                    }
                }
            }

            return sum;
        }

        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day10\\day10.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }
    }
}