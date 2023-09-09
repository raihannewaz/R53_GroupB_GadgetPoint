
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace R53_GroupB_GadgetPoint.Models
{
    public enum InspectionType
    {
        AllOk,
        Modified
    }

    public class Inspection
    {
        [Key]
        public int InspectionId { get; set; }

        public int RequistionId { get; set; }
        public Requisition? Requisition { get; set; }

        public DateTime InspectionDate { get; set; }

        public string? InspectionNote { get; set; }

        public string? InsepectionStatus { get; set; } //enum
    }
}