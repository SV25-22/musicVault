using MusicVault.Backend.BuildingBlocks.Storage;
using System;

namespace MusicVault.Backend.Model;

public class Pregled : IDAble {
    public virtual Korisnik Korisnik { get; set; }
    public virtual MuzickiSadrzaj.MuzickiSadrzaj MuzickiSadrzaj { get; set; }
    public DateOnly Datum { get; set; }

    public Pregled() { }

    public Pregled(Korisnik korisnik, MuzickiSadrzaj.MuzickiSadrzaj muzickiSadrzaj, DateOnly datum) {
        Korisnik = korisnik;
        MuzickiSadrzaj = muzickiSadrzaj;
        Datum = datum;
    }
}