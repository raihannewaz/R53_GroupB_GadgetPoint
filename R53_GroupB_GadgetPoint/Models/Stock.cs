using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public int StockQuantity { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}