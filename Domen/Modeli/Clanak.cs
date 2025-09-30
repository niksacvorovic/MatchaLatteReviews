using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    [JsonDerivedType(typeof(MuzickoDelo), 0)]
    [JsonDerivedType(typeof(Izvodjac), 1)]
    public class Clanak
    {
        private string naslov;
        private int id;
        private int ocena;
        private string sadrzaj;
        private DateTime datum;
        private Status odobren;
        private int pregledi;
        private int autorId;
        private List<int> recenzijeIds;
        private List<int> zanroviIds;
        private Urednik autor;
        private List<Recenzija> recenzije;
        private List<Zanr> zanrovi;


        public string Naslov { get => naslov; set => naslov = value; }
        public int Id { get => id; set => id = value; }
        public int Ocena { get => ocena; set => ocena = value; }
        public string Sadrzaj { get => sadrzaj; set => sadrzaj = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public Status Odobren { get => odobren; set => odobren = value; }
        public int Pregledi { get => pregledi; set => pregledi = value; }
        public int AutorId { get => autorId; set => autorId = value; }
        public List<int> RecenzijeIds { get => recenzijeIds; set => recenzijeIds = value; }
        public List<int> ZanroviIds { get => zanroviIds; set => zanroviIds = value; }

        [JsonIgnore]
        public Urednik Autor { get => autor; set => autor = value; }
        [JsonIgnore]
        public List<Recenzija> Recenzije { get => recenzije; set => recenzije = value; }
        [JsonIgnore]
        public List<Zanr> Zanrovi { get => zanrovi; set => zanrovi = value; }

        [JsonConstructor]
        public Clanak(string naslov, int id, int ocena, string sadrzaj, DateTime datum, Status odobren, int pregledi, int autorId, List<int> recenzijeIds, List<int> zanroviIds)
        {
            Naslov = naslov;
            Id = id;
            Ocena = ocena;
            Sadrzaj = sadrzaj;
            Datum = datum;
            Odobren = odobren;
            Pregledi = pregledi;
            AutorId = autorId;
            RecenzijeIds = recenzijeIds;
            ZanroviIds = zanroviIds;
        }
    }
}
