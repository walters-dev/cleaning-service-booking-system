using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface ICustomerService
    {
        Customers FindCustomer(string? customerId);
        void RegisterCustomer(Customers customer);
        Customers FindCustomerWithPhoneNumber(string phonenumber);
        string FindCustomerCount();
    }
}
