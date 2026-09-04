using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IHouseTypesRepository
    {
        IList<HouseTypes> GetHouseTypes();      //gets list of all House Types from storage
        HouseTypes GetHouseTypesById(string? Id);  //gets a specific House Type from storage
    }
}
