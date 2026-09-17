using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Application.Validators;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystemMain.Infrastructure;
using CleaningServiceBookingSystemMain.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.ConsoleUI.InputMethods
{
    public class AddOnInput
    {
        private readonly IAddOnsRepository _addOnsRepository = new InMemoryRepositoryAddOns();

        private readonly BookingValidator _validator;

        //public AddOnInput(IAddOnsRepository addOnsRepository)
        //{
        //    _addOnsRepository = addOnsRepository;
        //}

        public IList<AddOnSelection>? GetAddOnInput(out int carpetedRooms)
        {
            AddOnsService service = new AddOnsService(_addOnsRepository);
            IList<AddOns> addOns = service.ViewAllAddOns();
            List<AddOnSelection> selectedAddOns = new List<AddOnSelection>();
            while (true)
            {
                carpetedRooms = 0;
                Console.WriteLine();
                Console.WriteLine("===== ADD-ONS =====");

                Console.WriteLine("0. Finish selecting add-ons");

                int i;
                for (i = 0; i < addOns.Count; i++)
                {
                    Console.WriteLine(
                        $"{i + 1}. " +
                        $"{addOns[i].AddOnsName} " +
                        $"- R{addOns[i].Rate}");
                }


                Console.Write("Choose an add-on: ");

                int choice;


                if (!int.TryParse(Console.ReadLine(), out choice) || choice > i)
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }


                // Finish selecting
                if (choice == 0)
                {
                    break;
                }


                // Check menu option
                if (choice < 1 || choice > addOns.Count)
                {
                    Console.WriteLine("Please choose an option from the list.");
                    continue;
                }


                AddOns selectedAddOn = addOns[choice - 1];


                // Prevent duplicate add-ons
                bool alreadySelected = selectedAddOns.Any(x => x.AddOn.AddOnId == selectedAddOn.AddOnId);


                if (alreadySelected)
                {
                    Console.WriteLine("You already selected this add-on.");

                    continue;
                }


                int quantity = 1;


                // AD002 = Carpet Cleaning
                if (selectedAddOn.AddOnId == "AD002")
                {
                    while (true)
                    {
                        Console.Write("Enter number of carpeted rooms: ");

                        //int carpetedRooms;


                        if (!int.TryParse(Console.ReadLine(), out carpetedRooms))
                        {
                            Console.WriteLine("Please enter a valid number.");

                            continue;
                        }

                        //break;
                        // Put value into Booking
                        //bookings.CarpetedRooms = carpetedRooms; //need to get this as a parameter first--------------------------------------------------------------------------


                        // CALL BOOKING VALIDATOR
                        string errorMessage;

                       // bool isValid = _validator.ValidateCarpetedRooms(booking,out errorMessage);


                        //if (isValid)
                        //{
                        //    quantity = booking.CarpetedRooms;

                        //    break;
                        //}


                        //Console.WriteLine($"Error: {errorMessage}");
                    }
                }


                AddOnSelection selection =  
                   new AddOnSelection
                   {
                       AddOn = selectedAddOn,
                       Quantity = quantity
                   };

                selectedAddOns.Add(selection);
                Console.WriteLine($"{selectedAddOn.AddOnsName} added.");
            }
            return selectedAddOns;
        }
    }
}