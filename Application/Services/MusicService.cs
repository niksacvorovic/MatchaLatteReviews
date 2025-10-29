using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public void Add(Music music)
        {
            var articles = _articleRepository.GetAll();
            
            if (articles.FirstOrDefault(a => a.Id.Equals(music.Id)) != null)
            {
                throw new InvalidOperationException("Music already exists!");
            }

            _articleRepository.Add(music);
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

        public void Delete(string id)
        {
            var articles = _articleRepository.GetAll();
            if (articles.FirstOrDefault(a => a.Id.Equals(id)) == null)
                throw new InvalidOperationException("Music does not exist!");

            _articleRepository.DeleteById(id);
        }


        public IEnumerable<Music> GetAll()
        {
            return _articleRepository.GetAll().OfType<Music>();
        }
    }
}
