using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Reklama
    {
        private int reklamaId;
        private string link;
        public string Link { get => link; set => link = value; }
        public int ReklamaId { get => reklamaId; set => reklamaId = value; }
    }
}
