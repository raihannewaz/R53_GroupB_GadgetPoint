using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string? PaymentMethod { get; set; }


    }
}