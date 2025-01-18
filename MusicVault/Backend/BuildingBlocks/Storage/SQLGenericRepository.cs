using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MusicVault.Backend.BuildingBlocks.Storage;

public abstract class SQLGenericRepository<T> where T : IDAble {
    public SQLGenericRepository() { }

    public T Add(T entity) {
        using (var context = new SqlDbContext()) {
            context.Set<T>();
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }
    }

    public T? Get(int id) {
        using (var context = new SqlDbContext()) {
            return context.Set<T>().Find(id);
        }
    }

    public List<T> GetAll() {
        using (var context = new SqlDbContext()) {
            return context.Set<T>().AsEnumerable().ToList();
        }
    }

    public T? Update(T entity) {
        using (var context = new SqlDbContext()) {
            T? foundEntity = context.Set<T>().Find(entity.Id);
            if (foundEntity == null) { return null; }

            context.Set<T>().Entry(foundEntity).State = EntityState.Detached;
            context.Set<T>().Update(entity);

            context.SaveChanges();
            return entity;
        }
    }
}
