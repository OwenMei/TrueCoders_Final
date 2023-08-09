using System.Data;
using TESTING_ASP.Models;
using Dapper;
using Microsoft.AspNetCore.Components.Web;

namespace TESTING_ASP
{
    public class ReviewsRepository : IReviewRepository
    {
        private IDbConnection _conn;

        public ReviewsRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public IEnumerable<GameReview> GetAllReviews(string gameTitle)
        {
            return _conn.Query<GameReview>("SELECT * FROM REVIEWS WHERE GameName = @title", new {title = gameTitle});
        }

        public GameReview MakeReview(string gameTitle)
        {
            var newReview = new GameReview();
            newReview.GameName = gameTitle;
            return newReview;
        }

        public IEnumerable<GameReview> FilterGameReviews(string gameTitle, int starRating)
        {
            return _conn.Query<GameReview>("SELECT * FROM REVIEWS WHERE Rating = @stars AND GameName = @title", new { stars = starRating, title = gameTitle });
        }

        public GameReview GetGameReview(int id)
        {
            return _conn.QuerySingle<GameReview>("SELECT * FROM REVIEWS WHERE ReviewID = @iden", new { @iden = id});
        }

        public void AddReview(GameReview review)
        {
            _conn.Execute("INSERT INTO REVIEWS (ReviewID, GameName, Review, Rating) values (@reviewid, @gamename, @review, @rating)",
                new {reviewid = review.ReviewID, gamename = review.GameName, review = review.Review, rating = review.Rating });
        }

        public void RemoveReview(GameReview review)
        {
            _conn.Execute("DELETE FROM REVIEWS WHERE ReviewID = @reviewid",
                new { reviewid = review.ReviewID });
        }

        public void EditReview(GameReview review)
        {
            _conn.Execute("UPDATE REVIEWS SET Review = @review, Rating = @rating WHERE ReviewID = @id", 
                new {review = review.Review, rating = review.Rating, @id = review.ReviewID});
        }
    }
}
