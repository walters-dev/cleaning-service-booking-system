using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class Customers
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhyAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatdeBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
