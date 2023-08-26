﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_Entity.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? Email { get; set; }
        public int ContactNo { get; set; }
        public string? Address { get; set; }



    }
}