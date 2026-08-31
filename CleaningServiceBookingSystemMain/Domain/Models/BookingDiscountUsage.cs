using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class BookingDiscountUsage
    {
        public string DiscountName { get; set; }
        public string BookingId { get; set; }
        public string Fullname { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal AmountAfterDiscount { get; set; }
    }
}
