using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class AddOns
    {
        public string AddOnID { get; set; }
        public string AddOnsName { get; set; }
        public decimal Rate { get; set; }
        public string PricingType { get; set; }
        public bool IsActive { get; set; }
    }
}
