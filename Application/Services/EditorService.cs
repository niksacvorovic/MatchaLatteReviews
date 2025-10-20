using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace MatchaLatteReviews.Application.Services
{
    public class EditorService
    {
        private IUserRepository _userRepository;
        private IArticleRepository _articleRepository;
        private UserValidator _validator;

        public EditorService()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _articleRepository = Injector.CreateInstance<IArticleRepository>();
            _validator = Injector.CreateInstance<UserValidator>();
        }

        internal void Register(Editor registeredEditor)
        {
            Validate(registeredEditor);
            var existingUser = _userRepository.GetByUsername(registeredEditor.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }
            registeredEditor.Role = Domain.Enums.Role.Editor;
            _userRepository.Add(registeredEditor);
        }

        private void Validate(Editor editor)
        {
            _validator.ValidateUser(editor);
        }

        public void AddToTaskList(string username, Article article)
        {
            Editor assignedEditor = (Editor)_userRepository.GetByUsername(username);
            article.EditorId = assignedEditor.UserId;
            _articleRepository.Add(article);
            assignedEditor.ArticleIds.Add(article.Id);
        }

        public List<string> GetEditorsForGenre(Genre genre)
        {
            List<User> allEditors = _userRepository.GetAll().Where(e => e.GetType() == typeof(Editor)).ToList();
            List<string> usernames = new List<string>();
            foreach (Editor editor in allEditors)
            {
                if(editor.GenresIds.Contains(genre.Name)) usernames.Add(editor.Username);
            }
            return usernames;
        }
    }
}
