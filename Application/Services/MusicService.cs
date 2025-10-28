using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Application.Services
{
    public class MusicService
    {
        private IArticleRepository _articleRepository;

        public MusicService()
        {
            _articleRepository = Injector.CreateInstance<IArticleRepository>();
        }

        public void Add(Music music)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(music.Id)) != null)
            {
                throw new InvalidOperationException("Music already exists!");
            }
            _articleRepository.Add(music);
        }
    }
}
