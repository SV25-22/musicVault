using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.BuildingBlocks.Observer;
using MusicVault.Backend.Model;
using MusicVault.Backend.Repositories;

namespace MusicVault.Backend.Controllers;
public class ZanrController : GenericController<Zanr, ZanrRepository> {
    public ZanrController() { }

    public void DodajZanr(Zanr entity) {
        repository.DodajZanr(entity);
        Subject.NotifyObservers();
    }
}