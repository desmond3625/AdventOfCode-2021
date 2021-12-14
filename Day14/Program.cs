using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    class Solution
    {
        private char[] _polymer;
        private Dictionary<string, char> _insertRules = new();
        private Dictionary<char, long> _elemCount = new();
        private Dictionary<string, long> _pairCount = new();

        public void ParseInput(string[] input)
        {
            _polymer = input[0].ToCharArray();

            foreach (char c in _polymer)
                _elemCount.TryAdd(c, 0);

            for (int i = 2; i < input.Length; i++)
            {
                string[] param = input[i].Split(" -> ");
                char c = Char.Parse(param[1]);

                _insertRules.TryAdd(param[0], c);
                _pairCount.TryAdd(param[0], 0);
                _elemCount.TryAdd(c, 0);
            }
        }

        public long Result(int numberOfSteps)
        {
            foreach (var l in _elemCount)
                _elemCount[l.Key] = 0;

            for (int i = 0; i < _polymer.Length - 1; i++)
            {
                string pair = _polymer[i] + _polymer[i + 1].ToString();
                _pairCount[pair]++;
            }

            for (int i = 0; i < numberOfSteps; i++)
                Step();

            return Count();
        }

        public void Step()
        {
            var newDict = _pairCount.ToDictionary(x => x.Key,
                x => x.Value);

            foreach (var key in _pairCount.Keys)
            {
                long reps = _pairCount[key];
                if (reps > 0)
                {
                    char c = _insertRules[key];
                    string leftPair = key[0].ToString() + c;
                    string rightPair = c + key[1].ToString();

                    newDict[leftPair] += reps;
                    newDict[rightPair] += reps;
                    newDict[key] -= reps;
                }
            }

            _pairCount = newDict;
        }

        public long Count()
        {
            foreach (var pair in _pairCount)
            {
                _elemCount[pair.Key[0]] += pair.Value;
                _elemCount[pair.Key[1]] += pair.Value;
            }

            _elemCount[_polymer[0]]++;
            _elemCount[_polymer[^1]]++;

            foreach (var l in _elemCount)
                _elemCount[l.Key] /= 2;

            return _elemCount.Values.Max() - _elemCount.Values.Min();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day14\\day14.txt");

            Solution s = new Solution();
            s.ParseInput(input);
            Console.WriteLine(s.Result(10));
            Console.WriteLine(s.Result(40));
        }
    }
}