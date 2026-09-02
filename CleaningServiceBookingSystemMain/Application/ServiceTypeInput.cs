using System;
using System.Collections.Generic;
using System.Text;

using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class ServiceTypeInput
    {
        public ServiceType GetServiceTypeInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== SERVICE TYPE =====");

                Console.WriteLine("1. Standard Cleaning");
                Console.WriteLine("2. Deep Cleaning");
                Console.WriteLine("3. Move-Out Cleaning");

                Console.Write("Choose a service type: ");

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

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
                else if (choice == 3)
                {
                    return new ServiceType
                    {
                        ServiceTypeId = "ST003",
                        ServiceName = "Move-Out Cleaning",
                        ServiceRate = 700,
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