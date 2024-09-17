namespace API.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    
    IQueryable<T> GetQueryable();
}