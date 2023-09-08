using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.DTOs
{
    public class OrderDto
    {
        public string? BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO? ShipToAddress { get; set; }
    }
}
