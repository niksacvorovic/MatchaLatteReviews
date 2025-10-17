using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace MatchaLatteReviews.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public MusicRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Music Add(Music music)
        {
            var musics = GetAll().ToList();

            if (musics.Any(g => g.Title.Equals(music.Title)))
            {
                throw new InvalidOperationException($"Article with title {music.Title}  already exists.");
            }
            musics.Add(music);
            SaveAll(musics);
            return music;
        }

        public void DeleteById(string id)
        {
            var musics = GetAll().ToList();
            var music = musics.FirstOrDefault(g => g.Id == id);

            if (music != null)
            {
                musics.Remove(music);
                SaveAll(musics);
            }
        }

        public IEnumerable<Music> GetAll()
        {
            string content = _context.LoadContent();
            var musics = JsonConvert.DeserializeObject<List<Music>>(content, _serializerSettings)
                          ?? new List<Music>();
            return musics;
        }

        public Music GetById(string id)
        {
            return GetAll().FirstOrDefault(g => g.Id == id);
        }

        public void SaveAll(IEnumerable<Music> musics)
        {
            string content = JsonConvert.SerializeObject(musics, Formatting.Indented, _serializerSettings);
            MessageBox.Show(content);
            _context.SaveContent(content);
        }

        public Music Update(Music updatedMusic)
        {
            var musics = GetAll().ToList();
            var existingArticle = musics.FirstOrDefault(g => g.Id == updatedMusic.Id);

            if (existingArticle == null)
            {
                throw new InvalidOperationException($"Music with ID {updatedMusic.Id} not found.");
            }

            musics.Remove(existingArticle);
            musics.Add(updatedMusic);
            SaveAll(musics);

            return updatedMusic;
        }

    }
}
