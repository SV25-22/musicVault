using MusicVault.Backend.Common;
using MusicVault.Backend.Controllers;
using MusicVault.Backend.Model;
using System.Windows;

namespace MusicVault.Frontend.MainView;

public partial class MainWindow : Window {
    public MuzickiSadrzajController muzickiSadrzajController = new();
    public RecenzijaController recenzijaController = new();
    public KorisnikController korisnikController = new();
    public IzvodjacController izvodjacController = new();
    public GlasanjeController glasanjeController = new();
    public ZanrController zanrController = new();

    public Korisnik? ulogovaniKorsnik = null;

    public MainWindow() {
        Sistem.ZavrsiSveGlasove(glasanjeController);
        InitializeComponent();
        DataContext = this;
    }

    public void UlogujKorisnika(Korisnik korisnik) {
        glasanjeTab.Visibility = Visibility.Visible;
        profileTab.Visibility = Visibility.Visible;
        loginTab.Visibility = Visibility.Hidden;
        ulogovaniKorsnik = korisnik;
        profileTab.Load();
    }

    public void UlogujUrednika(Korisnik korisnik) {
        addContentTab.Visibility = Visibility.Visible;
        UlogujKorisnika(korisnik);
    }

    public void IzlogujSe() {
        ulogovaniKorsnik = null;
        addContentTab.Visibility = Visibility.Hidden;
        glasanjeTab.Visibility = Visibility.Hidden;
        profileTab.Visibility = Visibility.Hidden;
        loginTab.Visibility = Visibility.Visible;
    }
}
