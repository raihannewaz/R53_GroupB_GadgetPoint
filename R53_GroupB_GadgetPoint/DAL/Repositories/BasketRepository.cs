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
            var basket = await _context.CustomerBasket.Include(a=>a.BasketItem).FirstOrDefaultAsync(b => b.CustomerId == id);

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
            return await _context.CustomerBasket.Include(b=>b.BasketItem).FirstOrDefaultAsync(a=>a.CustomerId==id);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            CustomerBasket existingBasket = await _context.CustomerBasket.FirstOrDefaultAsync(b => b.CustomerId == basket.CustomerId);

            if (existingBasket != null)
            {

                foreach (var basketItem in basket.BasketItem)
                {
                    basketItem.CustomerBasket = existingBasket;
                }
                _context.BasketItems.UpdateRange(basket.BasketItem);
            }
            else
            {
                foreach (var basketItem in basket.BasketItem)
                {
                    basketItem.CustomerBasket = basket;
                }
                _context.CustomerBasket.Add(basket);
            }

            await _context.SaveChangesAsync();

            return basket;
        }

        public async Task DeleteBasketItem(int id)
        {
            var basket = await _context.BasketItems.FirstOrDefaultAsync(b => b.BasketItemId == id);
            _context.BasketItems.Remove(basket);
            await _context.SaveChangesAsync();
         
        }

    }
}
