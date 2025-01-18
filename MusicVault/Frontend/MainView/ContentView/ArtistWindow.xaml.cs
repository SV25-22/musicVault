using MusicVault.Backend.Controllers;
using System.Collections.ObjectModel;
using MusicVault.Backend.Model;
using MusicVault.Frontend.DTO;
using System.Windows;
using System.Linq;

namespace MusicVault.Frontend.MainView.ContentView;

public partial class ArtistWindow : Window {
    public ObservableCollection<SadrzajDTO> Nastupi { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Albumi { get; set; } = new();
    public ObservableCollection<SadrzajDTO> Dela { get; set; } = new();

    public ArtistWindow(Izvodjac izvodjac, MuzickiSadrzajController muzickiSadrzajController) {
        DataContext = this;
        InitializeComponent();

        IzvodjacLabel.Content = izvodjac.Opis;
        ZanroviLabel.Content = string.Join(", ", izvodjac.Zanrevi.Select(zanr => zanr.Naziv));
        muzickiSadrzajController.GetDela().Where(delo => delo.Izvodjaci.Any(i => i.Id == izvodjac.Id))
                                          .ToList().ForEach(delo => Dela.Add(new SadrzajDTO(delo)));
        muzickiSadrzajController.GetAlbumi().Where(album => album.Izvodjaci.Any(i => i.Id == izvodjac.Id))
                                            .ToList().ForEach(album => Albumi.Add(new SadrzajDTO(album)));
        muzickiSadrzajController.GetNastupi().Where(nastup => nastup.Izvodjaci.Any(i => i.Id == izvodjac.Id))
                                             .ToList().ForEach(nastup => Nastupi.Add(new SadrzajDTO(nastup)));
    }
}
