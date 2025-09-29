using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class RegistrovaniKorisnik : Korisnik
    {
        private bool premium;
        private List<Recenzija> recenzije;
        public bool Premium { get => premium; set => premium = value; }
        public List<Recenzija> Recenzije { get => recenzije; set => recenzije = value; }
    }
}
