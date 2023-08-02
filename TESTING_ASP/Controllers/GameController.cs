using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TESTING_ASP.Models;

namespace TESTING_ASP.Controllers
{
    public class GameController : Controller
    {
        private readonly IGamesRepository game;

        public GameController(IGamesRepository game)
        {
            this.game = game;
        }

        public IActionResult Index()
        {
            IEnumerable<Game> games = game.GetAllGames();
            return View(games);
        }
    }
}
