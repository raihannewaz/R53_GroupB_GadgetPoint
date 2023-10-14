using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public int StockQuantity { get; set; }
    }
}
