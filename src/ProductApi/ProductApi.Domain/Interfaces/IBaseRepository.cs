using ProductApi.Domain.Models;

namespace ProductApi.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
    }
}
