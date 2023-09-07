using Microsoft.AspNetCore.Server.IIS.Core;
using R53_GroubB_GadgetPoint.Models;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;
using System.Runtime.CompilerServices;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
       
        private readonly IDeliveryMethodRepository dmRepo;
        private readonly IProductRepository pRepo;
        private readonly IBasketRepository bRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(IDeliveryMethodRepository dmRepo, IProductRepository pRepo, IBasketRepository bRepo,IUnitOfWork unitOfWork)
        {
         
            this.dmRepo = dmRepo;
            this.pRepo = pRepo;
            this.bRepo = bRepo;
            this._unitOfWork = unitOfWork;
        }


        public async Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethodId, string id, ShippingAddress shippingAddress)
        {
            //basket
            var basket = await bRepo.GetBasketAsync(id);

            // product
            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItem)
            {
                var productItem = await pRepo.GetByIdAsync(item.BasketItemId);
                var itemOrderd = new ProductItemOrdered(productItem.ProductId,productItem.ProductName, productItem.ProductImage);
                var orderItem = new OrderItem(itemOrderd, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            //delivary
            var delivaryMethod = await dmRepo.GetByIdAsync(deliveryMethodId);


            //calculation

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            //create order
            var order = new Order(items,customerEmail,shippingAddress,delivaryMethod,subTotal);

            //save order
           _unitOfWork.Repository<Order>().Add(order);


            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            await bRepo.DeleteBasketAsync(id);

            return order;
        }


        public Task<IReadOnlyList<DeliveryMethod>> GetDelivaryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string customerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
