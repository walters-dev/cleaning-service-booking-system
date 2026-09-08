using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;
using CleaningServiceBookingSystem.Application.Validators;

namespace CleaningServiceBookingSystemMain.Application
{
    public class AddOnInput
    {
        private readonly IAddOnsRepository _addOnsRepository;

        private readonly BookingValidator _validator =
            new BookingValidator();

        public AddOnInput(IAddOnsRepository addOnsRepository)
        {
            _addOnsRepository = addOnsRepository;
        }

        public AddOns? GetAddOnInput()
        {
            IList<AddOns> addOns =
                _addOnsRepository.GetAddOns();

            Console.WriteLine();
            Console.WriteLine("===== ADD-ONS =====");
            Console.WriteLine("0. No Add-On");
            int i;
            for (i = 0; i < addOns.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. " +
                    $"{addOns[i].AddOnsName} " +
                    $"- R{addOns[i].Rate}");
            }

            while (true)
            {
                Console.Write("Choose an add-on: "); //this needs to loop until all addons added or user specifies that they dont want to add more

                if (!int.TryParse(Console.ReadLine(),out int choice) || choice > i)
                {
                    Console.WriteLine("Please enter a valid number."); 

                    continue;
                }
                
                //bool isValid = _validator.ValidateMenuChoice(choice,0,addOns.Count,"add-on",out string errorMessage);

                //if (!isValid)
                //{
                //    Console.WriteLine(errorMessage);
                //    continue;
                //}

                //if (choice == 0)
                //{
                //    return null;
                //}

                return addOns[choice - 1];
            }
        }

        public int GetAddOnQuantity()
        {
            while (true)
            {
                Console.Write("Enter add-on quantity: ");

                if (!int.TryParse(Console.ReadLine(),out int quantity))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                //bool isValid = _validator.ValidateAddOnQuantity(quantity, out string errorMessage);

                //if (isValid)
                //{
                //    return quantity;
                //}

                //Console.WriteLine(errorMessage);
            }
        }
    }
}