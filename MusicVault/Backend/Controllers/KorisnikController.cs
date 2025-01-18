using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.Repositories;
using MusicVault.Backend.Model.Enums;
using System.Collections.Generic;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Controllers;
public class KorisnikController : GenericController<Korisnik, KorisnikRepository> {
    public KorisnikController() { }

    // GUI Poziva ovu metodu samo sa validnim podacima
    public Korisnik? RegistrujKorisnika(Korisnik korisnik, string lozinka) {
        if (KorisnikRepository.MailPostoji(korisnik.Mejl)) {
            return null;
        }

        korisnik.Tip = TipKorisnika.Registrovani;
        repository.Add(korisnik);
        korisnik.PromeniLozinku(lozinka);
        repository.Update(korisnik);

        return korisnik;
    }

    // GUI Poziva ovu metodu samo sa validnim podacima
    public Korisnik? RegistrujUrednika(Korisnik korisnik, string lozinka) {
        if (KorisnikRepository.MailPostoji(korisnik.Mejl)) {
            return null;
        }

        korisnik.Tip = TipKorisnika.Urednik;
        repository.Add(korisnik);
        korisnik.PromeniLozinku(lozinka);
        repository.Update(korisnik);

        return korisnik;
    }

    public Korisnik? UlogujSe(string mejl, string lozinka) {
        var korisnik = KorisnikRepository.KorisnikNaOsnovuKredencijala(mejl, lozinka);
        if (korisnik == null && KorisnikRepository.MailPostoji(mejl)) {
            // todo pošalji mejl o neuspešnom pokušaju prijave na nalog
        }
        return korisnik;
    }

    public bool AzurirajKorisnika(Korisnik korisnik) {
        if (KorisnikRepository.MailPostoji(korisnik.Mejl, korisnik))
            return false;

        repository.Update(korisnik);
        return true;
    }

    public void BanujKorisnika(Korisnik korisnik) {
        // mora ovako jer prosleđeni korisnik nema šifru, niti može da je ima bez da se menja gomilu stvari zbog hešovanja
        var tmpKorisnik = repository.Get(korisnik.Id) ?? korisnik;
        tmpKorisnik.Banovan = true;
        repository.Update(tmpKorisnik);
    }

    public List<Korisnik> GetUrednici() {
        return KorisnikRepository.GetUrednici();
    }
}