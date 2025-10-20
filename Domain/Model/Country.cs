using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domain.Model
{
    public class Country
    {
        private string id;
        private string name;
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        [JsonConstructor]
        public Country(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public Country(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }
}
