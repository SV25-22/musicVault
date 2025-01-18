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

public partial class NastupWindow : Window, IObserver {
    public ObservableCollection<RecenzijaDTO> Recenzije { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Izvodjaci { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Albumi { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Dela { get; set; } = new();
    private RecenzijaController recenzijaController;
    private Korisnik korisnik;
    private Nastup nastup;

    public NastupWindow(Korisnik korisnik, RecenzijaController recenzijaController, Nastup nastup) {
        this.recenzijaController = recenzijaController;
        this.korisnik = korisnik;
        this.nastup = nastup;

        DataContext = this;
        recenzijaController.Subscribe(this);
        InitializeComponent();
        Update();
    }

    public void Update() {
        Recenzije.Clear();
        Izvodjaci.Clear();
        Albumi.Clear();
        Dela.Clear();

        NastupLabel.Content = nastup.Opis;
        nastup.Izvodjaci.ToList().ForEach(izvodjac => Izvodjaci.Add(new SadrzajDTO(izvodjac)));
        nastup.MuzickiSadrzaji.ToList().ForEach(sadrzaj => {
            if (sadrzaj is Album)
                Albumi.Add(new SadrzajDTO(sadrzaj));
            else
                Dela.Add(new SadrzajDTO(sadrzaj));
        });

        Recenzija recenzijaUrednika = recenzijaController.GetRecenzijaUrednika(nastup);
        UrednikLabel.Content = recenzijaUrednika.Urednik?.Ime + " " + recenzijaUrednika.Urednik?.Prezime;
        RecenzijaUrednikaTxtBox.Text = string.IsNullOrEmpty(recenzijaUrednika.Opis) ? "Čeka se recenzija urednika..." : recenzijaUrednika.Opis;
        OcenaUrednikaLabel.Content = recenzijaUrednika.Ocena == -1 ? "?" : recenzijaUrednika.Ocena;

        List<Recenzija> recenzije = recenzijaController.GetRecenzijaZa(nastup);
        recenzije.Where(recenzija => (recenzija.Urednik?.Javni ?? false) && recenzija.Urednik.Tip != TipKorisnika.Urednik).ToList().ForEach(recenzija => Recenzije.Add(new RecenzijaDTO(recenzija)));

        bool vecPostoji = recenzije.Any(recenzija => recenzija.Urednik?.Id == korisnik.Id) || korisnik.Tip == TipKorisnika.Urednik || korisnik.Tip == TipKorisnika.Neregistrovani;
        RecenzijaKorisnikaTxtBox.IsEnabled = !vecPostoji;
        AddRecenzijaBtn.IsEnabled = !vecPostoji;
        OcnSlider.IsEnabled = !vecPostoji;
    }

    private void AddRecenzijaBtn_Click(object sender, RoutedEventArgs e) {
        recenzijaController.DodajRecenziju(new Recenzija(korisnik, nastup, (int)OcnSlider.Value, RecenzijaKorisnikaTxtBox.Text, true));
        RecenzijaKorisnikaTxtBox.Text = "";
        OcnSlider.Value = 5;
    }
}
