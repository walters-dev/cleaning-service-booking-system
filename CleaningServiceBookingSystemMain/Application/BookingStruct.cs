using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application
{
    public struct BookingStruct
    {

        public BookingStruct(Bookings bookings)
        {
            HouseTypeId = bookings.HouseTypeId;
            ServiceTypeId = bookings.ServiceTypeId;
            DiscountRuleId = bookings.DiscountRuleId;
            BookingDate = bookings.BookingDate;
            NumberOfRooms = bookings.NumberOfRooms;
            IsRecurring = bookings.IsRecurring;
            RecurringBookingType = bookings.RecurringBookingType;
            SubTotal = bookings.SubTotal;
            DiscountAmount = bookings.DiscountAmount;
            SurchargeAmount = bookings.SurchargeAmount;
            TotalAmount = bookings.TotalAmount;
            BookingStatus = bookings.BookingStatus;
            UpdatedAt = bookings.UpdatedAt;
            UpdatedBy = bookings.UpdatedBy;
            CarpetedRooms = bookings.CarpetedRooms;
        }
        public string HouseTypeId { get; init; }//
        public string ServiceTypeId { get; init; }//
        public string? DiscountRuleId { get; init; }//
        public DateTime? BookingDate { get; init; }//
        public int NumberOfRooms { get; init; }//
        public bool IsRecurring { get; init; }//
        public string RecurringBookingType { get; init; }//
        public decimal SubTotal { get; init; }//
        public decimal DiscountAmount { get; init; }//
        public decimal SurchargeAmount { get; init; }//
        public decimal TotalAmount { get; init; }//
        public string BookingStatus { get; init; }//
        public DateTime? UpdatedAt { get; init; }//
        public string? UpdatedBy { get; init; }//
        public int CarpetedRooms { get; init; }//
    }


}
