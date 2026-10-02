using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;


namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class CustomerInput
    {
        private readonly BookingValidator _validator;
        private readonly ICustomerRepository _customerRepository;

        public CustomerInput(ICustomerRepository customerRepository, BookingValidator validator) 
        {
            _customerRepository = customerRepository;
            _validator = validator;
        }
        public Customers GetCustomerInput(string username)
        {
           while (true)
            {
                Customers customer = new Customers();

                Console.WriteLine();
                Console.WriteLine("===== CUSTOMER INFORMATION ====="); //Captures User Input
                ICustomerService service = new CustomerService(_customerRepository);
                customer.CustomerId = service.FindCustomerCount();

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

                bool isValid = _validator.ValidateCustomer(customer, out string errorMessage); //Valids If the customer Exists

                if (isValid)
                {
                    return customer;
                }
                else
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[red]Validation error: {errorMessage}[/]");
                    Console.WriteLine("Please enter the customer information again.");
                }
           }


        }

        public string GetEmail() // Validates if the email exists
        {
            while (true)
            {
                Console.WriteLine("Enter Email");
                string email = Console.ReadLine();

                bool isValid = _validator.ValidateCustomerEmailInput(email, out string errormessage);
                if (isValid)
                {
                    return email;
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"Validation error: {errormessage}");
                Console.WriteLine("Please Enter the email again");
            }
        }
        public string GetPhoneNumber() // Validates if the phone number exists
        {
            while (true)
            {
                Console.WriteLine("Enter phone number");
                string phonenumber = Console.ReadLine();

                bool isValid = _validator.ValidateCustomerPhoneNumberInput(phonenumber, out string errormessage);
                if (isValid)
                {
                    return phonenumber;
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"Validation error: {errormessage}");
                Console.WriteLine("Please Enter the email again");
            }
        }
    }
}