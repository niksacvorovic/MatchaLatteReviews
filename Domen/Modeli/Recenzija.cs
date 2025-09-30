using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Recenzija
    {
        private int recenzijaId;
        private int ocena;
        private string komentar;
        private bool javna;
        private bool prihvacena;
        private int autorId;
        private int clanakId;
        private RegistrovaniKorisnik autor;
        private Clanak clanak;
        public int RecenzijaId { get => recenzijaId; set => recenzijaId = value; }
        public int Ocena { get => ocena; set => ocena = value; }
        public string Komentar { get => komentar; set => komentar = value; }
        public bool Javna { get => javna; set => javna = value; }
        public bool Prihvacena { get => prihvacena; set => prihvacena = value; }
        public int AutorId { get => autorId; set => autorId = value; }
        public int ClanakId { get => clanakId; set => clanakId = value; }

        [JsonIgnore]
        public RegistrovaniKorisnik Autor { get => autor; set => autor = value; }
        [JsonIgnore]
        public Clanak Clanak { get => clanak; set => clanak = value; }

        [JsonConstructor]
        public Recenzija(int recenzijaId, int ocena, string komentar, bool javna, bool prihvacena, int autorId, int clanakId)
        {
            RecenzijaId = recenzijaId;
            Ocena = ocena;
            Komentar = komentar;
            Javna = javna;
            Prihvacena = prihvacena;
            AutorId = autorId;
            ClanakId = clanakId;
        }
    }
}
