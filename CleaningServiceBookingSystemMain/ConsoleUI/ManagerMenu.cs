using CleaningServiceBookingSystemMain.Application;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.ConsoleUI.InputMethods;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
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
            //creation of classes and services
            IBookingsRepository bookingsRepository = new RepositoryBookings();
            ICustomerRepository customerRepository = new RepositoryCustomers();
            IBookingService bookingService = new BookingService(bookingsRepository);
            CustomerInput customerInput = new CustomerInput(customerRepository);
            DateRangeInput dateRangeInput = new DateRangeInput();

            string phoneNumber;
            while (IsManagerRunning == true)
            {
                var managerChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option:")
                    .AddChoices("Bookings", "Summaries", "Trends", "Return to Main Menu"));// display manager menu options

                switch (managerChoices)
                {
                    case "Bookings"://Bookings chosen from manager menu
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Booking selected[/]");
                        var bookingChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose booking options:")
                            .AddChoices("Customer Booking History", "Booking List By Range", "Bookings Order By House Type"));// display booking options
                        switch (bookingChoices)
                        {
                            case "Customer Booking History"://Customer Booking History chosen from booking options
                                phoneNumber = customerInput.GetPhoneNumber();//get user input for customer
                                IList<CustomerBookingHistory> bookingsHistory = bookingService.FindCustomerBookingHistory(phoneNumber);//find user inputted customer with bookings in storage
                                if (bookingsHistory.Count == 0)//if no booking history found stop this loop 
                                {
                                    AnsiConsole.MarkupLine("[red]Customer booking history does not exist[/]");
                                    break;
                                }
                                var tableHistory = new Table();
                                tableHistory.DoubleBorder();
                                tableHistory.ShowRowSeparators();
                                tableHistory.BorderColor(Color.Blue);
                                tableHistory.Title($"{bookingsHistory[0].Fullname}'s booking history:");
                                tableHistory.AddColumn("House Type");
                                tableHistory.AddColumn("Service Type");
                                tableHistory.AddColumn("Booking Date");
                                tableHistory.AddColumn("Number of rooms");
                                tableHistory.AddColumn("Total amount");
                                tableHistory.AddColumn("Booking status");//sets up headers for table
                                foreach (var booking in bookingsHistory)
                                {
                                    //adds booking info to table then repeats till last booking
                                    tableHistory.AddRow(booking.HouseName, booking.ServiceName, booking.BookingDate.Date.ToString("dd MMM yyyy"), booking.NumberOfRooms.ToString(), booking.TotalAmount.ToString("C"), booking.BookingStatus);
                                }
                                var centered = Align.Center(tableHistory);
                                AnsiConsole.Write(centered);//displays all bookings asociated with specified customer
                                break;
                            case "Booking List By Range"://Booking List By Range chosen from booking options
                                var dateRange = dateRangeInput.GetDateRangeInput();//get date range input for bookings
                                IList<BookingByDate> bookingsByDate = bookingService.FindAllBookingsInDateRange(dateRange.startDate, dateRange.endDate);//finds bookings within date range from storage
                                if (bookingsByDate.Count == 0)//if no bookings found stop this loop 
                                {
                                    AnsiConsole.MarkupLine("[red]No bookings exist in that date range.[/]");
                                    break;
                                }
                                var tableRange = new Table();
                                tableRange.DoubleBorder();
                                tableRange.ShowRowSeparators();
                                tableRange.AddColumn("Customer name");
                                tableRange.AddColumn("House type");
                                tableRange.AddColumn("Service type");
                                tableRange.AddColumn("Booking Date");
                                tableRange.AddColumn("Number of rooms");
                                tableRange.AddColumn("Total amount");
                                tableRange.AddColumn("Booking status");//sets up headers for table

                                foreach (var booking in bookingsByDate)
                                {
                                    //adds booking info to table then repeats till last booking
                                    tableRange.AddRow(booking.Fullname, booking.HouseName, booking.ServiceName, booking.BookingDate.Date.ToString("dd MMM yyyy"), booking.NumberOfRooms.ToString(), booking.TotalAmount.ToString("C"), booking.BookingStatus.ToString());
                                }
                                AnsiConsole.Write(tableRange);//displays all bookings within date range
                                break;
                            case "Bookings Order By House Type"://Bookings Order By House Type chosen from booking options
                                var chart = new BarChart();
                                chart.Label("Bookings Order By House Type");
                                IList<BookingByHouseType> bookingsByHouses = bookingService.ViewBookingsByHouse();//finds bookings grouped by house type from storage
                                if (bookingsByHouses.Count == 0)//if no bookings found stop this loop 
                                {
                                    AnsiConsole.MarkupLine("[red]No bookings exist yet.[/]");
                                    break;
                                }
                                int countHouse = bookingsByHouses.Count;
                                foreach (var booking in bookingsByHouses)
                                {
                                    if (countHouse % 2 == 0)        //alternates 2 different bar colours used
                                    {
                                        chart.AddItem(booking.HouseName, booking.BookingCount, Color.IndianRed);
                                    }
                                    else
                                    {
                                        chart.AddItem(booking.HouseName, booking.BookingCount, Color.Purple);
                                    }
                                    countHouse -= 1;
                                }
                                AnsiConsole.Write(chart);//displays bookings grouped by house types
                                break;
                        }
                        break;
                    case "Summaries"://Summaries chosen from manager menu
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        var chartMoney = new BreakdownChart();
                        IList < BookingRevenueSummary > summary = bookingService.ViewRevenueSummary();//finds revenue summary bookings info from storage
                        if (summary.Count == 0)//if no bookings found stop this loop 
                        {
                            AnsiConsole.MarkupLine("[red]No bookings have been completed yet.[/]");
                            break;
                        }
                        chartMoney.ShowPercentage();
                        chartMoney.UseValueFormatter((value, culture) => $"R {value:N}");       //formats the items to currrency
                        int countSummary = summary.Count;
                        Table summaryTable = new Table()
                                    .HideRowSeparators()
                                    .NoBorder()
                                    .HideHeaders()
                                    .AddColumn("")
                                    .AddColumn("", col => col.Centered());//centres the 2nd column
                        foreach (var booking in summary)
                        {
                            if (countSummary % 4 == 0) //alternates 4 different bar colours used
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.DarkMagenta_1);
                                summaryTable.AddRow($"[DarkMagenta_1]{booking.ServiceName}: [/]", booking.BookingCount.ToString());
                                //adds summary info to table then repeats till last row
                            }
                            else if(countSummary % 4 == 1)
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.RoyalBlue1);
                                summaryTable.AddRow($"[RoyalBlue1]{booking.ServiceName}: [/]", booking.BookingCount.ToString());
                                //adds summary info to table then repeats till last row
                            }
                            else if (countSummary % 4 == 2)
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.DarkViolet);
                                summaryTable.AddRow($"[DarkViolet]{booking.ServiceName}: [/]", booking.BookingCount.ToString());
                                //adds summary info to table then repeats till last row
                            }
                            else
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.BlueViolet);
                                summaryTable.AddRow($"[BlueViolet]{booking.ServiceName}: [/]", booking.BookingCount.ToString());
                                //adds summary info to table then repeats till last row
                            }
                            countSummary -= 1;
                            
                        }
                        AnsiConsole.Write(chartMoney);//displays breakdown of bookings revenue by service
                        var singleBookingPanel = new Panel(summaryTable);
                        singleBookingPanel.Header("Booking Numbers per Service");
                        Console.WriteLine();
                        AnsiConsole.Write(singleBookingPanel);//displays count of bookings by service
                        break;
                    case "Trends"://Trends chosen from manager menu
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        IList <BookingDiscountUsage> bookingsDiscounts = bookingService.ViewDiscountUsage();//finds bookings with discount info from storage
                        if (bookingsDiscounts.Count == 0)//if no bookings found stop this loop 
                        {
                            AnsiConsole.MarkupLine("[red]No bookings have discounts yet.[/]");
                            break;
                        }
                        var discountTable = new Table();
                        discountTable.DoubleBorder();
                        discountTable.ShowRowSeparators();
                        discountTable.AddColumn("Discount name:");
                        discountTable.AddColumn("Customer name:");
                        discountTable.AddColumn("SubTotal:");
                        discountTable.AddColumn("Discount amount:");
                        discountTable.AddColumn("Amount after discount:");//sets up headers for table
                        foreach (var booking in bookingsDiscounts)
                        {
                            discountTable.AddRow(booking.DiscountName, booking.Fullname, booking.SubTotal.ToString("C"), booking.DiscountAmount.ToString("C"), booking.AmountAfterDiscount.ToString("C"));
                            //adds booking info to table then repeats till last booking
                        }
                        AnsiConsole.Write(discountTable);//displays all discounted bookings created today
                        break;
                    case "Return to Main Menu"://Return to Main Menu chosen from manager menu
                        Console.Clear();
                        IsManagerRunning = false;//this will exit the manager menu loop
                        break;
                }
            }
        }
    }
}
