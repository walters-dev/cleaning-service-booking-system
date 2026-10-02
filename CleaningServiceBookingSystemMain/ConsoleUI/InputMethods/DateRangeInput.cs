using CleaningServiceBookingSystemMain.Application.Validators;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public class DateRangeInput
    {
        BookingValidator _validator;
        public DateRangeInput(BookingValidator validator)
        {
            _validator = validator;
        }
        public (DateTime startDate, DateTime endDate) GetDateRangeInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== DATE RANGE ====="); // Captures The Input From the User For the Start And End Dates
                DateTime startDate = GetStartDateInput();
                DateTime endDate = GetEndDateInput();

                string errorMessage;

                bool isValid = _validator.ValidateStartEndDate(startDate, endDate, out errorMessage); // Validates if the Dates are valid 

                if (isValid)
                {
                    return(startDate, endDate);
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Error: {errorMessage}[/]"); // Outputs an Error Message If either of the dates are incorrect
            }
        }
        public DateTime GetEndDateInput() //Gets the End Date Input method
        {
            DateTime EndDate;

            while (true) // Valids if the user input of the date time data type
            {
                Console.WriteLine("Enter end date: ");

                if (DateTime.TryParse(Console.ReadLine(), out EndDate))
                {
                    return EndDate;
                }

                AnsiConsole.MarkupLine("[red]Please Enter A Valid Date[/]"); // Outputs an error message if the data type or format is incorrect
            }
        }
        public DateTime GetStartDateInput() //Gets the Start Date Input method
        {
            DateTime startDate;

            while (true) // Valids if the user input of the date time data type
            {
                Console.WriteLine("Enter Start Date: ");

                if (DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    return startDate;
                }

                AnsiConsole.MarkupLine("[red]Please Enter a Valid date[/]"); // Outputs an error message if the data type or format is incorrect
            }

        }
        }
    }

