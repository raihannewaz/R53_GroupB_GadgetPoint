using R53_GroupB_GadgetPoint.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroubB_GadgetPoint.Models
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
     
        }

        [Key]
        public int OrderDetailId { get; set; }
        //public int OrderId { get; set;}
        //public Order Order { get; set; }

        //public int ProductId { get; set; }
        //public Product Product { get; set; }

        public ProductItemOrdered? ItemOrdered { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}