using MusicVault.Backend.BuildingBlocks.Storage;
using MusicVault.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicVault.Backend.Model;

public class Glasanje : IDAble {
    public DateOnly PocetakGlasanja { get; set; }
    public DateOnly KrajGlasanja { get; set; }
    public bool Aktivno { get; set; }
    public string Naziv { get; set; }
    public virtual ICollection<Glas> Glasovi { get; set; }
    public virtual ICollection<MuzickiSadrzaj.MuzickiSadrzaj> OpcijeZaGlasanje { get; set; }

    public Glasanje() { }

    public Glasanje(DateOnly pocetakGlasanja, DateOnly krajGlasanja, bool aktivno, string naziv) {
        PocetakGlasanja = pocetakGlasanja;
        KrajGlasanja = krajGlasanja;
        Aktivno = aktivno;
        Naziv = naziv;
        Glasovi = new List<Glas>();
        OpcijeZaGlasanje = new List<MuzickiSadrzaj.MuzickiSadrzaj>();
    }

    public void DodajGlas(Glas glas) {
        Glasovi.Add(glas);
    }

    public void DodajOpcijuZaGlasanje(MuzickiSadrzaj.MuzickiSadrzaj muzickiSadrzaj) {
        OpcijeZaGlasanje.Add(muzickiSadrzaj);
    }

    public List<MuzickiSadrzaj.MuzickiSadrzaj> ObradaRezultata() {
        Dictionary<MuzickiSadrzaj.MuzickiSadrzaj, int> histogramGlasanja = new Dictionary<MuzickiSadrzaj.MuzickiSadrzaj, int>();

        foreach (Glas g in Glasovi) {
            if (!histogramGlasanja.ContainsKey(g.MuzickiSadrzaj)) {
                histogramGlasanja.Add(g.MuzickiSadrzaj, 0);
            }

            if (g.Korisnik.Tip == TipKorisnika.Urednik) {
                histogramGlasanja[g.MuzickiSadrzaj] += Glas.VrednostGlasaUrednika;
            } else if (g.Korisnik.Tip == TipKorisnika.Registrovani) {
                histogramGlasanja[g.MuzickiSadrzaj] += Glas.VrednostGlasaKorisnika;
            }
        }

        return histogramGlasanja.OrderBy(m => m.Value).Select(m => m.Key).ToList();
    }
}