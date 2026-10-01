using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class BookingAddOnService : IBookingAddOnService
    {
        private readonly IBookingAddOnsRepository _bookingAddOnsRepository;
        public BookingAddOnService(IBookingAddOnsRepository repository)//prevents the service from running without its dependency
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
        public int FindLastPrimaryKeyAddOnBookings()
        {
            return _bookingAddOnsRepository.GetLastPrimaryKeyAddOnBookings();
        }
        public IList<BookingAddOns> FindBookingAddOnsByBookingId(string Id)
        {
            return _bookingAddOnsRepository.GetBookingAddOnsByBookingId(Id);
        }
    }
}
