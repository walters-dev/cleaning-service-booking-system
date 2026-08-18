using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class BookingAddOns
    {
        public string BookingAddOnID { get; set; }
        public string BookingID { get; set; }
        public string AddOnID { get; set; }
        public int Quantity { get; set; }
        public string LineAmount { get; set; }
    }
}
