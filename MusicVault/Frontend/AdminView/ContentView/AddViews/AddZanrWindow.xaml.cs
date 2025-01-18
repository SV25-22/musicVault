using MusicVault.Backend.Controllers;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows;

namespace MusicVault.Frontend.AdminView.ContentView;

public partial class AddZanrWindow : Window {
    public List<Zanr> Zanrovi { get; set; } = new();
    private readonly ZanrController zanrController;

    public AddZanrWindow(ZanrController zanrController) {
        this.zanrController = zanrController;
        Zanrovi.Add(new Zanr(null, ""));
        zanrController.GetAll().ForEach(Zanrovi.Add);
        DataContext = this;

        InitializeComponent();
    }

    private void Add_Click(object sender, RoutedEventArgs e) {
        string naziv = NazivTxtBox.Text.Trim();
        Zanr? nadzanr = (Zanr?)NadzanrComboBox.SelectedValue;
        nadzanr = string.IsNullOrEmpty(nadzanr?.Naziv) ? null : nadzanr;

        if (string.IsNullOrEmpty(naziv)) {
            MessageBox.Show("Naziv ne može biti prazan!", "Greška dodavanja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        zanrController.DodajZanr(new Zanr(nadzanr, naziv));
        MessageBox.Show("Žanr uspešno dodat.", "Dodavanje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
    }
}
