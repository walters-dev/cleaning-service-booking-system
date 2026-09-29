using CleaningServiceBookingSystemMain.Application.Validators;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public class DateRangeInput
    {
        BookingValidator _validator = new BookingValidator();

        public (DateTime startDate, DateTime endDate) GetDateRangeInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== DATE RANGE =====");
                DateTime startDate = GetStartDateInput();
                DateTime endDate = GetEndDateInput();

                string errorMessage;

                bool isValid = _validator.ValidateStartEndDate(startDate, endDate, out errorMessage);

                if (isValid)
                {
                    return(startDate, endDate);
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Error: {errorMessage}[/]");
            }
        }
        public DateTime GetEndDateInput()
        {
            DateTime EndDate;

            while (true)
            {
                Console.WriteLine("Enter end date: ");

                if (DateTime.TryParse(Console.ReadLine(), out EndDate))
                {
                    return EndDate;
                }

                AnsiConsole.MarkupLine("[red]Please Enter A Valid Date[/]");
            }
        }
        public DateTime GetStartDateInput()
        {
            DateTime startDate;

            while (true)
            {
                Console.WriteLine("Enter Start Date: ");

                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    return startDate;
                }

                AnsiConsole.MarkupLine("[red]Please Enter a Valid date[/]");

            }
        }
    }
}
