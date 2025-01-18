using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Model.Recenzija;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Backend.Model;
using System.Windows.Controls;
using MusicVault.Frontend.DTO;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.MainView.RegistrovaniView.UrednikView;

public partial class CreateContentControl : UserControl, IObserver {
    public ObservableCollection<RecenzijaDTO> Recenzije { get; set; } = new();
    private MuzickiSadrzajController muzickiSadrzajController;
    private RecenzijaController recenzijaController;
    private IzvodjacController izvodjacController;
    private ZanrController zanrController;
    private Korisnik urednik;

    public CreateContentControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        recenzijaController = mainWindow?.recenzijaController ?? new();
        izvodjacController = mainWindow?.izvodjacController ?? new();
        zanrController = mainWindow?.zanrController ?? new();
        urednik = mainWindow?.ulogovaniKorsnik ?? new();

        recenzijaController.Subscribe(this);
        Update();
    }

    public void Update() {
        Recenzije.Clear();
        recenzijaController.GetAll().Select(recenzija => recenzijaController.GetEager(recenzija.Id))
                           .Where(recenzija => recenzija.Urednik != null && recenzija.Urednik.Id == urednik.Id && !recenzija.Objavljena)
                           .ToList().ForEach(recenzija => Recenzije.Add(new RecenzijaDTO(recenzija)));
        RecenzijaComboBox.SelectedIndex = 0;
    }

    private void AddRecenzijaBtn_Click(object sender, RoutedEventArgs e) {
        Recenzija recenzija = ((RecenzijaDTO)RecenzijaComboBox.SelectedValue).Recenzija;
        recenzija.Ocena = (int)OcnSlider.Value;
        recenzija.Opis = RecenzijaTxtBox.Text;
        recenzija.Stanje = Stanje.Objavljeno;
        recenzija.Objavljena = true;

        recenzijaController.Update(recenzija);

        RecenzijaTxtBox.Text = "";
        OcnSlider.Value = 5;
    }

    private void AddAlbumBtn_Click(object sender, RoutedEventArgs e) {
        new AddAlbumWindow(urednik, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
    }

    private void AddDeloBtn_Click(object sender, RoutedEventArgs e) {
        new AddTrackWindow(urednik, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
    }

    private void AddNastupBtn_Click(object sender, RoutedEventArgs e) {
        new AddNastupWindow(urednik, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
    }

    private void AddIzvodjacBtn_Click(object sender, RoutedEventArgs e) {
        new AddArtistWindow(zanrController, izvodjacController).Show();
    }

    private void RecenzijaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        AddRecenzijaBtn.IsEnabled = RecenzijaComboBox.SelectedValue != null;
        RecenzijaTxtBox.IsEnabled = RecenzijaComboBox.SelectedValue != null;
        OcnSlider.IsEnabled = RecenzijaComboBox.SelectedValue != null;
    }
}
