using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace R53_GroupB_GadgetPoint.Models
{
    public class CustomerBasket
    {
        [Key]
        [Column(TypeName = "nvarchar(100)")]
        public string CustomerId { get; set; }
        public List<BasketItem> BasketItem { get; set; } = new List<BasketItem>();

        public int? DelivaryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }

        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            CustomerId = id;
        }
    }
}
