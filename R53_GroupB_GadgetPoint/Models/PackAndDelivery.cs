using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public enum OrderStatus
    {
        Processing,
        Shipped,
        Delivered

    }

    public class PackAndDelivery
    {
        [Key]
        public int PackAndDeliveryId { get; set; }

        public string? DeliveryStatus { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}