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
    public class ArticleRepository : IArticleRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public ArticleRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Clanak Add(Clanak article)
        {
            var articles = GetAll().ToList();

            articles.Add(article);
            SaveAll(articles);
            return article;
        }

        public void DeleteById(int id)
        {
            var articles = GetAll().ToList();
            var article = articles.FirstOrDefault(a => a.Id == id);

            if (article != null)
            {
                articles.Remove(article);
                SaveAll(articles);
            }
        }

        public IEnumerable<Clanak> GetAll()
        {
            string content = _context.LoadContent();
            var articles = JsonConvert.DeserializeObject<List<Clanak>>(content, _serializerSettings)
                       ?? new List<Clanak>();
            return articles;
        }

        public Clanak GetById(int id)
        {
            return GetAll().FirstOrDefault(a => a.Id == id);
        }

        public void SaveAll(IEnumerable<Clanak> users)
        {
            string content = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Clanak Update(Clanak updatedArticle)
        {
            var articles = GetAll().ToList();
            var existingArticle = articles.FirstOrDefault(a => a.Id == updatedArticle.Id);

            if (existingArticle == null)
            {
                throw new InvalidOperationException($"User with id {updatedArticle.Id} not found.");
            }

            articles.Remove(existingArticle);
            articles.Add(updatedArticle);

            SaveAll(articles);
            return updatedArticle;
        }

    }
}
