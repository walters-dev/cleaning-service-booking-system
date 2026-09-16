using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Domain.Services;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class BookingInput
    {
        private readonly HouseTypeInput _houseTypeInput;
        private readonly ServiceTypeInput _serviceTypeInput;
        private readonly DiscountInput _discountInput;

        private readonly BookingValidator _validator =
            new BookingValidator();
        private readonly IBookingsRepository _bookingRepository = new InMemoryRepositoryBookings();

        //public BookingInput(
        //    HouseTypeInput houseTypeInput,
        //    ServiceTypeInput serviceTypeInput,
        //    DiscountInput discountInput)
        //{
        //    _houseTypeInput = houseTypeInput;
        //    _serviceTypeInput = serviceTypeInput;
        //    _discountInput = discountInput;
        //}

        public Bookings GetBookingInput(int carpetedRooms, IList<AddOnSelection> addOns, string email, string username)
        {
            while (true)
            {

                Bookings booking = new Bookings();
               
                Console.WriteLine();
                Console.WriteLine("===== BOOKING INFORMATION =====");

                BookingService bookingService = new BookingService(_bookingRepository);
                booking.BookingId = bookingService.FindBookingCount(); 

                HouseTypes houseTypes = new HouseTypes();
                houseTypes = _houseTypeInput.GetHouseTypeInput();
                booking.HouseTypeId = houseTypes.HouseTypeId;

                ServiceTypes serviceTypes = new ServiceTypes();
                serviceTypes = _serviceTypeInput.GetServiceTypeInput();
                booking.ServiceTypeId = serviceTypes.ServiceTypeId;



                Console.Write("Enter number of rooms: ");
                booking.NumberOfRooms = GetInteger();

                //Console.Write("Enter number of carpeted rooms: ");

                booking.CarpetedRooms = carpetedRooms;//...............................................................

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

                //DiscountRules discountRules = new DiscountRules();
                //discountRules = _discountInput.GetDiscountInput();
                //booking.DiscountRuleId = discountRules.DiscountRuleId;
                DiscountService discountService = new DiscountService();
                PricingService pricingService = new PricingService();
                if (bookingService.FindCustomerBookingHistory(email) == null)
                {
                    booking.FirstTimeBooking = true;
                }
                else
                {
                    booking.FirstTimeBooking = false;
                }
                booking.SubTotal = pricingService.CalculateSubtotal(booking, houseTypes, serviceTypes, addOns);
                booking.DiscountAmount = discountService.CalculateDiscountAmount(booking, booking.SubTotal);
                booking.SurchargeAmount = pricingService.CalculateWeekendSurcharge(booking, (booking.SubTotal - booking.DiscountAmount));
                booking.TotalAmount = pricingService.CalculateFinalTotal(booking, houseTypes, serviceTypes, addOns);
                booking.BookingStatus = "Pending";
                booking.CreatedAt = DateTime.Today;
                booking.CreatedBy = username;
                booking.UpdatedAt = DateTime.Today;
                booking.UpdatedBy = username;
                
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

        public DateTime GetSingleBookingDateInput()
        {
            while (true)
            {
                Console.WriteLine("Enter Booking Date");

               

                string bookingDate = Console.ReadLine();





                bool isValid = _validator.ValidateSingleDateInput(bookingDate, out string errorMessage);
                if (isValid)
                {
                    DateTime BookingDate = DateTime.Parse(bookingDate);
                    return BookingDate;
                }

                Console.WriteLine();
                Console.WriteLine($"Validation error:{errorMessage}");
                Console.WriteLine("Please enter the booking date again.");
            }
        }
    }
}