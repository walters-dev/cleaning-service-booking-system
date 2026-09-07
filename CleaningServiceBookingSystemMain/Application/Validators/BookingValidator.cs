using System;
using System.Text.RegularExpressions;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystem.Application.Validators
{
    /* SUMMARY:
     * Validates Customer and Booking data before it is priced or saved
    */
    public class BookingValidator
    {
        // Validates a Customer record.
        public bool ValidateCustomer(
            Customers customer,
            out string errorMessage)
        {
            // BRD 15: Names may not be blank
            if (string.IsNullOrWhiteSpace(customer.FullName))
            {
                errorMessage = "Customer name is required.";
                return false;
            }

            // BRD 15: Phone numbers must not be blank 
            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                errorMessage = "Phone number is required.";
                return false;
            }

            // BRD
            if (!Regex.IsMatch(
                customer.PhoneNumber,
                @"^[0-9]{10}$"))
            {
                errorMessage =
                    "Phone number must contain exactly 10 digits.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                if (!Regex.IsMatch(
                    customer.Email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    errorMessage = "Invalid email format.";
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(customer.PhyAddress))
            {
                errorMessage = "Address is required.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }


        // Validates a Booking record.
        public bool ValidateBooking(
            Bookings booking, HouseTypes houses,
            out string errorMessage)
        {
            // 
            if (booking.HouseType == null)
            {
                errorMessage = "A house type must be selected.";
                return false;
            }

            if (booking.ServiceType == null)
            {
                errorMessage = "A service type must be selected.";
                return false;
            }

            if (booking.NumberOfRooms <
                houses.MinRooms ||
                booking.NumberOfRooms >
                houses.MaxRooms)
            {
                errorMessage =
                    $"Number of rooms must be between " +
                    $"{houses.MinRooms} and " +
                    $"{houses.MaxRooms}.";

                return false;
            }

            if (booking.BookingDate < DateTime.Today)
            {
                errorMessage =
                    "Booking date cannot be in the past.";

                return false;
            }

            if (booking.CarpetedRooms < 0)
            {
                errorMessage =
                    "Carpeted rooms cannot be negative.";

                return false;
            }

            if (booking.CarpetedRooms >
                booking.NumberOfRooms)
            {
                errorMessage =
                    "Carpeted rooms cannot exceed total rooms.";

                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}