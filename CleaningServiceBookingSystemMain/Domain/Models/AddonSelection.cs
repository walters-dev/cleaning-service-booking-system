using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Domain.Models
{
    public class AddOnSelection
    {
        public AddOns AddOn { get; set; } = new AddOns();

        public int Quantity { get; set; }
    }
}
