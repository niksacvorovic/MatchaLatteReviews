using MatchaLatteReviews.Application.Constants;
using MatchaLatteReviews.Domen.RepositoryInterfaces;
using System.IO;

namespace MatchaLatteReviews.Repositories
{
    public class JsonPersistenceContext : IPersistenceContext
    {
        private readonly string _filePath;

        public JsonPersistenceContext(string entityName)
        {
            _filePath = Path.Combine(Constants.ProjectRoot, "Data", $"{entityName}.json");
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public string LoadContent()
        {
            return File.ReadAllText(_filePath);
        }

        public void SaveContent(string content)
        {
            File.WriteAllText(_filePath, content);
        }
    }
}
