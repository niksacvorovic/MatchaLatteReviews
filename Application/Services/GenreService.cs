using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Linq;

namespace MatchaLatteReviews.Application.Services
{
    public class GenreService
    {
        private IGenreRepository _genreRepository;

        public GenreService()
        {
            _genreRepository = Injector.CreateInstance<IGenreRepository>();
        }

        public void Add(Genre genre)
        {
            var genres = _genreRepository.GetAll();
            if (genres.FirstOrDefault(g => g.Name.Equals(genre.Name)) != null)
            {
                throw new InvalidOperationException("Genre already exists!");
            }
            _genreRepository.Add(genre);
        }
    }
}
