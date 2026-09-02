using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystem.Domain
{
    public class Addon
    {
        public string AddOnId { get; set; } = "";
        public string AddOnName { get; set; } = "";
        public decimal Rate { get; set; } 
        public string PricingType { get; set; } = "";
        public bool IsActive { get; set; } 
    }
}
