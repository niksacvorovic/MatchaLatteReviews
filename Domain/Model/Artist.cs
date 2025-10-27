using MatchaLatteReviews.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domain.Model
{
    public class Artist : Article
    {
        private int debut;
        private List<string> countryIds;
        private List<Country> countries;
        public int Debut { get => debut; set => debut = value; }
        public List<string> CountryIds { get => countryIds; set => countryIds = value; }

        [JsonIgnore]
        public List<Country> Countries { get => countries; set => countries = value; }

        [JsonConstructor]
        public Artist(string id, string title, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, int debut, List<string> countryIds) : 
            base(id, title, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Debut = debut;
            CountryIds = countryIds;
            Countries = countries;
        }

        public Artist(string title, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, int debut, List<string> countryIds) :
            base(title, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Debut = debut;
            CountryIds = countryIds;
            Countries = countries;
        }

    }
}
