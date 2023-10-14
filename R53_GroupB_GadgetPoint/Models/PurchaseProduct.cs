using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public class PurchaseProduct
    {
        [Key]
        public int PurchaseId { get; set; }
        public DateTimeOffset PurchaseDate { get; set; } = DateTimeOffset.Now;

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public int PurchaseQuantity { get; set; }

        public decimal PurchasePrice { get; set; }
    }
}