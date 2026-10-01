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
        Customers GetCustomersByPhoneNumber(string phonenumber);       //retrieves one customers record from storage 
        string CustomersRowCount(); //Gets customer count from storage
    } 
}
