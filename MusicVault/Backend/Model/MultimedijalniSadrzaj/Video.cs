namespace MusicVault.Backend.Model.MultimedijalniSadrzaj;

public class Video : MultimedijalniSadrzaj {
    public override void PrikaziSe() {
        throw new System.NotImplementedException();
    }

    public Video() { }

    public Video(string link) {
        Link = link;
    }
}