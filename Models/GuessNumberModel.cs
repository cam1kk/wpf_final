using System;
using System.Collections.Generic;

namespace wpf_final.Models
{
    public class GuessNumberModel
    {
        private readonly Random _random = new Random();
        public string TargetNumber { get; private set; }

        public GuessNumberModel()
        {
            GenerateTargetNumber();
        }

        public void GenerateTargetNumber()
        {
            TargetNumber = _random.Next(1, 100000).ToString("D5");
        }

        public List<GuessResult> CheckGuess(string userGuess)
        {
            var results = new List<GuessResult>()
            {
                new GuessResult("_", "_"),
                new GuessResult("_", "_"),
                new GuessResult("_", "_"),
                new GuessResult("_", "_"),
                new GuessResult("_", "_")
            };
            var used = new bool[5] { false, false, false, false, false };

            for (int i = 0; i < results.Count; i++)
            {
                if (userGuess[i] == TargetNumber[i])
                {
                    results[i] = new GuessResult($" {userGuess[i]} ", "Green");
                    used[i] = true;
                }
            }

            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < results.Count; j++)
                {
                    if (i != j && !used[i] && !used[j] && userGuess[i] == TargetNumber[j])
                    {
                        results[i] = new GuessResult($" {userGuess[i]} ", "Goldenrod");
                        used[i] = true;
                    }
                }
            }

            for (int i = 0; i < results.Count; i++)
            {
                if (!used[i])
                {
                    results[i] = new GuessResult($" {userGuess[i]} ", "Gray");
                }
            }
            
            return results;
        }
    }

    public class GuessResult
    {
        public string Digit { get; set; }
        public string Color { get; set; }

        public GuessResult(string digit, string color)
        {
            Digit = digit;
            Color = color;
        }
    }
}
