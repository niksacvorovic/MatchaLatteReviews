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
            var articleContext = new JsonPersistenceContext("articles");
            var musicContext = new JsonPersistenceContext("music");

            var userRepository = new UserRepository(userContext);
            var genreRepository = new GenreRepository(genreContext);
            var articleRepository = new ArticleRepository(articleContext);
            var musicRepository = new MusicRepository(musicContext);


            var userStore = new UserStore();

            _implementations[typeof(IUserRepository)] = userRepository;
            _implementations[typeof(IGenreRepository)] = genreRepository;
            _implementations[typeof(IArticleRepository)] = articleRepository;
            _implementations[typeof(IMusicRepository)] = musicRepository;

            var userValidator = new UserValidator();
            _implementations[typeof(UserValidator)] = userValidator;

            var musicService = new MusicService(musicRepository);
            var userService = new UserService(userRepository);
            var genreService = new GenreService();
            var editorService = new EditorService();
            var registeredUserService = new RegisteredUserService();

            _implementations[typeof(UserService)] = userService;
            _implementations[typeof(GenreService)] = genreService;
            _implementations[typeof(RegisteredUserService)] = registeredUserService;
            _implementations[typeof(EditorService)] = editorService;
            _implementations[typeof(MusicService)] = musicService;

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
