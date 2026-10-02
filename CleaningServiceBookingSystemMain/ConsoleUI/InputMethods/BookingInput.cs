using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Domain.Services;
using Spectre.Console;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class BookingInput // Collect the information needed to create a new booking object.
    {
        private readonly HouseTypeInput _houseTypeInput ; // store the house-type and service-type input helpers. readonly prevents reassignment after construction.
        private readonly ServiceTypeInput _serviceTypeInput;
        
        private readonly BookingValidator _validator;// Store the validator class supplied through the constructor.
        private readonly IBookingsRepository _bookingRepository; // Store the booking repository supplied through the constructor.

        public BookingInput(IBookingsRepository bookingRepository, BookingValidator validator, HouseTypeInput houseTypeInput, ServiceTypeInput serviceTypeInput) // The constructor receives the repository that the booking service will use.
        {
            _bookingRepository = bookingRepository; // Save the supplied repository in a field so this class can use it later.
            _validator = validator;
            _houseTypeInput = houseTypeInput;
            _serviceTypeInput = serviceTypeInput;
        }

        public Bookings GetBookingInput(int carpetedRooms, IList<AddOnSelection> addOns, string phonenumber, string username, string customerId) // Collect and return a valid booking using the supplied carpeted-room count, add-ons, phone number, username and customer ID.
        {
            while (true) // Repeat the input process when validation fails. Each attempt creates a fresh booking object.
            {

                Bookings booking = new Bookings(); // Create an empty booking object whose properties will hold the booking information.

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
                DiscountService discountService = new DiscountService();  // Create the service responsible for calculating discounts.
                PricingService pricingService = new PricingService(discountService); // Create the pricing service and provide the discount service it needs.
                if (bookingService.FindCustomerBookingHistory(phonenumber).Count == 0)//retrieves customer history from storage, if there is no history it becomes a first time booking
                {
                    booking.FirstTimeBooking = true; // Mark this as a first-time booking because the customer's booking history is empty.
                }
                else
                {
                    booking.FirstTimeBooking = false; // Mark this as a returning customer's booking because previous bookings exist.
                }
                string discountName;
                booking.CustomerId = customerId; // Link this booking object to the customer using the supplied customer ID.
                booking.SubTotal = pricingService.CalculateSubtotal(booking, houseTypes, serviceTypes, addOns); // Calculate the subtotal using the booking details, selected house and service types, and supplied add-ons.
                booking.DiscountAmount = discountService.CalculateDiscountAmount(booking, booking.SubTotal, out discountName); // Calculate the discount amount and receive the discount name through out.
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
                        booking.DiscountRuleId = null; // Leave the discount-rule ID empty if none of the listed discount names match.
                        break;
                }
                booking.SurchargeAmount = pricingService.CalculateWeekendSurcharge(booking, (booking.SubTotal - booking.DiscountAmount)); // Calculate the weekend surcharge using the subtotal after subtracting the discount.
                booking.TotalAmount = pricingService.CalculateFinalTotal(booking, houseTypes, serviceTypes, addOns); // Ask the pricing service to calculate the booking's final total.
                booking.BookingStatus = "Pending";  //pending is chosen as default
                booking.CreatedAt = DateTime.Today;
                booking.CreatedBy = username;       //username is the username of the admin that created the booking
                booking.UpdatedAt = DateTime.Today;
                booking.UpdatedBy = username;
                
                bool isValid =_validator.ValidateBooking(booking, houseTypes, out string errorMessage); // Validate the booking and selected house type. out provides any validation error message.

                if (isValid) // Return the booking only when the validator reports that it is valid.
                {
                    return booking; 
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Validation error: {errorMessage}[/]"); // Insert the error message into the text and display it in red using Spectre.Console markup.

                Console.WriteLine("Please enter the booking information again.");
            }
        }

        private int GetInteger() // A private helper used inside this class to read a whole number.
        {
            int number;

            while (!int.TryParse(Console.ReadLine(),out number))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid number: [/]");
            }

            return number;
        }

        private DateTime GetDate() // A private helper used inside this class to read a date.
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(),out date))
            {
                AnsiConsole.MarkupLine("[red]Please enter a valid date: [/]");
            }

            return date;
        }

        private bool GetRecurringChoice() // Ask whether the booking repeats and return the answer as a bool.
        {
            while (true)
            {
                var recurringChoice = AnsiConsole.Prompt(new SelectionPrompt<string>() // Display a menu that allows one choice: Yes or No.
                        .Title("Is This Booking Recurring?")
                        .AddChoices("Yes",
                                    "No"
                                    )
                        );
                if (recurringChoice == "Yes")
                {
                    return true; // Mark the booking as recurring when the user chooses Yes.
                }
                else
                {
                    Console.WriteLine("Booking is non-recurring");
                    return false; // Mark the booking as non-recurring 
                }
            }
        }

        private string GetRecurringType() // Ask how often the booking repeats and return the selected frequency as text.
        {
            while (true)
            {
                var recurringType = AnsiConsole.Prompt(new SelectionPrompt<string>() // Ask the user to choose the booking's repeat frequency.
                            .Title("Choose recurring Type: ")
                            .AddChoices("Weekly",
                                        "Bi-Weekly",
                                        "Monthly"
                                        )
                            );
                Console.WriteLine("Booking recurring type selected: "+ recurringType);
                return recurringType; // Store the selected repeat frequency in the booking.
            }
        }

        public DateTime GetSingleBookingDateInput() // A public method other classes can call to collect and validate a single booking date.
        {
            while (true)
            {
                Console.WriteLine("Enter Booking Date");

               

                string bookingDate = Console.ReadLine();





                bool isValid = _validator.ValidateSingleDateInput(bookingDate, out string errorMessage); // Validate the date text and receive any error message through out.
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