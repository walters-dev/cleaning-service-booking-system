using System;
using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    /* SUMMARY:
     * Calculates the full price breakdown for a booking.
     *
     * BRD reference: Section 8.6 - Pricing Formula
     * BaseAmount = HouseType.BaseRate + (NumberOfRooms * HouseType.RatePerRoom)
     * ServiceAmount = BaseAmount * ServiceType.Multiplier
     * AddOnTotal = Sum of selected add-on fees
     * Subtotal = ServiceAmount + AddOnTotal
     * DiscountAmount = Subtotal * HighestEligibleDiscountPercentage
     * AmountAfterDiscount = Subtotal - DiscountAmount
     * WeekendSurcharge = AmountAfterDiscount * 10% (when applicable)
     * FinalTotal = AmountAfterDiscount + WeekendSurcharge
     * 
     * Each method below implements exactly one line of that formula, so the class satisfies the 
     * Single Responsiblity Principle listed in the BRD section 11.3:
     * Pricing calculation is its own responsibility, seperate from discount-eligibility logic
     * (DiscountService), validation (BookingValidator) and persistence (BookingRepository).
    */

    public class PricingService
    {
        /* 
         * DicountService is supplied through the constructor rather than created internally.This follows
         * Dependency Inversion (BRD 11.3):
         * PricingService depends on being handed a discount calculator, rather than hard-coding itself to
         * one specific construction of DiscountService, which also makes it possible to substitute a test
         * double for DiscountService when unit testing this class.
         */
        private readonly DiscountService _discountService;

        public PricingService(DiscountService discountService)
        {
            _discountService = discountService;
        }

        /* BaseAmount = HouseType.BaseRate + (NumberOfRooms * HouseType.RatePerRoom)
         * Uses the base and per-room rate for the selected house type
         * (BRD section 8.1, e.g. Standard House: R650 base + R120 per room).
         */
        public decimal CalculateBaseAmount(Booking booking)
        {
            return booking.HouseType.BaseRate * (booking.NumberOfRooms * booking.HouseType.RatePerRoom);
        }

        /* ServiceAmount = BaseAmount * ServiceType.Multiplier
         * Applies the cleaning service type multiplier from BRD section 8.2
         * (Standard Clean 1.00, Deep Clean 1.35, Move-In/Move-Out 1.50) on top of the base amount.
         */
        public decimal CalculateServiceAmount(Booking booking)
        {
            decimal baseAmount = CalculateBaseAmount(booking);
            return baseAmount * booking.ServiceType.Multiplier;
        }

        /* AddOnTotal = Sum of selected add-on fees
         * 
         * Adds up every add-on chosen for this booking, applying the two pricing rules from BRD section 8.3:
         * -"Flat": a single fixed fee per booking (Window Cleaning R150, Laundry Add-On R100)
         * -"PerRoom": the rate multiplied by the number of carpeted rooms (Carpet Cleaning R200 per carpeted room).
         * 
         * Any AddOn whose PricingType is neither "Flat" nor "PerRoom" is silently skipped and contributes R0 -
         * this should only happen if bad seed data reaches this class, since section 12.2 requires AddOns to be seeded correctly.
         */
        public decimal CalculateAddOnTotal(Booking booking)
        {
            decimal addOnTotal = 0;
            foreach(AddOn addOn in booking.SelectedAddOns)
            {
                if (addOn.PricingType == "Flat")
                {
                    addOnTotal += addOn.Rate;
                }
                else if (addOn.PricingType == "PerRoom") 
                {
                    addOnTotal += addOn.Rate * booking.CarpetedRooms;
                }
            }
            return addOnTotal;
        }

        /* Subtotal = ServiceAmount + AddOnTotal
         * This is the pre-discount, pre-surcharge total, and is the figure discount percentages (BRD 8.4) are applied against.
         */
        public decimal CalculateSubtotal(Booking booking)
        {
            decimal serviceAmount = CalculateServiceAmount(booking);
            decimal addOnTotal = CalculateAddOnTotal(booking);
            return serviceAmount + addOnTotal;
        }

        /* DiscountAmount = Subtotal * HighestEligibleDiscountPercentage
         * 
         * Deciding which discount applies (first-time 10%, recurring 12%, large booking 6+ rooms 15%)
         * and enforcing the "only the single highest discount, never stacked" rule from BRD 8.4
         * is entirely DiscountService's responsibility, not this class's - PricingService only asks 
         * "given this subtotal, what is the discount amount?" and applies whatever comes back.
         */
        public decimal CalculateDiscountAmount(Booking booking, decimal subtotal)
        {
            DiscountResult discount = _discountService.CalculateDiscount(booking, subtotal);
            return discount.Amount;
        }

        /* AmountAfterDiscount = Subtotal - DiscountAmount
         * Both values are passed in rather than recalculated, for the same reason as CalculateDiscount above -
         * this keeps every side-effecting calculation (particularly the call into DiscountService) running exactly once per booking.
         */
        public decimal CalculateAmountAfterDiscount(decimal subtotal, decimal discountAmount)
        {
            return subtotal - discountAmount;
        }

        /* WeekendSurcharge = AmountAfterDiscount * 10% when applicable
         * BRD section 8.5: "Weekend surcharge: Saturday and Sunday bookings must add 10% after discount calculation."
         * The order matters - the surcharge is calculated on the amount after the discount has already been subtracted,
         * not on the original subtotal.
         */
        public decimal CalculateWeekendSurcharge(Booking booking, decimal amountAfterDiscount)
        {
            DayOfWeek day = booking.BookingDate.DayOfWeek;
            if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
            {
                return amountAfterDiscount * 0.10m;
            }

            return 0;
        }

        /* FinalTotal = AmountAfterDiscount + WeekendSurcharge
         * Runs the full pricing formula (BRD 8.6) for a booking, once, and writes every intermediate figure onto the 
         * Booking object so it can be displayed to staff before saving 
         * (FR-05: "System displays base amount, add-ons, discount, surcharge and final total before saving")
         * and persisted (FR-07, and the Bookings table columns in section 12.1: Subtotal, DiscountAmount, SurchargeAmount, TotalAmount).
         * 
         * Each formula step below runs EXACTLY ONCE and its result is passed forward into the next step , rather than each method being
         * left to silently recalculate earlier steps for itself. This matters most for discount calculation.
         */
        public void CalculateFinalTotal(Booking booking)
        {
            // Step 1: Subtotal
            decimal subtotal = CalculateSubtotal(booking);

            // Step 2: DiscountAmount
            decimal discountAmount = CalculateDiscount(booking, subtotal);

            // Step 3: AmountAfterDiscount
            decimal amountAfterDiscount = CalculateAmountAfterDiscount(subtotal, discountAmount);

            // Step 4: WeekendSurcharge
            decimal surcharge = CalculateWeekendSurcharge(booking, amountAfterDiscount);

            // Step 5: FinalTotal
            decimal finalTotal = amountAfterDiscount + surcharge;

            /* Linking this class to the booking class:
             * Persist every intermediate figure onto the booking so the full breakdown can be shown to staff (FR-05)
             * and saved to the Bookings table (section 12.1).
             */
            booking.Subtotal = subtotal;
            booking.DiscountAmount = discountAmount;
            booking.SurchageAmount = surcharge;
            booking.TotalAmount = finalTotal;
        }
    }
}