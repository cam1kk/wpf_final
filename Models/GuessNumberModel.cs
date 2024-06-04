using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace wpf_final.Models
{
    public class GuessNumberModel
    {
        private readonly Random _random = new Random();
        private string _targetNumber = string.Empty;
        private int _digitsCount;

        public GuessNumberModel(int digitsCount)
        {
            Update(digitsCount);
        }

        public void Update(int digitsCount)
        {
            _digitsCount = digitsCount;
            _targetNumber = _random.Next(1, int.Parse(Math.Pow(10, _digitsCount).ToString()) ).ToString($"D{_digitsCount}");
        }

        public List<GuessResult> CheckGuess(string userGuess)
        {
            var results = new List<GuessResult>();
            for (int i = 0; i < _digitsCount; i++)
            {
                results.Add(new GuessResult("_", "_"));
            }
            var green_cells = new bool[_digitsCount];
            for (int i = 0; i < _digitsCount; i++)
            {
                green_cells[i] = false;
            }
            var yellow_cells = new bool[_digitsCount];
            for (int i = 0; i < _digitsCount; i++)
            {
                yellow_cells[i] = false;
            }

            for (int i = 0; i < results.Count; i++)
            {
                if (userGuess[i] == _targetNumber[i])
                {
                    results[i] = new GuessResult($" {userGuess[i]} ", "Green");
                    green_cells[i] = true;
                }
            }

            for (int i = 0; i < results.Count; i++)
            {
                for (int j = 0; j < results.Count; j++)
                {
                    if (i != j && !green_cells[i] && !green_cells[j] && !yellow_cells[j] && userGuess[j] == _targetNumber[i])
                    {
                        results[j] = new GuessResult($" {userGuess[j]} ", "Goldenrod");
                        yellow_cells[j] = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < results.Count; i++)
            {
                if (!yellow_cells[i] && !green_cells[i])
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
