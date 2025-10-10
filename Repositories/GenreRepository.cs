using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchaLatteReviews.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public GenreRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Genre Add(Genre genre)
        {
            var genres = GetAll().ToList();

            if (genres.Any(g => g.Name == genre.Name))
            {
                throw new InvalidOperationException($"Genre with name {genre.Name}  already exists.");
            }
            genres.Add(genre);
            SaveAll(genres);
            return genre;
        }

        public void DeleteById(string id)
        {
            var genres = GetAll().ToList();
            var genre = genres.FirstOrDefault(g => g.Id == id);

            if (genre != null)
            {
                genres.Remove(genre);
                SaveAll(genres);
            }
        }

        public IEnumerable<Genre> GetAll()
        {
            string content = _context.LoadContent();
            var genres = JsonConvert.DeserializeObject<List<Genre>>(content, _serializerSettings)
                          ?? new List<Genre>();
            return genres;
        }

        public Genre GetById(string id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public void SaveAll(IEnumerable<Genre> genres)
        {
            string content = JsonConvert.SerializeObject(genres, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Genre Update(Genre updatedGenre)
        {
            var genres = GetAll().ToList();
            var existingGenre = genres.FirstOrDefault(g => g.Id == updatedGenre.Id);

            if (existingGenre == null)
            {
                throw new InvalidOperationException($"Genre with ID {updatedGenre.Id} not found.");
            }

            genres.Remove(existingGenre);
            genres.Add(updatedGenre);
            SaveAll(genres);

            return updatedGenre;
        }
    }
}
