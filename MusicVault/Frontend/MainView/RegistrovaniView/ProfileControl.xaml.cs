using MusicVault.Backend.Controllers;
using MusicVault.Backend.Model.Enums;
using MusicVault.Frontend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicVault.Frontend.MainView.RegistrovaniView;

public partial class ProfileControl : UserControl {
    public static IEnumerable<Pol> PolVrednosti => Enum.GetValues(typeof(Pol)).Cast<Pol>();
    private KorisnikController korisnikController;
    public KorisnikDTO Korisnik { get; set; }
    private MainWindow? mainWindow;

    public ProfileControl() { }

    public void Load() {
        mainWindow = Window.GetWindow(this) as MainWindow;
        korisnikController = mainWindow?.korisnikController ?? new();
        Korisnik = new KorisnikDTO(mainWindow?.ulogovaniKorsnik ?? new());
        DataContext = null;
        DataContext = this;

        InitializeComponent();
        GodRodjenjaPicker.DisplayDateEnd = DateTime.Now.AddYears(-10);
        GodRodjenjaPicker.DisplayDateStart = new DateTime(1950, 01, 01);
    }

    private void Update_Click(object sender, RoutedEventArgs e) {
        string? validationProblem = Korisnik.ValidationProblem;

        if (!string.IsNullOrEmpty(validationProblem)) {
            MessageBox.Show("Nije moguće ažurirati podatke. " + validationProblem, "Greška ažuriranja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (korisnikController.AzurirajKorisnika(Korisnik.ToKorisnik()))
            MessageBox.Show("Korisnik uspešno ažuriran.", "Ažuriranje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
        else
            MessageBox.Show("Nije moguće ažurirati podatke. Mail je već u upotrebi", "Greška ažuriranja", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void BirthdayPicker_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        if (!int.TryParse(e.Text, out _) && e.Text != ".")
            e.Handled = true;
    }

    private void LozinkaBox_LozinkaChanged(object sender, RoutedEventArgs e) {
        ((dynamic)DataContext).Korisnik.Lozinka = ((PasswordBox)sender).Password;
    }

    private void Logout_Click(object sender, RoutedEventArgs e) {
        mainWindow?.IzlogujSe();
    }
}
