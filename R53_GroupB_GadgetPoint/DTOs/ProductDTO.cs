using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.DTOs
{
    public class ProductDTO
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Brand { get; set; }
        public bool IsActive { get; set; }

    }
}