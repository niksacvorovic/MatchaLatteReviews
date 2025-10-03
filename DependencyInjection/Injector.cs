using MatchaLatteReviews.Domen.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using System;
using System.Collections.Generic;

namespace MatchaLatteReviews.DependencyInjection
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>();
        static Injector()
        {
            var userContext = new JsonPersistenceContext("users");

            var userRepository = new UserRepository(userContext);

            _implementations[typeof(IUserRepository)] = userRepository;
        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
