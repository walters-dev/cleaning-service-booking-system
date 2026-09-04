using CleaningServiceBookingSystem.Application;
using CleaningServiceBookingSystemMain;
using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;
using System;

namespace CleaningServiceBookingSystemMain.ConsoleUI
{


    public class AdminMenu
    {
        public void ViewAdminMenu()
        {

            //declare and intialize variables
            bool IsAdminMenuRunning, IsConfirmData, IsCorrectPassword;
            string username, password;

            AdminInput adminInput = new AdminInput();
            Admins admins = new Admins();
            admins = adminInput.GetAdminInput();                 //gets user input
            GetAdminPassword getAdminPassword = new GetAdminPassword(admins.Username);
            Encryption cryptography = new Encryption();//creates encryption class
            while (IsCorrectPassword = false)
            {
                IsCorrectPassword = cryptography.VerifyPassword(admins.AdminPassword, getAdminPassword.Password);
            }
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Signed in[/]");
            IsAdminMenuRunning = true;              //keeps admin menu in loop
            while (IsAdminMenuRunning == true)
            {
                var adminChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option")
                    .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customers", "Change user", "Add Admin")); // display admin menu options
                switch (adminChoices)
                {
                    case "Create Booking":                                                  //create booking chosen from admin menu
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
                                CustomerInput customerInput = new CustomerInput();
                                Customers customers = new Customers();
                                customers = customerInput.GetCustomerInput();
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the customer details correct:")
                                    .AddChoices("Yes", "No"));

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;
                                    //save customer data to sql
                                    InMemoryRepositoryCustomers inMemoryRepositoryCustomers = new InMemoryRepositoryCustomers();
                                    inMemoryRepositoryCustomers.Add(customers);
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
                    case "Create New Customer":                                         //create new customer chosen from admin menu

                        AnsiConsole.MarkupLine("[green]New Customer selected[/]");

                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                            //new customer proccess 
                            CustomerInput customerInput = new CustomerInput();
                            Customers customers = new Customers();
                            customers = customerInput.GetCustomerInput();

                            var confirmNewCusChoices = AnsiConsole.Prompt(
                               new SelectionPrompt<string>()
                               .Title("Is the customer details correct:")
                               .AddChoices("Yes", "No"));

                            if (confirmNewCusChoices == "Yes")
                            {
                                IsConfirmData = true;
                                //save customer data to sql
                                InMemoryRepositoryCustomers inMemoryRepositoryCustomers = new InMemoryRepositoryCustomers();
                                inMemoryRepositoryCustomers.Add(customers);
                            }
                            else
                            {
                                IsConfirmData = false; // loop it
                            }
                        }
                        break;
                    case "View Bookings":                                                           //view bookings chosen from admin menu
                        //enter booking date and customer
                        AnsiConsole.MarkupLine("[green]View Bookings selected[/]");
                        var viewBookingsChoices = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Choose option:")
                                .AddChoices("View", "Report", "Update", "Change status"));
                        break;
                    case "View Customers":                                                          //view customers chosen from admin menu
                        AnsiConsole.MarkupLine("[green]View Customers selected[/]");
                        //select customer by contact

                        break;
                    case "Add Admin":                                                           //add admin chosen from admin menu
                       //Console.WriteLine(cryptography.HashPassword(password));

                        break;
                    case "Change user":                                                           //add change user chosen from admin menu
                        IsAdminMenuRunning = false;                                               //this will exit the admin menu loop
                        break;
                    
                }
            }

        }
    }
}
