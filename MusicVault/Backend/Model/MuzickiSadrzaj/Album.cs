using MusicVault.Backend.Model.Enums;

namespace MusicVault.Backend.Model.MuzickiSadrzaj;

public class Album : MuzickiSadrzaj {
    public NacinCuvanja Tip;

    public Album(NacinCuvanja tip) {
        Tip = tip;
    }
}