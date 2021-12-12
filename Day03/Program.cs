using System;
using System.Collections.Generic;
using System.Linq;

namespace Day3
{
    class RatingCalculator
    {
        public int GetOxygenRating(List<string> data)
        {
            List<string> filtered = data;
            char mostCommon;

            for (int i = 0; i < data[0].Length; i++)
            {
                int totalReadings = filtered.Count();
                if (totalReadings == 1) break;
                int onesCount = filtered.Count(x => x[i] == '1');

                if (onesCount >= Math.Ceiling(totalReadings / 2.0))
                    mostCommon = '1';
                else
                    mostCommon = '0';
                
                filtered = filtered.Where(x => x[i] == mostCommon).Select(x => x).ToList();
            }

            int oxygenRating = Convert.ToInt32(filtered[0], 2);
            return oxygenRating;
        }

        public int GetScrubberRating(List<string> data)
        {
            List<string> filtered = data;

            char leastCommon;
            for (int i = 0; i < data[0].Length; i++)
            {
                int totalReadings = filtered.Count;
                if (totalReadings == 1) break;

                int onesCount = filtered.Count(x => x[i] == '1');

                if (onesCount < Math.Ceiling(totalReadings / 2.0))
                    leastCommon = '1';
                else
                    leastCommon = '0';
                filtered = filtered.Where(x => x[i] == leastCommon).Select(x => x).ToList();
            }

            int scrubberRating = Convert.ToInt32(filtered[0], 2);
            return scrubberRating;
        }

        public int GetPowerConsumption(List<string> data)
        {
            int readingsCount = data.Count;
            string gammaRateBinary = "";
            for (int i = 0; i < data[0].Length; i++)
            {
                if (data.Count(x => x[i] == '1') > readingsCount / 2)
                    gammaRateBinary += "1";
                else
                    gammaRateBinary += "0";
            }

            string epsilonRateBinary = gammaRateBinary.Replace('0', '*')
                .Replace('1', '0')
                .Replace('*', '1');

            int gammaRate = Convert.ToInt32(gammaRateBinary, 2);
            int epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

            return gammaRate * epsilonRate;
        }

        public int GetLifeSupportRating(List<string> data)
        {
            return GetOxygenRating(data) * GetScrubberRating(data);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<string> data = System.IO.File
                .ReadAllLines("C:\\UJ\\AdventOfCode\\AdventOfCode\\Day3\\day3.txt")
                .ToList();

            Console.WriteLine(new RatingCalculator().GetPowerConsumption(data));
            Console.WriteLine(new RatingCalculator().GetLifeSupportRating(data));
        }
    }
}