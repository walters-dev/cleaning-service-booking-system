using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Services;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Domain.Services;
using CleaningServiceBookingSystemMain.Infrastructure;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;


namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class UpdateInput // Collect the user's choices and apply changes to an existing booking.
    {
      private readonly HouseTypeInput _houseTypeInput; // Store the input helpers. readonly prevents these fields from being reassigned after construction.
        private readonly ServiceTypeInput _serviceTypeInput;

        private readonly BookingValidator _validator = new BookingValidator(); // Create the validator used to check whether the updated booking is valid.
        private readonly IHouseTypeService _houseTypeService; // Store the services used to find house-type and service-type details.
        private readonly IServiceTypesService _serviceTypesService;
        private readonly IAddOnsService _addOnsService; // Store the supplied add-on service. This field is currently unused in this class.
        private readonly IBookingAddOnService _bookingAddOnService;

        public UpdateInput(HouseTypeInput houseTypeInput, ServiceTypeInput serviceTypeInput, IHouseTypeService houseTypeService, IServiceTypesService serviceTypesService, IAddOnsService addOnsService, IBookingAddOnService bookingAddOnService) // The constructor receives the helper and service objects needed by this class.
        {
            _houseTypeInput = houseTypeInput; // Save the supplied objects in fields so the other methods can use them.
            _serviceTypeInput = serviceTypeInput;
            _houseTypeService = houseTypeService;
            _serviceTypesService = serviceTypesService;
            _addOnsService = addOnsService;
            _bookingAddOnService = bookingAddOnService;
        }

        public Bookings GetUpdateInput(Bookings singleBooking, out IList<AddOnSelection> addOnSelections, string username) // Update the supplied booking. out also returns an add-on selection list, and ? allows that list to be null.
        {
            while (true) // Repeat the update process until the booking passes validation and the method returns.
            {
                HouseTypes selectedHouseType = new HouseTypes(); // Create temporary objects to hold the house type and service type during this update attempt.
                ServiceTypes selectedServiceType = new ServiceTypes();
                //IHouseTypesRepository houseTypesRepository = new RepositoryHouseTypes();
                //HouseTypeService houseTypeService = new HouseTypeService(houseTypesRepository);
                //IServiceTypesRepository serviceTypesRepository = new RepositoryServiceTypes();
                //ServiceTypesService serviceTypesService = new ServiceTypesService(serviceTypesRepository);
                IAddOnsRepository addOnsRepository = new RepositoryAddOns(); // Create the repository that will be passed to the add-on input helper.
                //AddOnsService addOnsService = new AddOnsService(addOnsRepository);

                var UpdateChoices = AnsiConsole.Prompt // Display a menu that allows multiple selections and store the selected option names.
                    (new MultiSelectionPrompt<string>()
                    .Title("Choose what you want to update: ")
                    .InstructionsText("[grey]Press[blue]<space>[/] to select and [green]<enter>[/] when finished[/]") // Explain how to select options. The square-bracket tags colour the instruction text.
                    .AddChoices("Booking Date", // These option names must exactly match the strings used in the Contains checks below.
                                "Number Of Rooms",
                                "Carpeted Rooms",
                                "House Type",
                                "Service Type",
                                "Is Recurring",
                                "Recurring Type",
                                "Add Ons"
                                )
                    );

                // BOOKING DATE // Change the booking date only if the user selected this option.
                if ( UpdateChoices.Contains("Booking Date"))
                {
                    Console.Write("Enter New Booking Date: ");
                    singleBooking.BookingDate = GetDate(); // Read a date using the helper method and assign it to the booking.
                }

                // NUMBER OF ROOMS // Change the number of rooms only if the user selected this option.
                if (UpdateChoices.Contains("Number Of Rooms"))
                {
                    Console.Write("Enter New Number Of Rooms: ");
                    singleBooking.NumberOfRooms = GetInteger(); // Read a whole number and assign it to the booking's room count.
                }

                // CARPETED ROOMS // Change the carpeted-room count only if the user selected this option. 
                if (UpdateChoices.Contains("Carpeted Rooms"))
                {
                    Console.Write("Enter new Number of Carperted Rooms: ");
                    singleBooking.CarpetedRooms = GetInteger();
                }

                // HOUSE TYPES // Allow the user to select a different house type.
                if (UpdateChoices.Contains("House Type"))
                {
                    selectedHouseType = _houseTypeInput.GetHouseTypeInput(); // Use the house-type input helper to obtain the selected house-type object.
                    singleBooking.HouseTypeId = selectedHouseType.HouseTypeId; // Copy the selected house type's ID into the booking.
                    //currentHouseType = selectedHouseType;
                }

                // SERVICE TYPE // Allow the user to select a different service type.
                if (UpdateChoices.Contains("Service Type"))
                {
                    selectedServiceType = _serviceTypeInput.GetServiceTypeInput(); // Obtain the selected service-type object and copy its ID into the booking.
                    singleBooking.ServiceTypeId = selectedServiceType.ServiceTypeId;
                }

                //IS RECURRING// Allow the user to change whether the booking repeats.
                if (UpdateChoices.Contains("Is Recurring"))
                {
                    var recurringChoice = AnsiConsole.Prompt(new SelectionPrompt<string>() // Display a menu that allows one choice: Yes or No.
                        .Title("Is This Booking Recurring?")
                        .AddChoices("Yes",
                                    "No"
                                    )
                        );
                    if (recurringChoice == "Yes")
                    {
                        singleBooking.IsRecurring = true; // Mark the booking as recurring when the user chooses Yes.
                    }
                    else
                    {
                        singleBooking.IsRecurring = false; // Mark the booking as non-recurring and clear its previous recurring type.
                        singleBooking.RecurringBookingType = "";
                    }
                                                                                                                                         
                }

                // RECURRING TYPE // Allow the user to change how often a recurring booking repeats.
                if (UpdateChoices.Contains("Recurring Type"))
                {
                    if (singleBooking.IsRecurring) // Only allow a recurring type to be selected when IsRecurring is true.
                    {
                        var recurringType = AnsiConsole.Prompt(new SelectionPrompt<string>() // Ask the user to choose the booking's repeat frequency.
                            .Title("Choose recurring Type: ")
                            .AddChoices("Weekly",
                                        "Bi-Weekly",
                                        "Monthly"
                                        )
                            );
                        singleBooking.RecurringBookingType = recurringType; // Store the selected repeat frequency in the booking.
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Recurring type cannot be changed because this booking is not recurring.[/]"); // Display a red message explaining why a recurring type cannot be selected.
                    }
                }

                //ADD ONS // Collect new add-on selections if the user selected Add Ons.
                if (UpdateChoices.Contains("Add Ons"))
                {
                    AddOnInput addOnInput = new AddOnInput(addOnsRepository); // Create the add-on input helper using the add-ons repository.
                    int carpetedRooms =0; // Start the carpeted-room count at zero before collecting add-on input.
                    addOnSelections = addOnInput.GetAddOnInput(ref carpetedRooms); // Get the add-on list. ref also allows this method to change the carpetedRooms variable.
                    singleBooking.CarpetedRooms = carpetedRooms; // Replace the booking's carpeted-room count with the value supplied by add-on input.
                }
                else
                {
                    addOnSelections = null; // Return null for the add-on list when add-ons were not edited during this attempt.
                }

                selectedServiceType = _serviceTypesService.FindServiceType(singleBooking.ServiceTypeId); // Find the service-type and house-type details using the IDs currently stored in the booking.
                selectedHouseType = _houseTypeService.FindHouseType(singleBooking.HouseTypeId);

                DiscountService discountService = new DiscountService(); // Create the service responsible for calculating discounts.
                PricingService pricingService = new PricingService(discountService); // Create the pricing service and provide the discount service it needs.
                string discountName; // Declare a variable that will receive the discount name through an out parameter.
                if (addOnSelections == null)
                {
                    try
                    {
                        IList <BookingAddOns> bookingAddOns = _bookingAddOnService.FindBookingAddOnsByBookingId(singleBooking.BookingId);
                        foreach(var bookingAddOn in bookingAddOns)
                        {
                            AddOnSelection addOnSelection = new AddOnSelection();
                            addOnSelection.AddOn.AddOnId = bookingAddOn.AddOnId;
                            addOnSelection.AddOn.Rate = _addOnsService.FindAddOn(bookingAddOn.AddOnId).Rate;
                            addOnSelections.Add(addOnSelection);
                        }
                    }
                    catch
                    {
                        addOnSelections = new List<AddOnSelection>();
                    }
                }
                singleBooking.SubTotal = pricingService.CalculateSubtotal(singleBooking, selectedHouseType, selectedServiceType, addOnSelections); // Recalculate the subtotal using the updated booking details and supplied add-on list.
                singleBooking.DiscountAmount = discountService.CalculateDiscountAmount(singleBooking, singleBooking.SubTotal, out discountName); // Calculate the discount amount and receive the discount name through out.
                switch (discountName) // Match the discount name to the discount-rule ID stored in the booking.
                {
                    case "First-Time Customer Discount":
                        singleBooking.DiscountRuleId = "DR001";
                        break;
                    case "Recurring Booking Discount":
                        singleBooking.DiscountRuleId = "DR003";
                        break;
                    case "Large Booking Discount":
                        singleBooking.DiscountRuleId = "DR002";
                        break;
                    default:
                        singleBooking.DiscountRuleId = null; // Clear the discount-rule ID if none of the listed discount names match.
                        break;
                }
                singleBooking.SurchargeAmount = pricingService.CalculateWeekendSurcharge(singleBooking, (singleBooking.SubTotal - singleBooking.DiscountAmount)); // Calculate the weekend surcharge using the subtotal after subtracting the discount.
                singleBooking.TotalAmount = pricingService.CalculateFinalTotal(singleBooking, selectedHouseType, selectedServiceType, addOnSelections); // Ask the pricing service to recalculate the booking's final total.
                singleBooking.UpdatedAt = DateTime.Today; // Record today's date and the username of the person making the changes.
                singleBooking.UpdatedBy = username;
                // VALIDATES ALL THE CHANGES MADE // Validate the updated booking and receive any validation error message through out.
                bool isValid = _validator.ValidateBooking(singleBooking, selectedHouseType,  out string errorMessage);
                
                if (isValid) // If validation succeeds, display a confirmation and return the updated booking.
                {
                    Console.WriteLine();
                    Console.WriteLine("Booking changes are valid. ");

                    return singleBooking; // Return the updated booking object to the caller and end this method.
                }

                Console.WriteLine();
                AnsiConsole.MarkupLine($"[red]Validation error: {errorMessage}[/]"); // Display the validation error in red. The outer loop then allows another update attempt.
                Console.WriteLine(" Please Correct the Booking Information. ");
            }
        }

        private int GetInteger() // A private helper used by this class to read a whole number from the console.
        {
            int number;

            while (!int.TryParse(Console.ReadLine(), out number)) // TryParse stores the converted number through out. ! repeats the loop when conversion fails.
            {
                AnsiConsole.MarkupLine("[red]Please Enter a Valid Number: [/]");
            }
            return number; // Return the converted number. This helper accepts zero and negative numbers too.
        }

        private DateTime GetDate() // A private helper used by this class to read a date from the console.
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(),out date)) // Keep asking until TryParse successfully converts the entered text into a DateTime value.
            {
                AnsiConsole.MarkupLine("[red]Please Enter a Valid Date: [/]");
            }
            return date; // Return the successfully converted date to the calling code.
        }
    }

    
}