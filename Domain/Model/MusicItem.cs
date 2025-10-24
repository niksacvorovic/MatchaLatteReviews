using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domain.Model
{
    public class MusicItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public MusicItem(string title, string content, int rating) { 
            Title = title;
            Content = content;
            Rating = rating;
        }
    }
}
