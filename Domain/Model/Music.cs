using MatchaLatteReviews.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Type = MatchaLatteReviews.Domain.Enums.Type;

namespace MatchaLatteReviews.Domain.Model
{
    public class Music : Article
    {
        private Type type;
        private string name;
        private int length;
        private List<Version> versions;
        public Type Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int Length { get => length; set => length = value; }
        public List<Version> Versions { get => versions; set => versions = value; }

        [JsonConstructor]
        public Music(string id, string title, string image, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, Type type, string name, int length, List<Version> versions) : 
            base(id, title, image, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Type = type;
            Name = name;
            Length = length;
            Versions = versions;
        }

        public Music(string title, string image, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, Type type, string name, int length, List<Version> versions) :
            base(title, image, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Type = type;
            Name = name;
            Length = length;
            Versions = versions;
        }
    }
}
