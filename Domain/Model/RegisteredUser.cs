using MatchaLatteReviews.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domain.Model
{
    public class RegisteredUser:User
    {
        private bool premium;
        private List<string> reviewIds;
        private List<string> favoriteIds;
        private List<Review> reviews;
        private List<Article> favorites;

        public bool Premium { get => premium; set => premium = value; }
        public List<String> ReviewIds { get => reviewIds; set => reviewIds = value; }
        public List<String> FavoriteIds { get => favoriteIds; set => favoriteIds = value; }

        [JsonIgnore]
        public List<Review> Reviews { get => reviews; set => reviews = value; }

        [JsonIgnore]
        public List<Article> Favorites { get => favorites; set => favorites = value; }

        [JsonConstructor]
        public RegisteredUser(
            string username,
            string password,
            string firstName,
            string lastName,
            bool isPublic,
            Role role,
            bool premium,
            List<string> reviewIds,
            List<string> favoriteIds
        ) : base(username, password, firstName, lastName, isPublic, role)
        {
            UserId = Guid.NewGuid().ToString();
            Premium = premium;
            ReviewIds = reviewIds;
            FavoriteIds = favoriteIds;
        }
    }
}
