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
        private int id;
        private string name;
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        [JsonConstructor]
        public Country(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
