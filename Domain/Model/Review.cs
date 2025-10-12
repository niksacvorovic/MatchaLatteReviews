using System;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domain.Model
{
    public class Review
    {
        private string reviewId;
        private int rating;
        private string comment;
        private bool isPublic;
        private bool isApproved;
        private string authorId;
        private string articleId;
        private RegisteredUser author;
        private Article article;

        public string ReviewId { get => reviewId; set => reviewId = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Comment { get => comment; set => comment = value; }
        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public bool IsApproved { get => isApproved; set => isApproved = value; }
        public string AuthorId { get => authorId; set => authorId = value; }
        public string ArticleId { get => articleId; set => articleId = value; }

        [JsonIgnore]
        public RegisteredUser Author { get => author; set => author = value; }

        [JsonIgnore]
        public Article Article { get => article; set => article = value; }

        [JsonConstructor]
        public Review(int rating, string comment, bool isPublic, bool isApproved, string authorId, string articleId)
        {
            ReviewId = Guid.NewGuid().ToString();
            Rating = rating;
            Comment = comment;
            IsPublic = isPublic;
            IsApproved = isApproved;
            AuthorId = authorId;
            ArticleId = articleId;
        }
    }
}