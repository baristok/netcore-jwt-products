using System.Linq.Expressions;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace JwtApp.Back.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly UdemyJwtContext _udemyJwtContext;

    public Repository(UdemyJwtContext udemyJwtContext)
    {
        _udemyJwtContext = udemyJwtContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await this._udemyJwtContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        return await this._udemyJwtContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await this._udemyJwtContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
    }

    public async Task CreateAsync(T entity)
    {
        await this._udemyJwtContext.Set<T>().AddAsync(entity);
        await this._udemyJwtContext.SaveChangesAsync();
    }

    public Task UpdateAsync(T entity)
    {
        this._udemyJwtContext.Set<T>().Update(entity);
        return this._udemyJwtContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        this._udemyJwtContext.Set<T>().Remove(entity);
        await this._udemyJwtContext.SaveChangesAsync();
    }
}
