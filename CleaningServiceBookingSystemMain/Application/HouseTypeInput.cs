using System;
using System.Collections.Generic;
using System.Text;

using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class HouseTypeInput
    {
        public HouseType GetHouseTypeInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== HOUSE TYPE =====");

                Console.WriteLine("1. Apartment");
                Console.WriteLine("2. House");
                Console.WriteLine("3. Villa");

                Console.Write("Choose a house type: ");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                if (choice == 1)
                {
                    return new HouseType
                    {
                        HouseTypeId = "HT001",
                        HouseName = "Apartment",
                        BaseRate = 350,
                        RatePerRoom = 80,
                        MinRooms = 1,
                        MaxRooms = 4,
                        IsActive = true
                    };
                }
                else if (choice == 2)
                {
                    return new HouseType
                    {
                        HouseTypeId = "HT002",
                        HouseName = "House",
                        BaseRate = 500,
                        RatePerRoom = 100,
                        MinRooms = 2,
                        MaxRooms = 8,
                        IsActive = true
                    };
                }
                else if (choice == 3)
                {
                    return new HouseType
                    {
                        HouseTypeId = "HT003",
                        HouseName = "Villa",
                        BaseRate = 800,
                        RatePerRoom = 120,
                        MinRooms = 4,
                        MaxRooms = 15,
                        IsActive = true
                    };
                }
                else
                {
                    Console.WriteLine(
                        "Please choose option 1, 2 or 3.");
                }
            }
        }
    }
}
