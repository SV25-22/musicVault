using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Controllers;
using System.Windows;

namespace MusicVault.Frontend.AdminView;

public partial class AdminWindow : Window, IObserver {
    public readonly MuzickiSadrzajController muzickiSadrzajController;
    public readonly RecenzijaController recenzijaController;
    public readonly KorisnikController korisnikController;
    public readonly IzvodjacController izvodjacController;
    public readonly GlasanjeController glasanjeController;
    public readonly ZanrController zanrController;

    public AdminWindow(KorisnikController korisnikController, MuzickiSadrzajController muzickiSadrzajController, ZanrController zanrController,
                       RecenzijaController recenzijaController, IzvodjacController izvodjacController, GlasanjeController glasanjeController) {
        this.muzickiSadrzajController = muzickiSadrzajController;
        this.recenzijaController = recenzijaController;
        this.korisnikController = korisnikController;
        this.izvodjacController = izvodjacController;
        this.glasanjeController = glasanjeController;
        this.zanrController = zanrController;
        DataContext = this;

        muzickiSadrzajController.Subscribe(this);
        izvodjacController.Subscribe(this);
        korisnikController.Subscribe(this);
        zanrController.Subscribe(this);
        InitializeComponent();
    }

    public void Update() {
        urednikControl.Update();
        contentControl.Update();
        votingControl.Update();
    }
}
