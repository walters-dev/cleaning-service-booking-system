using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingAddOnService
    {
        void RegisterBookingAddOn(BookingAddOns bookingAddOns);//registers booking add on to system
        public void RemoveBookingAddOnsByBookingId(string Id);//deletes 1 booking add on 
        public int FindLastRowAddOnBookings();// returns the last row primary key number 
    }
}
