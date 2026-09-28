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
            IBookingsRepository bookingsRepository = new InMemoryRepositoryBookings();
            ICustomerRepository customerRepository = new InMemoryRepositoryCustomers();
            BookingService bookingService = new BookingService(bookingsRepository);
            CustomerInput customerInput = new CustomerInput(customerRepository);
            DateRangeInput dateRangeInput = new DateRangeInput();
            //Customers customer = new Customers();
            string phoneNumber;
            while (IsManagerRunning == true)
            {
                var managerChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option:")
                    .AddChoices("Bookings", "Summaries", "Trends", "Return to Main Menu"));
                
                switch (managerChoices)
                {
                    case "Bookings":
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Booking selected[/]");
                        var bookingChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose booking options:")
                            .AddChoices("Customer Booking History", "Booking List By Range", "Bookings Order By House Type"));
                        switch (bookingChoices)
                        {
                            case "Customer Booking History":
                                phoneNumber = customerInput.GetPhoneNumber();
                                IList<CustomerBookingHistory> bookingsHistory = bookingService.FindCustomerBookingHistory(phoneNumber);
                                if (bookingsHistory.Count == 0)
                                {
                                    AnsiConsole.MarkupLine("[red]Customer does not exist[/]");
                                    break;
                                }
                                var tableHistory = new Table();
                                tableHistory.DoubleBorder();
                                tableHistory.ShowRowSeparators();
                                tableHistory.BorderColor(Color.Blue);
                                tableHistory.Title($"{bookingsHistory[0].Fullname}'s booking history:");
                                //tableHistory.AddColumn("Customer name"); they know who the customer is 
                                tableHistory.AddColumn("House Type");
                                tableHistory.AddColumn("Service Type");
                                tableHistory.AddColumn("Booking Date");
                                tableHistory.AddColumn("Number of rooms");
                                tableHistory.AddColumn("Total amount");
                                tableHistory.AddColumn("Booking status");
                                foreach (var booking in bookingsHistory)
                                {
                                    tableHistory.AddRow(booking.HouseName, booking.ServiceName, booking.BookingDate.Date.ToString("dd MMM yyyy"), booking.NumberOfRooms.ToString(), booking.TotalAmount.ToString("C"), booking.BookingStatus);
                                }
                                var centered = Align.Center(tableHistory);
                                AnsiConsole.Write(centered);
                                break;
                            case "Booking List By Range":
                                var dateRange = dateRangeInput.GetDateRangeInput();
                                IList<BookingByDate> bookingsByDate = bookingService.FindAllBookingsInDateRange(dateRange.startDate, dateRange.endDate);
                                var tableRange = new Table();
                                tableRange.DoubleBorder();
                                tableRange.ShowRowSeparators();
                                tableRange.AddColumn("Customer name");
                                tableRange.AddColumn("House type");
                                tableRange.AddColumn("Service type");
                                tableRange.AddColumn("Booking Date");
                                tableRange.AddColumn("Number of rooms");
                                tableRange.AddColumn("Total amount");
                                tableRange.AddColumn("Booking status");
                                
                                foreach (var booking in bookingsByDate)
                                {
                                    tableRange.AddRow(booking.Fullname, booking.HouseName, booking.ServiceName, booking.BookingDate.Date.ToString("dd MMM yyyy"), booking.NumberOfRooms.ToString(), booking.TotalAmount.ToString("C"), booking.BookingStatus.ToString());
                                }
                                AnsiConsole.Write(tableRange);
                                break;
                            case "Bookings Order By House Type":
                                var chart = new BarChart();
                                chart.Label("Bookings Order By House Type");
                                IList<BookingByHouseType> bookingsByHouses = bookingService.ViewBookingsByHouse();
                                foreach (var booking in bookingsByHouses)
                                {
                                    if (bookingsByHouses.Count % 2 == 0)        //alternates 2 different bar colours used
                                    {
                                        chart.AddItem(booking.HouseName, booking.BookingCount, Color.Aqua);
                                    }
                                    else
                                    {
                                        chart.AddItem(booking.HouseName, booking.BookingCount, Color.Green);
                                    }
                                }
                                AnsiConsole.Write(chart);
                                break;
                        }
                        break;
                    case "Summaries":
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        var chartMoney = new BreakdownChart();
                        //var chart = new BarChart();
                        IList < BookingRevenueSummary > summary = bookingService.ViewRevenueSummary();
                        chartMoney.ShowPercentage();
                        chartMoney.UseValueFormatter((value, culture) => $"R {value:N}");       //formats the items to currrency
                        foreach (var booking in summary)
                        {
                            if (summary.Count % 4 == 0) //alternates 4 different bar colours used
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.Aqua);
                            }
                            else if(summary.Count % 4 == 1)
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.Green);
                            }
                            else if (summary.Count % 4 == 2)
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.Magenta1);
                            }
                            else
                            {
                                chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.Yellow);
                            }
                            
                        }
                        AnsiConsole.Write(chartMoney);
                        break;
                    case "Trends":
                        Console.Clear();
                        AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        IList <BookingDiscountUsage> bookingsDiscounts = bookingService.ViewDiscountUsage();
                        var discountTable = new Table();
                        discountTable.DoubleBorder();
                        discountTable.ShowRowSeparators();
                        discountTable.AddColumn("Discount name:");
                        discountTable.AddColumn("Customer name:");
                        discountTable.AddColumn("SubTotal:");
                        discountTable.AddColumn("Discount amount:");
                        discountTable.AddColumn("Amount after discount:");
                        foreach (var booking in bookingsDiscounts)
                        {
                            discountTable.AddRow(booking.DiscountName, booking.Fullname, booking.SubTotal.ToString("C"), booking.DiscountAmount.ToString("C"), booking.AmountAfterDiscount.ToString("C"));
                        }
                        AnsiConsole.Write(discountTable);
                        break;
                    case "Return to Main Menu":
                        Console.Clear();
                        IsManagerRunning = false;
                        break;
                }
            }
        }
    }
}
