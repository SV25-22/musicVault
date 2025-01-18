using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MusicVault.Backend.BuildingBlocks.Storage;
using MusicVault.Backend.Model.Enums;
using System;

namespace MusicVault.Backend.Model;

public class Korisnik : IDAble {
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public TipKorisnika Tip { get; set; }
    public string Mejl { get; set; }
    public string Telefon { get; set; }
    public DateOnly GodRodjenja { get; set; }
    public Pol Pol { get; set; }
    public string Lozinka { get; private set; }
    public bool Javni { get; set; }
    public bool Banovan { get; set; }

    // todo iskoristiti ovo
    public static string SifrujLozinku(string lozinka, int id) {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(lozinka, BitConverter.GetBytes(id), KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));
    }

    public bool ProveriLozinku(string lozinka) {
        return SifrujLozinku(lozinka, Id) == Lozinka;
    }

    public void PromeniLozinku(string lozinka) {
        Lozinka = SifrujLozinku(lozinka, Id);
    }

    public Korisnik() {
        Ime = "";
        Prezime = "";
        Tip = TipKorisnika.Neregistrovani;
        Mejl = "";
        Telefon = "";
        GodRodjenja = new DateOnly();
        Pol = Pol.Musko;
        Lozinka = "";
        Javni = false;
        Banovan = false;
    }

    public Korisnik(int id, string ime, string prezime, TipKorisnika tip, string mejl, string telefon, DateOnly godRodjenja, Pol pol, string lozinka, bool javni, bool banovan) {
        Id = id;
        Ime = ime;
        Prezime = prezime;
        Tip = tip;
        Mejl = mejl;
        Telefon = telefon;
        GodRodjenja = godRodjenja;
        Pol = pol;
        Lozinka = SifrujLozinku(lozinka, id);
        Javni = javni;
        Banovan = banovan;
    }
}