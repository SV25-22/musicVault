using MusicVault.Backend.BuildingBlocks.Storage;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using MusicVault.Backend.Model.Recenzija;
using MusicVault.Backend.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MusicVault.Backend.Repositories;

public class RecenzijaRepository : SQLGenericRepository<Recenzija> {
    public RecenzijaRepository() { }

    public Recenzija DodajRecenziju(Recenzija entity) {
        using (var context = new SqlDbContext()) {
            context.Set<Recenzija>();
            context.Attach(entity.Urednik);
            context.Attach(entity.MuzickiSadrzaj);
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }

    public Recenzija GetEager(int id) {
        using (var context = new SqlDbContext()) {
            return context.Recenzija
                .Include(r => r.MuzickiSadrzaj)
                .Include(r => r.Urednik)
                .Where(r => r.Id == id)
                .Single();
        }
    }

    public Recenzija GetRecenzijaUrednika(MuzickiSadrzaj sadrzaj) {
        using (var context = new SqlDbContext()) {
            return context.Recenzija
                .Include(r => r.MuzickiSadrzaj)
                .Include(r => r.Urednik)
                .Where(r => r.MuzickiSadrzaj.Id == sadrzaj.Id && r.Urednik != null && r.Urednik.Tip == TipKorisnika.Urednik)
                .Single();
        }
    }

    public List<Recenzija> GetRecenzijaZa(MuzickiSadrzaj sadrzaj) {
        using (var context = new SqlDbContext()) {
            return context.Recenzija
                .Include(r => r.MuzickiSadrzaj)
                .Include(r => r.Urednik)
                .Where(r => r.MuzickiSadrzaj.Id == sadrzaj.Id)
                .ToList();
        }
    }
}