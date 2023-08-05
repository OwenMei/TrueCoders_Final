using Org.BouncyCastle.Asn1.GM;
using TESTING_ASP.Models;

namespace TESTING_ASP
{
    public interface IGamesRepository
    {
        public IEnumerable<Game> GetAllGames();
        public IEnumerable<Game> GetAllGames(IReviewRepository repo);
        public string[] GetAllGenres();
        public IEnumerable<Game> GamesOfGenre(string genre);
        public double ReturnAverageRating(IReviewRepository repo, string gamename);
    }
}
