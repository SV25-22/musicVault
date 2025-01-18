using Microsoft.EntityFrameworkCore;
using MusicVault.Backend.BuildingBlocks.Storage;
using MusicVault.Backend.Model;

namespace MusicVault.Backend.Repositories;

public class ZanrRepository : SQLGenericRepository<Zanr> {
    public Zanr DodajZanr(Zanr entity) {
        using (var context = new SqlDbContext()) {
            context.Set<Zanr>();
            if (entity.NadZanr != null) {
                context.Attach(entity.NadZanr);
            }
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }
}