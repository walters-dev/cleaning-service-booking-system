using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingAddOnService
    {
        void RegisterBookingAddOn(BookingAddOns bookingAddOns);
        public void RemoveBookingAddOnsByBookingId(string Id);
        public int FindLastRowAddOnBookings();
    }
}
