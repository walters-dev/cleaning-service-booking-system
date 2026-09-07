using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;

namespace CleaningServiceBookingSystem.Application
{
    public class CustomerInput
    {

        public Customers GetCustomerInput(string username)
        {
            Customers customer = new Customers();

            Console.WriteLine("===== CUSTOMER INFORMATION =====");
            PrimaryKeyCreation primaryKeyCreation = new PrimaryKeyCreation();
            customer.CustomerId = primaryKeyCreation.CustomersRowCount();

            Console.Write("Enter full name: ");
            customer.FullName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            customer.PhoneNumber = Console.ReadLine();

            Console.Write("Enter email address: ");
            customer.Email = Console.ReadLine();

            Console.Write("Enter address: ");
            customer.PhyAddress = Console.ReadLine();

            customer.CreatedAt = DateTime.Today;
            customer.CreatedBy = username;

            return customer;
        }
    }
}