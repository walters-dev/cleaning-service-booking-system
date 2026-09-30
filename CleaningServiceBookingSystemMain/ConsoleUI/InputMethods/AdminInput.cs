using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class AdminInput
    {

        private readonly BookingValidator _bookingValidator = new BookingValidator();
        private readonly IAdminRepository _adminRepository;
        public AdminInput(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public Admins GetAdminInput()
        {
           while (true)
            {
                Admins admin = new Admins();

                Console.WriteLine();
                Console.WriteLine("===== ADMIN INFORMATION =====");

                IAdminService service = new AdminService(_adminRepository);
                admin.AdminId = service.FindAdminCount();

                Console.Write("Enter username: ");
                admin.Username = Console.ReadLine();

                Console.Write("Enter password: ");
                admin.AdminPassword = Console.ReadLine();

                Console.Write("Enter email: ");
                admin.Email = Console.ReadLine();

                //return admin;
                string errorMessage;

                bool isValid = _bookingValidator.ValidateAdmin(admin, out errorMessage);

                if (isValid)
                {
                    return admin;
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Error: {errorMessage}[/]");
                Console.WriteLine("Please try Again");
            }
        }
    }
}