using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Validators;
using System.ComponentModel.DataAnnotations;


namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class CustomerInput
    {
        private readonly BookingValidator _validator =
           new BookingValidator();

        public Customers GetCustomerInput(string username)
        {
           while (true)
            {
                Customers customer = new Customers();

                Console.WriteLine();
                Console.WriteLine("===== CUSTOMER INFORMATION =====");
                InMemoryRepositoryCustomers repositoryCustomers = new InMemoryRepositoryCustomers();
                customer.CustomerId = repositoryCustomers.CustomersRowCount();

                Console.Write("Enter full name: ");
                customer.FullName = Console.ReadLine() ?? "";

                Console.Write("Enter phone number: ");
                customer.PhoneNumber = Console.ReadLine() ?? "";

                Console.Write("Enter email address: ");
                customer.Email = Console.ReadLine() ?? "";

                Console.Write("Enter address: ");
                customer.PhyAddress = Console.ReadLine() ?? "";

                customer.CreatedAt = DateTime.Today;
                customer.CreatedBy = username;

                bool isValid = _validator.ValidateCustomer(customer, out string errorMessage);

                if (isValid)
                {
                    return customer;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Validation error: {errorMessage}");
                    Console.WriteLine("Please enter the customer information again.");
                }
           }


        }

        public string GetEmail(string Email)
        {
            while (true)
            {
                Console.WriteLine("Enter Customer Email");
                string email = Console.ReadLine();

                bool isValid = _validator.Validate(email, out string errormessage);
                if (isValid)
                {
                    return email;
                }

                Console.WriteLine();
                Console.WriteLine($"Validation error: {errormessage}");
                Console.WriteLine("Please Enter the email again");
            }
        }
    }
}