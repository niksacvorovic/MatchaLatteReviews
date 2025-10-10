using MatchaLatteReviews.Domain.Model;
namespace MatchaLatteReviews.Domain.RepositoryInterfaces
{
    public interface IUserRepository : ICrudRepository<User>
    {
        User GetByUsername(string username);
    }
}
