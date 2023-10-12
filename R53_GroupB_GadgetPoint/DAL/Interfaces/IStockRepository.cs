using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Interfaces
{
    public interface IStockRepository : IGenericRepository<Stock>
    {
        Task<Stock> UpdateStockQuantityAsync(int id, int entity);
    }
}
