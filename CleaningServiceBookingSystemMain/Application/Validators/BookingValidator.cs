using System;
using System.Text.RegularExpressions;
using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    /* SUMMARY:
     * Validates Customer and Booking data before it is priced or saved
    */
    public class BookingValidator
    {
        // Validates a Customer record.
        public bool ValidateCustomer(
            Customer customer,
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

            if (string.IsNullOrWhiteSpace(customer.Address))
            {
                errorMessage = "Address is required.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }


        // Validates a Booking record.
        public bool ValidateBooking(
            Booking booking,
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
                booking.HouseType.MinRooms ||
                booking.NumberOfRooms >
                booking.HouseType.MaxRooms)
            {
                errorMessage =
                    $"Number of rooms must be between " +
                    $"{booking.HouseType.MinRooms} and " +
                    $"{booking.HouseType.MaxRooms}.";

                return false;
            }

            if (booking.BookingDate.Date < DateTime.Today)
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