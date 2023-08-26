using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext store)
        {
            _context = store;
        }

        public async Task<Product> CreateAsync(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> DeleteAsync(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.Include(c=>c.Category)
                .Include(sc=>sc.SubCategory)
                .Include(b=>b.Brand)
                .FirstOrDefaultAsync(p=>p.ProductId==id);

        }

        public async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            return await _context.Products.Include(c => c.Category)
                .Include(sc => sc.SubCategory)
                .Include(b => b.Brand).ToListAsync();
        }

        public async Task<Product?> UpdateAsync(int id, Product entity)
        {
            var exentity = await _context.Products.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
