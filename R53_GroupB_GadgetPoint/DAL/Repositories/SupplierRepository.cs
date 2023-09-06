using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class SupplierRepository: ISupplierRepository
    {
        private readonly StoreContext _context;

        public SupplierRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<Supplier> spec)
        {
            throw new NotImplementedException();
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

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _context.Suppliers.FindAsync(id);

        }

        public Task<Supplier> GetEntityWithSpec(ISpecification<Supplier> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Supplier>> ListAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public Task<IReadOnlyList<Supplier>> ListAsync(ISpecification<Supplier> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Supplier> UpdateAsync(int id, Supplier entity)
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
