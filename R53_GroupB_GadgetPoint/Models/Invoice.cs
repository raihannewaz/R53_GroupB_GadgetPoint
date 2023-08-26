using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public decimal TotalPrice { get; set; } 


    }
}