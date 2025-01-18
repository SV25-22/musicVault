using MusicVault.Backend.BuildingBlocks.Storage;
using System.Collections.Generic;

namespace MusicVault.Backend.Model;

public class Izvodjac : IDAble {
    public string Opis { get; set; }
    public virtual ICollection<Zanr> Zanrevi { get; set; }
    public virtual ICollection<MultimedijalniSadrzaj.MultimedijalniSadrzaj> MultimedijalniSadrzaji { get; set; }

    public Izvodjac() { }

    public Izvodjac(string opis) {
        Opis = opis;
        Zanrevi = new List<Zanr>();
        MultimedijalniSadrzaji = new List<MultimedijalniSadrzaj.MultimedijalniSadrzaj>();
    }

    public void DodajZanr(Zanr zanr) {
        Zanrevi.Add(zanr);
    }

    public void DodajMultimedijalniSadrzaj(MultimedijalniSadrzaj.MultimedijalniSadrzaj multimedijalniSadrzaj) {
        MultimedijalniSadrzaji.Add(multimedijalniSadrzaj);
    }
}