using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? DelivaryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

    }
}