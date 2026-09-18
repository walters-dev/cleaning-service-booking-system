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
                                Console.WriteLine($"Customer name\tHouse type\tService type\tBooking Date\tNumber of rooms\tTotal amount\tBooking status");
                                IList<BookingByDate> bookingsByDate = bookingService.FindAllBookingsInDateRange(DateTime.Parse(Console.ReadLine()), DateTime.Parse(Console.ReadLine()));//need input.................................................
                                foreach (var booking in bookingsByDate)
                                {
                                    Console.WriteLine($"{booking.Fullname}\t{booking.HouseName}\t{booking.ServiceName}\t{booking.BookingDate}\t{booking.NumberOfRooms}\t{booking.TotalAmount}\t{booking.BookingStatus}");
                                }
                                break;
                            case "Bookings Order By House Type":
                                var chart = new BarChart();
                                chart.Label("Bookings Order By House Type");
                                IList<BookingByHouseType> bookingsByHouses = bookingService.ViewBookingsByHouse();
                                foreach (var booking in bookingsByHouses)
                                {
                                    chart.AddItem(booking.HouseName, booking.BookingCount);
                                }
                                AnsiConsole.Write(chart);
                                break;
                        }
                        break;
                    case "Summaries":
                        AnsiConsole.MarkupLine("[green]Summaries selected[/]");
                        bookingService.ViewRevenueSummary();
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
