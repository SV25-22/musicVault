using MusicVault.Frontend.CommonControls;
using System.Collections.ObjectModel;
using MusicVault.Backend.Controllers;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.AdminView.ContentView;

public partial class EditArtistWindow : Window {
    public ObservableCollection<MultiSelectItem> Zanrovi { get; set; } = new();
    private readonly IzvodjacController izvodjacController;
    private readonly Izvodjac izvodjac;

    public EditArtistWindow(Izvodjac izvodjac, ZanrController zanrController, IzvodjacController izvodjacController) {
        zanrController.GetAll().ForEach(zanr => Zanrovi.Add(new() { Key = zanr.Naziv, Value = zanr, IsSelected = izvodjac.Zanrevi?.Any(z => z.Id == zanr.Id) ?? false }));
        this.izvodjacController = izvodjacController;
        this.izvodjac = izvodjac;
        DataContext = this;

        InitializeComponent();
        OpisTxtBox.Text = izvodjac.Opis;
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string opis = OpisTxtBox.Text.Trim();
        List<Zanr?> zanrovi = Zanrovi.Where(zanr => zanr.IsSelected).Select(zanr => (Zanr?)zanr.Value).ToList();

        if (string.IsNullOrEmpty(opis)) {
            MessageBox.Show("Opis ne može biti prazan!", "Greška izmene", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        izvodjac.Opis = opis;
        izvodjac.Zanrevi.Clear();
        zanrovi.ForEach(zanr => { if (zanr != null) izvodjac.DodajZanr(zanr); });

        izvodjacController.UpdateIzvodjac(izvodjac);
        MessageBox.Show("Izvođač uspešno izmenjen.", "Izmena uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
