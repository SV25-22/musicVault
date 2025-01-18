using MusicVault.Backend.BuildingBlocks.Storage;
using System;

namespace MusicVault.Backend.Model;

public class Glas : IDAble {
    public static int VrednostGlasaUrednika = 3;
    public static int VrednostGlasaKorisnika = 1;
    public virtual Korisnik Korisnik { get; set; }
    public virtual MuzickiSadrzaj.MuzickiSadrzaj MuzickiSadrzaj { get; set; }
    public DateOnly Datum { get; }
    public int Ocena { get; set; }

    public Glas() { }

    public Glas(Korisnik korisnik, MuzickiSadrzaj.MuzickiSadrzaj muzickiSadrzaj, DateOnly datum, int ocena) {
        Korisnik = korisnik;
        MuzickiSadrzaj = muzickiSadrzaj;
        Datum = datum;
        Ocena = ocena;
    }
}