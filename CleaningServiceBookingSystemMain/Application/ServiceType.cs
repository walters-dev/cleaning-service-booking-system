using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystem.Domain
{
    public class ServiceType
    {
        public string ServiceTypeId { get; set; } = "";

        public string ServiceName { get; set; } = "";

        public decimal ServiceRate { get; set; }

        public bool IsActive { get; set; }
    }
}
