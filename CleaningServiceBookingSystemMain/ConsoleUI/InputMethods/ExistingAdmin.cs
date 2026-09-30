using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.InputMethods
{
    public class ExistingAdmin
    {
        private readonly BookingValidator _bookingValidator = new BookingValidator();

        public Admins GetAdminInput() // Gets the admin Input 
        {
            while (true) // Uses a loop to Capture user input and stores them
            {
                Admins admin = new Admins();

                Console.WriteLine();
                Console.WriteLine("=====ADMIN LOGIN=====");

                Console.Write("Username: ");
                admin.Username = Console.ReadLine();
                Console.Write("Password: ");
                admin.AdminPassword = Console.ReadLine();

                bool isValid = _bookingValidator.ValidateExistingAdmin(admin, out string errorMessage); // Checks Whether the Admin exists or not

                if (isValid)
                {
                    return admin; // Will return Admin if The Admin Exists
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Error: {errorMessage}[/]") ; // Will Display An Error Message if The admin does not exist and will prompt the user to try again
                Console.WriteLine("Please try Again");
            }
        }
    }
}