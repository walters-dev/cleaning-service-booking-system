using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class CustomerInput
    {
        public Customer GetCustomerInput()
        {
            Customer customer = new Customer();

            Console.WriteLine("===== CUSTOMER INFORMATION =====");

            Console.Write("Enter full name: ");
            customer.FullName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            customer.PhoneNumber = Console.ReadLine();

            Console.Write("Enter email address: ");
            customer.Email = Console.ReadLine();

            Console.Write("Enter address: ");
            customer.Address = Console.ReadLine();

            return customer;
        }
    }
}