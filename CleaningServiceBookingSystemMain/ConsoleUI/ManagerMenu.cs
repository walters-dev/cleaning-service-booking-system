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
                    .AddChoices("Bookings", "Summaries", "Trends", "Change user"));
                InMemoryRepositoryBookings inMemoryRepositoryBookings = new InMemoryRepositoryBookings();
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
                                inMemoryRepositoryBookings.BookingHistory("");//need email input.................................
                                break;
                            case "Booking List By Range":
                                inMemoryRepositoryBookings.ListByRange("", "");//need input.................................................
                                break;
                            case "Bookings Order By House Type":
                                inMemoryRepositoryBookings.BookingsByHouseType();
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
                    case "Change user":
                        IsManagerRunning = false;
                        break;
                }
            }
        }
    }
}
