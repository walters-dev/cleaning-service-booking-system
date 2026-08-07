using System;
using Spectre;
using Spectre.Console;
using RedAcademy.Manager;
using RedAcademy.Admin;
namespace CleaningServiceBookingSystemMain
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Declare variables and initialization
            bool IsAdmin = false, IsUserSelected = false, confirmData, IsAppRunning = true, IsAdminMenuRunning, IsManagerRunning;
            string username, password;
            ConsoleKeyInfo key;
            while (IsAppRunning == true)
            {
                Console.WriteLine("CHOOSE USER TYPE:\nPress 1 for Booking Administrator.\nPress 2 for Operations Manager\nPress 3 to terminate application");
                key = Console.ReadKey(true);
                IsUserSelected = false;
                while (IsUserSelected == false)
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            IsAdmin = true;
                            IsUserSelected = true;
                            Console.WriteLine("Booking Administrator is selected");
                            Console.Clear();
                            break;
                        case ConsoleKey.D2:
                            IsAdmin = false;
                            IsUserSelected = true;
                            Console.WriteLine("Operations Manager is selected");
                            Console.Clear();
                            break;
                        case ConsoleKey.D3:
                            IsAppRunning = false;
                            IsUserSelected = true;
                            break;
                        default:
                            key = Console.ReadKey(true);
                            IsUserSelected = false;
                            break;
                    }

                }
                if (IsAdmin == true)
                {
                    AdminMenu adminMenu = new AdminMenu();
                    adminMenu.ViewAdminMenu();

                }
                else if (IsAppRunning == false)
                {
                    Console.WriteLine("Application is terminated");
                }
                else if(IsAdmin == false)
                {
                    //manager side
                    ManagerMenu managerMenu = new ManagerMenu();
                    managerMenu.ViewManagerMenu();
                }
            }
            
        }
    }
}