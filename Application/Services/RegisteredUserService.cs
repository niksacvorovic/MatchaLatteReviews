using MatchaLatteReviews.Application.Utilities;
using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;

namespace MatchaLatteReviews.Application.Services
{
    public class RegisteredUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserValidator _validator;

        public RegisteredUserService()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
            _validator = Injector.CreateInstance<UserValidator>();
        }

        private void Validate(RegisteredUser registeredUser)
        {
            _validator.ValidateUser(registeredUser);
        }

        public void Register(RegisteredUser registeredUser)
        {
            Validate(registeredUser);
            var existingUser = _userRepository.GetByUsername(registeredUser.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }
            registeredUser.Role = Domain.Enums.Role.RegisteredUser;
            _userRepository.Add(registeredUser);
        }
    }
}
