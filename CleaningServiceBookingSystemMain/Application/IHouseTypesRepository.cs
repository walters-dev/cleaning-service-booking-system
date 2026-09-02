using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IHouseTypesRepository
    {
        IList<HouseTypes> GetHouseTypes();
        HouseTypes GetHouseTypesById(int? Id);
    }
}
