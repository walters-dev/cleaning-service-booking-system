using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IBookingAddOnsRepository
    {
        IList<BookingAddOns> GetBookingAddOns();    //gets list of all Booking Add Ons from storage
        BookingAddOns bookingAddOnsByID(string? Id);   //gets a specific Booking Add On from storage
        void Add(BookingAddOns bookingAddOns);      //Adds an Booking Add On to storage
        void Update(BookingAddOns bookingAddOns);   //edits an already existing Booking Add On record to storage
    }
}
