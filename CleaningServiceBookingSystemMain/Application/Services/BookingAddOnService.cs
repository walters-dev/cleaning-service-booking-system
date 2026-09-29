using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class BookingAddOnService
    {
        private readonly IBookingAddOnsRepository _bookingAddOnsRepository;
        public BookingAddOnService(IBookingAddOnsRepository repository)
        {
            _bookingAddOnsRepository = repository;
        }
        public void RegisterBookingAddOn(BookingAddOns bookingAddOns)
        {
            _bookingAddOnsRepository.Add(bookingAddOns);
        }
        public void RemoveBookingAddOnsByBookingId(string Id)
        {
            _bookingAddOnsRepository.DeleteBookingAddOnByBookingId(Id);
        }
        public int FindLastRowAddOnBookings()
        {
            return _bookingAddOnsRepository.GetLastRowAddOnBookings();
        }
    }
}
