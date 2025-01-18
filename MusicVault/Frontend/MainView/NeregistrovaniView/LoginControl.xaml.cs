using MusicVault.Backend.Controllers;
using MusicVault.Backend.Model.Enums;
using MusicVault.Frontend.AdminView;
using System.Windows.Controls;
using System.Windows;

namespace MusicVault.Frontend.MainView;

public partial class LoginControl : UserControl {
    public MuzickiSadrzajController muzickiSadrzajController;
    public RecenzijaController recenzijaController;
    public KorisnikController korisnikController;
    public IzvodjacController izvodjacController;
    public GlasanjeController glasanjeController;
    public ZanrController zanrController;
    private MainWindow? mainWindow;

    public LoginControl() {
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        mainWindow = Window.GetWindow(this) as MainWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        recenzijaController = mainWindow?.recenzijaController ?? new();
        korisnikController = mainWindow?.korisnikController ?? new();
        izvodjacController = mainWindow?.izvodjacController ?? new();
        glasanjeController = mainWindow?.glasanjeController ?? new();
        zanrController = mainWindow?.zanrController ?? new();
        DataContext = this;
    }

    private void ShowMe(object? sender, System.EventArgs e) => mainWindow?.Show();

    private void LoginButton_Click(object sender, RoutedEventArgs e) {
        if (korisnikController.UlogujSe(emailBox.Text, passwordBox.Password) is var korisnik && korisnik != null) {
            if (korisnik.Tip == TipKorisnika.Admin) {
                AdminWindow adminWindow = new(korisnikController, muzickiSadrzajController, zanrController, recenzijaController, izvodjacController, glasanjeController);
                adminWindow.Closed += ShowMe;
                adminWindow.Show();
                mainWindow?.Hide();
            } else if (korisnik.Tip == TipKorisnika.Registrovani) {
                mainWindow?.UlogujKorisnika(korisnik);
            } else if (korisnik.Tip == TipKorisnika.Urednik) {
                mainWindow?.UlogujUrednika(korisnik);
            }
        } else {
            MessageBox.Show("Ne postoji korisnik sa tim kredencijalima!", "Greška logovanja", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        passwordBox.Password = "";
        emailBox.Text = "";
    }
}
