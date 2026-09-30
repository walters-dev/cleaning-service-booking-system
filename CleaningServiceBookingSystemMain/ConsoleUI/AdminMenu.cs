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
using CleaningServiceBookingSystemMain.Domain.Services;
using CleaningServiceBookingSystemMain.Application;

namespace CleaningServiceBookingSystemMain.ConsoleUI
{


    public class AdminMenu
    {
        public void ViewAdminMenu()
        {

            //declare and intialize variables
            bool IsAdminMenuRunning, IsConfirmData, IsCorrectPassword;
            string username, password, email, phonenumber;
            //creation of classes and services
            IAddOnsRepository addOnsRepository = new RepositoryAddOns();
            IAddOnsService addOnsService = new AddOnsService(addOnsRepository);
            IAdminRepository adminRepository = new RepositoryAdmins();
            IAdminService adminService = new AdminService(adminRepository);
            ICustomerRepository customerRepository = new RepositoryCustomers();
            ICustomerService customerService = new CustomerService(customerRepository);
            IBookingsRepository bookingsRepository = new RepositoryBookings();
            IBookingService bookingService = new BookingService(bookingsRepository);
            IBookingAddOnsRepository bookingAddOnsRepository = new RepositoryBookingAddOns();
            IBookingAddOnService bookingAddOnService = new BookingAddOnService(bookingAddOnsRepository);   
            IHouseTypesRepository houseTypesRepository = new RepositoryHouseTypes();
            IHouseTypeService houseTypeService = new HouseTypeService(houseTypesRepository);
            IServiceTypesRepository serviceTypesRepository = new RepositoryServiceTypes();
            IServiceTypesService serviceTypesService = new ServiceTypesService(serviceTypesRepository);

            Customers customersBooking = new Customers();
            HouseTypes houseTypes = new HouseTypes();
            ServiceTypes serviceTypes = new ServiceTypes();
            BookingAddOns bookingAddOns = new BookingAddOns();
            DiscountService discountService = new DiscountService();
            PricingService pricingService = new PricingService(discountService);
            AddOnInput addOnInput = new AddOnInput(addOnsRepository);
            CustomerInput customerInput = new CustomerInput(customerRepository);
            HouseTypeInput houseTypeInput = new HouseTypeInput();
            ServiceTypeInput serviceTypeInput = new ServiceTypeInput();

            ExistingAdmin adminLogInInput = new ExistingAdmin();
            Admins admins = new Admins();
            Bookings singleBooking = new Bookings();
            BookingInput bookingInput = new BookingInput(bookingsRepository);

            Admins adminInDataSource = new Admins();
            Encryption cryptography = new Encryption();
            bool IsCorrectAdmin;
            admins = adminLogInInput.GetAdminInput();//gets new admin log in input
            adminInDataSource = adminService.FindAdminPassword(admins.Username);//finds the admin in data source
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
            while (IsCorrectAdmin == false)//runs if admin input incorrect
            {
                if (adminInDataSource.Username != admins.Username)          //checks if username exists
                {
                    AnsiConsole.MarkupLine("[red]No admin by that username[/]");
                    admins = adminLogInInput.GetAdminInput();                   //gets new admin log in input
                    adminInDataSource = adminService.FindAdminPassword(admins.Username);//finds the admin in data source
                    IsCorrectAdmin = false;
                    continue;
                }
                IsCorrectPassword = cryptography.VerifyPassword(adminInDataSource.AdminPassword, admins.AdminPassword);//returns bool true if password is correct
                if (IsCorrectPassword == false)
                {
                    AnsiConsole.MarkupLine("[red]Incorrect password[/]");
                    admins = adminLogInInput.GetAdminInput();               //gets new admin log in input
                    adminInDataSource = adminService.FindAdminPassword(admins.Username);//finds the admin in data source
                    IsCorrectAdmin = false;
                }
                else
                {
                    IsCorrectAdmin = true;
                }
            }

            Console.Clear();            //clears console so that the username and password
            AnsiConsole.MarkupLine("[green]Signed in[/]");
            IsAdminMenuRunning = true;              //keeps admin menu in loop
            while (IsAdminMenuRunning == true)
            {
                var adminChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option")
                    .AddChoices("Create Booking", "Create New Customer", "Bookings", "View Customer", "Add Admin", "Return to Main Menu")); // display admin menu options
                switch (adminChoices)
                {
                    case "Create Booking":                                                  //create booking chosen from admin menu
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Create booking selected[/]");
                        var createBookingChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose customer:")
                            .AddChoices("Existing customer", "New customer"));
                        
                        if (createBookingChoices == "Existing customer")            //Existing customer chosen from create booking menu
                        {
                            AnsiConsole.MarkupLine("[green]Existing customer selected[/]");
                            IsConfirmData = false;
                            while (IsConfirmData == false)
                            {
                                phonenumber = customerInput.GetPhoneNumber();
                                customersBooking = customerService.FindCustomerWithPhoneNumber(phonenumber);
                                if (customersBooking.PhoneNumber == null)       //if no customer found
                                {
                                    AnsiConsole.MarkupLine("[red]Customer does not exist[/]");
                                    continue;
                                }
                                var existingCustomerTable = new Table()
                                    .HideRowSeparators()
                                    .NoBorder()
                                    .AddColumn("Full name: ")
                                    .AddColumn(customersBooking.FullName)
                                    .AddRow("Phone number: ", customersBooking.PhoneNumber)
                                    .AddRow("Address: ", customersBooking.PhyAddress)
                                    .AddRow("Email: ", customersBooking.Email);
                                var existingCustomerPanel = new Panel(existingCustomerTable);
                                existingCustomerPanel.Header("Customer Information");
                                AnsiConsole.Write(existingCustomerPanel);
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
                                customersBooking = customerInput.GetCustomerInput(admins.Username);
                                var confirmNewCusChoices = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the customer details correct:")
                                    .AddChoices("Yes", "No"));                          // display confirmation options to see if it is the correct customer information

                                if (confirmNewCusChoices == "Yes")
                                {
                                    IsConfirmData = true;
                                    customerService.RegisterCustomer(customersBooking);                                 //saves customer data to sql
                                    AnsiConsole.MarkupLine("[green]Customer successfully added[/]");
                                }
                                else
                                {
                                    IsConfirmData = false;                                            // loops to get customer input again
                                }
                            }
                        }
                        
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            int carpetedRooms = 0;
                            IList<AddOnSelection> addOns = addOnInput.GetAddOnInput(ref carpetedRooms);
                            singleBooking.CarpetedRooms = carpetedRooms;
                            bookingAddOns.Quantity = addOns.Count;
                            bookingAddOns.LineAmount = pricingService.CalculateAddOnTotal(singleBooking, addOns);
                            singleBooking = bookingInput.GetBookingInput(carpetedRooms, addOns, customersBooking.PhoneNumber, admins.Username, customersBooking.CustomerId);
                            /*
                            System displays house types and service types from SQL Server
                            Staff enters number of rooms, booking date, add-ons and recurring option.
                            System validates all inputs and calculates subtotal, discount, surcharge and final total
                            */
                            var confirmBookingChoice = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("Is the booking details correct:")
                                    .AddChoices("Yes", "No"));                  // display confirmation options to see if it is the correct booking information

