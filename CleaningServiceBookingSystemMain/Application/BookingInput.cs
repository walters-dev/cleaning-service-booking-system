using System;
using System.Collections.Generic;
using System.Text;

using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class BookingInput
    {
        public Booking GetBookingInput()
        {
            Booking booking = new Booking();

            Console.WriteLine();
            Console.WriteLine("===== BOOKING INFORMATION =====");

            booking.HouseType = GetHouseType();

            booking.ServiceType = GetServiceType();

            Console.Write("Enter number of rooms: ");
            booking.NumberOfRooms = GetInteger();

            Console.Write("Enter number of carpeted rooms: ");
            booking.CarpetedRooms = GetInteger();

            Console.Write("Enter booking date (yyyy-MM-dd): ");
            booking.BookingDate = GetDate();

            booking.IsRecurring = GetRecurringChoice();

            if (booking.IsRecurring)
            {
                booking.RecurringBookingType = GetRecurringType();
            }
            else
            {
                booking.RecurringBookingType = "";
            }

            return booking;
        }


        
        

        private int GetInteger()
        {
            int number;

            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write(
                    "Invalid number. Please try again: ");
            }

            return number;
        }


        private DateTime GetDate()
        {
            DateTime date;

            while (!DateTime.TryParse(
                Console.ReadLine(), out date))
            {
                Console.Write(
                    "Invalid date. Please try again: ");
            }

            return date;
        }

        private bool GetRecurringChoice()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Is this a recurring booking?");

                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");

                Console.Write("Choice: ");

                int choice = GetInteger();

                if (choice == 1)
                {
                    return true;
                }
                else if (choice == 2)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine(
                        "Please select 1 or 2.");
                }
            }
        }


        private string GetRecurringType()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Select recurring booking type:");

                Console.WriteLine("1. Weekly");
                Console.WriteLine("2. Bi-weekly");
                Console.WriteLine("3. Monthly");

                Console.Write("Choice: ");

                int choice = GetInteger();

                if (choice == 1)
                {
                    return "Weekly";
                }
                else if (choice == 2)
                {
                    return "Bi-weekly";
                }
                else if (choice == 3)
                {
                    return "Monthly";
                }
                else
                {
                    Console.WriteLine(
                        "Please select 1, 2 or 3.");
                }
            }
        }


        private HouseType GetHouseType()
        {
            Console.WriteLine();
            Console.WriteLine("Select house type:");
            Console.WriteLine("1. Apartment");
            Console.WriteLine("2. House");
            Console.WriteLine("3. Villa");

            Console.Write("Choice: ");

            int choice = GetInteger();

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
            else
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
        }


        private ServiceType GetServiceType()
        {
            Console.WriteLine();
            Console.WriteLine("Select service type:");
            Console.WriteLine("1. Standard Cleaning");
            Console.WriteLine("2. Deep Cleaning");
            Console.WriteLine("3. Move-Out Cleaning");

            Console.Write("Choice: ");

            int choice = GetInteger();

            if (choice == 1)
            {
                return new ServiceType
                {
                    ServiceTypeId = "ST001",
                    ServiceName = "Standard Cleaning",
                    ServiceRate = 300,
                    IsActive = true
                };
            }
            else if (choice == 2)
            {
                return new ServiceType
                {
                    ServiceTypeId = "ST002",
                    ServiceName = "Deep Cleaning",
                    ServiceRate = 500,
                    IsActive = true
                };
            }
            else
            {
                return new ServiceType
                {
                    ServiceTypeId = "ST003",
                    ServiceName = "Move-Out Cleaning",
                    ServiceRate = 700,
                    IsActive = true
                };
            }
        }

     