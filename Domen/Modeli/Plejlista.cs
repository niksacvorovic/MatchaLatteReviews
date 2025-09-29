using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Plejlista
    {
        private String naziv;
        private bool javna;
        private List<MuzickoDelo> sadrzaj;
        private RegistrovaniKorisnik
        public string Naziv { get => naziv; set => naziv = value; }
        public bool Javna { get => javna; set => javna = value; }
        public List<MuzickoDelo> Sadrzaj { get => sadrzaj; set => sadrzaj = value; }
    }
}
