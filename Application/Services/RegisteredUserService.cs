using MatchaLatteReviews.DependencyInjection;
using MatchaLatteReviews.Domain.Model;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System;

namespace MatchaLatteReviews.Application.Services
{
    public class RegisteredUserService
    {
        private readonly IUserRepository _userRepository;

        public RegisteredUserService()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
        }

        public void Register(RegisteredUser registeredUser)
        {
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