                            if (confirmBookingChoice == "Yes")
                            {
                                IsConfirmData = true;
                                bookingService.RegisterBooking(singleBooking);
                                int count = bookingAddOnService.FindLastRowAddOnBookings();
                                //save booking data to storage
                                if (addOns.Count != 0) //checks if there was any addOns selected
                                {
                                    foreach (var addOn in addOns)//goes through each addOn selected and adds them to BookingAddOns to storage
                                    {
                                        bookingAddOns.AddOnId = addOn.AddOn.AddOnId;
                                        bookingAddOns.BookingId = singleBooking.BookingId;
                                        bookingAddOns.BookingAddOnId = "BA" + (count + 1);//+1 so that it does not overlap with the other primary keys
                                        count ++;
                                        bookingAddOnService.RegisterBookingAddOn(bookingAddOns);
                                        AnsiConsole.MarkupLine("[green]Booking successfully added[/]");
                                    }
                                }
                                
                            }
                            else
                            {
                                Console.Clear();
                                IsConfirmData = false;                   // clears console and loops to get bookings input again
                            }
                        }
                        break;
                    case "Create New Customer":                                 //create new customer chosen from admin menu
                        Console.Clear();
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AnsiConsole.MarkupLine("[green]New Customer selected[/]");
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
                    case "Bookings":                                                           //view bookings chosen from admin menu
                        //enter booking date and customer
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]View Bookings selected[/]");
                        var viewBookingsChoices = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Choose option:")
                                .AddChoices("View", "Report", "Update", "Change status"));         // display view booking menu options
                        switch (viewBookingsChoices)
                        {
                            case "View":
                                IList<Bookings> bookings = bookingService.ViewAllBookings();
                                var allBookingsTable = new Table()
                                .HeavyHeadBorder()
                                .ShowRowSeparators()
                                .AddColumn("Booking Date")
                                .AddColumn("Number of rooms")
                                .AddColumn("Booking Status")
                                .AddColumn("Total Amount")
                                .AddColumn("Created by")
                                .AddColumn("Created at")
                                .AddColumn("Carpeted Rooms")
                                .AddColumn("Recurring Type")
                                .AddColumn("Customer Name")
                                .AddColumn("Customer Address")
                                .AddColumn("Service Type")
                                .AddColumn("House Type");//creates headers for bookings 
                                foreach (var booking in bookings)
                                {
                                    customersBooking = customerService.FindCustomer(booking.CustomerId);
                                    houseTypes = houseTypeService.FindHouseType(booking.HouseTypeId);
                                    serviceTypes = serviceTypesService.FindServiceType(booking.ServiceTypeId);
                                    allBookingsTable.AddRow(booking.BookingDate.Value.Date.ToString("dd MMM yyyy"), booking.NumberOfRooms.ToString(), booking.BookingStatus, booking.TotalAmount.ToString("C"), booking.CreatedBy, booking.CreatedAt.Value.Date.ToString("dd MMM yyyy"), booking.CarpetedRooms.ToString(), booking.RecurringBookingType, customersBooking.FullName, customersBooking.PhyAddress, serviceTypes.ServiceName, houseTypes.Name);
                                    //displays booking info then repeats till last booking
                                }
                                AnsiConsole.Write(allBookingsTable);
                                break; 
                            case "Report":
                                IList <Bookings> bookingsReport = bookingService.ViewBookingsCreatedToday();
                                if (bookingsReport.Count == 0)
                                {
                                    AnsiConsole.MarkupLine("[red]No bookings have been created today yet[/]");
                                    break;
                                }
                                Table bookingsReportTable = new Table()
                                    .AddColumn("Booking Status")
                                    .AddColumn("Recurring Type")
                                    .AddColumn("Number of Rooms")
                                    .AddColumn("Carpeted Rooms")
                                    .AddColumn("Created by")
                                    .AddColumn("Total Amount");
                                foreach (var booking in bookingsReport)
                                {
                                    bookingsReportTable.AddRow(booking.BookingStatus, booking.RecurringBookingType, booking.NumberOfRooms.ToString(), booking.CarpetedRooms.ToString(), booking.CreatedBy, booking.TotalAmount.ToString("C"));
                                }
                                AnsiConsole.Write(bookingsReportTable);
                                break;
                            case "Change status":
                                //get booking by customer/date
                                //show booking? then confirmation
                                //phonenumber = customerInput.GetPhoneNumber();//validation that it exists is needed...........................................................................................
                                //bookingInput.GetSingleBookingDateInput();
                                //singleBooking = bookingService.FindCustomerBookingHistory(phonenumber);//validation that it exists is needed............................................................................
                                //procdure to find booking from email and date needed and validation that it exists is needed
                                //FindBookingsByPhoneNumberAndDate
                                
                                IsConfirmData = false;
                                while (IsConfirmData == false)
                                {
                                    singleBooking = bookingService.FindBookingsByPhoneNumberAndDate(customerInput.GetPhoneNumber(), bookingInput.GetSingleBookingDateInput());      //get booking by customer/date
                                    if (singleBooking.BookingId == null)
                                    {
                                        AnsiConsole.MarkupLine("[red]Booking does not exist[/]");
                                        break;
                                    }
                                    Table singleBookingTable = new Table()
                                    .HideRowSeparators()
                                    .NoBorder()
                                    .AddColumn("Customer: ")
                                    .AddColumn(customerService.FindCustomer(singleBooking.CustomerId).FullName)
                                    .AddRow("Booking date: ", singleBooking.BookingDate.Value.Date.ToString())
                                    .AddRow("Booking Status: ", singleBooking.BookingStatus)
                                    .AddRow("Total Amount: ", singleBooking.TotalAmount.ToString("C"));
                                    var singleBookingPanel = new Panel(singleBookingTable);
                                    singleBookingPanel.Header("Customer Information");
                                    AnsiConsole.Write(singleBookingPanel);           //displays customers info
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
                                                    singleBooking.UpdatedBy = admins.Username;
                                                    singleBooking.UpdatedAt = DateTime.Today;
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Confirmed":
                                                    singleBooking.BookingStatus = "Confirmed";
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Completed":
                                                    singleBooking.BookingStatus = "Completed";
                                                    singleBooking.UpdatedBy = admins.Username;
                                                    singleBooking.UpdatedAt = DateTime.Today;
                                                    bookingService.AmendBookingStatus(singleBooking);
                                                    break;
                                                case "Cancelled":
                                                    singleBooking.UpdatedBy = admins.Username;
                                                    singleBooking.UpdatedAt = DateTime.Today;
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
                                // updatedby, updatedat, recalc, addons which means delete booking addons where  addonid = addonid, change date, num of rooms carpeted rooms, number of rooms, house type, service type, isreccuring, recurring type
                                IsConfirmData = false;
                                while (IsConfirmData == false)
                                {
                                    singleBooking = bookingService.FindBookingsByPhoneNumberAndDate(customerInput.GetPhoneNumber(), bookingInput.GetSingleBookingDateInput());
                                    //confirm exists
                                    if (singleBooking.BookingId == null)
                                    {
                                        AnsiConsole.MarkupLine("[red]Booking does not exist[/]");
                                        continue;
                                    }
                                    Table singleBookingTable = new Table()
                                    .HideRowSeparators()
                                    .NoBorder()
                                    .AddColumn("Customer: ")
                                    .AddColumn(customerService.FindCustomer(singleBooking.CustomerId).FullName)
                                    .AddRow("Booking date: ", singleBooking.BookingDate.Value.Date.ToString())
                                    .AddRow("Booking Status: ", singleBooking.BookingStatus)
                                    .AddRow("Total Amount: ", singleBooking.TotalAmount.ToString("C"));
                                    var singleBookingPanel = new Panel(singleBookingTable);
                                    singleBookingPanel.Header("Customer Information");
                                    AnsiConsole.Write(singleBookingPanel);
                                    //show booking
                                    var confirmCorrectUpdateBookingChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the booking details correct:")
                                        .AddChoices("Yes", "No"));
                                    switch (confirmCorrectUpdateBookingChoices)
                                    {
                                        case "Yes":
                                            IsConfirmData = true;
                                            break;
                                        case "No":
                                            Console.Clear();
                                            break;
                                    }
                                }
                                
                                IsConfirmData = false;
                                while (IsConfirmData == false)
                                {
                                    UpdateInput updateInput = new UpdateInput(houseTypeInput, serviceTypeInput, houseTypeService, serviceTypesService, addOnsService);
                                    IList<AddOnSelection>? addOnSelections;
                                    singleBooking = updateInput.GetUpdateInput(singleBooking, out addOnSelections, admins.Username);
                                    var confirmSelectedUpdateChoices = AnsiConsole.Prompt(
                                        new SelectionPrompt<string>()
                                        .Title("Is the booking details correct:")
                                        .AddChoices("Yes", "No"));

                                    switch (confirmSelectedUpdateChoices)
                                    {
                                        case "Yes":
                                            bookingService.AmendBooking(singleBooking);
                                            if (addOnSelections != null)
                                            {
                                                bookingAddOns.Quantity = addOnSelections.Count;
                                                bookingAddOns.LineAmount = pricingService.CalculateAddOnTotal(singleBooking, addOnSelections);

                                                bookingAddOnService.RemoveBookingAddOnsByBookingId(singleBooking.BookingId);
                                                int count = bookingAddOnService.FindLastRowAddOnBookings();
                                                foreach (var addOnSelection in addOnSelections)
                                                {
                                                    bookingAddOnService.RemoveBookingAddOnsByBookingId(singleBooking.BookingId);
                                                    bookingAddOns.BookingId = singleBooking.BookingId;
                                                    bookingAddOns.AddOnId = addOnSelection.AddOn.AddOnId;
                                                    bookingAddOns.BookingAddOnId = "BA" + (count +1);//+1 so that it does not overlap with the other primary keys
                                                    bookingAddOnService.RegisterBookingAddOn(bookingAddOns);
                                                }
                                            }
                                            //save to sql
                                            IsConfirmData = true;
                                            break;
                                        case "No":

                                            break;
                                    }
                                }
                                break;
                        }

                        break;
                    case "View Customer":                                                          //view customers chosen from admin menu
                        AnsiConsole.MarkupLine("[green]View Customer selected[/]");
                        Customers customer = new Customers();
                        phonenumber = customerInput.GetPhoneNumber();
                        customer = customerService.FindCustomerWithPhoneNumber(phonenumber);
                        if (customer.FullName == null)
                        {
                            AnsiConsole.MarkupLine("[red]Customer does not exist[/]");
                            break;
                        }
                        var viewCustomerTable = new Table()
                                    .HideRowSeparators()
                                    .NoBorder()
                                    .AddColumn("Full name: ")
                                    .AddColumn(customer.FullName)
                                    .AddRow("Phone number: ", customer.PhoneNumber)
                                    .AddRow("Address: ", customer.PhyAddress)
                                    .AddRow("Email: ", customer.Email);
                        var viewCustomerPanel = new Panel(viewCustomerTable);
                        viewCustomerPanel.Header("Customer Information");
                        AnsiConsole.Write(viewCustomerPanel);
                        break;
                    case "Add Admin":                     //add admin chosen from admin menu
                        IsConfirmData = false;
                        while (IsConfirmData == false)
                        {
                            AdminInput newAdminInput = new AdminInput(adminRepository);
                            Admins newAdmin = new Admins();
                            newAdmin = newAdminInput.GetAdminInput();                 //gets user input

                            var confirmNewAdminChoices = AnsiConsole.Prompt(
                                   new SelectionPrompt<string>()
                                   .Title("Is the admin details correct:")
                                   .AddChoices("Yes", "No"));
                            if (confirmNewAdminChoices == "Yes")
                            {

                                newAdmin.AdminPassword = cryptography.HashPassword(newAdmin.AdminPassword);
                                adminService.RegisterAdmin(newAdmin);                      //saves customer data to sql
                                AnsiConsole.MarkupLine("[green]Admin successfully added[/]");
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
