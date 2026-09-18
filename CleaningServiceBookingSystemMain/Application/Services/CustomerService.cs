using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository repository)
        {
            _customerRepository = repository;
        }
        public IList<Customers> ViewAllCustomers()
        {
            return _customerRepository.GetCustomers();
        }
        public Customers FindCustomer(string? customerId)
        {
            return _customerRepository.GetCustomerById(customerId);
        }
        public void RegisterCustomer(Customers customer)
        {
            _customerRepository.Add(customer);
        }
        public void AmendCustomer(Customers customer)
        {
            _customerRepository.Update(customer);
        }
        public void DeleteCustomer(Customers customer)
        {
            _customerRepository.Delete(customer);
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
