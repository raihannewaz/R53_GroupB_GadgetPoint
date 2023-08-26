using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreContext _context;

        public BrandRepository(StoreContext store)
        {
            _context = store;
        }

        public async Task<Brand> CreateAsync(Brand entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Brands.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Brand> DeleteAsync(Brand entity)
        {
             _context.Brands.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            return  await _context.Brands.FindAsync(id);
 
        }

        public async Task<IReadOnlyList<Brand>> ListAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand?> UpdateAsync(int id, Brand entity)
        {
             var exentity = await _context.Brands.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;
           
        }
    }
}
