using MusicVault.Backend.BuildingBlocks.Storage;
using System.Collections.Generic;

namespace MusicVault.Backend.Model;

public class Reklama: IDAble {
    public virtual ICollection<Izvodjac> Izvodjaci { get; set; }
    public virtual MultimedijalniSadrzaj.MultimedijalniSadrzaj MultimedijalniSadrzaj { get; set; }
    public double Cena { get; }
    public Reklama() { }

    public Reklama(MultimedijalniSadrzaj.MultimedijalniSadrzaj multimedijalniSadrzaj, double cena) {
        Izvodjaci = new List<Izvodjac>();
        MultimedijalniSadrzaj = multimedijalniSadrzaj;
        Cena = cena;
    }

    public void DodajIzvodjaca(Izvodjac izvodjac) {
        Izvodjaci.Add(izvodjac);
    } 
}