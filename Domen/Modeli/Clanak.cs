using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Clanak
    {
        private string naslov;
        private int id;
        private int ocena;
        private string sadrzaj;
        private DateTime datum;
        private Status odobren;
        private int pregledi;
        private Urednik autor;
        private List<Recenzija> recenzije;
        private List<Zanr> zanrovi;

        public string Naslov { get => naslov; set => naslov = value; }
        public int Ocena { get => ocena; set => ocena = value; }
        public string Sadrzaj { get => sadrzaj; set => sadrzaj = value; }
        public DateTime Datum { get => datum; set => datum = value; }
        public Status Odobren { get => odobren; set => odobren = value; }
        public int Pregledi { get => pregledi; set => pregledi = value; }
        public Urednik Autor { get => autor; set => autor = value; }
        public List<Recenzija> Recenzije { get => recenzije; set => recenzije = value; }
        public List<Zanr> Zanrovi { get => zanrovi; set => zanrovi = value; }
        public int Id { get => id; set => id = value; }
    }
}
