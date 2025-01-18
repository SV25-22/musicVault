using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.BuildingBlocks.Storage;

namespace MusicVault.Backend.Model.Recenzija;

public class Recenzija : IDAble {
    public virtual Korisnik? Urednik { get; set; }
    public virtual MuzickiSadrzaj.MuzickiSadrzaj MuzickiSadrzaj { get; set; }
    public int Ocena { get; set; }
    public string Opis { get; set; }
    public bool Objavljena { get; set; }
    public StanjeRecenzije StanjeRecenzije { get; set; }
    private Stanje _stanje;
    public Stanje Stanje { 
        get {
            return _stanje;
        }
        set {
            _stanje = value;
            switch (_stanje) {
                case Stanje.NaIzradi: {
                    StanjeRecenzije = new NaIzradi(this);
                    break;
                }
                case Stanje.NaOveri: {
                    StanjeRecenzije = new NaOveri(this);
                    break;
                }
                case Stanje.Objavljeno: {
                    StanjeRecenzije = new Objavljeno(this);
                    break;
                }
                case Stanje.Rezervisano: {
                    StanjeRecenzije = new Rezervisano(this);
                    break;
                }
                case Stanje.Arhivirano: {
                    StanjeRecenzije = new Arhivirano(this);
                    break;
                }
            }
        }
    }

    public Recenzija() { }

    public Recenzija(Korisnik? urednik, MuzickiSadrzaj.MuzickiSadrzaj muzickiSadrzaj, int ocena, string opis, bool objavljena) {
        MuzickiSadrzaj = muzickiSadrzaj;
        Urednik = urednik;
        Ocena = ocena;
        Opis = opis;
        Objavljena = objavljena;
        StanjeRecenzije = new NaIzradi(this);
        Stanje = Stanje.NaIzradi;
    }

    public void SlanjeMaila(string mail, string poruka) {
        // todo salje se mail
    }

    // todo proveriti da li radi
    public void Objavi() {
        Objavljena = true;
        MuzickiSadrzaj.Objavljeno = true;
        InternalUpdate();
    }

    public void AzurirajUrednika(Korisnik novUrednik) {
        Urednik = novUrednik;
        InternalUpdate();
    }

    public void Preuzimanje() { StanjeRecenzije.Preuzimanje(); }
    public void Oduzimanje(Korisnik novUrendik) { StanjeRecenzije.Oduzimanje(novUrendik); }
    public void Prihvatanje() { StanjeRecenzije.Prihvatanje(); }
    public void Odbijanje() { StanjeRecenzije.Odbijanje(); }
    public void SlanjeNaOveru() { StanjeRecenzije.SlanjeNaOveru(); }
    public void Arhiviranje() { StanjeRecenzije.Arhiviranje(); }
    public void Vracanje() { StanjeRecenzije.Vracanje(); }
    public void ObjavaSadrzaja() { StanjeRecenzije.ObjavaSadrzaja(); }

    public void PromeniStanje(Stanje stanje) {
        switch (stanje) {
            case Stanje.NaIzradi: {
                StanjeRecenzije = new NaIzradi(this);
                break;
            }
            case Stanje.NaOveri: {
                StanjeRecenzije = new NaOveri(this);
                break;
            }
            case Stanje.Objavljeno: {
                StanjeRecenzije = new Objavljeno(this);
                break;
            }
            case Stanje.Rezervisano: {
                StanjeRecenzije = new Rezervisano(this);
                break;
            }
            case Stanje.Arhivirano: {
                StanjeRecenzije = new Arhivirano(this);
                break;
            }
        }

        Stanje = stanje;
        
        InternalUpdate();
    }

    private void InternalUpdate() {
        using (var context = new SqlDbContext()) {
            context.Set<Recenzija>().Entry(this).State = EntityState.Detached;
            context.Set<Recenzija>().Update(this);

            context.SaveChanges();
        }
    }
}

public enum Stanje {
    Rezervisano,
    NaIzradi,
    Arhivirano,
    Objavljeno,
    NaOveri
}