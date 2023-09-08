using R53_GroupB_GadgetPoint.DAL.SpecificQuery;

namespace R53_GroupB_GadgetPoint.DAL.Interfaces
{
    public interface IGenericCrud<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();


        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

    }
}
