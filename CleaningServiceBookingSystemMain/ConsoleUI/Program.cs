using System;
using Spectre;
using Spectre.Console;
namespace CleaningServiceBookingSystemMain.ConsoleUI
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
                            AdminMenu adminMenu = new AdminMenu();
                            adminMenu.ViewAdminMenu();
                            break;
                        case ConsoleKey.D2:
                            IsAdmin = false;
                            IsUserSelected = true;
                            Console.WriteLine("Operations Manager is selected");
                            Console.Clear();
                            ManagerMenu managerMenu = new ManagerMenu();
                            managerMenu.ViewManagerMenu();
                            break;
                        case ConsoleKey.D3:
                            Console.WriteLine("Application is terminated");
                            IsAppRunning = false;
                            IsAdmin = false;
                            IsUserSelected = true;
                            break;
                        default:
                            key = Console.ReadKey(true);
                            IsUserSelected = false;
                            break;
                    }

                }
            }
            
        }
    }
}