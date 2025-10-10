using MatchaLatteReviews.Application.Constants;
using MatchaLatteReviews.Domain.RepositoryInterfaces;
using System.IO;

namespace MatchaLatteReviews.Repositories
{
    public class JsonPersistenceContext : IPersistenceContext
    {
        private readonly string _filePath;

        public JsonPersistenceContext(string entityName)
        {
            var dataFolder = Path.Combine(Constants.ProjectRoot, "Data");
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }
            _filePath = Path.Combine(dataFolder, $"{entityName}.json");
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
