using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;

namespace MatchaLatteReviews.Application.Services
{
    public class EditorService
    {
        private IUserRepository _userRepository;
        private UserValidator _validator;

        public EditorService()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
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
            assignedEditor.Articles.Add(article);
            assignedEditor.ArticleIds.Add(article.Id);
        }

    }
}
