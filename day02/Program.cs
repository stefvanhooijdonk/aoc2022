using System;
using System.Collections;

namespace aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2022 - day 2.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            List<Round> rounds = new List<Round>();

            foreach(string line in lines){
                var round = new Round(line);

                //Console.WriteLine("{0} {1} {2}",round.opponent, round.strategy, round.Score);
                rounds.Add(round);
            }

            Console.WriteLine("Read input.txt with: {0} rounds.\r\n",rounds.Count);
            Console.WriteLine("Total score {0}.\r\n",rounds.Sum(r=> r.Score));



        }
    }

    public enum RPS{
            Rock,
            Paper,
            Scissors,
            Unknown
        }
    public enum RPSOutcome{
        Lose,
        Win,
        Draw,
        Unknown
    }

    public class Round
    {
        //A for Rock, B for Paper, and C for Scissors
        internal RPS opponent;
        //X for Rock, Y for Paper, and Z for Scissors.
        internal RPS strategy;
        internal RPSOutcome outcome;

        private RPS GetRPS(char letter){
            if(letter == 'A' || letter == 'X') return RPS.Rock;
            if(letter == 'B' || letter == 'Y') return RPS.Paper;
            if(letter == 'C' || letter == 'Z') return RPS.Scissors;
            return RPS.Unknown;
        }

        private RPSOutcome GetRPSOutcome(char letter){
            if(letter == 'X') return RPSOutcome.Lose;
            if(letter == 'Y') return RPSOutcome.Draw;
            if(letter == 'Z') return RPSOutcome.Win;
            return RPSOutcome.Unknown;
        }

        public Round(string line){
            opponent = GetRPS(line[0]);
            outcome = GetRPSOutcome(line[2]);

            if(outcome==RPSOutcome.Win){
                if(opponent==RPS.Paper) strategy= RPS.Scissors;
                if(opponent==RPS.Rock) strategy= RPS.Paper;
                if(opponent==RPS.Scissors) strategy= RPS.Rock;
            }
            if(outcome==RPSOutcome.Draw){
                strategy = opponent;
            }
            if(outcome==RPSOutcome.Lose){
                if(opponent==RPS.Paper) strategy= RPS.Rock;
                if(opponent==RPS.Rock) strategy= RPS.Scissors;
                if(opponent==RPS.Scissors) strategy= RPS.Paper;
            }
        }

        public int Score{
            get{
                int score = 0;
                //1 for Rock, 2 for Paper, and 3 for Scissors
                switch(strategy){
                    case RPS.Rock:
                        score=1; break;
                    case RPS.Paper:
                        score=2; break;
                    case RPS.Scissors:
                        score=3; break;
                }

                //(0 if you lost, 3 if the round was a draw, and 6 if you won)
                if(strategy==opponent){
                    score += 3;
                    return score;
                }

                if(strategy==RPS.Rock && opponent==RPS.Scissors){
                    score += 6;
                    return score;
                }
                if(strategy==RPS.Paper && opponent==RPS.Rock){
                    score += 6;
                    return score;
                }
                if(strategy==RPS.Scissors && opponent==RPS.Paper){
                    score += 6;
                    return score;
                }

                return score;
            }
        }
    }
}
