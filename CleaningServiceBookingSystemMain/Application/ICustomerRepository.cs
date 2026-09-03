using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface ICustomerRepository
    {
        IList<Customers> GetCustomers();    //gets list of all customers from storage
        Customers GetCustomerById(int? Id);// ? means it can be null  //gets a specific customer from storage
        void Add(Customers customers);      //Adds a customer to storage
        void Update(Customers customers);   //edits an already existing customer to storage
        void Delete(Customers customers);   //deletes an customer from storage
    } 
}
