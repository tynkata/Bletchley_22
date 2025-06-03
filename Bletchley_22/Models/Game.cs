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

        // Determines if the game has been solved
        public bool IsSolved => Guesses.Any(g => g.Code == SecretCode);

        // Returns total number of guesses
        public int GuessCount => Guesses.Count;

        // Evaluates a new guess and returns the Guess object
        public Guess EvaluateGuess(string input)
        {
            int correct = 0;
            int misplaced = 0;

            var secret = SecretCode.ToCharArray();
            var guess = input.ToCharArray();
            var secretMatched = new bool[secret.Length];
            var guessMatched = new bool[guess.Length];

            // First pass – check for correct positions
            for (int i = 0; i < secret.Length; i++)
            {
                if (i < guess.Length && guess[i] == secret[i])
                {
                    correct++;
                    secretMatched[i] = true;
                    guessMatched[i] = true;
                }
            }

            // Second pass – check for misplaced characters
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
