using R53_GroupB_GadgetPoint.Models;
using System.Linq.Expressions;

namespace R53_GroupB_GadgetPoint.DAL.SpecificQuery
{
    public class SpecificOrder : BaseSpecification<Order>
    {
        public SpecificOrder(string email):base(o=>o.CustomerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDesc(o => o.OrderDate);
        }

        public SpecificOrder(int id, string email) : base(o=>o.OrderId==id && o.CustomerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
