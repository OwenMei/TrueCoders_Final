using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using TESTING_ASP.Models;

namespace TESTING_ASP.Controllers
{
    public class GameController : Controller
    {
        private readonly IGamesRepository _game;
        private readonly IReviewRepository _reviewRepo;

        public GameController(IGamesRepository game, IReviewRepository review)
        {
            _game = game;
            _reviewRepo = review;
        }

        public IActionResult ViewReview(string name)
        {
            var gameReviews = _reviewRepo.GetAllReviews(name);
            ViewBag.gameName = name;
            return View(gameReviews);
        }

        public IActionResult CreateReview(string gameTitle)
        {
            var newGameReview = _reviewRepo.MakeReview(gameTitle);
            ViewBag.gameTitle = gameTitle;
            return View(newGameReview);
        }

        public IActionResult AddReviewToDatabase(GameReview gamereview)
        {
            _reviewRepo.AddReview(gamereview);
            return RedirectToAction("ViewReview",new { name = gamereview.GameName });
        }
        
        public IActionResult UpdateReview(int id)
        {
            var gamereview = _reviewRepo.GetGameReview(id);
            return View(gamereview);
        }

        public IActionResult EditReviewToDatabase(GameReview gamereview)
        {
            var gameReview = _reviewRepo.GetGameReview(gamereview.ReviewID);
            _reviewRepo.EditReview(gamereview);
            return RedirectToAction("ViewReview", new { name = gameReview.GameName });
        }
        public IActionResult DeleteReview(int id)
        {
            var gamereview = _reviewRepo.GetGameReview(id);
            _reviewRepo.RemoveReview(gamereview);
            return RedirectToAction("ViewReview", new { name = gamereview.GameName });
        }

        public IActionResult Index()
        {
            IEnumerable<Game> games = _game.GetAllGames(_reviewRepo);
            ViewBag.genres = _game.GetAllGenres();
            return View(games);
        }

        public IActionResult GamesByGenre(string genre)
        {
            ViewBag.genre = genre;
            ViewBag.genres = _game.GetAllGenres();
            IEnumerable<Game> gamesWithGenre = _game.GamesOfGenre(_reviewRepo, genre);
            return View(gamesWithGenre);
        }
    }
}
