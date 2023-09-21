using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace R53_GroupB_GadgetPoint.Models
{
    public class BasketItem
    {
        [Key]
        public int BasketItemId { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string? PicUrl { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }

        public string? CustomerId { get; set; }

        [JsonIgnore]
        public CustomerBasket? CustomerBasket { get; set; }
    }
}
