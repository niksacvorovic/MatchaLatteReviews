using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Glas
    {
        private int muzickoDeloId;
        private int korisnikId;
        private MuzickoDelo muzickoDelo;
        private RegistrovaniKorisnik korisnik;


        public int MuzickoDeloId { get => muzickoDeloId; set => muzickoDeloId = value; }
        public int KorisnikId { get => korisnikId; set => korisnikId = value; }

        [JsonIgnore]
        public MuzickoDelo MuzickoDelo { get => muzickoDelo; set => muzickoDelo = value; }
        [JsonIgnore]
        public RegistrovaniKorisnik Korisnik { get => korisnik; set => korisnik = value; }

        [JsonConstructor]
        public Glas(int muzickoDeloId, int korisnikId)
        {
            MuzickoDeloId = muzickoDeloId;
            KorisnikId = korisnikId;
        }
    }
}
