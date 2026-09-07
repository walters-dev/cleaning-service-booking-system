using CleaningServiceBookingSystem.Application;
using CleaningServiceBookingSystemMain;
using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.InputMethods;
using Spectre.Console;
using Microsoft.Data.SqlClient;
using System;
using System.Linq.Expressions;

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
           // GetAdminPassword getAdminPassword = new GetAdminPassword(admins.Username);
            InMemoryRepositoryAdmins inMemoryRepositoryAdmins = new InMemoryRepositoryAdmins();
            password =inMemoryRepositoryAdmins.GetAdminPasswordByUsername(admins.Username);
            while (password == null)
            {
                AnsiConsole.MarkupLine("[red]No admin by that username[/]");
                admins = adminInput.GetAdminInput();
                password = inMemoryRepositoryAdmins.GetAdminPasswordByUsername(admins.Username);
            }
            Encryption cryptography = new Encryption();//creates encryption class
            IsCorrectPassword = cryptography.VerifyPassword(password, admins.AdminPassword);
            while (IsCorrectPassword == false)
            {
                AnsiConsole.MarkupLine("[red]Incorrect password[/]");
                admins.AdminPassword = Console.ReadLine();//get new input.....................................................................................................................................................
                IsCorrectPassword = cryptography.VerifyPassword(password, admins.AdminPassword);
            }
            Console.Clear();
            AnsiConsole.MarkupLine("[green]Signed in[/]");
            IsAdminMenuRunning = true;              //keeps admin menu in loop
            while (IsAdminMenuRunning == true)
            {
                var adminChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option")
                    .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customer", "Change user", "Add Admin")); // display admin menu options
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
                                customers = customerInput.GetCustomerInput(admins.Username);
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
                        InMemoryRepositoryBookings createBooking = new InMemoryRepositoryBookings();
                        Bookings newBooking = new Bookings();
                        ServiceTypeInput serviceTypeInput = new ServiceTypeInput();//i thonk all of this is supposed to be in BookingInput.............................................................................
                        HouseTypeInput houseTypeInput = new HouseTypeInput();

                        //BookingInput bookingInput = new BookingInput(houseTypeInput, serviceTypeInput.GetServiceTypeInput(),);
                       // newBooking = ;
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
                                createBooking.Add(newBooking);
                            }
                            else
                            {
                                IsConfirmData = false; // loop it
                            }
                        }
                        break;
                    case "Create New Customer":                                         //create new customer chosen from admin menu

                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AnsiConsole.MarkupLine("[green]New Customer selected[/]");
                            //new customer proccess 
                            CustomerInput customerInput = new CustomerInput();
                            Customers customers = new Customers();
                            customers = customerInput.GetCustomerInput(admins.Username);

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
                                AnsiConsole.MarkupLine("[green]Customer successfully added[/]");
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
                                .AddChoices("View", "Report", "Update", "Change status"));                      //update could be removed?? also report of what................................................................................................................
                        InMemoryRepositoryBookings viewBookings = new InMemoryRepositoryBookings();
                        switch (viewBookingsChoices)
                        {
                            case "View":
                                Bookings booking = new Bookings();
                                IList<Bookings> bookings = viewBookings.GetBookings();
                                Console.WriteLine($"Booking Date\tNumber of rooms \tBooking Status\tTotal Amount\tCreated by\tCreated at\tUpdated at\tUpdated by\tCustomer Name\tCustomer Address"); //display headers for bookings
                                foreach (var element in bookings)
                                {
                                    Console.WriteLine($"{element.BookingDate}\t{element.NumberOfRooms}\t{element.BookingStatus}\t{element.TotalAmount}\t{element.CreatedBy}\t{element.CreatedAt}\t{element.UpdatedAt}\t{element.UpdatedBy} customers name then address"); //displays booking info then repeats till last booking
                                }
                                break;
                            case "Report":
                                break;
                            case "Change status":
                                break;
                        }
                        
                        break;
                    case "View Customer":                                                          //view customers chosen from admin menu
                        AnsiConsole.MarkupLine("[green]View Customer selected[/]");
                        //select customer by contact
                        //input for email.....................................................................................
                        InMemoryRepositoryCustomers viewCustomer = new InMemoryRepositoryCustomers();
                        Customers customer = new Customers();
                        customer = viewCustomer.GetCustomersByEmail(Console.ReadLine());
                        Console.WriteLine($"{customer.FullName} {customer.PhoneNumber} {customer.Email} {customer.PhyAddress}");
                        break;
                    case "Add Admin":                                                           //add admin chosen from admin menu
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AdminInput newAdminInput = new AdminInput();
                            Admins newAdmin = new Admins();
                            newAdmin = newAdminInput.GetAdminInput();                 //gets user input
                            InMemoryRepositoryAdmins newInMemoryRepositoryAdmins = new InMemoryRepositoryAdmins();

                            var confirmNewAdminChoices = AnsiConsole.Prompt(
                                   new SelectionPrompt<string>()
                                   .Title("Is the admin details correct:")
                                   .AddChoices("Yes", "No"));
                            if (confirmNewAdminChoices == "Yes")
                            {
                                newAdmin.AdminPassword = cryptography.HashPassword(newAdmin.AdminPassword);
                                newInMemoryRepositoryAdmins.Add(newAdmin);
                                IsConfirmData = true;
                            }
                            else
                            {
                                IsConfirmData = false;
                            }
                            
                        }
                        break;
                    case "Change user":                                                           //add change user chosen from admin menu
                        IsAdminMenuRunning = false;                                               //this will exit the admin menu loop
                        break;
                    
                }
            }

        }
    }
}
