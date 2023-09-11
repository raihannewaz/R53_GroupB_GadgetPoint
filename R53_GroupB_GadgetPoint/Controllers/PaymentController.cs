using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.Models;
using Stripe;



namespace R53_GroubB_GadgetPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentRepository rpPayment;
        private readonly ILogger logger;
        private const string WhSecret = "";

        public PaymentController(IPaymentRepository ripository, ILogger<IPaymentRepository> logger)
        {
            rpPayment = ripository;
            this.logger = logger;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)

        {
            var basket = await rpPayment.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return BadRequest("Problem with your basket");

            return basket;
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signatiure"],WhSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment Succeeded", intent.Id);
                     order = await rpPayment.UpdateOrderPaymentSucceeded(intent.Id);
                    logger.LogInformation("Order Updated to payment recieved: ", order.OrderId);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment Failed", intent.Id);
                    order = await rpPayment.UpdateOrderPaymentFailed(intent.Id);
                    logger.LogInformation("Payment failed: ",order.OrderId);
                    break;

            }
            return new EmptyResult();
        }

    }
}
