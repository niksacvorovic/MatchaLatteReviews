using MatchaLatteReviews.Domain.Model;

namespace MatchaLatteReviews.Stores

{
    public class UserStore
    {
        public User CurrentUser { get; private set; }
        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
        public User GetCurrentUser()
        {
            return CurrentUser;
        }
    }
}
