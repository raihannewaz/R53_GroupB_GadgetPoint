using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Interfaces
{
    public interface IPurchaseRepository : IGenericRepository<PurchaseProduct>
    {
        Task<PurchaseProduct> UpdateStockQuantityAsync(int id, int entity);
    }
}
