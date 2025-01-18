using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Model.Recenzija;
using MusicVault.Frontend.CommonControls;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Backend.Model.Enums;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using MusicVault.Frontend.DTO;
using System.Windows;
using System.Linq;
using System;

namespace MusicVault.Frontend.AdminView.ContentView.AddViews;

public partial class AddAlbumWindow : Window {
    public static IEnumerable<NacinCuvanja> NaciniCuvanja => Enum.GetValues(typeof(NacinCuvanja)).Cast<NacinCuvanja>();
    public ObservableCollection<MultiSelectItem> Izvodjaci { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Zanrovi { get; set; } = new();
    public ObservableCollection<KorisnikDTO> Urednici { get; set; } = new();

    private readonly MuzickiSadrzajController muzickiSadrzajController;
    private readonly RecenzijaController recenzijaController;

    public AddAlbumWindow(KorisnikController korisnikController, ZanrController zanrController, IzvodjacController izvodjacController, MuzickiSadrzajController muzickiSadrzajController, RecenzijaController recenzijaController) {
        izvodjacController.GetAll().ForEach(izvodjac => Izvodjaci.Add(new() { Key = izvodjac.Opis, Value = izvodjac, IsSelected = false }));
        zanrController.GetAll().ForEach(zanr => Zanrovi.Add(new() { Key = zanr.Naziv, Value = zanr, IsSelected = false }));
        korisnikController.GetUrednici().ForEach(urednik => Urednici.Add(new KorisnikDTO(urednik)));
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.recenzijaController = recenzijaController;
        DataContext = this;

        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string opis = OpisTxtBox.Text.Trim();
        List<Izvodjac?> izvodjaci = Izvodjaci.Where(izvodjac => izvodjac.IsSelected).Select(izvodjac => (Izvodjac?)izvodjac.Value).ToList();
        List<Zanr?> zanrovi = Zanrovi.Where(zanr => zanr.IsSelected).Select(zanr => (Zanr?)zanr.Value).ToList();

        if (string.IsNullOrEmpty(opis)) {
            MessageBox.Show("Opis ne može biti prazan!", "Greška dodavanja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Album album = new((NacinCuvanja)NacinComboBox.SelectedValue) { Opis = opis };
        zanrovi.ForEach(zanr => { if (zanr != null) album.DodajZanr(zanr); });
        izvodjaci.ForEach(izvodjac => { if (izvodjac != null) album.DodajIzvodjaca(izvodjac); });
        muzickiSadrzajController.DodajMuzickiSadrzaj(album);

        // dodavanje prazne recenzije
        recenzijaController.DodajRecenziju(new Recenzija(((KorisnikDTO)UrednikComboBox.SelectedValue).ToKorisnik(), album, -1, "", false));

        MessageBox.Show("Album uspešno dodat.", "Dodavanje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
