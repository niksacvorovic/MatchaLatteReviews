using MatchaLatteReviews.Domen.Modeli;
using MatchaLatteReviews.Domen.RepositoryInterfaces;
using System;

namespace MatchaLatteReviews.Application.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        internal Korisnik GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}
