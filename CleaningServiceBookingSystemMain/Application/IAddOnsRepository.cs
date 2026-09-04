using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IAddOnsRepository
    {
        IList<AddOns> GetAddOns(); //gets list of all Add Ons from storage
        AddOns AddOnsByID(string? Id);//gets a specific Add On from storage
    }
}
