using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    class Solution
    {
        private int i = 0;
        private string binary;
        private int versionSum;

        public void GetBinary(string input)
        {
            string binarystring = String.Join(String.Empty,
                input.Select(c => Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2)
                    .PadLeft(4, '0')));

            binary = binarystring;
        }

        public void DecodeValuePacket()
        {
            string literalVal = "";
            bool isLast = false;
            while (!isLast)
            {
                if (binary[i] == '0') isLast=true;
                literalVal += binary.Substring(i + 1, 4);
                i += 5;
            }

            long literalValDec = Convert.ToInt64(literalVal, 2);
            //Console.WriteLine("Literal val: " + literalValDec);
        }

        public void DecodeOperatorPacket()
        {
            char lengthTypeId = binary[i];
            i++;
            
            if (lengthTypeId == '0')
            {
                int lengthOfSubpackets = Convert.ToInt32(binary.Substring(i, 15), 2);
                i += 15;
                int curr = i;

                while (i < curr + lengthOfSubpackets)
                    Decode();
            }
            else
            {
                int numberOfSubpackets = Convert.ToInt32(binary.Substring(i, 11), 2);
                i += 11;
                for (int k = 0; k < numberOfSubpackets; k++)
                    Decode();
            }

        }

        public void Decode()
        {
            int version = Convert.ToInt32(binary.Substring(i, 3), 2);
            i += 3;
            versionSum += version;

            int id = Convert.ToInt32(binary.Substring(i, 3), 2);

            i += 3;
            
            if (id == 4)
                DecodeValuePacket();
            else
                DecodeOperatorPacket();
        }

        public int GetVersionSum()
        {
            return versionSum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File
                .ReadAllText("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day16\\day16.txt");

            Solution s = new Solution();
            s.GetBinary(input);
            s.Decode();
            Console.WriteLine(s.GetVersionSum());
        }
    }
}