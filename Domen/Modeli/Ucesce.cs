using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Ucesce
    {
        private int izvodjacId;
        private int muzickoDeloId;
        private string uloga;
        private Izvodjac izvodjac;
        private MuzickoDelo muzickoDelo;


        public int IzvodjacId { get => izvodjacId; set => izvodjacId = value; }
        public int MuzickoDeloId { get => muzickoDeloId; set => muzickoDeloId = value; }
        public string Uloga { get => uloga; set => uloga = value; }

        [JsonIgnore]
        public Izvodjac Izvodjac { get => izvodjac; set => izvodjac = value; }
        [JsonIgnore]
        public MuzickoDelo MuzickoDelo { get => muzickoDelo; set => muzickoDelo = value; }

        [JsonConstructor]
        public Ucesce(int izvodjacId, int muzickoDeloId, string uloga)
        {
            IzvodjacId = izvodjacId;
            MuzickoDeloId = muzickoDeloId;
            Uloga = uloga;
        }
    }
}
