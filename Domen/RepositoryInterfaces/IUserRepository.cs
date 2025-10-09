using MatchaLatteReviews.Domen.Modeli;

namespace MatchaLatteReviews.Domen.RepositoryInterfaces
{
    public interface IUserRepository : ICrudRepository<Korisnik>
    {
        Korisnik GetByUsername(string username);
    }
}
