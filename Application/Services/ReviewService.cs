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
            var reviews = _reviewRepository.GetAll();
            if (reviews.FirstOrDefault(g => g.ReviewId.Equals(review.ReviewId)) != null)
            {
                throw new InvalidOperationException("Review already exists!");
            }
            _reviewRepository.Add(review);
        }
    }
}
