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

        public Admins GetAdminInput() 
        {
            while (true)
            {
                Admins admin = new Admins();

                Console.WriteLine();
                Console.WriteLine("=====ADMIN LOGIN=====");

                Console.Write("Username: ");
                admin.Username = Console.ReadLine();
                Console.Write("Password: ");
                admin.AdminPassword = Console.ReadLine();

                bool isValid = _bookingValidator.ValidateExistingAdmin(admin, out string errorMessage);

                if (isValid)
                {
                    return admin;
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Error: {errorMessage}[/]") ;
                Console.WriteLine("Please try Again");
            }
        }
    }
}