using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IAddOnsService
    {
        IList<AddOns> ViewAllAddOns();//returns all add ons
        AddOns FindAddOn(string Id);
    }
}
