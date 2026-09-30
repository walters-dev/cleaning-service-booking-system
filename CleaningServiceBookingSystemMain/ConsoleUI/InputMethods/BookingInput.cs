using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Domain.Services;
using Spectre.Console;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class BookingInput
    {
        private readonly HouseTypeInput _houseTypeInput = new HouseTypeInput();
        private readonly ServiceTypeInput _serviceTypeInput = new ServiceTypeInput();
        
        private readonly BookingValidator _validator =
            new BookingValidator();
        private readonly IBookingsRepository _bookingRepository;

        public BookingInput(IBookingsRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Bookings GetBookingInput(int carpetedRooms, IList<AddOnSelection> addOns, string phonenumber, string username, string customerId)
        {
            while (true)
            {

                Bookings booking = new Bookings();
               
                Console.WriteLine();
                Console.WriteLine("===== BOOKING INFORMATION =====");

                IBookingService bookingService = new BookingService(_bookingRepository);
                booking.BookingId = bookingService.FindBookingCount(); //gets booking count to create primary key that does not overlap

                HouseTypes houseTypes = new HouseTypes();
                houseTypes = _houseTypeInput.GetHouseTypeInput();//user chooses house type
                booking.HouseTypeId = houseTypes.HouseTypeId;       //saves the housetype id

                ServiceTypes serviceTypes = new ServiceTypes();
                serviceTypes = _serviceTypeInput.GetServiceTypeInput();//user chooses service type
                booking.ServiceTypeId = serviceTypes.ServiceTypeId;     //saves the service type id

                Console.Write("Enter number of rooms: ");
                booking.NumberOfRooms = GetInteger();

                booking.CarpetedRooms = carpetedRooms;//carpeted rooms would've been inputed by addon input

                Console.Write("Enter booking date: ");
                booking.BookingDate = GetDate();

                //checks if a booking with the same date has been made with the cutomer
                while (bookingService.FindBookingsByPhoneNumberAndDate(phonenumber, booking.BookingDate.Value).BookingId != null)
                {
                    AnsiConsole.MarkupLine("[red]Customer already has a booking on this date. Please select a new date.[/]");
                    booking.BookingDate = GetDate();
                }

                booking.IsRecurring = GetRecurringChoice();//boolean- user chooses if booking is reccuring

                if (booking.IsRecurring)
                {
                    booking.RecurringBookingType =GetRecurringType();//if it is recurring, user chooses what recurring type
                }
                else
                {
                    booking.RecurringBookingType = "";
                }
                DiscountService discountService = new DiscountService();
                PricingService pricingService = new PricingService(discountService);
                if (bookingService.FindCustomerBookingHistory(phonenumber).Count == 0)//retrieves customer history from storage, if there is no history it becomes a first time booking
                {
                    booking.FirstTimeBooking = true;
                }
                else
                {
                    booking.FirstTimeBooking = false;
                }
                string discountName;
                booking.CustomerId = customerId;
                booking.SubTotal = pricingService.CalculateSubtotal(booking, houseTypes, serviceTypes, addOns);
                booking.DiscountAmount = discountService.CalculateDiscountAmount(booking, booking.SubTotal, out discountName);
                switch (discountName)           //Gets the discount id that is in use
                {
                    case "First-Time Customer Discount":
                        booking.DiscountRuleId = "DR001";
                        break;
                    case "Recurring Booking Discount":
                        booking.DiscountRuleId = "DR003";
                        break;
                    case "Large Booking Discount":
                        booking.DiscountRuleId = "DR002";
                        break;
                    default:
                        booking.DiscountRuleId = null;
                        break;
                }
                booking.SurchargeAmount = pricingService.CalculateWeekendSurcharge(booking, (booking.SubTotal - booking.DiscountAmount));
                booking.TotalAmount = pricingService.CalculateFinalTotal(booking, houseTypes, serviceTypes, addOns);
                booking.BookingStatus = "Pending";  //pending is chosen as default
                booking.CreatedAt = DateTime.Today;
                booking.CreatedBy = username;       //username is the username of the admin that created the booking
                booking.UpdatedAt = DateTime.Today;
                booking.UpdatedBy = username;
                
                bool isValid =_validator.ValidateBooking(booking, houseTypes, out string errorMessage);

                if (isValid)
                {
                    return booking;
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Validation error: {errorMessage}[/]");

                Console.WriteLine("Please enter the booking information again.");
            }
        }

        private int GetInteger()
        {
            int number;

            while (!int.TryParse(Console.ReadLine(),out number))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid number: [/]");
            }

            return number;
        }

        private DateTime GetDate()
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(),out date))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid date: [/]");
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
                AnsiConsole.MarkupLine($"[red]Validation error: {errorMessage}[/]");
                Console.WriteLine("Please enter the booking date again.");
            }
        }
        
    }
}