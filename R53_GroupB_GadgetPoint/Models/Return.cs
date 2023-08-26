using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public enum ReturnPolicy
    {
        Damaged,
        IncorrectItem,
        MissingAccessories
    }

    public class Return
    {
        [Key]
        public int ReturnId { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string? ReturnReason { get; set; }

        
    }
}