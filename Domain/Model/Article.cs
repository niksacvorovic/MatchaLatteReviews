using MatchaLatteReviews.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MatchaLatteReviews.Domain.Model
{
    public class Article
    {
        private string id;
        private string title;
        private string image;
        private int rating;
        private string content;
        private DateTime date;
        private Status status;
        private int views;
        private string editorId;
        private List<string> reviewIds;
        private List<string> genreIds;
        private Editor author;
        private List<Review> reviews;
        private List<Genre> genres;


        public string Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Image { get => image; set => image = value; }
        public int Rating { get => rating; set => rating = value; }
        public string EditorId { get => editorId; set => editorId = value; }
        public string Content { get => content; set => content = value; }
        public DateTime Date { get => date; set => date = value; }
        public Status Status { get => status; set => status = value; }
        public int Views { get => views; set => views = value; }
        public List<string> ReviewIds { get => reviewIds; set => reviewIds = value; }
        public List<string> GenreIds { get => genreIds; set => genreIds = value; }

        [JsonIgnore]
        public List<Review> Reviews { get => reviews; set => reviews = value; }

        [JsonIgnore]
        public List<Genre> Genres { get => genres; set => genres = value; }

        [JsonIgnore]
        internal Editor Author { get => author; set => author = value; }
        [JsonConstructor]
        public Article(string id, string title, string image, int rating, string content, DateTime date, Status status, int views, 
            string editorId, List<string> reviewIds, List<string> genreIds)
        {
            Id = id;
            Title = title;
            Image = image;
            Rating = rating;
            Content = content;
            Date = date;
            Status = status;
            Views = views;
            EditorId = editorId;
            ReviewIds = reviewIds;
            GenreIds = genreIds;
        }

        public Article(string title, string image, int rating, string content, DateTime date, Status status, int views, 
            string editorId, List<string> reviewIds, List<string> genreIds)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Image = image;
            Rating = rating;
            Content = content;
            Date = date;
            Status = status;
            Views = views;
            EditorId = editorId;
            ReviewIds = reviewIds;
            GenreIds = genreIds;
        }
    }
}
