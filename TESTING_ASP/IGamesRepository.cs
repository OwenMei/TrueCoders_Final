using TESTING_ASP.Models;

namespace TESTING_ASP
{
    public interface IGamesRepository
    {
        public IEnumerable<Game> GetAllGames();
    }
}
