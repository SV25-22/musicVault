using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Frontend.CommonControls;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.AdminView.ContentView;

public partial class EditTrackWindow : Window {
    public ObservableCollection<MultiSelectItem> Izvodjaci { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Zanrovi { get; set; } = new();
    public ObservableCollection<MultiSelectItem> Albumi { get; set; } = new();

    private readonly MuzickiSadrzajController muzickiSadrzajController;
    private readonly Delo delo;

    public EditTrackWindow(Delo delo, ZanrController zanrController, IzvodjacController izvodjacController, MuzickiSadrzajController muzickiSadrzajController) {
        muzickiSadrzajController.GetAlbumi().ForEach(album => Albumi.Add(new() { Key = album.Opis, Value = album, IsSelected = delo.MuzickiSadrzaji?.Any(a => a.Id == album.Id) ?? false }));
        zanrController.GetAll().ForEach(zanr => Zanrovi.Add(new() { Key = zanr.Naziv, Value = zanr, IsSelected = delo.Zanrevi?.Any(z => z.Id == zanr.Id) ?? false }));
        izvodjacController.GetAll().ForEach(izvodjac => Izvodjaci.Add(new() { Key = izvodjac.Opis, Value = izvodjac, IsSelected = delo.Izvodjaci?.Any(i => i.Id == izvodjac.Id) ?? false }));
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.delo = delo;

        DataContext = this;
        InitializeComponent();
        OpisTxtBox.Text = delo.Opis;
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string opis = OpisTxtBox.Text.Trim();
        List<Izvodjac?> izvodjaci = Izvodjaci.Where(izvodjac => izvodjac.IsSelected).Select(izvodjac => (Izvodjac?)izvodjac.Value).ToList();
        List<Album?> albumi = Albumi.Where(album => album.IsSelected).Select(album => (Album?)album.Value).ToList();
        List<Zanr?> zanrovi = Zanrovi.Where(zanr => zanr.IsSelected).Select(zanr => (Zanr?)zanr.Value).ToList();

        if (string.IsNullOrEmpty(opis)) {
            MessageBox.Show("Opis ne može biti prazan!", "Greška izmene", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        delo.Opis = opis;
        delo.Zanrevi.Clear();
        delo.MuzickiSadrzaji.Clear();
        delo.Izvodjaci.Clear();
        zanrovi.ForEach(zanr => { if (zanr != null) delo.DodajZanr(zanr); });
        albumi.ForEach(album => { if (album != null) delo.DodajMuzickiSadrzaj(album); });
        izvodjaci.ForEach(izvodjac => { if (izvodjac != null) delo.DodajIzvodjaca(izvodjac); });

        muzickiSadrzajController.UpdateDelo(delo);
        MessageBox.Show("Delo uspešno izmenjeno.", "Izmena uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
