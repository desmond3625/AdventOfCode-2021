using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    class Solution
    {
        private int i = 0;
        private string _message;
        private int _versionSum;

        public void ConvertToBinary(string input)
        {
            string binarystring = String.Join(String.Empty, 
                input.Select(c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2)
                    .PadLeft(4, '0')));

            _message = binarystring;
        }

        public long DecodeValuePacket()
        {
            string literalVal = "";
            bool isLast = false;
            while (!isLast)
            {
                if (_message[i] == '0') isLast = true;
                literalVal += _message.Substring(i + 1, 4);
                i += 5;
            }
            return Convert.ToInt64(literalVal, 2);
        }

        public long DecodeOperatorPacket(int id)
        {
            List<long> operands = new List<long>();
            char lengthTypeId = _message[i++];
            
            if (lengthTypeId == '0')
            {
                int lengthOfSubpackets = Convert.ToInt32(_message.Substring(i, 15), 2);
                i += 15;
                int curr = i;

                while (i < curr + lengthOfSubpackets)
                    operands.Add(Decode());
            }
            else
            {
                int numberOfSubpackets = Convert.ToInt32(_message.Substring(i, 11), 2);
                i += 11;
                for (int k = 0; k < numberOfSubpackets; k++)
                    operands.Add(Decode());
            }

            if (id == 0) return operands.Sum();
            if (id == 1) return operands.Aggregate(1L, (x, y) => x * y);
            if (id == 2) return operands.Min();
            if (id == 3) return operands.Max();
            if (id == 5) return operands[0] > operands[1] ? 1 : 0;
            if (id == 6) return operands[0] < operands[1] ? 1 : 0;

            return operands[0] == operands[1] ? 1 : 0;
        }

        public long Decode()
        {
            int version = Convert.ToInt32(_message.Substring(i, 3), 2);
            i += 3;
            _versionSum += version;

            int id = Convert.ToInt32(_message.Substring(i, 3), 2);
            i += 3;

            if (id == 4)
                return DecodeValuePacket();

            return DecodeOperatorPacket(id);
        }

        public long Part1()
        {
            Decode();
            return _versionSum;
        }

        public long Part2()
        {
            i = 0;
            return Decode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File
                .ReadAllText("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day16\\day16.txt");
            
            Solution s = new Solution();
            s.ConvertToBinary(input);
            Console.WriteLine(s.Part1());
            Console.WriteLine(s.Part2());
        }
    }
}