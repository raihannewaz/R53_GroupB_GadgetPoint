using R53_GroupB_GadgetPoint.Models;
using System.Linq.Expressions;

namespace R53_GroupB_GadgetPoint.DAL.SpecificQuery
{
    public class SpecifiedOrder : BaseSpecification<Order>
    {
        public SpecifiedOrder(string email):base(o=>o.CustomerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDesc(o => o.OrderDate);
        }

        public SpecifiedOrder(int id, string email) : base(o=>o.OrderId==id && o.CustomerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
