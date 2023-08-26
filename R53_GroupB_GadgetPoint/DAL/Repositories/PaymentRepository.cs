using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using Project_Entity.Models;
using R53_GroupB_GadgetPoint.DAL.Interface;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly StoreContext _context;

        public PaymentRepository(StoreContext store)
        {
            _context = store;
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

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);

        }

        public async Task<IReadOnlyList<Payment>> ListAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> UpdateAsync(int id, Payment entity)
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
