using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystem.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.InputMethods
{
    public class AdminInput
    {
        public Admins GetAdminInput()
        {
            Admins admin = new Admins();

            Console.WriteLine();
            Console.WriteLine("===== ADMIN INFORMATION =====");

            Console.Write("Enter admin ID: ");
            admin.AdminId = Console.ReadLine() ?? "";//call procedure to make id

            Console.Write("Enter username: ");
            admin.Username = Console.ReadLine() ?? "";

            Console.Write("Enter password: ");
            admin.AdminPassword = Console.ReadLine() ?? "";

            Console.Write("Enter email: ");
            admin.Email = Console.ReadLine() ?? "";

            return admin;
        }
    }
}