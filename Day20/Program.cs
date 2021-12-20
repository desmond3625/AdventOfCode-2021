using System;
using System.Text;

namespace Day20
{
    class Solution
    {
        private char[,] _image;
        private int Rows { get; set; }
        private int Cols { get; set; }
        private string _algorithm;
        public bool IsEvenIteration;

        private void ParseInput(string[] input)
        {
            _algorithm = input[0];

            Cols = input[2].Length;
            Rows = input.Length - 2;

            _image = new char[Rows, Cols];

            for (int i = 2; i < input.Length; i++)
            {
                for (int j = 0; j < Cols; j++)
                    _image[i - 2, j] = input[i][j];
            }
        }
        public int CountLitPixels()
        {
            int count = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    if (_image[i, j] == '#')
                        count++;
            }

            return count;
        }

        public char GetOutputPixel(int r, int c)
        {
            char borderChar = '.';
            if (r == 0 || c == 0 || r == Rows - 1 || c == Cols - 1)
            {
                if (!IsEvenIteration)
                    borderChar = '#';

                return borderChar;
            }

            StringBuilder binaryCode = new StringBuilder();

            for (int i = r - 1; i <= r + 1; i++)
            {
                for (int j = c - 1; j <= c + 1; j++)
                {
                    char x = _image[i, j] == '#' ? '1' : '0';
                    binaryCode.Append(x);
                }
            }
            
            int decimalCode = Convert.ToInt32(binaryCode.ToString(), 2);
            return _algorithm[decimalCode];
        }

        public void EnhanceImage()
        {
            char[,] enhancedImage = new char[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    enhancedImage[i, j] = GetOutputPixel(i, j);
            }

            _image = enhancedImage;
        }
        public void ExtendImage(int margin)
        {
            int newRows = Rows + margin * 2;
            int newCols = Cols + margin * 2;
            char[,] extendedImage = new char[Rows + margin * 2, Cols + margin * 2];

            char borderChar = IsEvenIteration ? '#' : '.';
            
            for (int k = 0; k < newRows; k++)
            {
                for (int m = 0; m < margin; m++)
                {
                    extendedImage[k, m] = borderChar;
                    extendedImage[k, newCols - m-1] = borderChar;
                }
            }
            
            for (int k = 0; k < newCols; k++)
            {
                for (int m = 0; m < margin; m++)
                {
                    extendedImage[m, k] = borderChar;
                    extendedImage[newRows - m-1, k] = borderChar;
                }
            }
            
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    extendedImage[i + margin, j + margin] = _image[i, j];
            }

            _image = extendedImage;
            Rows = newRows;
            Cols = newCols;
        }

        private void MultipleEnhance(int iter)
        {
            IsEvenIteration = false;
            for (int i = 0; i < iter; i++)
            {
                ExtendImage(2);
                EnhanceImage();
                IsEvenIteration = !IsEvenIteration;
            }
        }
        public void Part1(string[] input)
        {
            ParseInput(input);
            MultipleEnhance(2);
            Console.WriteLine(CountLitPixels());
        }
        
        public void Part2(string[] input)
        {
            ParseInput(input);
            MultipleEnhance(50);
            Console.WriteLine(CountLitPixels());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day20\\day20.txt");

            Solution s = new Solution();
            s.Part1(input);
            s.Part2(input);
        }
    }
}