using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class CustomerRepository
    {
        private readonly StoreContext _context;

        public CustomerRepository(StoreContext store)
        {
            _context = store;
        }

        public async Task<Customer> CreateAsync(Customer entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer> DeleteAsync(Customer entity)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);

        }

        public async Task<IReadOnlyList<Customer>> ListAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateAsync(int id, Customer entity)
        {
            var exentity = await _context.Customers.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
