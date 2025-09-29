using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class MatchaLatteReviews
    {
        private List<Korisnik> korisnici;
        private List<Clanak> clanci;
        private List<Reklama> reklame;
        public List<Korisnik> Korisnici { get => korisnici; set => korisnici = value; }
        public List<Clanak> Clanci { get => clanci; set => clanci = value; }
        public List<Reklama> Reklame { get => reklame; set => reklame = value; }
    }
}
