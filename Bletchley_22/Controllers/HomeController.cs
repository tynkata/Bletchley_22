using System.Diagnostics;
using Bletchley_22.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bletchley_22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const int CodeLength = 4;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Play()
        {
            var model = new Game
            {
                CodeLength = CodeLength,
                GuessesMade = 0,
                StatusMessage = "Keep trying"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitGuess(string guess)
        {
            int guessesMade = TempData.ContainsKey("Guesses") ? (int)TempData["Guesses"] : 0;
            guessesMade++;
            TempData["Guesses"] = guessesMade;

            var model = new Game
            {
                CodeLength = CodeLength,
                GuessesMade = guessesMade,
                StatusMessage = "Guess submitted"
            };

            return View("Play", model);
        }

        public IActionResult GameStatus()
        {
            int guessesMade = TempData.ContainsKey("Guesses") ? (int)TempData["Guesses"] : 0;
            var model = new Game
            {
                CodeLength = CodeLength,
                GuessesMade = guessesMade,
                StatusMessage = "Current game status"
            };

            return View(model);
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
