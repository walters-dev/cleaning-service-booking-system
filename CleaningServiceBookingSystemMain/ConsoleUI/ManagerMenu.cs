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
            BookingService bookingService = new BookingService(bookingsRepository);
            CustomerInput customerInput = new CustomerInput();
            DateRangeInput dateRangeInput = new DateRangeInput();
            //BookingRevenueSummary summary = new BookingRevenueSummary();
            while (IsManagerRunning == true)
            {
                var managerChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option:")
                    .AddChoices("Bookings", "Summaries", "Trends", "Return to Main Menu"));
                
                switch (managerChoices)
                {
                    case "Bookings":
                        AnsiConsole.MarkupLine("[green]Booking selected[/]");
                        var bookingChoices = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Choose booking options:")
                            .AddChoices("Customer Booking History", "Booking List By Range", "Bookings Order By House Type"));
                        switch (bookingChoices)
                        {
                            case "Customer Booking History":
                                customerInput.GetEmail();
                                Console.WriteLine($"Customer name\tHouse type\tService type\tBooking Date\tNumber of rooms\tTotal amount\tBooking status");

                                IList<CustomerBookingHistory> bookingsHistory = bookingService.FindCustomerBookingHistory("");//need phone number input.................................
                                foreach (var booking in bookingsHistory)
                                {
                                    Console.WriteLine($"{booking.Fullname}\t{booking.HouseName}\t{booking.ServiceName}\t{booking.BookingDate}\t{booking.NumberOfRooms}\t{booking.TotalAmount}\t{booking.BookingStatus}");

                                }
                                break;
                            case "Booking List By Range":
                                var date = dateRangeInput.GetDateRangeInput();
                                IList<BookingByDate> bookingsByDate = bookingService.FindAllBookingsInDateRange(date.startDate, date.endDate);//need input.................................................
                                var table = new Table();
                                //table.RoundedBorder();
                                table.DoubleBorder();
                                table.ShowRowSeparators();
                                table.AddColumn("Customer name");
                                table.AddColumn("House type");
                                table.AddColumn("Service type");
                                table.AddColumn("Booking Date");
                                table.AddColumn("Number of rooms");
                                table.AddColumn("Total amount");
                                table.AddColumn("Booking status");
                                
                                foreach (var booking in bookingsByDate)
                                {
                                    table.AddRow(booking.Fullname, booking.HouseName, booking.ServiceName, booking.BookingDate.Date.ToString(), booking.NumberOfRooms.ToString(), booking.TotalAmount.ToString(), booking.BookingStatus.ToString());
                                    //Console.WriteLine($"{booking.Fullname}\t{booking.HouseName}\t{booking.ServiceName}\t{booking.BookingDate}\t{booking.NumberOfRooms}\t{booking.TotalAmount}\t{booking.BookingStatus}");
                                }
                                AnsiConsole.Write(table);
                                break;
                            case "Bookings Order By House Type":
                                var chart = new BarChart();
                                chart.Label("Bookings Order By House Type");
                                IList<BookingByHouseType> bookingsByHouses = bookingService.ViewBookingsByHouse();
                                foreach (var booking in bookingsByHouses)
                                {
                                    if (bookingsByHouses.Count % 2 == 0)        //alternates bar colours used
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
                        AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        var chartMoney = new BreakdownChart();
                        //var chart = new BarChart();
                        IList < BookingRevenueSummary > summary = bookingService.ViewRevenueSummary();
                        chartMoney.ShowPercentage();
                        chartMoney.UseValueFormatter((value, culture) => $"R {value:N}");
                        foreach (var booking in summary)
                        {
                            chartMoney.AddItem(booking.ServiceName, decimal.ToDouble(booking.TotalRevenue), Color.Aqua);
                        }
                        AnsiConsole.Write(chartMoney);
                        break;
                    case "Trends":
                        AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        bookingService.ViewDiscountUsage();
                        break;
                    case "Return to Main Menu":
                        IsManagerRunning = false;
                        break;
                }
            }
        }
    }
}
