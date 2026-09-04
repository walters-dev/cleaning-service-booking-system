using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IBookingsRepository
    {
        IList<Bookings> GetBookings();                                              //gets list of all bookings from storage
        Bookings GetBookingsById(string? Id);                                          //gets a specific booking from storage
        void Add(Bookings bookings);                                                //Adds a booking to storage
        void Update(Bookings bookings);                                             //edits an already existing booking record to storage
        void Delete(Bookings bookings);                                             //deletes an booking from storage
        IList<BookingByDate> ListByRange(DateTime startDate, DateTime endDate);     //gets list of all bookings from a specific date range from storage 
        IList<CustomerBookingHistory> BookingHistory();                             //gets list of all bookings of a specific customer from storage
        IList<BookingRevenueSummary> RevenueSummary();                              //gets list of all bookings that have not been cancelled from storage
        IList<BookingByHouseType> BookingsByHouseType();                            //gets list of all bookings of a specific house type from storage
        IList<BookingDiscountUsage> DiscountUsage();                                //gets list of all bookings from storage....................................................
    }
}
