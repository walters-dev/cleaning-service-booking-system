using Spectre.Console;
using System;
using RedAcademy.Encryption;

namespace RedAcademy.Admin
{


    public class AdminMenu
    {
        public void ViewAdminMenu()
        {

            //declare and intialize variables
            bool IsAdminMenuRunning, IsConfirmData;
            string username, password;
            Console.WriteLine("Enter Username:");
            username = Console.ReadLine();
            //username validation

            Encryption.Encryption cryptography = new Encryption.Encryption();
            Console.WriteLine("Enter Password:");
            password = Console.ReadLine();
            //cryptography.VerifyPassword(password, password);
            //password validation
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Signed in[/]");
            IsAdminMenuRunning = true;
            while (IsAdminMenuRunning == true)
            {
                var adminChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option")
                    .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customers", "Change user", "Add Admin"));
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
                            IsConfirmData = false;
                            while (IsConfirmData == false)
                            {
                                AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                                //new customer proccess 
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the customer details correct:")
                                    .AddChoices("Yes", "No"));

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;
                                    //save customer data to sql
                                }
                                else
                                {
                                    IsConfirmData = false; // loop it
                                }
                            }
                        }
                        /*
                        System displays house types and service types from SQL Server
                        Staff enters number of rooms, booking date, add-ons and recurring option.
                        System validates all inputs and calculates subtotal, discount, surcharge and final total
                        */
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            var confirmBookingChoice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the booking details correct:")
                                    .AddChoices("Yes", "No"));

                            if (confirmBookingChoice == "Yes")
                            {
                                IsConfirmData = true;
                                //save booking data to sql
                            }
                            else
                            {
                                IsConfirmData = false; // loop it
                            }
                        }
                        break;
                    case "Create New Customer":

                        AnsiConsole.MarkupLine("[green]New Customer selected[/]");

                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                            //new customer proccess 
                            var confirmNewCusChoices = AnsiConsole.Prompt(
                               new SelectionPrompt<string>()
                               .Title("Is the customer details correct:")
                               .AddChoices("Yes", "No"));

                            if (confirmNewCusChoices == "Yes")
                            {
                                IsConfirmData = true;
                                //save customer data to sql
                            }
                            else
                            {
                                IsConfirmData = false; // loop it
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
                    case "Add Admin":
                        Console.WriteLine(cryptography.HashPassword(password));

                        break;
                    case "Change user":
                        IsAdminMenuRunning = false;
                        break;
                    
                }
            }

        }
    }
}
