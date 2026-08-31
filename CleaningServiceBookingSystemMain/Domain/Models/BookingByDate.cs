using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class BookingByDate
    {
        public string BookingId { get; set; }
        public string Fullname { get; set; }
        public string HouseName { get; set; }
        public string ServiceName { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; }
    }
}
