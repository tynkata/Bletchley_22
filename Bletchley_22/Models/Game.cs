using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bletchley_22.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string SecretCode { get; set; } = "";

        public DateTime StartTime { get; set; } = DateTime.Now;

        public List<Guess> Guesses { get; set; } = new List<Guess>();

        public int CodeLength => SecretCode.Length;

        public int GuessesMade => Guesses.Count;

        public string StatusMessage
        {
            get
            {
                if (IsSolved) return "Game solved";
                if (Guesses.Count == 0) return "Make your first guess";
                return "Keep trying";
            }
        }

        public bool IsSolved => Guesses.Any(g => g.Code == SecretCode);

        public Guess EvaluateGuess(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length != SecretCode.Length)
                throw new ArgumentException($"Guess must be exactly {SecretCode.Length} characters");

            int correct = 0;
            int misplaced = 0;

            var secret = SecretCode.ToCharArray();
            var guess = input.ToCharArray();
            var secretMatched = new bool[secret.Length];
            var guessMatched = new bool[guess.Length];

            for (int i = 0; i < secret.Length; i++)
            {
                if (guess[i] == secret[i])
                {
                    correct++;
                    secretMatched[i] = true;
                    guessMatched[i] = true;
                }
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (guessMatched[i]) continue;

                for (int j = 0; j < secret.Length; j++)
                {
                    if (!secretMatched[j] && guess[i] == secret[j])
                    {
                        misplaced++;
                        secretMatched[j] = true;
                        break;
                    }
                }
            }

            var guessEntry = new Guess
            {
                Code = input,
                Correct = correct,
                Misplaced = misplaced,
                Timestamp = DateTime.Now
            };

            Guesses.Add(guessEntry);
            return guessEntry;
        }
    }
}
