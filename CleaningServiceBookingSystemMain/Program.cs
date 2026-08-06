using System;
using Spectre;
using Spectre.Console;
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
                    Console.WriteLine("Enter Username:");
                    username = Console.ReadLine();
                    //username validation

                    Console.WriteLine("Enter Password:");
                    password = Console.ReadLine();
                    //password validation
                    Console.Clear();
                    AnsiConsole.MarkupLine("[green]Signed in[/]");
                    IsAdminMenuRunning = true;
                    while (IsAdminMenuRunning == true)
                    {
                        var adminChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose menu option")
                            .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customers","Change user"));
                        switch (adminChoices)
                        {
                            case "Create Booking":
                                AnsiConsole.MarkupLine("[green]Create booking selected[/]");
                                var createBookingChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Choose customer:")
                                    .AddChoices("Existing customer", "New customer"));

                                if (createBookingChoices == "Existing customer")
                                {
                                    AnsiConsole.MarkupLine("[green]Existing customer selected[/]");
                                }
                                else if (createBookingChoices == "New customer")
                                {
                                    confirmData = false;
                                    while (confirmData == false)
                                    {
                                        AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                                        //new customer proccess 
                                        var confirmNewCusChoices = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("Is the customer details correct:")
                                            .AddChoices("Yes", "No"));

                                        if (confirmNewCusChoices == "Yes")
                                        {
                                            confirmData = true;
                                            //save customer data to sql
                                        }
                                        else
                                        {
                                            confirmData = false; // loop it
                                        }
                                    }
                                }
                                /*
                                 System displays house types and service types from SQL Server
                                 Staff enters number of rooms, booking date, add-ons and recurring option.
                                 System validates all inputs and calculates subtotal, discount, surcharge and final total
                                 */
                                confirmData = false;
                                while (confirmData == false)
                                {
                                    var confirmBookingChoice = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("Is the booking details correct:")
                                            .AddChoices("Yes", "No"));

                                    if (confirmBookingChoice == "Yes")
                                    {
                                        confirmData = true;
                                        //save booking data to sql
                                    }
                                    else
                                    {
                                        confirmData = false; // loop it
                                    }
                                }
                                break;
                            case "Create New Customer":

                                AnsiConsole.MarkupLine("[green]New Customer selected[/]");

                                confirmData = false;
                                while (confirmData == false)
                                {
                                    AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                                    //new customer proccess 
                                     var confirmNewCusChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the customer details correct:")
                                        .AddChoices("Yes", "No"));

                                    if (confirmNewCusChoices == "Yes")
                                    {
                                        confirmData = true;
                                        //save customer data to sql
                                    }
                                    else
                                    {
                                        confirmData = false; // loop it
                                    }
                                }
                                break;
                            case "View Bookings":
                                //enter booking date and customer
                                AnsiConsole.MarkupLine("[green]View Bookings selected[/]");
                                var viewBookingsChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Choose option:")
                                        .AddChoices("View", "Report", "Update", "Change status"));
                                break;
                            case "View Customers":
                                AnsiConsole.MarkupLine("[green]View Customers selected[/]");
                                //select customer by contact
                                break;
                            case "Change user":
                                IsAdminMenuRunning = false;
                                break;
                        }
                    }
                }
                else if (IsAppRunning == false)
                {
                    Console.WriteLine("Application is terminated");
                }
                else if(IsAdmin == false)
                {
                    //manager side
                    IsManagerRunning = true;
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
    }
}