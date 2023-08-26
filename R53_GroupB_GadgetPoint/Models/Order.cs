using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public enum CustomerType
    {
       Online,
       Offline
    }

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string? CustomerType{ get; set; }

        public DateTime OrderDate { get; set; }

        public string? ShippingAddress { get; set; }


        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public virtual List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    }
}