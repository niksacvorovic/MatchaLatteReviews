using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Izdanje
    {
        private string nazivIzdanja;
        private Format format;
        private DateTime datumIzdavanja;
        public string NazivIzdanja { get => nazivIzdanja; set => nazivIzdanja = value; }
        public Format Format { get => format; set => format = value; }
        public DateTime DatumIzdavanja { get => datumIzdavanja; set => datumIzdavanja = value; }

        [JsonConstructor]
        public Izdanje(string nazivIzdanja, Format format, DateTime datumIzdavanja)
        {
            NazivIzdanja = nazivIzdanja;
            Format = format;
            DatumIzdavanja = datumIzdavanja;
        }
    }
}
