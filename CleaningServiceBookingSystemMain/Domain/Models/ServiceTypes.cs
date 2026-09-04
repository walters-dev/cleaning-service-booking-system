using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class ServiceTypes
    {
        public string ServiceTypeId { get; set; }
        public string ServiceName { get; set; }
        public decimal Multiplier { get; set; }
        public string ServiceDescription { get; set; }
        public bool? IsActive { get; set; }//? because the bool in the database is sometimes null
    }
}
