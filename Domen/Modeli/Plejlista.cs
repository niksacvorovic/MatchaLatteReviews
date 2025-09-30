using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Plejlista
    {
        private int plejlistaId;
        private String naziv;
        private bool javna;
        private List<int> sadrzajIds;
        private int autorId;
        private List<MuzickoDelo> sadrzaj;
        private RegistrovaniKorisnik autor;
        public int Id { get => plejlistaId; set => plejlistaId = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public bool Javna { get => javna; set => javna = value; }
        public List<int> SadrzajIds { get => sadrzajIds; set => sadrzajIds = value; }
        public int AutorId { get => autorId; set => autorId = value; }

        [JsonIgnore]
        public List<MuzickoDelo> Sadrzaj { get => sadrzaj; set => sadrzaj = value; }
        [JsonIgnore]
        public RegistrovaniKorisnik Autor { get => autor; set => autor = value; }

        [JsonConstructor]
        public Plejlista(int id, string naziv, bool javna, List<int> sadrzajIds, int autorId)
        {
            Id = id;
            Naziv = naziv;
            Javna = javna;
            SadrzajIds = sadrzajIds;
            AutorId = autorId;
        }
    }
}
