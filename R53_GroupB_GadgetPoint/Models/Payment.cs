using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }


    }
}