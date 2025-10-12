using System;
using System.Text.Json.Serialization;
using MatchaLatteReviews.Domain.Enums;

namespace MatchaLatteReviews.Domain.Model
{
    [JsonDerivedType(typeof(User), 0)]
    [JsonDerivedType(typeof(RegisteredUser), 1)]
    [JsonDerivedType(typeof(Editor), 2)]
    public class User
    {
        private string userId;
        private string username;
        private string password;
        private string firstName;
        private string lastName;
        private bool isPublic;
        private Role role;

        public string UserId { get => userId; set => userId = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public Role Role { get => role; set => role = value; }

        [JsonConstructor]
        public User(string userId, string username, string password, string firstName, string lastName, bool isPublic, Role role)
        {
            UserId = userId;
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            IsPublic = isPublic;
            Role = role;
        }

        public User(string username, string password, string firstName, string lastName, bool isPublic, Role role)
        {
            UserId = Guid.NewGuid().ToString();
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            IsPublic = isPublic;
            Role = role;
        }
    }
}