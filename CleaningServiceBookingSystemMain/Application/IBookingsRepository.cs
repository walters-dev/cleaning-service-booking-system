using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IBookingsRepository
    {
        IList<Bookings> GetBookings();
        Bookings GetBookingsById(int? Id);
        void Add(Bookings bookings);
        void Update(Bookings bookings);
        void Delete(Bookings bookings);
        IList<BookingByDate> ListByRange(DateTime startDate, DateTime endDate);
        IList<CustomerBookingHistory> BookingHistory();
        IList<BookingRevenueSummary> RevenueSummary();
        IList<BookingByHouseType> BookingsByHouseType();
        IList<BookingDiscountUsage> DiscountUsage();
    }
}
