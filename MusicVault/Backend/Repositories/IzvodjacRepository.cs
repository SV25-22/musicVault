using MusicVault.Backend.BuildingBlocks.Storage;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.Model.MuzickiSadrzaj;

namespace MusicVault.Backend.Repositories;

public class IzvodjacRepository : SQLGenericRepository<Izvodjac> {
    public static List<Izvodjac> Search(string search = "") {
        using var context = new SqlDbContext();
        return context.Izvodjac.Where(i => string.IsNullOrEmpty(search) || i.Opis.ToLower().Contains(search.ToLower())).ToList();
    }

    public Izvodjac DodajIzvodjaca(Izvodjac entity) {
        using (var context = new SqlDbContext()) {
            context.Set<Izvodjac>();
            foreach (var zanr in entity.Zanrevi) {
                context.Attach(zanr);
            }
            foreach (var multimedijalniSadrzaj in entity.MultimedijalniSadrzaji) {
                context.Attach(multimedijalniSadrzaj);
            }
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }

    public Izvodjac GetIzvodjacEager(int id) {
        using (var context = new SqlDbContext()) {
            return context.Izvodjac
                .Include(i => i.Zanrevi)
                .Where(i => i.Id == id)
                .Single();
        }
    }

    public void UpdateIzvodjac(Izvodjac izvodjac) {
        using (var context = new SqlDbContext()) {
            context.Set<Izvodjac>();

            Izvodjac stariIzvodjac = context.Izvodjac.Include(d => d.Zanrevi).Where(d => d.Id == izvodjac.Id).Single();

            stariIzvodjac.Zanrevi.Clear();

            context.Entry(stariIzvodjac).State = EntityState.Detached;
            context.Update(stariIzvodjac);
            context.SaveChanges();
        }

        using (var context = new SqlDbContext()) {
            context.Set<Izvodjac>();

            Izvodjac stariIzvodjac = context.Izvodjac.Include(d => d.Zanrevi).Where(d => d.Id == izvodjac.Id).Single();

            foreach (var zanr in izvodjac.Zanrevi) {
                context.Attach(zanr);
            }

            stariIzvodjac.Opis = izvodjac.Opis;
            stariIzvodjac.Zanrevi = izvodjac.Zanrevi;

            context.Entry(stariIzvodjac).State = EntityState.Detached;
            context.Update(stariIzvodjac);

            context.SaveChanges();
        }
    }
}