using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class MuzickoDelo : Clanak
    {
        private Tip tipDela;
        private string naziv;
        private int trajanje;
        private List<Izdanje> izdanja;

        public Tip TipDela { get => tipDela; set => tipDela = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public int Trajanje { get => trajanje; set => trajanje = value; }
        public List<Izdanje> Izdanja { get => izdanja; set => izdanja = value; }

        [JsonConstructor]
        public MuzickoDelo(string naslov, int id, int ocena, string sadrzaj, DateTime datum, Status odobren, int pregledi, int autorId, int recenzijeIds,
            int zanroviIds, Tip tipDela, string naziv, int trajanje, List<Izdanje> izdanja) : base(naslov, id, ocena, sadrzaj, datum, odobren, pregledi, autorId, recenzijeIds, zanroviIds)
        {
            TipDela = tipDela;
            Naziv = naziv;
            Trajanje = trajanje;
            Izdanja = izdanja;
        }
    }
}
