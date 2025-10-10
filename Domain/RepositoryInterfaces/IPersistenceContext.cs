namespace MatchaLatteReviews.Domain.RepositoryInterfaces
{
    public interface IPersistenceContext
    {
        string LoadContent();
        void SaveContent(string content);
    }
}
