﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.Models
{
    public class CustomerBasket
    {
        [Key]
        public int CustomerBasketId { get; set; }
        public string CustomerId { get; set; }
        public List<BasketItem> BasketItem { get; set; } = new List<BasketItem>();

        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            CustomerId = id;
        }
    }
}