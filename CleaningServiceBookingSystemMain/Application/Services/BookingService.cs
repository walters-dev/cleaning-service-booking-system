using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingsRepository _bookingRepository;
        public BookingService(IBookingsRepository repository)//prevents the service from running without its dependency
        {
            _bookingRepository = repository;
        }
        public IList<Bookings> ViewAllBookings()
        {
            return _bookingRepository.GetBookings();
        }
        public void RegisterBooking(Bookings booking)
        {
            _bookingRepository.Add(booking);
        }
        public void AmendBooking(Bookings booking)
        {
            _bookingRepository.Update(booking);
        }
        public IList<BookingByDate> FindAllBookingsInDateRange(DateTime startDate, DateTime endDate)
        {
            return _bookingRepository.ListByRange(startDate, endDate);
        }
        public IList<CustomerBookingHistory> FindCustomerBookingHistory(string phonenumber)
        {
            return _bookingRepository.BookingHistory(phonenumber);
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
        public Bookings FindBookingsByPhoneNumberAndDate(string phonenumber, DateTime date)
        {
            return _bookingRepository.GetBookingsByPhoneNumberAndDate(phonenumber, date);
        }
    }
}
