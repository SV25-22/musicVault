using MusicVault.Backend.BuildingBlocks.Storage;
using System.Collections.Generic;

namespace MusicVault.Backend.Model.MuzickiSadrzaj;

public abstract class MuzickiSadrzaj : IDAble {
    public string Opis { get; set; }
    public bool Objavljeno { get; set; }
    public virtual ICollection<MultimedijalniSadrzaj.MultimedijalniSadrzaj> MultimedijalniSadrzaji { get; set; }
    public virtual ICollection<MuzickiSadrzaj> MuzickiSadrzaji { get; set; } = new List<MuzickiSadrzaj>();
    public virtual ICollection<Zanr> Zanrevi { get; set; } = new List<Zanr>();
    public virtual ICollection<Izvodjac> Izvodjaci { get; set; } = new List<Izvodjac>();

    public MuzickiSadrzaj() { }

    public MuzickiSadrzaj(string opis) {
        Opis = opis;
        MultimedijalniSadrzaji = new List<MultimedijalniSadrzaj.MultimedijalniSadrzaj>();
    }

    public void DodajMultimedijalniSadrzaj(MultimedijalniSadrzaj.MultimedijalniSadrzaj multimedijalniSadrzaj) {
        MultimedijalniSadrzaji.Add(multimedijalniSadrzaj);
    }

    public void DodajMuzickiSadrzaj(MuzickiSadrzaj muzickiSadrzaj) {
        MuzickiSadrzaji.Add(muzickiSadrzaj);
    }

    public void DodajZanr(Zanr zanr) {
        Zanrevi.Add(zanr);
    }

    public void DodajIzvodjaca(Izvodjac izvodjac) {
        Izvodjaci.Add(izvodjac);
    }
}