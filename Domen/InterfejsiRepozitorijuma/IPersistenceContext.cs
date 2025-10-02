namespace MatchaLatteReviews.Domen.InterfejsiRepozitorijuma
{
    public interface IPersistenceContext
    {
        string LoadContent();
        void SaveContent(string content);
    }
}
