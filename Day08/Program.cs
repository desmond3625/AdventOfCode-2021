using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    class Solution
    {
        public int CountUniqueDigits(string[] outputData)
        {
            int result = outputData
                .Select(x => x.Split(" ").Count(y => y.Length is 2 or 3 or 4 or 7))
                .Sum();

            return result;
        }

        public int GetOutputsSum(string[] inputData, string[] outputData)
        {
            int sum = 0;
            for (int n = 0; n < inputData.Length; n++)
            {
                string[] codes = inputData[n].Split(" ").OrderBy(x => x.Length).ToArray()
                    .Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();

                Dictionary<int, string> dict = GetMapping(codes);

                sum += Decode(dict, outputData[n]);
            }

            return sum;
        }

        public Dictionary<int, string> GetMapping(string[] codes)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            
            dict.Add(1, codes[0]);
            dict.Add(7, codes[1]);
            dict.Add(4, codes[2]);
            dict.Add(8, codes[9]);
            
            for (int i = 6; i <= 8; i++)
            {
                if (IsSubcode(codes[i], codes[2]))
                    dict.Add(9, codes[i]);
                else if (!IsSubcode(codes[i], codes[2]) && IsSubcode(codes[i], codes[0]))
                    dict.Add(0, codes[i]);
                else
                    dict.Add(6, codes[i]);
            }

            dict.TryGetValue(6, out string code6);

            for (int i = 3; i <= 5; i++)
            {
                if (IsSubcode(code6, codes[i]))
                    dict.Add(5, codes[i]);
                else if (IsSubcode(codes[i], codes[0]))
                    dict.Add(3, codes[i]);
                else
                    dict.Add(2, codes[i]);
            }

            return dict;
        }

        public int Decode(Dictionary<int, string> dict, string output)
        {
            string[] outputCodes = output.Split(" ").ToArray()
                .Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();

            String result = "";

            for (int i = 0; i < outputCodes.Length; i++)
            {
                foreach (var entry in dict)
                {
                    if (entry.Value == outputCodes[i])
                        result += entry.Key;
                }
            }

            return Int32.Parse(result);
        }

        public Boolean IsSubcode(string code, string subCode)
        {
            char[] subCodeChars = subCode.ToCharArray();
            for (int i = 0; i < subCodeChars.Length; i++)
                if (!code.Contains(subCodeChars[i]))
                    return false;

            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] allData = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day08\\day8.txt");

            string[] inputData = allData.Select(x => x.Split(" | ")[0]).ToArray();
            string[] outputData = allData.Select(x => x.Split(" | ")[1]).ToArray();

            Console.WriteLine(new Solution().CountUniqueDigits(outputData));
            Console.WriteLine(new Solution().GetOutputsSum(inputData, outputData));
        }
    }
}