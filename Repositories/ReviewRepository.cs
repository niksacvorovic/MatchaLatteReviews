using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using Newtonsoft.Json;

namespace MatchaLatteReviews.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public ReviewRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Review Add(Review review)
        {
            var reviews = GetAll().ToList();

            if (reviews.Any(g => g.ReviewId == review.ReviewId))
            {
                throw new InvalidOperationException($"Review with ID {review.ReviewId} already exists.");
            }
            reviews.Add(review);
            SaveAll(reviews);
            return review;
        }

        public void DeleteById(string id)
        {
            var reviews = GetAll().ToList();
            var review = reviews.FirstOrDefault(g => g.ReviewId == id);

            if (review != null)
            {
                reviews.Remove(review);
                SaveAll(reviews);
            }
        }

        public IEnumerable<Review> GetAll()
        {
            string content = _context.LoadContent();
            var reviews = JsonConvert.DeserializeObject<List<Review>>(content, _serializerSettings)
                          ?? new List<Review>();
            return reviews;
        }

        public Review GetById(string id)
        {
            return GetAll().FirstOrDefault(g => g.ReviewId == id);
        }

        public void SaveAll(IEnumerable<Review> reviews)
        {
            string content = JsonConvert.SerializeObject(reviews, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Review Update(Review updatedReview)
        {
            var reviews = GetAll().ToList();
            var existingReview = reviews.FirstOrDefault(g => g.ReviewId == updatedReview.ReviewId);

            if (existingReview == null)
            {
                throw new InvalidOperationException($"Review with ID {updatedReview.ReviewId} not found.");
            }

            reviews.Remove(existingReview);
            reviews.Add(updatedReview);
            SaveAll(reviews);

            return updatedReview;
        }
    }
}
