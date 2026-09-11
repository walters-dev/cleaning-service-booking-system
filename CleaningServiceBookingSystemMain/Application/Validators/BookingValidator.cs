using System;
using System.Text.RegularExpressions;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.Validators
{
    /* SUMMARY:
     * Validates Customer and Booking data before it is priced or saved
     * 
     * BRD reference: Section 15, Validation and Error Handling Requirements.
     * Every check in this class maps directly to one of the bullet points in section 15 -
     * See the comment above each check for the exact rule it enforces.
     * 
     * This class only validates data that has already been parsed into the correct type
     * (a real int for NumberOfRooms, a real DateTime for BookingDate, etc).
     * Preventing a crash when the user types letters where a number is expected 
     * (the last bullet point in section 15) is a seperate concern handled at the point 
     * where raw console input is read (i.e. with int.TryParse / DateTime.TryParse loops),
     * before a Booking object is even constructed. That keeps this class focused on a single
     * responsibility: is this data valid, yes or no.
     */
    public class BookingValidator
    {
        /* Validates a Customer record.
         * BRD: FR-02 and section 15
         * returns True if the customer passes all checks.
         */
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

            // BRD 15: Phone numbers must not be blank and should be checked for reasonable length
            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                errorMessage = "Phone number is required.";
                return false;
            }

            /* BRD 15: enforces reasonable length here as exactly 10 digits,
             * matching a standard South African phone number format (i.e. 0801234567).
             */
            if (!Regex.IsMatch(
                customer.PhoneNumber,
                @"^[0-9]{10}$"))
            {
                errorMessage =
                    "Phone number must contain exactly 10 digits.";
                return false;
            }

            /* BRD 15: email may be optional, but if entered it should contain a basic valid format.
             * Since email is optional, an empty value is skipped entirely and is not treated as an error -
             * only a non-blank, badly formatted email fails this check.
             */
            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                // Basic format: Some_Word@Something.letters
                // Domain suffix requires at least 2 letters (i.e. .com OR .co)
                if (!Regex.IsMatch(
                    customer.Email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    errorMessage = "Invalid email format.";
                    return false;
                }
            }

            /* Address is required by the Customers table definition in BRD section 12.1,
             * even though section 15 does not list it explicitly - a booking cannot be useful without a service address.
             */
            if (string.IsNullOrWhiteSpace(customer.PhyAddress))
            {
                errorMessage = "Address is required.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }


        /* Validates a Booking record.
         * BRD: FR-04, FR-05, FR-08, section 8.5, and section 15.
         * returns True if the booking passes all checks.
         */
        public bool ValidateBooking(
            Bookings booking, HouseTypes houseType,
            out string errorMessage)
        {
            // A booking cannot be priced without a house type - BaseRate and RatePerRoom both come from HouseTypes.
            if (string.IsNullOrWhiteSpace(booking.HouseTypeId))
            {
                errorMessage = "A house type must be selected.";
                return false;
            }

            // ServiceType supplies the pricing Multiplier (BRD section 8.2) used by PricingService.
            if (string.IsNullOrWhiteSpace(booking.ServiceTypeId))
            {
                errorMessage = "A service type must be selected.";
                return false;
            }

            /* BRD 15: number of rooms must be numeric and within the range allowed for the selected house type.
             * The numeric part of this rule is guaranteed by the type system (NumberOfRooms is an int)
             * plus safe parsing at the console input stage. The within range part is enforced by this check,
             * using the MinRooms/MaxRooms that come from the house type's own row in BRD section 8.1.
             */
            if (booking.NumberOfRooms <
                houseType.MinRooms ||
                booking.NumberOfRooms >
                houseType.MaxRooms)
            {
                errorMessage =
                    $"Number of rooms must be between " +
                    $"{houseType.MinRooms} and " +
                    $"{houseType.MaxRooms}.";

                return false;
            }

            // BRD 15 and section 8.5: Booking date must not be in the past.
            if (!booking.BookingDate.HasValue)
            {
                errorMessage =
                    "Booking date is required.";

                return false;
            }

            if (booking.BookingDate.Value.Date < DateTime.Today)
            {
                errorMessage = "Booking date cannot be in the past.";
                return false;
            }

            // Not explicitly named in section 15, but implied by data
            // quality (section 10, Non-Functional Requirements) - a
            // carpet-cleaning add-on (BRD 8.3) is priced per carpeted
            // room, so a negative count would produce a negative charge.
            if (booking.CarpetedRooms < 0)
            {
                errorMessage =
                    "Carpeted rooms cannot be negative.";

                return false;
            }

            // A booking cannot have more carpeted rooms than total rooms
            // - protects the Carpet Cleaning add-on calculation in
            // PricingService.CalculateAddOnTotal from producing an
            // inflated, meaningless total.
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

        public bool ValidateAdmin(Admins admin, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(admin.Username))
            {
                errorMessage = "Admin username is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(admin.AdminPassword))
            {
                errorMessage = "Admin password is required.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(admin.Email))
            {
                // Basic format: Some_Word@Something.letters
                // Domain suffix requires at least 2 letters (i.e. .com OR .co)
                if (!Regex.IsMatch(
                    admin.Email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    errorMessage = "Invalid email format.";
                    return false;
                }
            }

            errorMessage = string.Empty;
            return true;
        }

    }
}