using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Anketa
    {
        private int anketaId;
        private string naziv;
        private DateTime pocetak;
        private DateTime kraj;
        public string Naziv { get => naziv; set => naziv = value; }
        public DateTime Pocetak { get => pocetak; set => pocetak = value; }
        public DateTime Kraj { get => kraj; set => kraj = value; }
        public int AnketaId { get => anketaId; set => anketaId = value; }
    }
}
