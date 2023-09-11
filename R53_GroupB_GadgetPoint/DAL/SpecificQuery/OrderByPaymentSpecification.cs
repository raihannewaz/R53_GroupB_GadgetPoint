using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DAL.SpecificQuery
{
    public class OrderByPaymentSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentSpecification(string paymentInetntId):base(o=>o.PaymentIntentId==paymentInetntId)
        {
        }
    }
}
