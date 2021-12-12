using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Board
    {
        private int[,] board;
        public int Width { get; set; }
        public int Height { get; set; }
        private int[] hitRowsCounter;
        private int[] hitColumnsCounter;
        public Boolean HasAlreadyWon { get; set; }


        public Board(int width, int height)
        {
            board = new int[width, height];
            Width = width;
            Height = height;
            hitColumnsCounter = new int[width];
            hitRowsCounter = new int[height];
        }

        public Boolean Mark(int x)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (board[i, j] == x)
                    {
                        board[i, j] = -1;

                        if (++hitColumnsCounter[j] == 5 || ++hitRowsCounter[i] == 5)
                        {
                            HasAlreadyWon = true;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void SetValue(int row, int col, int x)
        {
            board[row, col] = x;
        }

        public int UnmarkedSum()
        {
            int sum = 0;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (board[i, j] != -1)
                        sum += board[i, j];
                }
            }

            return sum;
        }
    }

    class Bingo
    {
        private List<Board> _boardList;
        private int[] _drawnNumbers;

        public Bingo(int[] drawnNumbers)
        {
            _boardList = new List<Board>();
            _drawnNumbers = drawnNumbers;
        }

        public void AddBoard(Board board)
        {
            _boardList.Add(board);
        }

        public int PlayBingoPart1()
        {
            int result = 0;
            for (int i = 0; i < _drawnNumbers.Length; i++)
            {
                for (int b = 0; b < _boardList.Count; b++)
                {
                    Board board = _boardList[b];

                    Boolean isWin = board.Mark(_drawnNumbers[i]);
                    if (isWin)
                    {
                        result = _drawnNumbers[i] * board.UnmarkedSum();
                        return result;
                    }
                }
            }

            return result;
        }

        public int PlayBingoPart2()
        {
            int result = 0;
            for (int i = 0; i < _drawnNumbers.Length; i++)
            {
                for (int b = 0; b < _boardList.Count; b++)
                {
                    Board board = _boardList[b];
                    if (!board.HasAlreadyWon)
                    {
                        Boolean isWin = board.Mark(_drawnNumbers[i]);
                        if (isWin)
                            result = _drawnNumbers[i] * board.UnmarkedSum();
                    }
                }
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines("C:\\UJ\\AdventOfCode\\AdventOfCode\\Day4\\day4.txt");

            int[] drawnNumbers = lines[0].Split(",")
                .Select(x => Int32.Parse(x)).ToArray();

            Bingo bingo = new Bingo(drawnNumbers);

            for (int line = 2; line < lines.Length; line += 6)
            {
                Board board = new Board(5, 5);

                for (int i = 0; i < 5; i++)
                {
                    int[] row = Regex.Split(lines[line + i].Trim(' '), @"\D+")
                        .Select(x => Int32.Parse(x))
                        .ToArray();

                    for (int j = 0; j < 5; j++)
                        board.SetValue(i, j, row[j]);
                }

                bingo.AddBoard(board);
            }

            Console.WriteLine(bingo.PlayBingoPart1());
            Console.WriteLine(bingo.PlayBingoPart2());
        }
    }
}