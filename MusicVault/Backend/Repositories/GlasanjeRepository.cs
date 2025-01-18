using MusicVault.Backend.BuildingBlocks.Storage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MusicVault.Backend.Model;
using System.Linq;
using System.Windows.Input;

namespace MusicVault.Backend.Repositories;

public class GlasanjeRepository : SQLGenericRepository<Glasanje> {
    public List<Glasanje> GetGlasanja() {
        using var context = new SqlDbContext();
        return context.Glasanje.Include(g => g.OpcijeZaGlasanje).Include(g => g.Glasovi).ToList();
    }

    public void DodajGlasanje(Glasanje glasanje) {
        using var context = new SqlDbContext();
        context.Set<Glasanje>();

        if (glasanje.OpcijeZaGlasanje != null)
            foreach (var opcija in glasanje.OpcijeZaGlasanje)
                context.Attach(opcija);

        if (glasanje.Glasovi != null)
            foreach (var glas in glasanje.Glasovi)
                context.Attach(glas);

        context.Add(glasanje);
        context.SaveChanges();
    }

    public void UpdateGlasanje(Glasanje glasanje) {
        using (var context = new SqlDbContext()) {
            context.Set<Glasanje>();

            Glasanje staroGlasanje = context.Glasanje.Include(g => g.OpcijeZaGlasanje).Include(g => g.Glasovi).Where(g => g.Id == glasanje.Id).Single();

            staroGlasanje.OpcijeZaGlasanje.Clear();
            staroGlasanje.Glasovi.Clear();

            context.Entry(staroGlasanje).State = EntityState.Detached;
            context.Update(staroGlasanje);
            context.SaveChanges();
        }

        using (var context = new SqlDbContext()) {
            context.Set<Glasanje>();

            Glasanje staroGlasanje = context.Glasanje.Include(g => g.OpcijeZaGlasanje).Include(g => g.Glasovi).Where(g => g.Id == glasanje.Id).Single();

            if (glasanje.OpcijeZaGlasanje != null)
                foreach (var opcija in glasanje.OpcijeZaGlasanje)
                    context.Attach(opcija);

            if (glasanje.Glasovi != null)
                foreach (var glas in glasanje.Glasovi)
                    context.Attach(glas);

            staroGlasanje.Naziv = glasanje.Naziv;
            staroGlasanje.PocetakGlasanja = glasanje.PocetakGlasanja;
            staroGlasanje.KrajGlasanja = glasanje.KrajGlasanja;
            staroGlasanje.Aktivno = glasanje.Aktivno;
            if (glasanje.OpcijeZaGlasanje != null)
                staroGlasanje.OpcijeZaGlasanje = glasanje.OpcijeZaGlasanje;
            if (glasanje.Glasovi != null)
                staroGlasanje.Glasovi = glasanje.Glasovi;

            context.Entry(staroGlasanje).State = EntityState.Detached;
            context.Update(staroGlasanje);

            context.SaveChanges();
        }
    }

    public List<Glasanje> GetAllEager() {
        using (var context = new SqlDbContext()) {
            return context.Glasanje
                .Include(g => g.OpcijeZaGlasanje)
                .Include(g => g.Glasovi)
                .ThenInclude(g => g.Korisnik)
                .AsEnumerable()
                .ToList();
        }
    }
}