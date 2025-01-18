using MusicVault.Backend.Controllers;
using MusicVault.Backend.Model.Enums;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using MusicVault.Frontend.DTO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System;

namespace MusicVault.Frontend.AdminView.UredniciView;

public partial class EditUrednikWindow : Window {
    public static IEnumerable<Pol> PolVrednosti => Enum.GetValues(typeof(Pol)).Cast<Pol>();
    private KorisnikController korisnikController;
    public KorisnikDTO Urednik { get; set; }
    private bool editing = true;

    public EditUrednikWindow(KorisnikController korisnikController, KorisnikDTO? urednik = null) {
        this.korisnikController = korisnikController;
        Urednik = urednik ?? new();
        DataContext = this;

        InitializeComponent();
        GodRodjenjaPicker.DisplayDateEnd = DateTime.Now.AddYears(-10);
        GodRodjenjaPicker.DisplayDateStart = new DateTime(1950, 01, 01);

        if (urednik == null) {
            editing = false;
            Title = "Dodavanje urednika";
            titleLabel.Content = "Dodavanje urednika";
            editBtn.Content = "Dodaj";
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e) {
        string? validationProblem = Urednik.ValidationProblem;
        Urednik.Tip = TipKorisnika.Urednik;

        if (!string.IsNullOrEmpty(validationProblem)) {
            MessageBox.Show($"Nije moguće {(editing ? "izmeniti" : "dodati")} urednika. " + validationProblem, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (AddOrEdit(Urednik.ToKorisnik())) {
            MessageBox.Show($"Urednik uspešno {(editing ? "izmenjen" : "dodat")}!", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        } else {
            MessageBox.Show($"Nije moguće {(editing ? "izmeniti" : "dodati")} urednika. Mail je već u upotrebi", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private bool AddOrEdit(Korisnik urednik) {
        if (editing)
            return korisnikController.AzurirajKorisnika(urednik);
        else
            return korisnikController.RegistrujUrednika(Urednik.ToKorisnik(), Urednik.Lozinka) is Korisnik korisnik && korisnik != null;
    }

    private void BirthdayPicker_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        if (!int.TryParse(e.Text, out _) && e.Text != ".")
            e.Handled = true;
    }

    private void LozinkaBox_LozinkaChanged(object sender, RoutedEventArgs e) {
        ((dynamic)DataContext).Urednik.Lozinka = ((PasswordBox)sender).Password;
    }
}
