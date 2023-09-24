using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Interface
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);

        Task DeleteBasketItem(int id);
    }
}
