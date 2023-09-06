using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly StoreContext _context;

        public CustomerRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<Customer> spec)
        {
            throw new NotImplementedException();
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

        public Task<Customer> GetEntityWithSpec(ISpecification<Customer> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Customer>> ListAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public Task<IReadOnlyList<Customer>> ListAsync(ISpecification<Customer> spec)
        {
            throw new NotImplementedException();
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
