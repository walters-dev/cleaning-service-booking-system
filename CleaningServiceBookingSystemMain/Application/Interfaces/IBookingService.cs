using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IBookingService
    {
        IList<Bookings> ViewAllBookings();
        void RegisterBooking(Bookings booking);
        void AmendBooking(Bookings booking);
        IList<BookingByDate> FindAllBookingsInDateRange(DateTime startDate, DateTime endDate);
        IList<CustomerBookingHistory> FindCustomerBookingHistory(string phonenumber);
        IList<BookingRevenueSummary> ViewRevenueSummary();
        IList<BookingByHouseType> ViewBookingsByHouse();
        IList<BookingDiscountUsage> ViewDiscountUsage();
        void AmendBookingStatus(Bookings booking);
        string FindBookingCount();
        IList<Bookings> ViewBookingsCreatedToday();
        Bookings FindBookingsByPhoneNumberAndDate(string phonenumber, DateTime date);
    }
}
