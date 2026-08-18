using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class Bookings
    {
        public string BookingId { get; set; }
        public string CustomerId { get; set; }
        public string HouseTypeId { get; set; }
        public string ServiceTypeId { get; set; }
        public string DiscountRuleId { get; set; }
        public DateTime BookingDate { get; set; }
        public int NumberOfRooms { get; set; }
        public bool IsRecurring { get; set; }
        public string RecurringBookingType { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string BookingStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatdeBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
