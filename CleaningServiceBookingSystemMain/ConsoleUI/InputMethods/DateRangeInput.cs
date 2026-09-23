using CleaningServiceBookingSystemMain.Application.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public class DateRangeInput
    {
        //private readonly StartDateInput _startDateInput;
        //private readonly EndDateInput _endDateInput;
        BookingValidator _validator = new BookingValidator();

        //public DateRangeInput(StartDateInput startDateInput, 
        //                      EndDateInput endDateInput,
        //                      BookingValidator validator)
        //{
        //    _startDateInput = startDateInput;
        //    _endDateInput = endDateInput;
        //    _validator = validator;
        //}

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
                Console.WriteLine($"Error: {errorMessage}");
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

                Console.WriteLine("Please Enter A Valid Date");
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

                Console.WriteLine("Please Enter a Valid date");

            }
        }
    }
}
