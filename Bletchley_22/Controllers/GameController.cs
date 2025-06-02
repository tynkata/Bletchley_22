using Microsoft.AspNetCore.Mvc;
using Bletchley_22.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bletchley_22.Controllers
{
    public class GameController : Controller
    {
        private static Game currentGame;

        public IActionResult Index()
        {
            if (currentGame == null)
            {
                return RedirectToAction("New");
            }
            return View(currentGame);
        }

        public IActionResult New()
        {
            currentGame = new Game
            {
                Id = 1,
                SecretCode = "ABCD", 
                Guesses = new List<Guess>()
            };
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MakeGuess(string guessCode)
        {
            if (currentGame == null)
            {
                return RedirectToAction("New");
            }

            if (string.IsNullOrEmpty(guessCode) || guessCode.Length != currentGame.SecretCode.Length)
            {
                ModelState.AddModelError("", "Invalid guess length");
                return View("Index", currentGame);
            }

            // process guess
            var guess = new Guess
            {
                Code = guessCode.ToUpper()
            };

            guess.Correct = 0;
            guess.Misplaced = 0;

            for (int i = 0; i < guess.Code.Length; i++)
            {
                if (guess.Code[i] == currentGame.SecretCode[i])
                {
                    guess.Correct++;
                }
                else if (currentGame.SecretCode.Contains(guess.Code[i]))
                {
                    guess.Misplaced++;
                }
            }

            currentGame.Guesses.Add(guess);

            // Optionally add win check here to redirect to Win page

            return RedirectToAction("Index");
        }

        // GET: /Game/Win
        public IActionResult Win()
        {
            return View();
        }
    }
}
