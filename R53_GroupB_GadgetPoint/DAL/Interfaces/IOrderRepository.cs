using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.Interface
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethod, string id, ShippingAddress shippingAddress);

        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail);

        Task<Order> GetOrderByIdAsync(int id, string customerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDelivaryMethodAsync();
    }
}
