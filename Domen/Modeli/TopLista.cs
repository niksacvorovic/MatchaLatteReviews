using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class TopLista
    {
        private int topListaId;
        private string naziv;
        private Period period;
        private Anketa anketa;
        public string Naziv { get => naziv; set => naziv = value; }
        public Period Period { get => period; set => period = value; }
        public Anketa Anketa { get => anketa; set => anketa = value; }
        public int TopListaId { get => topListaId; set => topListaId = value; }
    }
}
