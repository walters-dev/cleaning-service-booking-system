using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class BookingAddOns
    {
        public string BookingAddOnId { get; set; }
        public string BookingId { get; set; }
        public string AddOnId { get; set; }
        public int Quantity { get; set; }
        public decimal LineAmount { get; set; }
    }
}
