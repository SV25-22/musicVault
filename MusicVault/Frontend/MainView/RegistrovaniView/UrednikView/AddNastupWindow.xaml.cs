using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Model.Recenzija;
using MusicVault.Frontend.CommonControls;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.MainView.RegistrovaniView.UrednikView;

public partial class AddNastupWindow : Window {
    public ObservableCollection<MultiSelectItem> Izvodjaci { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Zanrovi { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Albumi { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Dela { get; set; } = new();

    private readonly MuzickiSadrzajController muzickiSadrzajController;
    private readonly RecenzijaController recenzijaController;
    private readonly Korisnik urednik;

    public AddNastupWindow(Korisnik urednik, ZanrController zanrController, IzvodjacController izvodjacController, MuzickiSadrzajController muzickiSadrzajController, RecenzijaController recenzijaController) {
        izvodjacController.GetAll().ForEach(izvodjac => Izvodjaci.Add(new() { Key = izvodjac.Opis, Value = izvodjac, IsSelected = false }));
        muzickiSadrzajController.GetAlbumi().ForEach(album => Albumi.Add(new() { Key = album.Opis, Value = album, IsSelected = false }));
        muzickiSadrzajController.GetDela().ForEach(delo => Dela.Add(new() { Key = delo.Opis, Value = delo, IsSelected = false }));
        zanrController.GetAll().ForEach(zanr => Zanrovi.Add(new() { Key = zanr.Naziv, Value = zanr, IsSelected = false }));
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.recenzijaController = recenzijaController;
        this.urednik = urednik;
        DataContext = this;

        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string opis = OpisTxtBox.Text.Trim();
        List<Izvodjac?> izvodjaci = Izvodjaci.Where(izvodjac => izvodjac.IsSelected).Select(izvodjac => (Izvodjac?)izvodjac.Value).ToList();
        List<Album?> albumi = Albumi.Where(album => album.IsSelected).Select(album => (Album?)album.Value).ToList();
        List<Zanr?> zanrovi = Zanrovi.Where(zanr => zanr.IsSelected).Select(zanr => (Zanr?)zanr.Value).ToList();
        List<Delo?> dela = Dela.Where(delo => delo.IsSelected).Select(delo => (Delo?)delo.Value).ToList();

        if (string.IsNullOrEmpty(opis)) {
            MessageBox.Show("Opis ne može biti prazan!", "Greška dodavanja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Nastup nastup = new() { Opis = opis };
        zanrovi.ForEach(zanr => { if (zanr != null) nastup.DodajZanr(zanr); });
        dela.ForEach(delo => { if (delo != null) nastup.DodajMuzickiSadrzaj(delo); });
        albumi.ForEach(album => { if (album != null) nastup.DodajMuzickiSadrzaj(album); });
        izvodjaci.ForEach(izvodjac => { if (izvodjac != null) nastup.DodajIzvodjaca(izvodjac); });
        muzickiSadrzajController.DodajMuzickiSadrzaj(nastup);

        recenzijaController.DodajRecenziju(new Recenzija(urednik, nastup, (int)OcnSlider.Value, RecenzijaTxtBox.Text, true));

        MessageBox.Show("Nastup uspešno dodat.", "Dodavanje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
