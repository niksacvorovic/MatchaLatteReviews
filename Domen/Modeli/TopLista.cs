using MatchaLatteReviews.Domen.Enumeracije;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class TopLista
    {
        private int topListaId;
        private string naziv;
        private Period period;
        private int anketaId;
        private Anketa anketa;
        public int TopListaId { get => topListaId; set => topListaId = value; }
        public string Naziv { get => naziv; set => naziv = value; }
        public Period Period { get => period; set => period = value; }
        public int AnketaId { get => anketaId; set => anketaId = value; }

        [JsonIgnore]
        public Anketa Anketa { get => anketa; set => anketa = value; }

        [JsonConstructor]
        public TopLista(int topListaId, string naziv, Period period, int anketaId)
        {
            TopListaId = topListaId;
            Naziv = naziv;
            Period = period;
            AnketaId = anketaId;
        }
    }
}
