using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public Article Add(Article article)
        {
            var articles = GetAll().ToList();

            if (articles.Any(g => g.Title.Equals(article.Title)))
            {
                throw new InvalidOperationException($"Article with title {article.Title}  already exists.");
            }
            articles.Add(article);
            SaveAll(articles);
            return article;
        }

        public Article Add(Music music)
        {
            var articles = GetAll().ToList();

            if (articles.Any(g => g.Title.Equals(music.Title)))
            {
                throw new InvalidOperationException($"Music with title {music.Title}  already exists.");
            }
            articles.Add(music);
            SaveAll(articles);
            return music;
        }

        public void DeleteById(string id)
        {
            var articles = GetAll().ToList();
            var article = articles.FirstOrDefault(g => g.Id == id);

            if (article != null)
            {
                articles.Remove(article);
                SaveAll(articles);
            }
        }

        public IEnumerable<Article> GetAll()
        {
            string content = _context.LoadContent();
            var articles = JsonConvert.DeserializeObject<List<Article>>(content, _serializerSettings)
                          ?? new List<Article>();
            return articles;
        }

        public Article GetById(string id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public void SaveAll(IEnumerable<Article> articles)
        {
            string content = JsonConvert.SerializeObject(articles, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Article Update(Article updatedArticle)
        {
            var articles = GetAll().ToList();
            var existingArticle = articles.FirstOrDefault(g => g.Id == updatedArticle.Id);

            if (existingArticle == null)
            {
                throw new InvalidOperationException($"Article with ID {updatedArticle.Id} not found.");
            }

            articles.Remove(existingArticle);
            articles.Add(updatedArticle);
            SaveAll(articles);

            return updatedArticle;
        }

    }
}
