using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;

namespace R53_GroupB_GadgetPoint.DAL.Interface
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Product> GetSpecProduct(ISpecification<Product> spec);

        Task<IReadOnlyList<Product>> GetAllProduct(ISpecification<Product> spec);
    }
}
