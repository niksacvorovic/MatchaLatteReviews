using MatchaLatteReviews.Domain.Model;
using System;
using System.Text.RegularExpressions;

namespace MatchaLatteReviews.Application.Utilities
{
    public class UserValidator
    {
        private static readonly Regex NamePattern = new Regex(@"^[A-ZČĆŠĐŽ][a-zčćšđž]+(?:[- ][A-ZČĆŠĐŽ][a-zčćšđž]+)*$");
        private static readonly Regex UsernamePattern = new Regex(@"^[a-z0-9_]{3,20}$");
        private static readonly Regex PasswordPattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$");

        public void ValidateUser(User user)
        {
            ValidateName(user.FirstName, "First name");
            ValidateName(user.LastName, "Last name");
            ValidateUsername(user.Username);
            ValidatePassword(user.Password);
        }


        private void EnsureNotEmpty(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{fieldName} cannot be empty.");
            }
        }

        private void EnsureMatch(string value, Regex pattern, string errorMessage)
        {
            if (!pattern.IsMatch(value))
            {
                throw new ArgumentException(errorMessage);
            }
        }

        // ---- Validations ----
        public void ValidateName(string name, string fieldName)
        {
            EnsureNotEmpty(name, fieldName);
            EnsureMatch(name, NamePattern, $"{fieldName} must start with a capital letter and contain only letters.");
        }

        public void ValidateUsername(string username)
        {
            EnsureNotEmpty(username, "Username");
            EnsureMatch(username, UsernamePattern, "Username must contain only lowercase letters, numbers, or underscores.");
        }

        public void ValidatePassword(string password)
        {
            EnsureNotEmpty(password, "Password");
            EnsureMatch(password, PasswordPattern,
                "Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.");
        }
    }
}
