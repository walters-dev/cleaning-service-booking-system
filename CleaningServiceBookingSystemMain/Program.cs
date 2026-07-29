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
                    if (key.Key == ConsoleKey.D1)
                    {
                        IsAdmin = true;
                        IsUserSelected = true;
                        Console.WriteLine("Booking Administrator is selected");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if (key.Key == ConsoleKey.D2)
                    {
                        IsAdmin = false;
                        IsUserSelected = true;
                        Console.WriteLine("Operations Manager is selected");
                        Thread.Sleep(2000);
                        Console.Clear();

                    }
                    else if (key.Key == ConsoleKey.D3)
                    {
                        
                        IsAppRunning = false;
                        
                        break;
                    }
                    else
                    {
                        key = Console.ReadKey(true);
                        IsUserSelected = false;
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
                    IsAdminMenuRunning = true;
                    while (IsAdminMenuRunning == true)
                    {
                        var adminChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose menu option")
                            .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customers","Change user"));
                        if (adminChoices == "Create Booking")
                        {
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

                        }
                        else if (adminChoices == "Create New Customer")
                        {

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
                        }
                        else if (adminChoices == "View Bookings")
                        {
                            //enter booking date and customer
                            AnsiConsole.MarkupLine("[green]View Bookings selected[/]");
                            var confirmNewCusChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the customer details correct:")
                                    .AddChoices("View", "Report", "Update", "Change status"));
                        }
                        else if (adminChoices == "View Customers")
                        {
                            AnsiConsole.MarkupLine("[green]View Customers selected[/]");
                            //select customer by contact
                        }
                        else if (adminChoices == "Change user")
                        {
                            IsAdminMenuRunning = false;
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
                        if (managerChoices == "Bookings")
                        {
                            AnsiConsole.MarkupLine("[green]Booking selected[/]");
                        }
                        else if (managerChoices == "Summaries")
                        {
                            AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        }
                        else if (managerChoices == "Trends")
                        {
                            AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        }
                        else if(managerChoices == "Change user")
                        {
                            IsManagerRunning = false;
                        }
                    }
                }
            }
            
        }
    }
}