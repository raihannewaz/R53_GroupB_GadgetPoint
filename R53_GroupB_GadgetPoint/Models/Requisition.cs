using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public class Requisition
    {
        [Key]
        public int RequisitionId { get; set; }

        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int SubCategoryId { get; set; }

        public SubCategory? SubCategory { get; set; }

        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int Quantity { get; set; }

    }
}