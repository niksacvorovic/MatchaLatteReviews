using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchaLatteReviews.Application.Services
{
    public class MusicService
    {
        private IArticleRepository _articleRepository;

        public MusicService()
        {
            _articleRepository = Injector.CreateInstance<IArticleRepository>();
        }

        public void Update(Music music)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(music.Id)) == null)
            {
                throw new InvalidOperationException("Artist does not exist!");
            }
            _articleRepository.Update(music);
        }


        public IEnumerable<Music> GetAll()
        {
            return _articleRepository.GetAll().OfType<Music>();
        }
    }
}