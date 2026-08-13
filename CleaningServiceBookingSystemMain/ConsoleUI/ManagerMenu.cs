using Spectre.Console;
using System;

namespace CleaningServiceBookingSystemMain.ConsoleUI
{


    public class ManagerMenu
    {
        public void ViewManagerMenu()
        {
            //declare and intialize variables
            bool IsManagerRunning = true;
            while (IsManagerRunning == true)
            {
                var managerChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option:")
                    .AddChoices("Bookings", "Summaries", "Trends", "Change user"));
                switch (managerChoices)
                {
                    case "Bookings":
                        AnsiConsole.MarkupLine("[green]Booking selected[/]");
                        break;
                    case "Summaries":
                        AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        break;
                    case "Trends":
                        AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        break;
                    case "Change user":
                        IsManagerRunning = false;
                        break;
                }
            }
        }
    }
}
