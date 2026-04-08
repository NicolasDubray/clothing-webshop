using Microsoft.EntityFrameworkCore;

using Services.Interfaces;

namespace EFCore.Repositories;

public class Repository<T>(WebshopDbContext context) : IRepository<T> where T : class
{
    protected readonly DbSet<T> dbSet = context.Set<T>();

    public Task<List<T>> GetAllAsync()
        => dbSet.AsNoTracking().ToListAsync();

    public Task<T?> GetByIdAsync(int id)
        => dbSet.FindAsync(id).AsTask();

    public void Add(T entity)
        => dbSet.Add(entity);

    public void Update(T entity)
        => dbSet.Update(entity);

    public void Delete(T entity)
        => dbSet.Remove(entity);
}
