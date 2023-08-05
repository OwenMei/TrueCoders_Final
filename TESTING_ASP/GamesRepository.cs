using System.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TESTING_ASP.Models;

namespace TESTING_ASP
{
    public class GamesRepository : IGamesRepository
    {
        public IEnumerable<Game> GetAllGames() 
        {
            List<Game> ListOfGames = new List<Game>(); 
            var client = new HttpClient();
            var url = "https://www.freetogame.com/api/games";
            var games = client.GetStringAsync(url).Result;
            var gameIndices = JArray.Parse(games);

            foreach (var game in gameIndices)
            {
                ListOfGames.Add(new Game 
                { 
                    ID = int.Parse(game["id"].ToString()),
                    Title = game["title"].ToString(),
                    Thumbnail = game["thumbnail"].ToString(),
                    Genre = game["genre"].ToString(),
                    ShortDescription = game["short_description"].ToString()
                });
            }

            return ListOfGames;
        }
        public IEnumerable<Game> GetAllGames(IReviewRepository repo) 
        {
            List<Game> ListOfGames = new List<Game>(); 
            var client = new HttpClient();
            var url = "https://www.freetogame.com/api/games";
            var games = client.GetStringAsync(url).Result;
            var gameIndices = JArray.Parse(games);

            foreach (var game in gameIndices)
            {
                ListOfGames.Add(new Game 
                { 
                    ID = int.Parse(game["id"].ToString()),
                    Title = game["title"].ToString(),
                    Thumbnail = game["thumbnail"].ToString(),
                    Genre = game["genre"].ToString(),
                    ShortDescription = game["short_description"].ToString(),
                    AverageRating = ReturnAverageRating(repo, game["title"].ToString())
                });
            }

            return ListOfGames;
        }

        public IEnumerable<Game> GamesOfGenre(string genre)
        {
            var allGames = GetAllGames();
            List<Game> gamesWithGenre = new List<Game>();
            foreach(var game in allGames)
            {
                if(game.Genre == genre)
                {
                    gamesWithGenre.Add(game);
                }
            }
            return gamesWithGenre;
        }

        public string[] GetAllGenres()
        {
            HashSet<string> listOfGenres = new HashSet<string>();
            var allGames = GetAllGames();
            foreach( var game in allGames)
            {
                listOfGenres.Add(game.Genre);
            }
            return listOfGenres.ToArray();
        }

        public double ReturnAverageRating(IReviewRepository repo,string gamename)
        {
            var allRatings = repo.GetAllReviews(gamename);
            double totalRatings = 0;
            double ratingSum = 0;
            foreach ( var rating in allRatings)
            {
                ratingSum += rating.Rating;
                totalRatings += 1;
            }
            return ratingSum / totalRatings;
        }
    }
}
