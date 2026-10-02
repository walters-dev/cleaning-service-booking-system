using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class HouseTypes
    {
        public string HouseTypeId { get; set; }
        public string Name { get; set; }
        public decimal BaseRate { get; set; }
        public decimal RatePerRoom { get; set; }
        public int MinRooms { get; set; }
        public int MaxRooms { get; set; }
    }
}
