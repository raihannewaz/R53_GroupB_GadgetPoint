using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class SubCategoryRepository:ISubCategoryRepository
    {
        private readonly StoreContext _context;

        public SubCategoryRepository(StoreContext store)
        {
            _context = store;
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

        public async Task<IReadOnlyList<SubCategory>> ListAllAsync()
        {
            return await _context.SubCategories.ToListAsync();
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
