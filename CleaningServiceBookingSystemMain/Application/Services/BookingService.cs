using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class BookingService
    {
        private readonly IBookingsRepository _bookingRepository;
        public BookingService(IBookingsRepository repository)
        {
            _bookingRepository = repository;
        }
        public IList<Bookings> ViewAllBookings()
        {
            return _bookingRepository.GetBookings();
        }
        public Bookings FindBooking(string? bookingId)
        {
            return _bookingRepository.GetBookingsById(bookingId);
        }
        public void RegisterBooking(Bookings booking)
        {
            _bookingRepository.Add(booking);
        }
        public void AmendBooking(Bookings booking)
        {
            _bookingRepository.Update(booking);
        }
        public void DeleteBooking(Bookings booking)
        {
            _bookingRepository.Delete(booking);
        }
        public IList<BookingByDate> FindAllBookingsInDateRange(DateTime startDate, DateTime endDate)
        {
            return _bookingRepository.ListByRange(startDate, endDate);
        }
        public IList<CustomerBookingHistory> FindCustomerBookingHistory(string email)
        {
            return _bookingRepository.BookingHistory(email);
        }
        public IList<BookingRevenueSummary> ViewRevenueSummary()
        {
            return _bookingRepository.RevenueSummary();
        }
        public IList<BookingByHouseType> ViewBookingsByHouse()
        {
            return _bookingRepository.BookingsByHouseType();
        }
        public IList<BookingDiscountUsage> ViewDiscountUsage()
        {
            return _bookingRepository.DiscountUsage();
        }
        public void AmendBookingStatus(Bookings booking)
        {
            _bookingRepository.ChangeBoookingStatus(booking);
        }
        public string FindBookingCount()
        {
            return _bookingRepository.BookingsRowCount();
        }
        public IList<Bookings> ViewBookingsCreatedToday()
        {
            return _bookingRepository.GetBookingsCreatedToday();
        }
    }
}
