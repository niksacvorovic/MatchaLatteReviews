using MatchaLatteReviews.Domen.Modeli;

namespace MatchaLatteReviews.Stores

{
    public class UserStore
    {
        public Korisnik CurrentUser { get; private set; }
        public void SetCurrentUser(Korisnik user)
        {
            CurrentUser = user;
        }
    }
}
