using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IBookingAddOnsRepository
    {
        IList<BookingAddOns> GetBookingAddOns();
        BookingAddOns bookingAddOnsByID(int? Id);
        void Add(BookingAddOns bookingAddOns);
        void Update(BookingAddOns bookingAddOns);
    }
}
