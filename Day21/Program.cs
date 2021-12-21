using System;
using System.Collections.Generic;

namespace Day21
{
    class Dice
    {
        private int Value { get; set; }

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
        public int Score { get; set; }
        public int StartPos { get; set; }
        public int CurrPos { get; set; }

        public Player(int startPos)
        {
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
    }

    class Solution
    {
        private Player _player1;
        private Player _player2;
        private Dice _dice;
        private readonly List<int> _frequencies = new() {1, 3, 6, 7, 6, 3, 1};
        private readonly List<int> _outcomes = new() {3, 4, 5, 6, 7, 8, 9};

        public void SetGame(string[] input)
        {
            _player1 = new Player(Int32.Parse(input[0].Split(":")[1]));
            _player2 = new Player(Int32.Parse(input[1].Split(":")[1]));
            _dice = new Dice();
        }


        public void Part1()
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

        private int Next(int curr, int hops)
        {
            if ((curr + hops) % 10 == 0)
                return 10;

            return (curr + hops) % 10;
        }

        public void Part2()
        {
            long[,,,] states = new long[11, 11, 21, 21];
            long player1Wins = 0;
            long player2Wins = 0;
            long gamesLeft = 1;

            states[_player1.StartPos, _player2.StartPos, 0, 0] = 1;
            
            bool currPlayerFirst = true;

            while (gamesLeft > 0) 
            {
                long[,,,] newStates = new long[11, 11, 21, 21];
                gamesLeft = 0;
                
                for (int i = 0; i < 11; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        for (int k = 0; k < 21; k++)
                        {
                            for (int l = 0; l < 21; l++)
                            {
                                long universesNum = states[i, j, k, l];

                                if (currPlayerFirst)
                                {
                                    for (int m = 0; m < 7; m++)
                                    {
                                        int nextPos = Next(i, _outcomes[m]);
                                        int newPoints = k + nextPos;
                                        if (newPoints >= 21)
                                        {
                                            player1Wins += universesNum * _frequencies[m];
                                            continue;
                                        }
                                        
                                        newStates[nextPos, j, newPoints, l] += universesNum * _frequencies[m];
                                        gamesLeft += universesNum * _frequencies[m];

                                    }
                                }
                                else
                                {
                                    for (int m = 0; m < 7; m++)
                                    {
                                        int nextPos = Next(j, _outcomes[m]);
                                        int newPoints = l + nextPos;
                                        if (newPoints >= 21)
                                        {
                                            player2Wins += universesNum * _frequencies[m];
                                            continue;
                                        }

                                        newStates[i, nextPos, k,newPoints] += universesNum * _frequencies[m];
                                        gamesLeft += universesNum * _frequencies[m];

                                    }
                                }
                            }
                        }
                    }
                }
                currPlayerFirst = !currPlayerFirst;
                states = newStates;
            }

            Console.WriteLine(Math.Max(player1Wins,player2Wins));
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
            s.Part1();
            s.Part2();
        }
    }
}