using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class SubCategoryRepository:ISubCategoryRepository
    {
        private readonly StoreContext _context;

        public SubCategoryRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<SubCategory> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> CreateAsync(SubCategory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.SubCategories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SubCategory> DeleteAsync(SubCategory entity)
        {
            _context.SubCategories.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SubCategory> GetByIdAsync(int id)
        {
            return await _context.SubCategories.FindAsync(id);

        }

        public Task<SubCategory> GetEntityWithSpec(ISpecification<SubCategory> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<SubCategory>> ListAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public Task<IReadOnlyList<SubCategory>> ListAsync(ISpecification<SubCategory> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> UpdateAsync(int id, SubCategory entity)
        {
            var exentity = await _context.SubCategories.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
