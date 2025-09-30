using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class RegistrovaniKorisnik : Korisnik
    {
        private bool premium;
        private List<int> recenzijeIds;
        private List<int> favoritiIds;
        private List<Recenzija> recenzije;
        private List<Clanak> favoriti;
        public bool Premium { get => premium; set => premium = value; }
        public List<int> RecenzijeIds { get => recenzijeIds; set => recenzijeIds = value; }
        public List<int> FavoritiIds { get => favoritiIds; set => favoritiIds = value; }

        [JsonIgnore]
        public List<Recenzija> Recenzije { get => recenzije; set => recenzije = value; }
        [JsonIgnore]
        public List<Clanak> Favoriti { get => favoriti; set => favoriti = value; }

        [JsonConstructor]
        public RegistrovaniKorisnik(int korisnikId, string korisnickoIme, string lozinka, string ime, string prezime, bool javni, Uloga uloga, 
            bool premium, List<int> recenzijeIds, List<int> favoritiIds) : base(korisnikId, korisnickoIme, lozinka, ime, prezime, javni, uloga)
        {
            Premium = premium;
            RecenzijeIds = recenzijeIds;
            FavoritiIds = favoritiIds;
        }
    }
}
