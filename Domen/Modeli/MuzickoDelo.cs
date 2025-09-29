using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class MuzickoDelo : Clanak
    {
        private Tip tipDela;
        private string naziv;
        private int trajanje;
        public Tip TipDela { get => tipDela; set => tipDela = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Trajanje { get => trajanje; set => trajanje = value; }
    }
}
