using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Backend.Model;
using System.Windows.Controls;
using System.Windows;
using System.Linq;
using System;

namespace MusicVault.Frontend.MainView.RegistrovaniView;

public partial class VotingControl : UserControl, IObserver {
    public ObservableCollection<MuzickiSadrzaj> Odgovori { get; set; } = new();
    public ObservableCollection<Glasanje> Glasanja { get; set; } = new();
    public MuzickiSadrzajController muzickiSadrzajController;
    public GlasanjeController glasanjeController;
    private Korisnik korisnik;

    public VotingControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        glasanjeController = mainWindow?.glasanjeController ?? new();
        korisnik = mainWindow?.ulogovaniKorsnik ?? new();
        glasanjeController.Subscribe(this);
        Update();
    }

    public void Update() {
        Glasanja.Clear();
        Odgovori.Clear();
        glasanjeController.GetAllEager()
            .Where(glasanje => 
                glasanje.Aktivno && 
                glasanje.Glasovi.All(
                    glas => glas.Korisnik.Id != korisnik.Id
                    ))
                    .ToList()
                    .ForEach(Glasanja.Add);
    }

    private void odgBtn_Click(object sender, RoutedEventArgs e) {
        if (GlasanjeComboBox.SelectedValue is Glasanje glasanje && glasanje != null) {
            glasanje.DodajGlas(new Glas(korisnik, (MuzickiSadrzaj)OdgovorComboBox.SelectedValue, DateOnly.FromDateTime(DateTime.Today), 1));
            glasanjeController.UpdateGlasanje(glasanje);
        }
    }

    private void OdgovorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        odgBtn.IsEnabled = GlasanjeComboBox.SelectedValue != null && OdgovorComboBox.SelectedValue != null;
    }

    private void GlasanjeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        odgBtn.IsEnabled = GlasanjeComboBox.SelectedValue != null && OdgovorComboBox.SelectedValue != null;
        if (GlasanjeComboBox.SelectedValue is Glasanje glasanje && glasanje != null)
            glasanje.OpcijeZaGlasanje.ToList().ForEach(Odgovori.Add);
    }
}
