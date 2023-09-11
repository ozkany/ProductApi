using ProductApi.Domain.Models;

namespace ProductApi.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<IList<T>> ListAllAsync();
        //Task<IList<T>> ListAsync(ISpecification<T> spec);
        //Task<T?> FirstOrDefaultAsync(ISpecification<T?> spec);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //Task<int> CountAsync(ISpecification<T> spec);

        Task<int> SaveChangesAsync();
    }
}
