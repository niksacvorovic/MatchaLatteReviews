using MatchaLatteReviews.Domen.Modeli;
using MatchaLatteReviews.Domen.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Recenzija Add(Recenzija review)
        {
            var reviews = GetAll().ToList();

            reviews.Add(review);
            SaveAll(reviews);
            return review;
        }

        public void DeleteById(int id)
        {
            var reviews = GetAll().ToList();
            var review = reviews.FirstOrDefault(r => r.RecenzijaId == id);

            if (review != null)
            {
                reviews.Remove(review);
                SaveAll(reviews);
            }
        }

        public IEnumerable<Recenzija> GetAll()
        {
            string content = _context.LoadContent();
            var reviews = JsonConvert.DeserializeObject<List<Recenzija>>(content, _serializerSettings)
                       ?? new List<Recenzija>();
            return reviews;
        }

        public Recenzija GetById(int id)
        {
            return GetAll().FirstOrDefault(r => r.RecenzijaId == id);
        }

        public void SaveAll(IEnumerable<Recenzija> users)
        {
            string content = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Recenzija Update(Recenzija updatedReview)
        {
            var reviews = GetAll().ToList();
            var existingArticle = reviews.FirstOrDefault(r => r.RecenzijaId == updatedReview.RecenzijaId);

            if (existingArticle == null)
            {
                throw new InvalidOperationException($"User with id {updatedReview.RecenzijaId} not found.");
            }

            reviews.Remove(existingArticle);
            reviews.Add(updatedReview);

            SaveAll(reviews);
            return updatedReview;
        }

    }
}
