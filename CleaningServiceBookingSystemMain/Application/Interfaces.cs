using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface ICustomerRepository
    {
        void Add(string Fullname);
        void Update(string);
    }
    public interface IBookingsRepository 
    {
        
    }
    public interface IAdmin
    {

    }
    public interface IHouseTypes
    {

    }
    public interface IServiceTypes
    {

    }
    public interface IDiscountRules
    {

    }
    public interface IAddOns
    {

    }
    public interface IBookingAddOns
    {

    }
}
