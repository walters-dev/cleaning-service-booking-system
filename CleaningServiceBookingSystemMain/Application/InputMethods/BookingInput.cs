using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystem.Application.Validators;

namespace CleaningServiceBookingSystemMain.Application.InputMethods
{
    public class BookingInput
    {
        private readonly HouseTypeInput _houseTypeInput;
        private readonly ServiceTypeInput _serviceTypeInput;
        private readonly DiscountInput _discountInput;

        private readonly BookingValidator _validator =
            new BookingValidator();

        //public BookingInput(
        //    HouseTypeInput houseTypeInput,
        //    ServiceTypeInput serviceTypeInput,
        //    DiscountInput discountInput)
        //{
        //    _houseTypeInput = houseTypeInput;
        //    _serviceTypeInput = serviceTypeInput;
        //    _discountInput = discountInput;
        //}

        public Bookings GetBookingInput()
        {
            while (true)
            {

                Bookings booking = new Bookings();
               
                Console.WriteLine();
                Console.WriteLine("===== BOOKING INFORMATION =====");

                HouseTypes houseTypes = new HouseTypes();
                houseTypes = _houseTypeInput.GetHouseTypeInput();
                booking.HouseTypeId = houseTypes.HouseTypeId;

                ServiceTypes serviceTypes = new ServiceTypes();
                serviceTypes = _serviceTypeInput.GetServiceTypeInput();
                booking.ServiceTypeId = serviceTypes.ServiceTypeId;



                Console.Write("Enter number of rooms: ");
                booking.NumberOfRooms = GetInteger();

                Console.Write("Enter number of carpeted rooms: ");

                booking.CarpetedRooms = GetInteger();//...............................................................

                Console.Write("Enter booking date: ");
                booking.BookingDate = GetDate();

                booking.IsRecurring = GetRecurringChoice();

                if (booking.IsRecurring)
                {
                    booking.RecurringBookingType =GetRecurringType();
                }
                else
                {
                    booking.RecurringBookingType = "";
                }

                DiscountRules discountRules = new DiscountRules();
                discountRules = _discountInput.GetDiscountInput();
                booking.DiscountRuleId = discountRules.DiscountRuleId;

                bool isValid =_validator.ValidateBooking(booking, houseTypes, out string errorMessage);

                if (isValid)
                {
                    return booking;
                }

                Console.WriteLine();
                Console.WriteLine($"Validation error: {errorMessage}");

                Console.WriteLine("Please enter the booking information again.");
            }
        }

        private int GetInteger()
        {
            int number;

            while (!int.TryParse(Console.ReadLine(),out number))
            {
                Console.Write("Please enter a valid number: ");
            }

            return number;
        }

        private DateTime GetDate()
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(),out date))
            {
                Console.Write("Please enter a valid date: ");
            }

            return date;
        }

        private bool GetRecurringChoice()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Is this a recurring booking?");

                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.Write("Choice: ");

                int choice = GetInteger();

                if (choice == 1)
                {
                    return true;
                }

                if (choice == 2)
                {
                    return false;
                }

                Console.WriteLine("Please choose 1 or 2.");
            }
        }

        private string GetRecurringType()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Select recurring type:");

                Console.WriteLine("1. Weekly");
                Console.WriteLine("2. Bi-weekly");
                Console.WriteLine("3. Monthly");
                Console.Write("Choice: ");

                int choice = GetInteger();

                if (choice == 1)
                {
                    return "Weekly";
                }

                if (choice == 2)
                {
                    return "Bi-weekly";
                }

                if (choice == 3)
                {
                    return "Monthly";
                }

                Console.WriteLine("Please choose 1, 2 or 3.");
            }
        }
    }
}