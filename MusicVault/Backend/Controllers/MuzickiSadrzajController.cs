using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Repositories;
using System.Collections.Generic;

namespace MusicVault.Backend.Controllers;
public class MuzickiSadrzajController : GenericController<MuzickiSadrzaj, MuzickiSadrzajRepository> {
    public MuzickiSadrzajController() { }
    
    public List<Delo> GetDela(string search = "") => MuzickiSadrzajRepository.GetDela(search);

    public List<Album> GetAlbumi(string search = "") => MuzickiSadrzajRepository.GetAlbumi(search);

    public List<Nastup> GetNastupi(string search = "") => MuzickiSadrzajRepository.GetNastupi(search);

    public void DodajMuzickiSadrzaj(MuzickiSadrzaj entity) {
        repository.DodajMuzickiSadrzaj(entity);
        Subject.NotifyObservers();
    }

    public Delo GetDeloEager(int id) {
        return repository.GetDeloEager(id);
    }

    public Album GetAlbumEager(int id) {
        return repository.GetAlbumEager(id);
    }

    public Nastup GetNastupEager(int id) {
        return repository.GetNastupEager(id);
    }

    public void UpdateDelo(Delo delo) {
        repository.UpdateDelo(delo);
        Subject.NotifyObservers();
    }

    public void UpdateNastup(Nastup nastup) {
        repository.UpdateNastup(nastup);
        Subject.NotifyObservers();
    }

    public void UpdateAlbum(Album album) {
        repository.UpdateAlbum(album);
        Subject.NotifyObservers();
    }
}