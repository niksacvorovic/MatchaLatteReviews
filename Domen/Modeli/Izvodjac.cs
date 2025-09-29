using System;
using System.Collections.Generic;

namespace MatchaLatteReviews.Domen.Modeli
{
    public class Izvodjac : Clanak
    {
        int debi;
        List<Drzava> drzave;

        public int Debi { get => debi; set => debi = value; }
        public List<Drzava> Drzave { get => drzave; set => drzave = value; }
    }
}
