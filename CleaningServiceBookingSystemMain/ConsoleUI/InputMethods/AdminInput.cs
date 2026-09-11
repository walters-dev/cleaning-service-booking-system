using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class AdminInput
    {
        public Admins GetAdminInput()
        {
            Admins admin = new Admins();

            Console.WriteLine();
            Console.WriteLine("===== ADMIN INFORMATION =====");

            InMemoryRepositoryAdmins repositoryAdmins = new InMemoryRepositoryAdmins();
            admin.AdminId = repositoryAdmins.AdminRowCount();

            Console.Write("Enter username: ");
            admin.Username = Console.ReadLine();

            Console.Write("Enter password: ");
            admin.AdminPassword = Console.ReadLine();

            Console.Write("Enter email: ");
            admin.Email = Console.ReadLine();

            return admin;
        }
    }
}