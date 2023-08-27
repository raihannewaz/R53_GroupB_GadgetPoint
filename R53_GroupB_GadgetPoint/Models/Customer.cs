using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FarstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int ContactNo { get; set; }
        public string Address { get; set; }


    }
}