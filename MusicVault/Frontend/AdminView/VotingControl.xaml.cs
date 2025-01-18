using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Frontend.CommonControls;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System;

namespace MusicVault.Frontend.AdminView;

public partial class VotingControl : UserControl, IObserver {
    public ObservableCollection<MultiSelectItem> Odgovori { get; set; } = new();
    public ObservableCollection<Glasanje> Glasanja { get; set; } = new();
    public MuzickiSadrzajController muzickiSadrzajController;
    public GlasanjeController glasanjeController;

    public VotingControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        AdminWindow? mainWindow = Window.GetWindow(this) as AdminWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        glasanjeController = mainWindow?.glasanjeController ?? new();
        glasanjeController.Subscribe(this);
        Update();
    }

    public void Update() {
        Glasanja.Clear();
        Odgovori.Clear();
        muzickiSadrzajController.GetAll().ForEach(sadrzaj => Odgovori.Add(new() { Key = sadrzaj.Opis, Value = sadrzaj, IsSelected = false }));
        glasanjeController.GetAll().Where(glasanje => glasanje.Aktivno).ToList().ForEach(Glasanja.Add);
    }

    private void AddBtn_Click(object sender, RoutedEventArgs e) {
        List<MuzickiSadrzaj?> odgovori = Odgovori.Where(odgvor => odgvor.IsSelected).Select(odgvor => (MuzickiSadrzaj?)odgvor.Value).ToList();
        DateOnly startDate = DateOnly.FromDateTime(StartDatePicker.SelectedDate ?? DateTime.Today);
        DateOnly endDate = DateOnly.FromDateTime(EndDatePicker.SelectedDate ?? DateTime.Today);
        string naziv = PitanjeTxtBox.Text.Trim();

        if (string.IsNullOrEmpty(naziv)) {
            MessageBox.Show("Pitanje ne može biti prazno!", "Greška dodavanja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (odgovori.Count == 0) {
            MessageBox.Show("Odgovori ne mogu biti prazni!", "Greška dodavanja", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Glasanje glasanje = new(startDate, endDate, true, naziv);
        odgovori.ForEach(odgovor => { if (odgovor != null) glasanje.DodajOpcijuZaGlasanje(odgovor); });
        glasanjeController.DodajGlasanje(glasanje);
        PitanjeTxtBox.Text = "";

        MessageBox.Show("Glasanje uspešno dodato.", "Dodavanje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void EndBtn_Click(object sender, RoutedEventArgs e) {
        glasanjeController.ZavrsiGlasanje((Glasanje)GlasanjeComboBox.SelectedValue);
    }

    private void GlasanjeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        endBtn.IsEnabled = GlasanjeComboBox.SelectedValue != null;
    }

    private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        if (!int.TryParse(e.Text, out _) && e.Text != ".")
            e.Handled = true;
    }
}
