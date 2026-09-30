using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository repository)//prevents the service from running without its dependency
        {
            _customerRepository = repository;
        }
        public Customers FindCustomer(string? customerId)
        {
            return _customerRepository.GetCustomerById(customerId);
        }
        public void RegisterCustomer(Customers customer)
        {
            _customerRepository.Add(customer);
        }
        public Customers FindCustomerWithPhoneNumber(string phonenumber)
        {
            return _customerRepository.GetCustomersByPhoneNumber(phonenumber);
        }
        public string FindCustomerCount()
        {
            return _customerRepository.CustomersRowCount();
        }
    }
}
