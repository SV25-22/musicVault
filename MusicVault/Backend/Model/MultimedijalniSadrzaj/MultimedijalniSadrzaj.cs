using MusicVault.Backend.BuildingBlocks.Storage;

namespace MusicVault.Backend.Model.MultimedijalniSadrzaj;

public abstract class MultimedijalniSadrzaj : IDAble {
    public string Link { get; set; }
    public abstract void PrikaziSe();
}