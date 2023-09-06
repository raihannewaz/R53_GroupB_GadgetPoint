using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly StoreContext _context;

        public PaymentRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<Payment> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> CreateAsync(Payment entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.Payments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment> DeleteAsync(Payment entity)
        {
            _context.Payments.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);

        }

        public Task<Payment> GetEntityWithSpec(ISpecification<Payment> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Payment>> ListAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public Task<IReadOnlyList<Payment>> ListAsync(ISpecification<Payment> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> UpdateAsync(int id, Payment entity)
        {
            var exentity = await _context.Payments.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
