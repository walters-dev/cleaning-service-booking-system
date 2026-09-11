using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Application.Validators;

using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.InputMethods
{
    public class ExistingAdmin
    {
        private readonly BookingValidator _bookingValidator;

        public ExistingAdmin(BookingValidator bookingValidator)
        {
            _bookingValidator = bookingValidator;
        }

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

                string errorMessage;

                bool isValid = _bookingValidator.Validate(admin, out errorMessage);

                if (isValid)
                {
                    return admin;
                }

                Console.WriteLine();
                Console.WriteLine($"Error: {errorMessage}") ;
                Console.WriteLine("Please try Again");
            }
        }
    }
}