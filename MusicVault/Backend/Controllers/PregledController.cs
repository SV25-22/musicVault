using MusicVault.Backend.BuildingBlocks.Controller;
using MusicVault.Backend.Model;
using MusicVault.Backend.Repositories;

namespace MusicVault.Backend.Controllers;
public class PregledController : GenericController<Pregled, PregledRepository> {
    public PregledController() { }
}