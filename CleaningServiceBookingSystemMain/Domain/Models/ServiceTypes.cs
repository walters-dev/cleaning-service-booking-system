using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class ServiceTypes
    {
        public string ServiceTypeID { get; set; }
        public decimal Multiplier { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
