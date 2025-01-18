using MusicVault.Backend.Controllers;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System;

namespace MusicVault.Backend.Common;

public class Sistem {
    public static void ZavrsiSveGlasove(GlasanjeController glasanjeController) {
        List<Glasanje> svaGlasanja = glasanjeController.GetAll();

        foreach (Glasanje gl in svaGlasanja) {
            if (DateOnly.FromDateTime(DateTime.Now) > gl.KrajGlasanja) {
                glasanjeController.ZavrsiGlasanje(gl);
            }
        }
    }
}