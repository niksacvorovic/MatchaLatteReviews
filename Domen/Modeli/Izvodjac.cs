using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Izvodjac : Clanak
    {
        private int debi;
        private List<int> drzaveIds;
        private List<Drzava> drzave;
        public int Debi { get => debi; set => debi = value; }
        public List<int> DrzaveIds { get => drzaveIds; set => drzaveIds = value; }

        [JsonIgnore]
        public List<Drzava> Drzave { get => drzave; set => drzave = value; }

        [JsonConstructor]
        public Izvodjac(string naslov, int id, int ocena, string sadrzaj, DateTime datum, Status odobren, int pregledi, int autorId, int recenzijeIds, 
            int zanroviIds, int debi, List<int> drzaveIds) : base(naslov, id, ocena, sadrzaj, datum, odobren, pregledi, autorId, recenzijeIds, zanroviIds)
        {
            Debi = debi;
            DrzaveIds = drzaveIds;
        }
    }
}
