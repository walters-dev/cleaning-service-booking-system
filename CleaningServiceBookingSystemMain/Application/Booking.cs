using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystem.Domain
{
    public class Booking
    {
        public string BookingId { get; set; } = "";

        public HouseType? HouseType { get; set; }

        public ServiceType? ServiceType { get; set; }

        public int NumberOfRooms { get; set; }

        public DateTime BookingDate { get; set; }

        public int CarpetedRooms { get; set; }

        public bool IsRecurring { get; set; }

        public string RecurringBookingType { get; set; } = "";
    }
}
