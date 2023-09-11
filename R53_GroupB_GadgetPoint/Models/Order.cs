using R53_GroubB_GadgetPoint.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public enum CustomerType
    {
        Online,
        Offline
    }

    public class Order
    {
        public Order()
        {
        }

        public Order(IReadOnlyList<OrderItem> orderItems, string customerEmail, ShippingAddress shippingAddress, DeliveryMethod deliveryMethod,  decimal subtotal, string paymentIntentId)
        {
            CustomerEmail = customerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        [Key]
        public int OrderId { get; set; }

        //public int CustomerId { get; set; }
        //public Customer Customer { get; set; }

        //public string CustomerType { get; set; }

        public string CustomerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public ShippingAddress ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public IReadOnlyList<OrderItem> OrderItems { get; set; }

        public decimal Subtotal { get; set; }
        //public int PaymentId { get; set; }
        //public Payment Payment { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public string? PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }

    }
}