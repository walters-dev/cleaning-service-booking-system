using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystem.Domain
{
    public class HouseType
    {
        public string HouseTypeId { get; set; } = "";

        public string HouseName { get; set; } = "";

        public decimal BaseRate { get; set; }

        public decimal RatePerRoom { get; set; }

        public int MinRooms { get; set; }

        public int MaxRooms { get; set; }

        public bool IsActive { get; set; }
    }
}