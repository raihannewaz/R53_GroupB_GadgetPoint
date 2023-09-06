using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class DeliveryMethodRepository : IDeliveryMethodRepository
    {
        private readonly StoreContext _context;

        public DeliveryMethodRepository(StoreContext store)
        {
            _context = store;
        }

        public Task<int> CountAsync(ISpecification<DeliveryMethod> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryMethod> CreateAsync(DeliveryMethod entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _context.DeliveryMethods.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeliveryMethod> DeleteAsync(DeliveryMethod entity)
        {
            _context.DeliveryMethods.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeliveryMethod> GetByIdAsync(int id)
        {
            return await _context.DeliveryMethods.FindAsync(id);

        }

        public Task<DeliveryMethod> GetEntityWithSpec(ISpecification<DeliveryMethod> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<DeliveryMethod>> ListAllAsync()
        {
            return await _context.DeliveryMethods.ToListAsync();
        }

        public Task<IReadOnlyList<DeliveryMethod>> ListAsync(ISpecification<DeliveryMethod> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryMethod> UpdateAsync(int id, DeliveryMethod entity)
        {
            var exentity = await _context.DeliveryMethods.FindAsync(id);
            if (exentity != null)
            {
                _context.Entry(exentity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exentity;

        }
    }
}
