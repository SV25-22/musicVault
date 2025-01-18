using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.Repositories;
using System.Collections.Generic;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Controllers;
public class GlasanjeController : GenericController<Glasanje, GlasanjeRepository> {
    public GlasanjeController() { }

    public void ZavrsiGlasanje(Glasanje glasanje) {
        glasanje.Aktivno = false;
        Update(glasanje);

        // glasanje.ObradaRezultata();
        // todo poslati obavestenje o rezultatu adminu
    }

    public List<Glasanje> GetGlasanja() {
        return repository.GetGlasanja();
    }

    public void DodajGlasanje(Glasanje glasanje) {
        repository.DodajGlasanje(glasanje);
        Subject.NotifyObservers();
    }

    public void UpdateGlasanje(Glasanje glasanje) {
        repository.UpdateGlasanje(glasanje);
        Subject.NotifyObservers();
    }

    public List<Glasanje> GetAllEager() {
        return repository.GetAllEager();
    }
}