using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface ICustomerRepository
    {
        IList<Customers> GetCustomers();
        Customers GetCustomerById(int? Id);// ? means it can be null
        void Add(Customers customers);
        void Update(Customers customers);
        void Delete(Customers customers);
    } 
}
