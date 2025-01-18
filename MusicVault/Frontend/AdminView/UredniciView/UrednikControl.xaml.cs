using MusicVault.Frontend.AdminView.UredniciView;
using System.Collections.ObjectModel;
using MusicVault.Backend.Controllers;
using MusicVault.Frontend.DTO;
using System.Windows.Controls;
using System.Windows;

namespace MusicVault.Frontend.AdminView;

public partial class UrednikControl : UserControl {
    public ObservableCollection<KorisnikDTO> Urednici { get; set; } = new();
    private KorisnikController korisnikController;

    public UrednikControl() {
        DataContext = this;
        InitializeComponent();
    }

    private void OnControlLoaded(object sender, RoutedEventArgs e) {
        AdminWindow? mainWindow = Window.GetWindow(this) as AdminWindow;
        korisnikController = mainWindow?.korisnikController ?? new();
        RefreshDataGrid();
    }

    private void RefreshDataGrid() {
        Urednici.Clear();
        korisnikController.GetUrednici().ForEach(urednik => Urednici.Add(new KorisnikDTO(urednik)));
    }

    private void AddBtn_Click(object sender, RoutedEventArgs e) {
        new EditUrednikWindow(korisnikController).Show();
    }

    private void EditBtn_Click(object sender, RoutedEventArgs e) {
        new EditUrednikWindow(korisnikController, (KorisnikDTO)UredniciDataGrid.SelectedValue).Show();
    }

    private void DeleteBtn_Click(object sender, RoutedEventArgs e) {
        korisnikController.BanujKorisnika(((KorisnikDTO)UredniciDataGrid.SelectedValue).ToKorisnik());
        RefreshDataGrid();
    }

    public void Update() => RefreshDataGrid();

    private void UredniciDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        DeleteBtn.IsEnabled = UredniciDataGrid.SelectedItem != null;
        EditBtn.IsEnabled = UredniciDataGrid.SelectedItem != null;
    }
}
