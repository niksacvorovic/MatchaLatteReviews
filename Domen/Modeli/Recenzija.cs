using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private RegistrovaniKorisnik autor;
        private Clanak clanak;
        public int Ocena { get => ocena; set => ocena = value; }
        public string Komentar { get => komentar; set => komentar = value; }
        public bool Javna { get => javna; set => javna = value; }
        public bool Prihvacena { get => prihvacena; set => prihvacena = value; }
        public RegistrovaniKorisnik Autor { get => autor; set => autor = value; }
        public Clanak Clanak { get => clanak; set => clanak = value; }
        public int RecenzijaId { get => recenzijaId; set => recenzijaId = value; }
    }
}
