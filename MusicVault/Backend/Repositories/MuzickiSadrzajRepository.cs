using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.BuildingBlocks.Storage;
using MusicVault.Backend.Model.MuzickiSadrzaj;
using System.Collections.Generic;
using System.Linq;

namespace MusicVault.Backend.Repositories;

public class MuzickiSadrzajRepository : SQLGenericRepository<MuzickiSadrzaj> {
    public static List<Delo> GetDela(string search = "") {
        using var context = new SqlDbContext();
        return context.Delo
            .Include(d => d.MuzickiSadrzaji)
            .Include(d => d.Izvodjaci)
            .Where(d => string.IsNullOrEmpty(search) || d.Opis.ToLower().Contains(search.ToLower())).ToList();
    }

    public static List<Album> GetAlbumi(string search = "") {
        using var context = new SqlDbContext();
        return context.Album
            .Include(a => a.MuzickiSadrzaji)
            .Include(a => a.Izvodjaci)
            .Where(a => string.IsNullOrEmpty(search) || a.Opis.ToLower().Contains(search.ToLower())).ToList();
    }

    public static List<Nastup> GetNastupi(string search = "") {
        using var context = new SqlDbContext();
        return context.Nastup
            .Include(n => n.MuzickiSadrzaji)
            .Include(n => n.Izvodjaci)
            .Where(n => string.IsNullOrEmpty(search) || n.Opis.ToLower().Contains(search.ToLower())).ToList();
    }

    public MuzickiSadrzaj DodajMuzickiSadrzaj(MuzickiSadrzaj entity) {
        using (var context = new SqlDbContext()) {
            context.Set<MuzickiSadrzaj>();
            foreach (var muzickiSadrzaj in entity.MuzickiSadrzaji) {
                context.Attach(muzickiSadrzaj);
            }
            foreach (var zanr in entity.Zanrevi) {
                context.Attach(zanr);
            }
            foreach (var izvodjac in entity.Izvodjaci) {
                context.Attach(izvodjac);
            }
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }

    public Delo GetDeloEager(int id) {
        using (var context = new SqlDbContext()) {
            return context.Delo
                .Include(d => d.Zanrevi)
                .Include(d => d.Izvodjaci)
                .Include(d => d.MuzickiSadrzaji)
                .Where(d => d.Id == id)
                .Single();
        }
    }

    public Album GetAlbumEager(int id) {
        using (var context = new SqlDbContext()) {
            return context.Album
                .Include(a => a.Zanrevi)
                .Include(a => a.Izvodjaci)
                .Include(a => a.MuzickiSadrzaji)
                .Where(a => a.Id == id)
                .Single();
        }
    }

    public Nastup GetNastupEager(int id) {
        using (var context = new SqlDbContext()) {
            return context.Nastup
                .Include(n => n.Zanrevi)
                .Include(n => n.Izvodjaci)
                .Include(n => n.MuzickiSadrzaji)
                .Where(n => n.Id == id)
                .Single();
        }
    }

    public void UpdateDelo(Delo delo) {
        using (var context = new SqlDbContext()) {
            context.Set<Delo>();

            Delo staroDelo = context.Delo.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == delo.Id).Single();

            staroDelo.Zanrevi.Clear();
            staroDelo.MuzickiSadrzaji.Clear();
            staroDelo.Izvodjaci.Clear();

            context.Entry(staroDelo).State = EntityState.Detached;
            context.Update(staroDelo);
            context.SaveChanges();
        }

        using (var context = new SqlDbContext()) {
            context.Set<Delo>();

            Delo staroDelo = context.Delo.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == delo.Id).Single();

            foreach (var zanr in delo.Zanrevi) {
                context.Attach(zanr);
            }

            foreach (var muzickiSadrzaj in delo.MuzickiSadrzaji) {
                context.Attach(muzickiSadrzaj);
            }

            foreach (var izvodjac in delo.Izvodjaci) {
                context.Attach(izvodjac);
            }

            staroDelo.Objavljeno = delo.Objavljeno;
            staroDelo.Opis = delo.Opis;
            staroDelo.MuzickiSadrzaji = delo.MuzickiSadrzaji;
            staroDelo.Zanrevi = delo.Zanrevi;
            staroDelo.Izvodjaci = delo.Izvodjaci;

            context.Entry(staroDelo).State = EntityState.Detached;
            context.Update(staroDelo);

            context.SaveChanges();
        }
    }

    public void UpdateNastup(Nastup nastup) {
        using (var context = new SqlDbContext()) {
            context.Set<Nastup>();

            Nastup stariNastup = context.Nastup.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == nastup.Id).Single();

            stariNastup.Zanrevi.Clear();
            stariNastup.MuzickiSadrzaji.Clear();
            stariNastup.Izvodjaci.Clear();

            context.Entry(stariNastup).State = EntityState.Detached;
            context.Update(stariNastup);
            context.SaveChanges();
        }

        using (var context = new SqlDbContext()) {
            context.Set<Nastup>();

            Nastup stariNastup = context.Nastup.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == nastup.Id).Single();

            foreach (var zanr in nastup.Zanrevi) {
                context.Attach(zanr);
            }

            foreach (var muzickiSadrzaj in nastup.MuzickiSadrzaji) {
                context.Attach(muzickiSadrzaj);
            }

            foreach (var izvodjac in nastup.Izvodjaci) {
                context.Attach(izvodjac);
            }

            stariNastup.Objavljeno = nastup.Objavljeno;
            stariNastup.Opis = nastup.Opis;
            stariNastup.MuzickiSadrzaji = nastup.MuzickiSadrzaji;
            stariNastup.Zanrevi = nastup.Zanrevi;
            stariNastup.Izvodjaci = nastup.Izvodjaci;

            context.Entry(stariNastup).State = EntityState.Detached;
            context.Update(stariNastup);

            context.SaveChanges();
        }
    }

    public void UpdateAlbum(Album album) {
        using (var context = new SqlDbContext()) {
            context.Set<Album>();

            Album stariAlbum = context.Album.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == album.Id).Single();

            stariAlbum.Zanrevi.Clear();
            stariAlbum.MuzickiSadrzaji.Clear();
            stariAlbum.Izvodjaci.Clear();

            context.Entry(stariAlbum).State = EntityState.Detached;
            context.Update(stariAlbum);
            context.SaveChanges();
        }

        using (var context = new SqlDbContext()) {
            context.Set<Album>();

            Album stariAlbum = context.Album.Include(d => d.MuzickiSadrzaji).Include(d => d.Izvodjaci).Include(d => d.Zanrevi).Where(d => d.Id == album.Id).Single();

            foreach (var zanr in album.Zanrevi) {
                context.Attach(zanr);
            }

            foreach (var muzickiSadrzaj in album.MuzickiSadrzaji) {
                context.Attach(muzickiSadrzaj);
            }

            foreach (var izvodjac in album.Izvodjaci) {
                context.Attach(izvodjac);
            }

            stariAlbum.Opis = album.Opis;
            stariAlbum.Tip = album.Tip;
            stariAlbum.Objavljeno = album.Objavljeno;
            stariAlbum.MuzickiSadrzaji = album.MuzickiSadrzaji;
            stariAlbum.Zanrevi = album.Zanrevi;
            stariAlbum.Izvodjaci = album.Izvodjaci;

            context.Entry(stariAlbum).State = EntityState.Detached;
            context.Update(stariAlbum);

            context.SaveChanges();
        }
    }
}