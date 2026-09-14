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
        public IList<BookingAddOns> ViewAllBookingAddOns()
        {
            return _bookingAddOnsRepository.GetBookingAddOns();
        }
        public BookingAddOns FindBookingAddOn(string? bookingAddOnId)
        {
            return _bookingAddOnsRepository.bookingAddOnsByID(bookingAddOnId);
        }
        public void RegisterBookingAddOn(BookingAddOns bookingAddOns)
        {
            _bookingAddOnsRepository.Add(bookingAddOns);
        }
        public void AmendBookingAddOn(BookingAddOns bookingAddOns)
        {
            _bookingAddOnsRepository.Update(bookingAddOns);
        }
        public string FindBookingAddOnCount()
        {
            return _bookingAddOnsRepository.BookingAddOnsRowCount();
        }
    }
}
