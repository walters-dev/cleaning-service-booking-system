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
            bool IsUserSelected = false;
            IsUserSelected = false;
            while (IsUserSelected == false)
            {
                var menuChoices = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose menu option")
                .AddChoices("Booking Administrator", "Operations Manager", "Terminate application"));   //menu options output
                switch (menuChoices)
                {
                    case "Booking Administrator":
                        Console.WriteLine("Booking Administrator is selected");
                        Console.Clear();
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.ViewAdminMenu();
                        break;
                    case "Operations Manager":
                        Console.WriteLine("Operations Manager is selected");
                        Console.Clear();
                        ManagerMenu managerMenu = new ManagerMenu();
                        managerMenu.ViewManagerMenu();
                        break;
                    case "Terminate application":
                        Console.WriteLine("Application is terminated");
                        IsUserSelected = true;                          //stops menu loop
                        break;
                }

            }


        }
    }
}