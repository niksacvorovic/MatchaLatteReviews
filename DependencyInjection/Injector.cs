using MatchaLatteReviews.Domain.RepositoryInterfaces;
using MatchaLatteReviews.Repositories;
using MatchaLatteReviews.Stores;
using MatchaLatteReviews.Application.Services;
using System;
using System.Collections.Generic;
using MatchaLatteReviews.Application.Utilities;

namespace MatchaLatteReviews.DependencyInjection
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>();
        static Injector()
        {
            var userContext = new JsonPersistenceContext("users");
            var genreContext = new JsonPersistenceContext("genres");

            var userRepository = new UserRepository(userContext);
            var genreRepository = new GenreRepository(genreContext);


            var userStore = new UserStore();

            _implementations[typeof(IUserRepository)] = userRepository;
            _implementations[typeof(IGenreRepository)] = genreRepository;

            var userValidator = new UserValidator();
            _implementations[typeof(UserValidator)] = userValidator;

            var userService = new UserService(userRepository);
            var editorService = new EditorService();
            var registeredUserService = new RegisteredUserService();

            _implementations[typeof(UserService)] = userService;
            _implementations[typeof(RegisteredUserService)] = registeredUserService;
            _implementations[typeof(EditorService)] = editorService;

            _implementations[typeof(UserStore)] = userStore;
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
