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
       

        private readonly IProductRepository pRepo;
        private readonly IBasketRepository bRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        IGenericCrud<DeliveryMethod> _method;

        public OrderRepository(IGenericCrud<DeliveryMethod> method, IProductRepository pRepo, IBasketRepository bRepo,IUnitOfWork unitOfWork, IPaymentRepository paymentRepository)
        {
            _method = method;
            this.pRepo = pRepo;
            this.bRepo = bRepo;
            this._unitOfWork = unitOfWork;
            this._paymentRepository = paymentRepository;
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
            var delivaryMethod = await _method.GetByIdAsync(deliveryMethodId);


            //calculation

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            //payment check
            var spec = new OrderByPaymentSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (existingOrder != null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentRepository.CreateOrUpdatePaymentIntent(basket.PaymentIntentId);
            }

            //create order
            var order = new Order(items,customerEmail,shippingAddress,delivaryMethod,subTotal,basket.PaymentIntentId);

            //save order
           _unitOfWork.Repository<Order>().Add(order);


            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                return null;
            }

            return order;
        }


        public async Task<IReadOnlyList<DeliveryMethod>> GetDelivaryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string customerEmail)
        {
            var spec = new SpecificOrder(id, customerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);   
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string customerEmail)
        {
            var spec = new SpecificOrder(customerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
