using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystem.Application
{
    public class AdminInput
    {
        public Admins GetAdminInput()
        {
            Admins admin = new Admins();

            Console.WriteLine();
            Console.WriteLine("======== ADMIN INFORMATION ========");

            Console.Write("Enter Admin Id: ");
            admin.AdminId = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Username: ");
            admin.Username = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Password: ");
            admin.AdminPassword = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Email: ");
            admin.Email = Console.ReadLine() ?? "";

            return admin;
        }
    }
}