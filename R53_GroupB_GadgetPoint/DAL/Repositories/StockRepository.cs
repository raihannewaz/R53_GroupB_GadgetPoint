using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StoreContext _context;

        public StockRepository(StoreContext storeContext)
        {
            _context = storeContext;
        }

        public async Task<Stock> CreateAsync(Stock entity)
        {
           entity.PurchaseDate = DateTime.Now;
           await _context.Stocks.AddAsync(entity);
           await _context.SaveChangesAsync();
           return entity;

        }

        public async Task<Stock> DeleteAsync(Stock entity)
        {
            _context.Stocks.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Stock> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Product)
               .Include(sc => sc.Supplier)
               .FirstOrDefaultAsync(p => p.StockId == id);
        }

        public Task<Stock> GetEntityWithSpec(ISpecification<Stock> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Stock>> ListAllAsync()
        {
            return await _context.Stocks.Include(c => c.Product)
                .Include(sc => sc.Supplier)
                .ToListAsync();
        }

        public Task<IReadOnlyList<Stock>> ListAsync(ISpecification<Stock> spec)
        {
            throw new NotImplementedException();
        }
        public async Task<Stock> UpdateAsync(int id, Stock entity)
        {
            var exentity = await _context.Stocks.FindAsync(id);

            if (exentity == null)
            {
                return null;
            }

            exentity.PurchaseDate = entity.PurchaseDate;
            exentity.StockQuantity = entity.StockQuantity;
            exentity.PurchasePrice = entity.PurchasePrice;   

            await _context.SaveChangesAsync();

            return exentity;
        }


        public Task<int> CountAsync(ISpecification<Stock> spec)
        {
            throw new NotImplementedException();
        }

        //public async Task<Stock> UpdateStockQuantityAsync(int id, Stock entity)
        //{
        //    var exentity = await _context.Stocks.FindAsync(id);

        //    if (exentity == null)
        //    {
        //        return null;
        //    }

        //    exentity.StockQuantity = entity.StockQuantity;
        //    await _context.SaveChangesAsync();
        //    return exentity;
        //}



        public async Task<Stock> UpdateStockQuantityAsync(int id, int quantityChange)
        {
            var exentity = await _context.Stocks.FindAsync(id);

            if (exentity == null)
            {
                return null;
            }

            exentity.StockQuantity += quantityChange;
           // _context.Entry(exentity).State = EntityState.Modified;


            await _context.SaveChangesAsync();
            return exentity;
        }

    }
}
