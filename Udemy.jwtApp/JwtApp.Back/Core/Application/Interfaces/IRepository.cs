using System.Linq.Expressions;

namespace JwtApp.Back.Core.Application.Interfaces;

public interface IRepository <T> where T : class, new()
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate);
    
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}