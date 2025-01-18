namespace MusicVault.Backend.Model.MultimedijalniSadrzaj;

public class Gif : MultimedijalniSadrzaj {
    public override void PrikaziSe() {
        throw new System.NotImplementedException();
    }

    public Gif() { }

    public Gif(string link) { 
        Link = link;
    }
}