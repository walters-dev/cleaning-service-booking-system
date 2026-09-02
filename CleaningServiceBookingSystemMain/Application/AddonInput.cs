using System;
using System.Collections.Generic;
using System.Text;

using CleaningServiceBookingSystem.Domain;

namespace CleaningServiceBookingSystem.Application
{
    public class AddonInput
    {
        public Addon? GetAddonInput()
        {
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("==== ADD-ONS ====");
                Console.WriteLine();
                Console.WriteLine("1. Laundry Add-On");
                Console.WriteLine("2. Window Cleaning");
                Console.WriteLine("3. Carpet Cleaning");
                Console.WriteLine("0. No Add-On");

                Console.Write("Choose an Add-On: ");

                int Choice;
            }
        }
    }
}