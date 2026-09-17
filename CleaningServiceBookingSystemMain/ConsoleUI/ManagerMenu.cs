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
            while (IsManagerRunning == true)
            {
                var managerChoices = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose menu option:")
                    .AddChoices("Bookings", "Summaries", "Trends", "Return to Main Menu"));
                InMemoryRepositoryBookings inMemoryRepositoryBookings = new InMemoryRepositoryBookings();//................................................................................................................................
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
                                Console.WriteLine($"Customer name\tHouse type\tService type\tBooking Date\tNumber of rooms\tTotal amount\tBooking status");
                                IList<CustomerBookingHistory> bookingsHistory = inMemoryRepositoryBookings.BookingHistory("");//need email input.................................
                                foreach (var booking in bookingsHistory)
                                {
                                    Console.WriteLine($"{booking.Fullname}\t{booking.HouseName}\t{booking.ServiceName}\t{booking.BookingDate}\t{booking.NumberOfRooms}\t{booking.TotalAmount}\t{booking.BookingStatus}");

                                }
                                break;
                            case "Booking List By Range":
                                Console.WriteLine($"Customer name\tHouse type\tService type\tBooking Date\tNumber of rooms\tTotal amount\tBooking status");
                                IList<BookingByDate> bookingsByDate = inMemoryRepositoryBookings.ListByRange(DateTime.Parse(Console.ReadLine()), DateTime.Parse(Console.ReadLine()));//need input.................................................
                                foreach (var booking in bookingsByDate)
                                {
                                    Console.WriteLine($"{booking.Fullname}\t{booking.HouseName}\t{booking.ServiceName}\t{booking.BookingDate}\t{booking.NumberOfRooms}\t{booking.TotalAmount}\t{booking.BookingStatus}");
                                }
                                break;
                            case "Bookings Order By House Type":
                                var chart = new BarChart();
                                chart.Label("Bookings Order By House Type");
                                IList<BookingByHouseType> bookingsByHouses = inMemoryRepositoryBookings.BookingsByHouseType();
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
                        inMemoryRepositoryBookings.RevenueSummary();
                        break;
                    case "Trends":
                        AnsiConsole.MarkupLine("[green]Trends selected[/]");
                        inMemoryRepositoryBookings.DiscountUsage();
                        break;
                    case "Return to Main Menu":
                        IsManagerRunning = false;
                        break;
                }
            }
        }
    }
}
