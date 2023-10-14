using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly StoreContext _context;

        public PurchaseRepository(StoreContext storeContext)
        {
            _context = storeContext;
        }



        public async Task<PurchaseProduct> CreateAsync(PurchaseProduct entity)
        {

            var existingStock = await _context.Stock.SingleOrDefaultAsync(s => s.ProductId == entity.ProductId);

            if (existingStock == null)
            {
                var newStock = new Stock
                {
                    ProductId = entity.ProductId,
                    StockQuantity = entity.PurchaseQuantity
                };

                _context.Stock.Add(newStock);
            }
            else
            {
                existingStock.StockQuantity += entity.PurchaseQuantity;
                _context.Entry(existingStock).State = EntityState.Modified;
            }

            await _context.PurchaseProducts.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }



        public async Task<PurchaseProduct> DeleteAsync(PurchaseProduct entity)
        {
            _context.PurchaseProducts.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<PurchaseProduct> GetByIdAsync(int id)
        {
            return await _context.PurchaseProducts.Include(c => c.Product)
               .Include(sc => sc.Supplier)
               .FirstOrDefaultAsync(p => p.PurchaseId == id);
        }

        public Task<PurchaseProduct> GetEntityWithSpec(ISpecification<PurchaseProduct> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<PurchaseProduct>> ListAllAsync()
        {
            return await _context.PurchaseProducts.Include(c => c.Product)
                .Include(sc => sc.Supplier)
                .ToListAsync();
        }

        public Task<IReadOnlyList<PurchaseProduct>> ListAsync(ISpecification<PurchaseProduct> spec)
        {
            throw new NotImplementedException();
        }
        public async Task<PurchaseProduct> UpdateAsync(int id, PurchaseProduct entity)
        {
            var exentity = await _context.PurchaseProducts.FindAsync(id);

            if (exentity == null)
            {
                return null;
            }

            exentity.PurchaseDate = entity.PurchaseDate;
            exentity.PurchaseQuantity = entity.PurchaseQuantity;
            exentity.PurchasePrice = entity.PurchasePrice;   

            await _context.SaveChangesAsync();

            return exentity;
        }


        public Task<int> CountAsync(ISpecification<PurchaseProduct> spec)
        {
            throw new NotImplementedException();
        }



        public async Task<Stock> UpdateStockQuantityAsync(int id, int quantityChange)
        {
            var exentity = await _context.Stock.FindAsync(id);

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
