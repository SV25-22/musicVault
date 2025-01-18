using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.Model;
using MusicVault.Backend.Repositories;

namespace MusicVault.Backend.Controllers;
public class PlejlistaController : GenericController<Plejlista, PlejlistaRepository> {
    public PlejlistaController() { }
}