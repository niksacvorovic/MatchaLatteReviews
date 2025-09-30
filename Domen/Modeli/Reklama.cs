using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Reklama
    {
        private int reklamaId;
        private string link;
        public int ReklamaId { get => reklamaId; set => reklamaId = value; }
        public string Link { get => link; set => link = value; }

        [JsonConstructor]
        public Reklama(int reklamaId, string link)
        {
            ReklamaId = reklamaId;
            Link = link;
        }
    }
}
