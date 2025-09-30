using MatchaLatteReviews.Domen.Enumeracije;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Korisnik
    {
        private int korisnikId;
        private string korisnickoIme;
        private string lozinka;
        private string ime;
        private string prezime;
        private bool javni;
        private Uloga uloga;
        public int KorisnikId { get => korisnikId; set => korisnikId = value; }
        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Lozinka { get => lozinka; set => lozinka = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public bool Javni { get => javni; set => javni = value; }
        public Uloga Uloga { get => uloga; set => uloga = value; }

        [JsonConstructor]
        public Korisnik(int korisnikId, string korisnickoIme, string lozinka, string ime, string prezime, bool javni, Uloga uloga)
        {
            KorisnikId = korisnikId;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Javni = javni;
            Uloga = uloga;
        }
    }
}
