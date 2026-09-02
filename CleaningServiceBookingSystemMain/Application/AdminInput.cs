using System;
using System.Collections.Generic;
using System.Text;

using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class AdminInput
    {
        public Admin GetAdminInput()
        {
            Admin admin = new Admin();

            Console.WriteLine();
            Console.WriteLine("======== ADMIN INFORMATION ========");

            Console.Write("Enter Admin Id: ");
            admin.AdminId = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Username: ");
            admin.AdminName = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Password: ");
            admin.Password = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Email: ");
            admin.Email = Console.ReadLine() ?? "";

            return admin;
        }
    }
}