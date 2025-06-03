using System.Diagnostics;
using Bletchley_22.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bletchley_22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const int CodeLength = 4;

        private static Game? currentGame;  // made nullable to fix CS8618

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            // Initialize currentGame if null
            if (currentGame == null)
            {
                currentGame = new Game
                {
                    SecretCode = GenerateSecretCode(CodeLength)
                };
            }
        }

        // Helper to generate a random secret code (A-Z letters)
        private string GenerateSecretCode(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Range(0, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Play()
        {
            if (currentGame == null)
            {
                currentGame = new Game
                {
                    SecretCode = GenerateSecretCode(CodeLength)
                };
            }

            return View(currentGame);
        }

        [HttpPost]
        public IActionResult SubmitGuess(string guess)
        {
            if (currentGame == null)
            {
                currentGame = new Game
                {
                    SecretCode = GenerateSecretCode(CodeLength)
                };
            }

            if (string.IsNullOrEmpty(guess) || guess.Length != CodeLength)
            {
                ModelState.AddModelError("", $"Guess must be exactly {CodeLength} characters");
                return View("Play", currentGame);
            }

            // Evaluate guess and add to currentGame
            var evaluatedGuess = currentGame.EvaluateGuess(guess.ToUpper());

            if (currentGame.IsSolved)
            {
                return RedirectToAction("Win");
            }

            return View("Play", currentGame);
        }

        public IActionResult GameStatus()
        {
            if (currentGame == null)
            {
                currentGame = new Game
                {
                    SecretCode = GenerateSecretCode(CodeLength)
                };
            }

            return View(currentGame);
        }

        public IActionResult Win()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
