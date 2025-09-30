using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Zanr
    {
        private int id;
        private string naziv;
        public int Id { get => id; set => id = value; }
        public string Naziv { get => naziv; set => naziv = value; }

        [JsonConstructor]
        public Zanr(int id, string naziv)
        {
            Id = id;
            Naziv = naziv;
        }
    }
}
