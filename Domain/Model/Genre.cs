using System;
using Newtonsoft.Json;

namespace MatchaLatteReviews.Domain.Model
{
    public class Genre
    {
        private string id;
        private string name;
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        [JsonConstructor]
        public Genre(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Genre(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public override String ToString() => Name;
    }
}
