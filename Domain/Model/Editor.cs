using MatchaLatteReviews.Domain.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domain.Model
{
    internal class Editor : User
    {
        private List<string> articleIds;
        private List<string> taskListIds;
        private List<string> genresIds;
        private List<Article> articles;
        private List<Article> taskList;
        private List<Genre> genres;

        public List<string> ArticleIds { get => articleIds; set => articleIds = value; }
        public List<string> TaskListIds { get => taskListIds; set => taskListIds = value; }
        public List<string> GenresIds { get => genresIds; set => genresIds = value; }

        [JsonIgnore]
        public List<Article> Articles { get => articles; set => articles = value; }
        [JsonIgnore]
        public List<Article> TaskList { get => taskList; set => taskList = value; }

        [JsonIgnore]
        public List<Genre> Genres { get => genres; set => genres = value; }

        [JsonConstructor]
        public Editor(string userId, string username, string password, string firstName, string lastName, bool isPublic, Role role, List<string> articleIds, List<string> taskListIds, List<string> genreIds)
           : base(userId, username, password, firstName, lastName, isPublic, role)
        {
            ArticleIds = articleIds;
            TaskListIds = taskListIds;
            GenresIds = genreIds;
        }

        public Editor(string username, string password, string firstName, string lastName, bool isPublic, Role role, List<string> articleIds, List<string> taskListIds, List<string> genreIds)
           : base(username, password, firstName, lastName, isPublic, role) 
        {
            ArticleIds = articleIds;
            TaskListIds = taskListIds;
            GenresIds = genreIds;
        }
    }
}