using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingService
    {
        IList<Bookings> ViewAllBookings();//returns all bookings
        void RegisterBooking(Bookings booking);//registers 1 booking to the system
        void AmendBooking(Bookings booking);//edits a preexisting booking in the system
        IList<BookingByDate> FindAllBookingsInDateRange(DateTime startDate, DateTime endDate);//returns all bookings in date range
        IList<CustomerBookingHistory> FindCustomerBookingHistory(string phonenumber);//returns all bookings related to specified customer
        IList<BookingRevenueSummary> ViewRevenueSummary();//returns all bookings with completed status
        IList<BookingByHouseType> ViewBookingsByHouse();//returns number of bookings grouped by house type
        IList<BookingDiscountUsage> ViewDiscountUsage();//returns all bookings discount information
        void AmendBookingStatus(Bookings booking);//edits a preexisting bookings status in the system
        string FindBookingCount();//finds count of admins in system
        IList<Bookings> ViewBookingsCreatedToday();//returns all bookings created today
        Bookings FindBookingsByPhoneNumberAndDate(string phonenumber, DateTime date);//returns 1 booking via customer phone number and booking date
    }
}
