using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace R53_GroupB_GadgetPoint.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }

    }
}