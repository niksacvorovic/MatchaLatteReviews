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
        private IArticleRepository _musicRepository;

        public MusicService()
        {
            _musicRepository = Injector.CreateInstance<IArticleRepository>();
        }

        public void Add(Music music)
        {
            var musics = _musicRepository.GetAll();
            if (musics.FirstOrDefault(g => g.Title.Equals(music.Title)) != null)
            {
                throw new InvalidOperationException("Music work already exists!");
            }
            _musicRepository.Add(music);
        }
    }
}
