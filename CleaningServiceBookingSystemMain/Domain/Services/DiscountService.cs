using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application
{
    /* SUMMARY:
     * Determines which single discount, if any, applies to a booking.
     * 
     * BRD reference: Section 8.4, Discount Rules:
     * - First-Time Customer Discount: 10%. Customer has no previous completed booking. Apply once only.
     * - Recurring Booking Discount: 12%. Customer chooses weekly or bi-weekly recurring service. Do not stack with large booking discount.
     * - Large Booking Discount: 15%. Booking has 6 or more rooms. Do not stack with first-time discount.
     * - Discount stacking rule: "Where more than one discount applies, the system must apply the single highest discount only. 
     * Discounts must not be cumulative."
     * 
     * This class has no dependency on a repository or the database. The "has no previous completed booking" check needed for the
     * first-time discount requires booking history, which this class does not look up itself - instead it trusts a flag
     * (Customer.IsFirstTimeCustomer) that must be set correctly by the time a Customer reaches this class. 
     * That lookup belongs to the BookingService, which already holds a repository dependency - 
     * keeping it out of DiscountService follows Single Responsibility (BRD 11.3): this class's only job is, "given eligibility facts, 
     * pick the highest applicable discount", not "go find out if this customer is first-time". It also means DiscountService can be unit 
     * tested with a plain Customer/Booking object, no repository or database required.
     */
    public class DiscountService
    {
        /* BRD 8.4: Percentages are defined as named constants, rather than being scattered as magic numbers through the method below.
         */
        private const decimal FirstTimeCustomerPercentage = 0.10m;
        private const decimal RecurringbookingPercentage = 0.12m;
        private const decimal LargeBookingPercentage = 0.15m;

        /* BRD 8.4: Booking must have 6 or more rooms to be considered a large booking.
         */
        private const int LargeBookingMinimumRooms = 6;

        /* Works out the single highest discount this booking is eligible for, and returns the amount it's worth against the given subtotal.
         * return => DiscountResult describing which discount (if any) was applied, its percentage, and the resulting monetary amount.
         */
        public decimal CalculateDiscountAmount(Bookings booking, decimal subtotal)
        {
            /* Starts with "No discount" as the default, and only replace it if a higher-percentage eligible discount is found.
             * This avoids stacking two discounts together, as BRD 8.4 states: "the system must apply the single highest discount only."
             */
            string highestDiscountName = "No discount";
            decimal highestPercentage = 0m;

            if (booking.IsFirstTimeCustomer && FirstTimeCustomerPercentage > highestPercentage)
            {
                highestDiscountName = "First-Time Customer Discount";
                highestPercentage = FirstTimeCustomerPercentage;
            }

            if (booking.IsRecurring && RecurringbookingPercentage > highestPercentage)
            {
                highestDiscountName = "Recurring Booking Discount";
                highestPercentage = RecurringbookingPercentage;
            }

            if (booking.NumberOfRooms >= LargeBookingMinimumRooms &&
                LargeBookingPercentage > highestPercentage)
            {
                highestDiscountName = "Large Booking Discount";
                highestPercentage = LargeBookingPercentage;
            }

            /* DiscountAmount = Subtotal * HighestEligibleDiscountPercentage (BRD 8.6, pricing formula)
             * If no discount was eligible, highestPercentage remains 0m and this naturally evaluates to 0.
             */
            decimal discountAmount = subtotal * highestPercentage;

            return discountAmount;
        }
    }
}