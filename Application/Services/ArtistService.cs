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
    internal class ArtistService
    {
        private IArticleRepository _articleRepository;

        public ArtistService()
        {
            _articleRepository = Injector.CreateInstance<IArticleRepository>();
        }

        public void Add(Artist artist)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(artist.Id)) != null)
            {
                throw new InvalidOperationException("Artist already exists!");
            }
            _articleRepository.Add(artist);
        }


        public void Update(Artist artist)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(artist.Id)) == null)
            {
                throw new InvalidOperationException("Artist does not exist!");
            }
            _articleRepository.Update(artist);
        }

        public void Delete(String id)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(id)) == null)
            {
                throw new InvalidOperationException("Artist does not exist!");
            }
            _articleRepository.DeleteById(id);
        }

        public IEnumerable<Artist> GetAll()
        {
            return _articleRepository.GetAll().OfType<Artist>();
        }


        public Artist GetById(string id)
            => _articleRepository.GetAll().OfType<Artist>().FirstOrDefault(a => a.Id == id);
    }
}
