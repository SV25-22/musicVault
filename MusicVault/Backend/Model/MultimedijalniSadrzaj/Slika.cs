namespace MusicVault.Backend.Model.MultimedijalniSadrzaj;

public class Slika : MultimedijalniSadrzaj {
    public override void PrikaziSe() {
        throw new System.NotImplementedException();
    }

    public Slika() { }

    public Slika(string link) {
        Link = link;
    }
}