using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface ICustomerService
    {
        Customers FindCustomer(string? customerId);//finds 1 customer 
        void RegisterCustomer(Customers customer);//register customer in system
        Customers FindCustomerWithPhoneNumber(string phonenumber); //finds 1 customer via phone number
        string FindCustomerCount();//finds count of customers in system
    }
}
