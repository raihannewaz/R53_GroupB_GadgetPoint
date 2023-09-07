

using R53_GroupB_GadgetPoint.DAL.SpecificQuery;

namespace R53_GroupB_GadgetPoint.DAL.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id,T entity);
        Task<T> DeleteAsync(T entity);

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

    }
}
