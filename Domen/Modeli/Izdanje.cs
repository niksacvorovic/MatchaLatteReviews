using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
