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

    public async Task AddAsync(T entity)
    {
        dbSet.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }
}

