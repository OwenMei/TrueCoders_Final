using Newtonsoft.Json.Linq;
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
                ListOfGames.Add(new Game { ID = int.Parse(game["id"].ToString()) , Title = game["title"].ToString() });
            }

            return ListOfGames;
        }
    }
}
