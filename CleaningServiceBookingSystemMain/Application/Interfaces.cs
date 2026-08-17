using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface ICustomerRepository
    {
        IList<Customers> GetCustomers();
        Customers GetCustomerByID(int? ID);// ? means it can be null
        void Add(Customers customers);
        void Update(Customers customers);
        void Delete(Customers customers);
    }
    public interface IBookingsRepository 
    {
        IList<Bookings> GetBookings();
        Bookings GetBookingsByID(int? ID);
        void Add(Bookings bookings);
        void Update(Bookings bookings);
        void Delete(Bookings bookings);
        void DiaplayListByRange(Bookings bookings);
        void DisplayBookingHistory(Bookings bookings);
        void DisplayRevenueSummary(Bookings bookings);
        void DisplayBookingsByHouseType(Bookings bookings);
        void DisplayDiscountUsage(Bookings bookings);
    }
    public interface IAdminRepository
    {
        IList<Admins> GetAdmins();
        Admins GetAdminsByID(int? ID);
        void Add(Admins admins);
        void Update(Admins admins);
        void Delete(Admins admins);
    }  
    public interface IHouseTypesRepository
    {
       IList<HouseTypes> GetHouseTypes();
       HouseTypes GetHouseTypesByID(int? ID);
    }
    public interface IServiceTypesRepository
    {
        IList<ServiceTypes> GetServiceTypes();
        ServiceTypes GetServiceTypesByID(int? ID);
    }
    public interface IDiscountRulesRepository
    {
        IList<DiscountRules> GetDiscountRules();
        DiscountRules GetDiscountRulesByID(int? ID);
        void Add(DiscountRules discountRules);
        //add update and delete if have time
    }
    public interface IAddOnsRepository
    {
        IList<AddOns> GetAddOns();
        AddOns AddOnsByID(int? ID);
    }
    public interface IBookingAddOnsRepository
    {
        IList<BookingAddOns> GetBookingAddOns();
        BookingAddOns bookingAddOnsByID(int? ID);
    }
}
