using MatchaLatteReviews.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Type = MatchaLatteReviews.Domain.Enums.Type;

namespace MatchaLatteReviews.Domain.Model
{
    public class Music : Article
    {
        private Type type;
        //name = title
        private int length;
        private List<Version> versions;
        public Type Type { get => type; set => type = value; }
        public int Length { get => length; set => length = value; }
        public List<Version> Versions { get => versions; set => versions = value; }

        [JsonConstructor]
        public Music(string id, string title, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, Type type, int length, List<Version> versions) :
            base(id, title, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Type = type;
            Title = title;
            Length = length;
            Versions = versions;
        }

        public Music(string title, int rating, string content, DateTime date, Status status, int views,
            string editorId, List<string> reviewIds, List<string> genreIds, Type type, int length, List<Version> versions) :
            base(title, rating, content, date, status, views, editorId, reviewIds, genreIds)
        {
            Type = type;
            Title = title;
            Length = length;
            Versions = versions;
        }
    }
}
