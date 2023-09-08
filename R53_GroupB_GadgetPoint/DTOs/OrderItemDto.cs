using R53_GroubB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DTOs
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
