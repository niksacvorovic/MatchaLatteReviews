using MatchaLatteReviews.Domen.Enumeracije;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Urednik : Korisnik
    {
        private List<int> clanciIds;
        private List<int> taskListaIds;
        private List<Clanak> clanci;
        private List<Clanak> taskLista;

        public List<int> ClanciIds { get => clanciIds; set => clanciIds = value; }
        public List<int> TaskListaId { get => taskListaIds; set => taskListaIds = value; }

        [JsonIgnore]
        public List<Clanak> Clanci { get => clanci; set => clanci = value; }
        [JsonIgnore]
        public List<Clanak> TaskLista { get => taskLista; set => taskLista = value; }

        [JsonConstructor]
        public Urednik(int korisnikId, string korisnickoIme, string lozinka, string ime, string prezime, bool javni, Uloga uloga, List<int> clanciIds, List<int> taskListaId) : base(korisnikId, korisnickoIme, lozinka, ime, prezime, javni, uloga)
        {
            ClanciIds = clanciIds;
            TaskListaId = taskListaId;
        }

    }
}
