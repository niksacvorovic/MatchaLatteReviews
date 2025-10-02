namespace MatchaLatteReviews.Domen.RepositoryInterfaces
{
    public interface IPersistenceContext
    {
        string LoadContent();
        void SaveContent(string content);
    }
}
