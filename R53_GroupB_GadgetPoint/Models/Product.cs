using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProdcutName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ProductImage { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }


    }
}