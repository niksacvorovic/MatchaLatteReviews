using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Korisnik
    {
        private int korisnikId;
        private String korisnickoIme;
        private String lozinka;
        private String ime;
        private string prezime;
        private bool javni;
        private Uloga uloga;
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public bool Javni { get => javni; set => javni = value; }
        public Uloga Uloga { get => uloga; set => uloga = value; }
        public int KorisnikId { get => korisnikId; set => korisnikId = value; }
    }
}
