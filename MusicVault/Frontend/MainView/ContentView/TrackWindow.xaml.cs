using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Model.Recenzija;
using MusicVault.Backend.Controllers;
using MusicVault.Backend.Model.Enums;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using MusicVault.Frontend.DTO;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.MainView.ContentView;

public partial class TrackWindow : Window, IObserver {
    public ObservableCollection<RecenzijaDTO> Recenzije { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Izvodjaci { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Nastupi { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Albumi { get; set; } = new();
    private MuzickiSadrzajController muzickiSadrzajController;
    private RecenzijaController recenzijaController;
    private Korisnik korisnik;
    private Delo delo;

    public TrackWindow(Korisnik korisnik, RecenzijaController recenzijaController, Delo delo, MuzickiSadrzajController muzickiSadrzajController) {
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.recenzijaController = recenzijaController;
        this.korisnik = korisnik;
        this.delo = delo;

        DataContext = this;
        recenzijaController.Subscribe(this);
        InitializeComponent();
        Update();
    }

    public void Update() {
        Recenzije.Clear();
        Izvodjaci.Clear();
        Nastupi.Clear();
        Albumi.Clear();

        DeloLabel.Content = delo.Opis;
        delo.MuzickiSadrzaji.ToList().ForEach(sadrzaj => Albumi.Add(new SadrzajDTO(sadrzaj)));
        delo.Izvodjaci.ToList().ForEach(izvodjac => Izvodjaci.Add(new SadrzajDTO(izvodjac)));
        muzickiSadrzajController.GetNastupi().Where(nastup => nastup.MuzickiSadrzaji.Any(sadrzaj => sadrzaj.Id == delo.Id))
                                             .ToList().ForEach(nastup => Nastupi.Add(new SadrzajDTO(nastup)));

        Recenzija recenzijaUrednika = recenzijaController.GetRecenzijaUrednika(delo);
        UrednikLabel.Content = recenzijaUrednika.Urednik?.Ime + " " + recenzijaUrednika.Urednik?.Prezime;
        RecenzijaUrednikaTxtBox.Text = string.IsNullOrEmpty(recenzijaUrednika.Opis) ? "Čeka se recenzija urednika..." : recenzijaUrednika.Opis;
        OcenaUrednikaLabel.Content = recenzijaUrednika.Ocena == -1 ? "?" : recenzijaUrednika.Ocena;

        List<Recenzija> recenzije = recenzijaController.GetRecenzijaZa(delo);
        recenzije.Where(recenzija => (recenzija.Urednik?.Javni ?? false) && recenzija.Urednik.Tip != TipKorisnika.Urednik).ToList().ForEach(recenzija => Recenzije.Add(new RecenzijaDTO(recenzija)));

        bool vecPostoji = recenzije.Any(recenzija => recenzija.Urednik?.Id == korisnik.Id) || korisnik.Tip == TipKorisnika.Urednik || korisnik.Tip == TipKorisnika.Neregistrovani;
        RecenzijaKorisnikaTxtBox.IsEnabled = !vecPostoji;
        AddRecenzijaBtn.IsEnabled = !vecPostoji;
        OcnSlider.IsEnabled = !vecPostoji;
    }

    private void AddRecenzijaBtn_Click(object sender, RoutedEventArgs e) {
        recenzijaController.DodajRecenziju(new Recenzija(korisnik, delo, (int)OcnSlider.Value, RecenzijaKorisnikaTxtBox.Text, true));
        RecenzijaKorisnikaTxtBox.Text = "";
        OcnSlider.Value = 5;
    }
}
