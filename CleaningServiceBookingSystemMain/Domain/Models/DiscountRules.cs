using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class DiscountRules
    {
        public string DiscountRuleId { get; set; }
        public string Name { get; set; }
        public decimal DisPercentage { get; set; }
        public string CriteriaDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
