using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroubB_GadgetPoint.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly StoreContext _context;
        private readonly DbSet<T> _dbSet;


        public GenericRepository(StoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();

        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }

            return existingEntity;
        }


        public async Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }


}
