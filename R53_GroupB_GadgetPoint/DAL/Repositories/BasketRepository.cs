using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;
using System.Text.Json;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreContext _context;

        public BasketRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var basket = await _context.CustomerBasket.Where(b => b.CustomerId == id).FirstOrDefaultAsync();

            if (basket == null)
            {
                return true;
            }

            _context.CustomerBasket.Remove(basket);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            return await _context.CustomerBasket.Where(b => b.CustomerId == id).Include(b=>b.BasketItem).FirstOrDefaultAsync();

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {

            _context.CustomerBasket.Update(basket);
            await _context.SaveChangesAsync();

            return basket;
        }


    }
}
