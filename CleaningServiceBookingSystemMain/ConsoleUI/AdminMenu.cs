using CleaningServiceBookingSystemMain;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;
using Microsoft.Data.SqlClient;
using System;
using System.Linq.Expressions;
using CleaningServiceBookingSystemMain.ConsoleUI.InputMethods;
using CleaningServiceBookingSystemMain.Application.Interfaces;

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


            InMemoryRepositoryAdmins inMemoryRepositoryAdmins = new InMemoryRepositoryAdmins();
            password =inMemoryRepositoryAdmins.GetAdminPasswordByUsername(admins.Username);
            while (password == null)
            {
                AnsiConsole.MarkupLine("[red]No admin by that username[/]");
                admins = adminInput.GetAdminInput();
                password = inMemoryRepositoryAdmins.GetAdminPasswordByUsername(admins.Username);
                Encryption cryptography = new Encryption();//creates encryption class
                IsCorrectPassword = cryptography.VerifyPassword(password, admins.AdminPassword);
                while (IsCorrectPassword == false)
                {
                    AnsiConsole.MarkupLine("[red]Incorrect password[/]");
                    admins = adminInput.GetAdminInput();//get new input.....................................................................................................................................................
                    IsCorrectPassword = cryptography.VerifyPassword(password, admins.AdminPassword);
                }
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

                        if (createBookingChoices == "Existing customer")            //Existing customer chosen from create booking menu
                        {
                            IsConfirmData = false;
                            while (IsConfirmData == false)
                            {
                                AnsiConsole.MarkupLine("[green]Existing customer selected[/]");
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the customer details correct:")
                                        .AddChoices("Yes", "No"));

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;                                             //Confirms the correct customer
                                }
                                else
                                {
                                    IsConfirmData = false;                                            //loops to get customer input again
                                }
                            }
                                
                        }
                        else if (createBookingChoices == "New customer")            //create new customer chosen from create booking menu
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
                                    InMemoryRepositoryCustomers inMemoryRepositoryCustomers = new InMemoryRepositoryCustomers();
                                    inMemoryRepositoryCustomers.Add(customers);                                                         //saves customer data to sql
                                }
                                else
                                {
                                    IsConfirmData = false;                                            // loops to get customer input again
                                }
                            }
                        }
                        InMemoryRepositoryBookings createBooking = new InMemoryRepositoryBookings();
                        Bookings newBooking = new Bookings();

                        AddOnInput addOnInput = new AddOnInput();
                        addOnInput.GetAddOnInput(ref newBooking);
                        //bookingAddOns input
                        BookingInput bookingInput = new BookingInput();
                        newBooking = bookingInput.GetBookingInput();

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
                                createBooking.Add(newBooking);                  //save booking data to sql
                            }
                            else
                            {
                                IsConfirmData = false;                   // loop it
                            }
                        }
                        break;
                    case "Create New Customer":                                 //create new customer chosen from admin menu

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
                                Console.WriteLine($"Booking Date\tNumber of rooms \tBooking Status\tTotal Amount\tCreated by\tCreated at\tCarpeted Rooms\tCustomer Name\tCustomer Address"); //display headers for bookings
                                foreach (var element in bookings)
                                {
                                    Console.WriteLine($"{element.BookingDate}\t{element.NumberOfRooms}\t{element.BookingStatus}\t{element.TotalAmount}\t{element.CreatedBy}\t{element.CreatedAt}\t{element.CarpetedRooms}\t customers name then address"); //displays booking info then repeats till last booking
                                }
                                break;
                            case "Report":

                                break;
                            case "Change status":
                                //get booking by customer/date
                                //show booking? then confirmation
                                IsConfirmData = false;
                                while (IsConfirmData == false)
                                {
                                    var confirmSelectedBookingChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the booking details correct:")
                                        .AddChoices("Yes", "No"));
                                    switch (confirmSelectedBookingChoices)
                                    {
                                        case "Yes":
                                            IsConfirmData = true;
                                            var changeStatusChoices = AnsiConsole.Prompt(
                                                new SelectionPrompt<string>()
                                                .Title("Choose option:")
                                                .AddChoices("Pending", "Confirmed", "Completed", "Cancelled"));
                                            switch (changeStatusChoices)
                                            {
                                                case "Pending":

                                                    break;
                                                case "Confirmed":

                                                    break;
                                                case "Completed":

                                                    break;
                                                case "Cancelled":

                                                    break;
                                            }
                                            break;
                                        case "No":

                                            break;
                                    }
                                }       
                                break;
                            case "Update":
                                //input Date and Customer
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
                                Encryption newCryptography = new Encryption();//creates encryption class
                                newAdmin.AdminPassword = newCryptography.HashPassword(newAdmin.AdminPassword);
                                newInMemoryRepositoryAdmins.Add(newAdmin);                      //saves customer data to sql
                                IsConfirmData = true;
                            }
                            else
                            {
                                IsConfirmData = false;                      // loops to get admin input again
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
