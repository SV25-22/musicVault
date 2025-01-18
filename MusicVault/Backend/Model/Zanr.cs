using MusicVault.Backend.BuildingBlocks.Storage;

namespace MusicVault.Backend.Model;

public class Zanr : IDAble {
    public virtual Zanr? NadZanr { get; set; }
    public string Naziv { get; set; }

    public Zanr() { }

    public Zanr(Zanr? nadZanr, string naziv) {
        NadZanr = nadZanr;
        Naziv = naziv;
    }
}