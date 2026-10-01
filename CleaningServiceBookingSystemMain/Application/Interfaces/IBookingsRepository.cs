using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingsRepository
    {
        IList<Bookings> GetBookings();                                              //gets list of all bookings from storage
        void Add(Bookings bookings);                                                //Adds a booking to storage
        void Update(Bookings bookings);                                             //edits an already existing booking record to storage
        IList<BookingByDate> ListByRange(DateTime startDate, DateTime endDate);     //gets list of all bookings from a specific date range from storage 
        IList<CustomerBookingHistory> BookingHistory(string phonenumber);                             //gets list of all bookings of a specific customer from storage
        IList<BookingRevenueSummary> RevenueSummary();                              //gets list of all bookings that have not been cancelled from storage
        IList<BookingByHouseType> BookingsByHouseType();                            //gets list of all bookings of a specific house type from storage
        IList<BookingDiscountUsage> DiscountUsage();                                //gets list of all Discount Usage from storage
        void ChangeBoookingStatus(Bookings bookings);       //edits the booking status of an already existing booking record to storage
        string BookingsRowCount(); //Gets bookings count from storage
        IList<Bookings> GetBookingsCreatedToday();                              //retrieves list of all created bookings made today from storage
        Bookings GetBookingsByPhoneNumberAndDate(string phonenumber, DateTime date); //retrieves booking with customers phone number and booking date from storage
    }
}
