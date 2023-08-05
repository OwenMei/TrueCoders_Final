using TESTING_ASP.Models;

namespace TESTING_ASP
{
    public interface IReviewRepository
    {
        public IEnumerable<GameReview> GetAllReviews(string gameTitle);
        public GameReview MakeReview(string gameTitle);
        public void AddReview(GameReview review);
        public void RemoveReview(GameReview review);
        public IEnumerable<GameReview> FilterGameReviews(string gameTitle, int starRating);
        public void EditReview(GameReview review);
        public GameReview GetGameReview(int id);
    }
}
