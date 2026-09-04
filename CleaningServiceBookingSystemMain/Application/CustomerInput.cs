using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystem.Application
{
    public class CustomerInput
    {

        public Customers GetCustomerInput()
        {
            Customers customer = new Customers();

            Console.WriteLine("===== CUSTOMER INFORMATION =====");

            Console.Write("Enter full name: ");
            customer.FullName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            customer.PhoneNumber = Console.ReadLine();

            Console.Write("Enter email address: ");
            customer.Email = Console.ReadLine();

            Console.Write("Enter address: ");
            customer.PhyAddress = Console.ReadLine();

            return customer;
        }
    }
}