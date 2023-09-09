
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ProductImage { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        public int SubCategoryId { get; set; }
        [JsonIgnore]
        public SubCategory? SubCategory { get; set; }

        public int BrandId { get; set; }

        [JsonIgnore]
        public Brand? Brand { get; set; }
        public bool? IsActive { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }




    }
}