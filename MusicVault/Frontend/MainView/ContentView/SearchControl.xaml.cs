using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Backend.Model;
using MusicVault.Frontend.DTO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace MusicVault.Frontend.MainView.ContentView;

public partial class SearchControl : UserControl, IObserver {
    public static string[] TipoviSadrzaja { get; set; } = new string[] { "dela", "albumi", "nastupi", "izvođači" };
    public ObservableCollection<SadrzajDTO> Sadrzaj { get; set; } = new();

    private MuzickiSadrzajController muzickiSadrzajController;
    private RecenzijaController recenzijaController;
    private IzvodjacController izvodjacController;
    private Korisnik korisnik;

    public SearchControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        MainWindow? mainWindow = Window.GetWindow(this) as MainWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        recenzijaController = mainWindow?.recenzijaController ?? new();
        izvodjacController = mainWindow?.izvodjacController ?? new();
        korisnik = mainWindow?.ulogovaniKorsnik ?? new();
        muzickiSadrzajController.Subscribe(this);
        izvodjacController.Subscribe(this);
        RefreshDataGrid();
    }

    private void SadrzajDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
        if (TypeComboBox.SelectedValue.ToString() == "dela")
            new TrackWindow(korisnik, recenzijaController, muzickiSadrzajController.GetDeloEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), muzickiSadrzajController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "albumi")
            new AlbumWindow(korisnik, recenzijaController, muzickiSadrzajController.GetAlbumEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), muzickiSadrzajController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "nastupi")
            new NastupWindow(korisnik, recenzijaController, muzickiSadrzajController.GetNastupEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id)).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "izvođači")
            new ArtistWindow(izvodjacController.GetIzvodjacEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), muzickiSadrzajController).Show();
    }

    private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => RefreshDataGrid();

    private void SearchTxtBox_TextChanged(object sender, TextChangedEventArgs e) => RefreshDataGrid();

    public void Update() => RefreshDataGrid();

    public void RefreshDataGrid() {
        Sadrzaj.Clear();
        string search = SearchTxtBox.Text;

        if (TypeComboBox.SelectedValue.ToString() == "dela")
            muzickiSadrzajController?.GetDela(search).ForEach(delo => Sadrzaj.Add(new SadrzajDTO(delo)));
        else if (TypeComboBox.SelectedValue.ToString() == "albumi")
            muzickiSadrzajController?.GetAlbumi(search).ForEach(album => Sadrzaj.Add(new SadrzajDTO(album)));
        else if (TypeComboBox.SelectedValue.ToString() == "nastupi")
            muzickiSadrzajController?.GetNastupi(search).ForEach(nastup => Sadrzaj.Add(new SadrzajDTO(nastup)));
        else if (TypeComboBox.SelectedValue.ToString() == "izvođači")
            izvodjacController?.Search(search).ForEach(izvodjac => Sadrzaj.Add(new SadrzajDTO(izvodjac)));
    }
}
