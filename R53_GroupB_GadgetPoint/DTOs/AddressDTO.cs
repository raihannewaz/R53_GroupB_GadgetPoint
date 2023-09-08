using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.DTOs
{
    public class AddressDTO
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }
    }
}
