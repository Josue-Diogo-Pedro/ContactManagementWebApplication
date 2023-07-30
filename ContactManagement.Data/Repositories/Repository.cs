using ContactManagement.Business.Interfaces;
using ContactManagement.Business.Models;
using ContactManagement.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactManagement.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ManagementAppContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(ManagementAppContext context)
    {
        Db = context;
        DbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task Add(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChanges();
    }

    public async Task Remove(Guid id)
    {
        TEntity contact = await GetById(id);
        DbSet.Remove(contact);
        await SaveChanges();
    }

    public async Task<int> SaveChanges()
    {
        return await Db.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChanges();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
