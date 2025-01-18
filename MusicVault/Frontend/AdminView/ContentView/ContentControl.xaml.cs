using MusicVault.Frontend.AdminView.ContentView.AddViews;
using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Frontend.AdminView.ContentView;
using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Frontend.DTO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace MusicVault.Frontend.AdminView;

public partial class ContentControl : UserControl, IObserver {
    public static string[] TipoviSadrzaja { get; set; } = new string[] { "dela", "albumi", "nastupi", "izvođači" };
    public ObservableCollection<SadrzajDTO> Sadrzaj { get; set; } = new();

    private MuzickiSadrzajController muzickiSadrzajController;
    private RecenzijaController recenzijaController;
    private KorisnikController korisnikController;
    private IzvodjacController izvodjacController;
    private ZanrController zanrController;

    public ContentControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        AdminWindow? mainWindow = Window.GetWindow(this) as AdminWindow;
        muzickiSadrzajController = mainWindow?.muzickiSadrzajController ?? new();
        recenzijaController = mainWindow?.recenzijaController ?? new();
        korisnikController = mainWindow?.korisnikController ?? new();
        izvodjacController = mainWindow?.izvodjacController ?? new();
        zanrController = mainWindow?.zanrController ?? new();
        RefreshDataGrid();
    }

    private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        if (TypeComboBox.SelectedValue.ToString() == "dela")
            AddContentBtn.Content = "Dodaj delo";
        else if (TypeComboBox.SelectedValue.ToString() == "albumi")
            AddContentBtn.Content = "Dodaj album";
        else if (TypeComboBox.SelectedValue.ToString() == "nastupi")
            AddContentBtn.Content = "Dodaj nastup";
        else if (TypeComboBox.SelectedValue.ToString() == "izvođači")
            AddContentBtn.Content = "Dodaj izvođača";
        RefreshDataGrid();
    }

    private void AddZnrBtn_Click(object sender, RoutedEventArgs e) {
        new AddZanrWindow(zanrController).Show();
    }

    private void AddContentBtn_Click(object sender, RoutedEventArgs e) {
        if (TypeComboBox.SelectedValue.ToString() == "dela")
            new AddTrackWindow(korisnikController, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "albumi")
            new AddAlbumWindow(korisnikController, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "nastupi")
            new AddNastupWindow(korisnikController, zanrController, izvodjacController, muzickiSadrzajController, recenzijaController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "izvođači")
            new AddArtistWindow(zanrController, izvodjacController).Show();
    }

    private void SadrzajDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
        if (TypeComboBox.SelectedValue.ToString() == "dela")
            new EditTrackWindow(muzickiSadrzajController.GetDeloEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), zanrController, izvodjacController, muzickiSadrzajController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "albumi")
            new EditAlbumWindow(muzickiSadrzajController.GetAlbumEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), zanrController, izvodjacController, muzickiSadrzajController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "nastupi")
            new EditNastupWindow(muzickiSadrzajController.GetNastupEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), zanrController, izvodjacController, muzickiSadrzajController).Show();
        else if (TypeComboBox.SelectedValue.ToString() == "izvođači")
            new EditArtistWindow(izvodjacController.GetIzvodjacEager(((SadrzajDTO)SadrzajDataGrid.SelectedValue).Id), zanrController, izvodjacController).Show();
    }

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
