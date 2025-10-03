using MatchaLatteReviews.Domen.Modeli;
using MatchaLatteReviews.Domen.RepositoryInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchaLatteReviews.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IPersistenceContext _context;
        private readonly JsonSerializerSettings _serializerSettings;

        public UserRepository(IPersistenceContext context)
        {
            _context = context;
            _serializerSettings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        public Korisnik Add(Korisnik user)
        {
            var users = GetAll().ToList();

            if (users.Any(u => u.KorisnickoIme == user.KorisnickoIme))
            {
                throw new InvalidOperationException($"User with username {user.KorisnickoIme} already exists.");
            }
            users.Add(user);
            SaveAll(users);
            return user;
        }

        public void DeleteById(int id)
        {
            var users = GetAll().ToList();
            var user = users.FirstOrDefault(u => u.KorisnikId == id);

            if (user != null)
            {
                users.Remove(user);
                SaveAll(users);
            }
        }

        public IEnumerable<Korisnik> GetAll()
        {
            string content = _context.LoadContent();
            var users = JsonConvert.DeserializeObject<List<Korisnik>>(content, _serializerSettings)
                       ?? new List<Korisnik>();
            return users;
        }

        public Korisnik GetById(int id)
        {
            return GetAll().FirstOrDefault(u => u.KorisnikId == id);
        }

        public void SaveAll(IEnumerable<Korisnik> users)
        {
            string content = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public Korisnik Update(Korisnik updatedUser)
        {
            var users = GetAll().ToList();
            var existingUser = users.FirstOrDefault(u => u.KorisnikId == updatedUser.KorisnikId);

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with id {updatedUser.KorisnikId} not found.");
            }

            users.Remove(existingUser);
            users.Add(updatedUser);

            SaveAll(users);
            return updatedUser;
        }
    }
}
