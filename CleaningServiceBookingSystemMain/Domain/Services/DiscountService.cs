using CleaningServiceBookingSystem.Domain

namespace CleaningServiceBookingSystemMain.Application
{
    /* SUMMARY:
     * Determines which single discount, if any, applies to a booking.
     * 
     * 
     * 
     * 
     * 
     * 
     */
    public class DiscountService
    {
        // Percentages are defined as named constants
        private const decimal FirstTimeCustomerPercentage = 0.10m;
        private const decimal RecurringbookingPercentage = 0.12m;
        private const decimal LargeBookingPercentage = 0.15m;

        // BRD 8.4: Booking has 6 or more rooms
        private const int LargeBookingMinimumRooms = 6;

        // Works out the single highest discount this booking is eligible for, and returns
        // the amount it's worth against the given subtotal
        public DiscountResult CalculateDiscountAmount(Booking booking, decimal subtotal)
        {
            // 
            bool isEligibleForFirstTime = IsEligibleForFirstTimeDiscount(booking);
            bool isEligibleForRecurring = IsEligibleForRecurringDiscount(booking);
            bool isEligibleForLargeBooking = IsEligibleForLargeBookingDiscount(booking);

            // 
            string highestDiscountName = "None";
            decimal highestPercentage = 0m;

            if (isEligibleForFirstTime && FirstTimeCustomerPercentage > highestPercentage)
            {
                highestDiscountName = "First-Time Customer Discount";
                highestPercentage = FirstTimeCustomerPercentage;
            }

            if (isEligibleForRecurring && RecurringbookingPercentage > highestPercentage)
            {
                highestDiscountName = "Recurring Booking Discount";
                highestPercentage = RecurringbookingPercentage;
            }
            
            if (isEligibleForLargeBooking && LargeBookingPercentage > highestPercentage)
            {
                highestDiscountName = "Large Booking Discount";
                highestPercentage = LargeBookingPercentage;
            }

            // DiscountAmount = Subtotal * HighestEligibleDiscountPercentage
            // If no discount was eliglible, highestPercentage remains 0m and this
            // naturally evaluates to 0.
            decimal discountAmount = subtotal * highestPercentage;

            return new DiscountResult
            {
                DiscountName = highestDiscountName,
                Percentage = highestPercentage,
                Amount = discountAmount
            };
        }

        // First-Time Customer Discount (10%) - 
        private bool IsEligibleForFirstTimeDiscount(Booking booking)
        {
            if (booking.Customer == null)
            {  return false; }

            return booking.Customer.IsFirstTimeCustomer;
        }

        // Recurring Booking Discount (12%) -
        private bool IsEligibleForRecurringDiscount(Booking booking)
        {
            return booking.IsRecurring;
        }

        // Large Booking Discount (15%) -
        private bool IsEligibleForLargeBookingDiscount(Booking booking)
        {
            return booking.NumberOfRooms >= LargeBookingMinimumRooms;
        }

        // End
    }
}