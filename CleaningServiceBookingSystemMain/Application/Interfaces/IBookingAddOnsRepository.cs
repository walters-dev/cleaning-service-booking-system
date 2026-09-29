using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingAddOnsRepository
    {
        void Add(BookingAddOns bookingAddOns);      //Adds an Booking Add On to storage
        void DeleteBookingAddOnByBookingId(string Id);  //Removes BookingAddOns related to a single booking id
        int GetLastRowAddOnBookings();      //Gets booking add ons count from storage
    }
}
