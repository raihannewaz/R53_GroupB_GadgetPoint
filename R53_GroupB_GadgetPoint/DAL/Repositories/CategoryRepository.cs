using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly StoreContext _context;

        public CategoryRepository(StoreContext store)
        {
            _context = store;
        }

        public async Task<Category> CreateAsync(Category entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> DeleteAsync(Category entity)
        {
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);

        }

        public async Task<IReadOnlyList<Category>> ListAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var exentity = await _context.Categories.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
