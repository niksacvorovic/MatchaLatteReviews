using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
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

        public User Add(User user)
        {
            var users = GetAll().ToList();

            if (users.Any(u => u.Username == user.Username))
            {
                throw new InvalidOperationException($"User with username {user.Username} already exists.");
            }
            users.Add(user);
            SaveAll(users);
            return user;
        }

        public void DeleteById(string id)
        {
            var users = GetAll().ToList();
            var user = users.FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                users.Remove(user);
                SaveAll(users);
            }
        }

        public IEnumerable<User> GetAll()
        {
            string content = _context.LoadContent();
            var users = JsonConvert.DeserializeObject<List<User>>(content, _serializerSettings)
                       ?? new List<User>();
            return users;
        }

        public User GetByUsername(string username)
        {
           return GetAll().FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        
        public User GetById(string id)
        {
            return GetAll().FirstOrDefault(u => u.UserId == id);
        }

        public void SaveAll(IEnumerable<User> users)
        {
            string content = JsonConvert.SerializeObject(users, Formatting.Indented, _serializerSettings);
            _context.SaveContent(content);
        }

        public User Update(User updatedUser)
        {
            var users = GetAll().ToList();
            var existingUser = users.FirstOrDefault(u => u.UserId == updatedUser.UserId);

            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with id {updatedUser.UserId} not found.");
            }

            users.Remove(existingUser);
            users.Add(updatedUser);

            SaveAll(users);
            return updatedUser;
        }
    }
}
