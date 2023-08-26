using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class SupplierRepository: ISupplierRepository
    {
        private readonly StoreContext _context;

        public SupplierRepository(StoreContext store)
        {
            _context = store;
        }

        public async Task<Supplier> CreateAsync(Supplier entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Suppliers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Supplier> DeleteAsync(Supplier entity)
        {
            _context.Suppliers.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);

        }

        public async Task<IReadOnlyList<Supplier>> ListAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> UpdateAsync(int id, Supplier entity)
        {
            var exentity = await _context.Suppliers.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
