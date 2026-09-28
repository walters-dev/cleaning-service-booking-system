using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Application.Validators;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Services;


namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class UpdateInput
    {
      private readonly HouseTypeInput _houseTypeInput;
      private readonly ServiceTypeInput _serviceTypeInput;

      private readonly BookingValidator _validator = new BookingValidator();

        public UpdateInput(HouseTypeInput houseTypeInput, ServiceTypeInput serviceTypeInput)
        {
            _houseTypeInput = houseTypeInput;
            _serviceTypeInput = serviceTypeInput;
        }

        public Bookings GetUpdateInput(Bookings singleBooking, out IList<AddOnSelection>? addOnSelections)
        {
            while (true)
            {
                HouseTypes selectedHouseType = new HouseTypes();
                IHouseTypesRepository houseTypesRepository = new InMemoryRepositoryHouseTypes();
                HouseTypeService houseTypeService = new HouseTypeService(houseTypesRepository);
                IAddOnsRepository addOnsRepository = new InMemoryRepositoryAddOns();
                AddOnsService addOnsService = new AddOnsService(addOnsRepository);
                var UpdateChoices = AnsiConsole.Prompt
                    (new MultiSelectionPrompt<string>()
                    .Title("Choose what you want to update: ")
                    .InstructionsText("{grey](Press[blue]<space>[/] to select and [green]<enter>[/] when finished[/]")
                    .AddChoices("Booking Date",
                                "Number Of Rooms",
                                "Carpeted Rooms",
                                "House Type",
                                "ServiceType",
                                "Is Recurring",
                                "Recurring Type",
                                "Add Ons"
                                )
                    );

                // BOOKING DATE 
                if ( UpdateChoices.Contains("Booking Date"))
                {
                    Console.Write("Enter New Booking Date: ");
                    singleBooking.BookingDate = GetDate();
                }

                // NUMBER OF ROOMS
                if (UpdateChoices.Contains("Number Of Rooms"))
                {
                    Console.Write("Enter New Number Of Rooms: ");
                    singleBooking.NumberOfRooms = GetInteger();
                }

                // CARPETED ROOMS 
                if (UpdateChoices.Contains("Carpeted Rooms"))
                {
                    Console.Write("Enter new Number of Carperted Rooms: ");
                    singleBooking.CarpetedRooms = GetInteger();
                }

                // HOUSE TYPES
                if (UpdateChoices.Contains("House Type"))
                {
                    selectedHouseType = _houseTypeInput.GetHouseTypeInput();
                    singleBooking.HouseTypeId = selectedHouseType.HouseTypeId;
                    //currentHouseType = selectedHouseType;
                }

                // SERVICE TYPE
                if (UpdateChoices.Contains("Service Type"))
                {
                    ServiceTypes selectedServiceType = _serviceTypeInput.GetServiceTypeInput();
                    singleBooking.ServiceTypeId = selectedServiceType.ServiceTypeId;
                }

                //IS RECURRING
                if (UpdateChoices.Contains("Is Recurring"))
                {
                    var recurringChoice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("Is This Booking Recurring?")
                        .AddChoices("Yes",
                                    "No"
                                    )
                        );
                    if (recurringChoice == "Yes")
                    {
                        singleBooking.IsRecurring = true;
                    }
                    else
                    {
                        singleBooking.IsRecurring = false;
                        singleBooking.RecurringBookingType = "";
                    }
                                                                                                                                         
                }

                // RECURRING TYPE 
                if (UpdateChoices.Contains("Recurring Type"))
                {
                    if (singleBooking.IsRecurring)
                    {
                        var recurringType = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("Choose recurring Type: ")
                            .AddChoices("Weekly",
                                        "Bi-Weekly",
                                        "Monthly"
                                        )
                            );
                        singleBooking.RecurringBookingType = recurringType;
                    }
                    else
                    {
                        Console.WriteLine("Recurring type cannot be changed because this booking is not recurring.");
                    }
                }

                //ADD ONS
                if (UpdateChoices.Contains("Add Ons"))
                {
                    AddOnInput addOnInput = new AddOnInput(addOnsRepository);
                    int carpetedRooms =0;
                    addOnSelections = addOnInput.GetAddOnInput(ref carpetedRooms);
                    singleBooking.CarpetedRooms = carpetedRooms;
                }
                else
                {
                    addOnSelections = null;
                }
                
                selectedHouseType = houseTypeService.FindHouseType(singleBooking.HouseTypeId);
                // VALIDATES ALL THE CHANGES MADE 
                bool isValid = _validator.ValidateBooking(singleBooking, selectedHouseType,  out string errorMessage);
                
                if (isValid)
                {
                    Console.WriteLine();
                    Console.WriteLine("Booking changes are valid. ");

                    return singleBooking;
                }

                Console.WriteLine();
                Console.WriteLine($"Validation error: {errorMessage}");
                Console.WriteLine(" Please Correct the Booking Information. ");
            }
        }

        private int GetInteger()
        {
            int number;

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Please Enter a Valid Number: ");
            }
            return number;
        }

        private DateTime GetDate()
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(),out date))
            {
                Console.Write("Please Enter a Valid Date: ");
            }
            return date;
        }
    }

    
}