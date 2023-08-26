using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Entity.Models
{
    public class SubCategory
    {
       [Key]
       public int SubCategoryId { get; set; }
       public string? SubCategoryName { get; set;}



    }
}