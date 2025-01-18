using MusicVault.Backend.BuildingBlocks.Storage;
using System.Collections.Generic;

namespace MusicVault.Backend.Model;

public class Plejlista : IDAble {
    public string Naziv { get; set; }
    public virtual ICollection<MuzickiSadrzaj.MuzickiSadrzaj> MuzickiSadrzaji { get; set; }
    public virtual ICollection<Zanr> Zanrovi { get; set; }

    public Plejlista() { }

    public Plejlista(string naziv) {
        Naziv = naziv;
        MuzickiSadrzaji = new List<MuzickiSadrzaj.MuzickiSadrzaj>();
        Zanrovi = new List<Zanr>();
    }

    public void DodajZanr(Zanr zanr) {
        Zanrovi.Add(zanr);
    }

    public void DodajMuzickiSadrzaj(MuzickiSadrzaj.MuzickiSadrzaj muzickiSadrzaj) {
        MuzickiSadrzaji.Add(muzickiSadrzaj);
    }
}