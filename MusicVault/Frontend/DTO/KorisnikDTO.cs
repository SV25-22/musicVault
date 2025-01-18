using MusicVault.Backend.Model.Enums;
using System.Text.RegularExpressions;
using MusicVault.Backend.Model;
using System.ComponentModel;
using System;

namespace MusicVault.Frontend.DTO;

public class KorisnikDTO : INotifyPropertyChanged, IDataErrorInfo {
    public int Id { get; set; }
    private string ime = "";
    public string Ime { get { return ime; } set { ime = value; OnPropertyChanged($"{nameof(Ime)}"); } }
    private string prezime = "";
    public string Prezime { get { return prezime; } set { prezime = value; OnPropertyChanged($"{nameof(Prezime)}"); } }
    public TipKorisnika Tip { get; set; }
    private string mejl = "";
    public string Mejl { get { return mejl; } set { mejl = value; OnPropertyChanged($"{nameof(Mejl)}"); } }
    private string telefon = "";
    public string Telefon { get { return telefon; } set { telefon = value; OnPropertyChanged($"{nameof(Telefon)}"); } }
    private DateTime godRodjenja;
    public DateTime GodRodjenja { get { return godRodjenja; } set { godRodjenja = value; OnPropertyChanged($"{nameof(GodRodjenja)}"); } }
    private Pol pol;
    public Pol Pol { get { return pol; } set { pol = value; OnPropertyChanged($"{nameof(Pol)}"); } }
    private string lozinka = "";
    public string Lozinka { get { return lozinka; } set { lozinka = value; OnPropertyChanged($"{nameof(Lozinka)}"); } }
    public bool Javni { get; set; }
    public bool Banovan { get; set; }

    public string ImePrezime { get { return Ime + " " + Prezime; } }

    public string Error => "";
    public event PropertyChangedEventHandler? PropertyChanged;

    public KorisnikDTO() {
        Tip = TipKorisnika.Neregistrovani;
        GodRodjenja = DateTime.Now.AddYears(-20);
        Banovan = false;
        Javni = false;
    }

    public KorisnikDTO(Korisnik korisnik) {
        Id = korisnik.Id;
        Ime = korisnik.Ime;
        Prezime = korisnik.Prezime;
        Tip = korisnik.Tip;
        Mejl = korisnik.Mejl;
        Telefon = korisnik.Telefon;
        GodRodjenja = korisnik.GodRodjenja.ToDateTime(TimeOnly.MinValue);
        Pol = korisnik.Pol;
        Javni = korisnik.Javni;
        Banovan = korisnik.Banovan;
    }

    public Korisnik ToKorisnik() {
        return new Korisnik(
            Id, Ime, Prezime, Tip,
            Mejl, Telefon, new DateOnly(GodRodjenja.Year, GodRodjenja.Month, GodRodjenja.Day),
            Pol, lozinka, Javni, Banovan);
    }

    private readonly string[] _validatedProperties = {
        $"{nameof(Ime)}",
        $"{nameof(Prezime)}",
        $"{nameof(GodRodjenja)}",
        $"{nameof(Telefon)}",
        $"{nameof(Mejl)}",
        $"{nameof(Lozinka)}",
    };

    public string? ValidationProblem {
        get {
            foreach (var property in _validatedProperties)
                if (!string.IsNullOrEmpty(this[property]))
                    return this[property];

            return null;
        }
    }

    private readonly Regex _TelefonRegex = new("^\\d{10}$");
    private readonly Regex _LozinkaRegex = new("[\\s\\S]{8,}");

    public string this[string columnName] {
        get {
            switch (columnName) {
                case $"{nameof(Ime)}": {
                    if (string.IsNullOrEmpty(Ime))
                        return "Ime je potrebno.";
                    break;
                }
                case $"{nameof(Prezime)}": {
                    if (string.IsNullOrEmpty(Prezime))
                        return "Prezime je potrebno.";
                    break;
                }
                case $"{nameof(GodRodjenja)}": {
                    if (GodRodjenja == DateTime.MinValue)
                        return "Datum rodjenja je potreban.";
                    break;
                }
                case $"{nameof(Telefon)}": {
                    if (string.IsNullOrEmpty(Telefon))
                        return "Telefon je potreban.";
                    Match TelefonMatch = _TelefonRegex.Match(Telefon);
                    if (!TelefonMatch.Success)
                        return "Telefon mora imati bar 10 cifara.";
                    break;
                }
                case $"{nameof(Mejl)}": {
                    if (string.IsNullOrEmpty(Mejl))
                        return "Mejl je potreban.";
                    break;
                }
                case $"{nameof(Lozinka)}": {
                    if (string.IsNullOrEmpty(Lozinka))
                        return "Lozinka je potrebna.";
                    Match LozinkaMatch = _LozinkaRegex.Match(Lozinka);
                    if (!LozinkaMatch.Success)
                        return "Lozinka mora imati bar 8 karaktera.";
                    break;
                }
            }

            return "";
        }
    }

    protected virtual void OnPropertyChanged(string name) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
