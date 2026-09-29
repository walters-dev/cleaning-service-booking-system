using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Customers GetCustomerById(string? Id);// ? means it can be null  //gets a specific customer from storage
        void Add(Customers customers);      //Adds a customer to storage
        Customers GetCustomersByPhoneNumber(string phonenumber);
        string CustomersRowCount();
    } 
}
