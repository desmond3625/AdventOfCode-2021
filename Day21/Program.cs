using System;

namespace Day21
{
    class Dice
    {
        public int Value { get; set; }

        public int Roll()
        {
            Value++;
            if (Value == 101)
                Value = 1;
            return Value;
        }
    }

    class Player
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public int StartPos { get; set; }
        public int CurrPos { get; set; }

        public Player(int id, int startPos)
        {
            ID = id;
            Score = 0;
            StartPos = startPos;
            CurrPos = startPos;
        }

        public void PlayRound(Dice dice)
        {
            int result = dice.Roll() + dice.Roll() + dice.Roll();

            if ((CurrPos + result) % 10 == 0)
                CurrPos = 10;
            else
                CurrPos = (CurrPos + result) % 10;

            Score += CurrPos;
        }

        public override string ToString()
        {
            return "Player " + ID + ", current position: " + CurrPos + ", score: " + Score;
        }
    }

    class Solution
    {
        private Player _player1;
        private Player _player2;
        private Dice _dice;
        
        public void SetGame(string[] input)
        {
            _player1 = new Player(1, Int32.Parse(input[0].Split(":")[1]));
            _player2 = new Player(2, Int32.Parse(input[1].Split(":")[1]));
            _dice = new Dice();
        }


        public void Play()
        {
            int totalRolls = 0;
            int loserScore = 0;

            while (true)
            {
                _player1.PlayRound(_dice);
                totalRolls += 3;
                if (_player1.Score >= 1000)
                {
                    loserScore = _player2.Score;
                    break;
                }

                _player2.PlayRound(_dice);
                totalRolls += 3;
                if (_player2.Score >= 1000)
                {
                    loserScore = _player1.Score;
                    break;
                }
            }

            Console.WriteLine(totalRolls * loserScore);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File
                .ReadAllLines("C:\\AdventOfCode-2021\\AdventOfCode-2021\\Day21\\day21.txt");

            Solution s = new Solution();
            s.SetGame(input);
            s.Play();
        }
    }
}