using Microsoft.EntityFrameworkCore;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.SpecificQuery;
using R53_GroupB_GadgetPoint.Models;
using Stripe;
using Product = R53_GroupB_GadgetPoint.Models.Product;

namespace R53_GroupB_GadgetPoint.DAL.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
  
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentRepository(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
            this._configuration = configuration;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var basket =await _basketRepository.GetBasketAsync(basketId);
            if (basket == null)
            {
                return null;
            }
            var shippingPrice = 0m;
            if (basket.DelivaryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DelivaryMethodId);
                if (deliveryMethod != null)
                {
                    shippingPrice = deliveryMethod.Price;
                }
            }


            foreach (var item in basket.BasketItem)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.BasketItemId);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItem.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "BDT",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(option);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItem.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                };
                await service.UpdateAsync(basket.PaymentIntentId, option);
            }



            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order == null)
            {
                return null;
            }
            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.Complete();

            return null;

        }
    }
}
