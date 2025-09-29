using System.Collections.Generic;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Urednik : Korisnik
    {
        private List<Clanak> clanci;
        private List<Clanak> taskLista;

        public List<Clanak> Clanci { get => clanci; set => clanci = value; }
        public List<Clanak> TaskLista { get => taskLista; set => taskLista = value; }
    }
}
