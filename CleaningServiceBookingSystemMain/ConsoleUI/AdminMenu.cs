using CleaningServiceBookingSystemMain;
using CleaningServiceBookingSystemMain.Application.InputMethods;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.ConsoleUI.InputMethods;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using System;
using System.Linq.Expressions;
using CleaningServiceBookingSystemMain.Application.Services;

namespace CleaningServiceBookingSystemMain.ConsoleUI
{


    public class AdminMenu
    {
        public void ViewAdminMenu()
        {

            //declare and intialize variables
            bool IsAdminMenuRunning, IsConfirmData, IsCorrectPassword;
            string username, password, email;
            //creation of classes and services
            AddOnInput addOnInput = new AddOnInput();
            IAdminRepository adminRepository = new InMemoryRepositoryAdmins();
            AdminService adminService = new AdminService(adminRepository);
            ICustomerRepository customerRepository = new InMemoryRepositoryCustomers();
            CustomerService customerService = new CustomerService(customerRepository);
            IBookingsRepository bookingsRepository = new InMemoryRepositoryBookings();
            BookingService bookingService = new BookingService(bookingsRepository);
            CustomerInput customerEmailInput = new CustomerInput();

            ExistingAdmin adminLogInInput = new ExistingAdmin();
            Admins admins = new Admins();
            Bookings singleBooking = new Bookings();
            BookingInput bookingInput = new BookingInput();
            //gets user input
            /*===========================validation=================================*/
            Admins adminInDataSource = new Admins();
            Encryption cryptography = new Encryption();
            bool IsCorrectAdmin;
            admins = adminLogInInput.GetAdminInput();
            adminInDataSource = adminService.FindAdminPassword(admins.Username);
            if (adminInDataSource.Username != admins.Username)
            {
                IsCorrectAdmin = false;
            }
            else 
            {
                IsCorrectPassword = cryptography.VerifyPassword(adminInDataSource.AdminPassword, admins.AdminPassword); //returns bool true if password is correct
                if (IsCorrectPassword == false)
                {
                    IsCorrectAdmin = false;
                }
                else
                {
                    IsCorrectAdmin = true;
                }
            }
            while (IsCorrectAdmin == false)
            {
                if (adminInDataSource.Username != admins.Username)          //checks if username exists
                {
                    AnsiConsole.MarkupLine("[red]No admin by that username[/]");
                    admins = adminLogInInput.GetAdminInput();
                    adminInDataSource = adminService.FindAdminPassword(admins.Username);
                    IsCorrectAdmin = false;
                    continue;
                }
                //creates encryption class
                IsCorrectPassword = cryptography.VerifyPassword(adminInDataSource.AdminPassword, admins.AdminPassword);//returns bool true if password is correct
                if (IsCorrectPassword == false)
                {
                    AnsiConsole.MarkupLine("[red]Incorrect password[/]");
                    admins = adminLogInInput.GetAdminInput();                               //gets new admin log in input
                    IsCorrectAdmin = false;
                }
                else
                {
                    IsCorrectAdmin = true;
                }
            }

            /*===========================end validation=================================*/

            Console.Clear();
            AnsiConsole.MarkupLine("[green]Signed in[/]");
            IsAdminMenuRunning = true;              //keeps admin menu in loop
            while (IsAdminMenuRunning == true)
            {
                var adminChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option")
                    .AddChoices("Create Booking", "Create New Customer", "View Bookings", "View Customer", "Add Admin", "Return to Main Menu")); // display admin menu options
                switch (adminChoices)
                {
                    case "Create Booking":                                                  //create booking chosen from admin menu
                        AnsiConsole.MarkupLine("[green]Create booking selected[/]");
                        var createBookingChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose customer:")
                            .AddChoices("Existing customer", "New customer"));
                        Customers customersBooking = new Customers();
                        if (createBookingChoices == "Existing customer")            //Existing customer chosen from create booking menu
                        {
                            IsConfirmData = false;
                            while (IsConfirmData == false)
                            {
                                AnsiConsole.MarkupLine("[green]Existing customer selected[/]");

                                email = customerEmailInput.GetEmail();
                                customersBooking = customerService.FindCustomerWithEmail(email);//validation that it exists is needed........................................................................................
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the customer details correct:")
                                        .AddChoices("Yes", "No"));                      // display confirmation options to see if it is the correct customer

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;                     //Confirms the correct customer
                                }
                                else
                                {
                                    IsConfirmData = false;                     //loops to get customer input again
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
                                customersBooking = customerInput.GetCustomerInput(admins.Username);
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the customer details correct:")
                                    .AddChoices("Yes", "No"));                          // display confirmation options to see if it is the correct customer information

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;
                                    customerService.RegisterCustomer(customersBooking);                                 //saves customer data to sql
                                }
                                else
                                {
                                    IsConfirmData = false;                                            // loops to get customer input again
                                }
                            }
                        }

                        IList<AddOnSelection> addOns = addOnInput.GetAddOnInput(out int carpetedRooms);

                        //bookingAddOns input...........................................................................................................................................

                        singleBooking = bookingInput.GetBookingInput(carpetedRooms, addOns, customersBooking.Email, admins.Username);

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
                                    .AddChoices("Yes", "No"));                  // display confirmation options to see if it is the correct booking information

                            if (confirmBookingChoice == "Yes")
                            {
                                IsConfirmData = true;
                                bookingService.RegisterBooking(singleBooking);                  //save booking data to sql
                            }
                            else
                            {
                                IsConfirmData = false;                   // loops to get bookings input again
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
                               .AddChoices("Yes", "No"));               // display confirmation options to see if it is the correct customer information

                            if (confirmNewCusChoices == "Yes")
                            {
                                IsConfirmData = true;
                                //save customer data to sql
                                customerService.RegisterCustomer(customers);
                                AnsiConsole.MarkupLine("[green]Customer successfully added[/]");
                            }
                            else
                            {
                                IsConfirmData = false;                  // loops to get customer input again
                            }
                        }
                        break;
                    case "View Bookings":                                                           //view bookings chosen from admin menu
                        //enter booking date and customer
                        AnsiConsole.MarkupLine("[green]View Bookings selected[/]");
                        var viewBookingsChoices = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Choose option:")
                                .AddChoices("View", "Report", "Update", "Change status"));         // display view booking menu options
                        switch (viewBookingsChoices)
                        {
                            case "View":
                                IList<Bookings> bookings = bookingService.ViewAllBookings();
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
                                email = customerEmailInput.GetEmail();//validation that it exists is needed...........................................................................................
                                bookingInput.GetSingleBookingDateInput();
                                //singleBooking = bookingService.FindCustomerBookingHistory(email);//validation that it exists is needed............................................................................
                                //procdure to find booking from id and date needed and validation that it exists is needed
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
                                                    singleBooking.BookingStatus = "Pending";
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Confirmed":
                                                    singleBooking.BookingStatus = "Confirmed";
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Completed":
                                                    singleBooking.BookingStatus = "Completed";
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Cancelled":
                                                    singleBooking.BookingStatus = "Cancelled";
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                            }
                                            break;
                                        case "No":

                                            break;
                                    }
                                }
                                break;
                            case "Update":
                                //input Date and Customer to find the booking needed then display the booking then confirm if correct booking
                                break;
                        }

                        break;
                    case "View Customer":                                                          //view customers chosen from admin menu
                        AnsiConsole.MarkupLine("[green]View Customer selected[/]");
                        //input for email.....................................................................................
                        Customers customer = new Customers();
                        email = customerEmailInput.GetEmail();
                        customer = customerService.FindCustomerWithEmail(email);
                        Console.WriteLine($"{customer.FullName} {customer.PhoneNumber} {customer.Email} {customer.PhyAddress}");
                        break;
                    case "Add Admin":                                                           //add admin chosen from admin menu
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AdminInput newAdminInput = new AdminInput();
                            Admins newAdmin = new Admins();
                            newAdmin = newAdminInput.GetAdminInput();                 //gets user input

                            var confirmNewAdminChoices = AnsiConsole.Prompt(
                                   new SelectionPrompt<string>()
                                   .Title("Is the admin details correct:")
                                   .AddChoices("Yes", "No"));
                            if (confirmNewAdminChoices == "Yes")
                            {

                                //newAdmin.AdminPassword = cryptography.HashPassword(newAdmin.AdminPassword);
                                adminService.RegisterAdmin(newAdmin);                      //saves customer data to sql
                                IsConfirmData = true;
                            }
                            else
                            {
                                IsConfirmData = false;                      // loops to get admin input again
                            }

                        }
                        break;
                    case "Return to Main Menu":                                                           //add change user chosen from admin menu
                        IsAdminMenuRunning = false;                                               //this will exit the admin menu loop
                        break;

                }
            }

        }
    }
}
