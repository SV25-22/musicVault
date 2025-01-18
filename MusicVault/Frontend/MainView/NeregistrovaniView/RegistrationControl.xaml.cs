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

namespace MusicVault.Frontend.MainView;

public partial class RegistrationControl : UserControl {
    public static IEnumerable<Pol> PolVrednosti => Enum.GetValues(typeof(Pol)).Cast<Pol>();
    private KorisnikController korisnikController;
    public KorisnikDTO Korisnik { get; set; }
    private MainWindow? mainWindow;

    public RegistrationControl() {
        Korisnik = new();
        DataContext = this;
        InitializeComponent();
        GodRodjenjaPicker.DisplayDateEnd = DateTime.Now.AddYears(-10);
        GodRodjenjaPicker.DisplayDateStart = new DateTime(1950, 01, 01);
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        mainWindow = Window.GetWindow(this) as MainWindow;
        korisnikController = mainWindow?.korisnikController ?? new();
    }

    private void Register_Click(object sender, RoutedEventArgs e) {
        string? validationProblem = Korisnik.ValidationProblem;

        if (!string.IsNullOrEmpty(validationProblem)) {
            MessageBox.Show("Nije moguće registrovati se. " + validationProblem, "Greška registracije", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (korisnikController.RegistrujKorisnika(Korisnik.ToKorisnik(), Korisnik.Lozinka) is Korisnik korisnik && korisnik != null) {
            MessageBox.Show("Korisnik uspešno registrovan.", "Registracija uspešna", MessageBoxButton.OK, MessageBoxImage.Information);
            mainWindow?.UlogujKorisnika(korisnik);
            LozinkaBox.Password = "";
            Korisnik = new();
            DataContext = null;
            DataContext = this;
        } else {
            MessageBox.Show("Nije moguće registrovati se. Mail je već u upotrebi", "Greška registracije", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void BirthdayPicker_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        if (!int.TryParse(e.Text, out _) && e.Text != ".")
            e.Handled = true;
    }

    private void LozinkaBox_LozinkaChanged(object sender, RoutedEventArgs e) {
        ((dynamic)DataContext).Korisnik.Lozinka = ((PasswordBox)sender).Password;
    }
}
