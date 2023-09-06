using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreContext _context;

        public BrandRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<Brand> spec)
        {
            throw new NotImplementedException();
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

        public async Task<Brand> GetByIdAsync(int id)
        {
            return  await _context.Brands.FindAsync(id);
 
        }

        public Task<Brand> GetEntityWithSpec(ISpecification<Brand> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Brand>> ListAllAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public Task<IReadOnlyList<Brand>> ListAsync(ISpecification<Brand> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Brand> UpdateAsync(int id, Brand entity)
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
