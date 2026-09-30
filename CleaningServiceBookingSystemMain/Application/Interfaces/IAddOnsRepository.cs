using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IAddOnsRepository
    {
        IList<AddOns> GetAddOns(); //gets list of all Add Ons from storage
        AddOns GetAddOnByAddOnId(string Id);
    }
}
