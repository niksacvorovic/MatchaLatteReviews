using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;

namespace MatchaLatteReviews.Application.Services
{
    public class ReviewService
    {
        private IReviewRepository _reviewRepository;

        public ReviewService()
        {
            _reviewRepository = Injector.CreateInstance<IReviewRepository>();
        }


        public void Add(Review review)
        {
            EnsureValid(review);

            // jedinstven ReviewId
            if (_reviewRepository.GetAll()
                                 .Any(r => string.Equals(r.ReviewId, review.ReviewId)))
            {
                throw new InvalidOperationException("Review already exists!");
            }

            _reviewRepository.Add(review);
        }

        public void Update(Review review)
        {
            EnsureValid(review);

            if (!_reviewRepository.GetAll()
                                  .Any(r => string.Equals(r.ReviewId, review.ReviewId)))
            {
                throw new InvalidOperationException("Review does not exist!");
            }

            _reviewRepository.Update(review);
        }

        public void Delete(string reviewId)
        {
            if (string.IsNullOrWhiteSpace(reviewId))
                throw new ArgumentException("reviewId is required.", nameof(reviewId));

            if (!_reviewRepository.GetAll()
                                  .Any(r => string.Equals(r.ReviewId, reviewId)))
            {
                throw new InvalidOperationException("Review does not exist!");
            }

            _reviewRepository.DeleteById(reviewId);
        }

        public Review GetById(string reviewId)
        {
            if (string.IsNullOrWhiteSpace(reviewId)) return null;

            return _reviewRepository.GetAll()
                                    .FirstOrDefault(r => string.Equals(r.ReviewId, reviewId));
        }

        public IEnumerable<Review> GetArticleReviews(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId)) return Enumerable.Empty<Review>();

            return _reviewRepository.GetAll()
                                    .Where(r => string.Equals(r.ArticleId, articleId));
        }

        public bool HasUserReviewed(string articleId, string userId)
        {
            if (string.IsNullOrWhiteSpace(articleId) || string.IsNullOrWhiteSpace(userId)) return false;

            return _reviewRepository.GetAll().Any(r =>
                string.Equals(r.ArticleId, articleId) &&
                string.Equals(r.AuthorId, userId));
        }

        public double? GetArticleAverageRating(string articleId)
        {
            var list = GetArticleReviews(articleId).ToList();
            if (list.Count == 0) return null;

            return list.Average(r => r.Rating);
        }

        private static void EnsureValid(Review review)
        {
            if (review == null) throw new ArgumentNullException(nameof(review));
            if (string.IsNullOrWhiteSpace(review.ReviewId)) throw new ArgumentException("ReviewId is required.");
            if (string.IsNullOrWhiteSpace(review.ArticleId)) throw new ArgumentException("ArticleId is required.");
            if (string.IsNullOrWhiteSpace(review.AuthorId)) throw new ArgumentException("UserId is required.");

        }

    }
}
