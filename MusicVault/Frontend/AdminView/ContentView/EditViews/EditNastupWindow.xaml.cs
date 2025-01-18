using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Frontend.CommonControls;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.AdminView.ContentView;

public partial class EditNastupWindow : Window {
    public ObservableCollection<MultiSelectItem> Izvodjaci { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Zanrovi { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Albumi { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Dela { get; set; } = new();

    private readonly MuzickiSadrzajController muzickiSadrzajController;
    private readonly Nastup nastup;

    public EditNastupWindow(Nastup nastup, ZanrController zanrController, IzvodjacController izvodjacController, MuzickiSadrzajController muzickiSadrzajController) {
        muzickiSadrzajController.GetAlbumi().ForEach(album => Albumi.Add(new() { Key = album.Opis, Value = album, IsSelected = nastup.MuzickiSadrzaji?.Any(a => a.Id == album.Id) ?? false }));
        muzickiSadrzajController.GetDela().ForEach(delo => Dela.Add(new() { Key = delo.Opis, Value = delo, IsSelected = nastup.MuzickiSadrzaji?.Any(d => d.Id == delo.Id) ?? false }));
        zanrController.GetAll().ForEach(zanr => Zanrovi.Add(new() { Key = zanr.Naziv, Value = zanr, IsSelected = nastup.Zanrevi?.Any(z => z.Id == zanr.Id) ?? false }));
        izvodjacController.GetAll().ForEach(izvodjac => Izvodjaci.Add(new() { Key = izvodjac.Opis, Value = izvodjac, IsSelected = nastup.Izvodjaci?.Any(i => i.Id == izvodjac.Id) ?? false }));
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.nastup = nastup;

        DataContext = this;
        InitializeComponent();
        OpisTxtBox.Text = nastup.Opis;
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string opis = OpisTxtBox.Text.Trim();
        List<Izvodjac?> izvodjaci = Izvodjaci.Where(izvodjac => izvodjac.IsSelected).Select(izvodjac => (Izvodjac?)izvodjac.Value).ToList();
        List<Album?> albumi = Albumi.Where(album => album.IsSelected).Select(album => (Album?)album.Value).ToList();
        List<Zanr?> zanrovi = Zanrovi.Where(zanr => zanr.IsSelected).Select(zanr => (Zanr?)zanr.Value).ToList();
        List<Delo?> dela = Dela.Where(delo => delo.IsSelected).Select(delo => (Delo?)delo.Value).ToList();

        if (string.IsNullOrEmpty(opis)) {
            MessageBox.Show("Opis ne može biti prazan!", "Greška izmene", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        nastup.Opis = opis;
        nastup.Zanrevi.Clear();
        nastup.MuzickiSadrzaji.Clear();
        nastup.Izvodjaci.Clear();
        zanrovi.ForEach(zanr => { if (zanr != null) nastup.DodajZanr(zanr); });
        dela.ForEach(delo => { if (delo != null) nastup.DodajMuzickiSadrzaj(delo); });
        albumi.ForEach(album => { if (album != null) nastup.DodajMuzickiSadrzaj(album); });
        izvodjaci.ForEach(izvodjac => { if (izvodjac != null) nastup.DodajIzvodjaca(izvodjac); });

        muzickiSadrzajController.UpdateNastup(nastup);
        MessageBox.Show("Nastup uspešno izmenjen.", "Izmena uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
