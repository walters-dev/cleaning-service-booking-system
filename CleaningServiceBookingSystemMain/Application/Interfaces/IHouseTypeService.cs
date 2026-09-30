using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IHouseTypeService
    {
        IList<HouseTypes> ViewAllHouseTypes();
        HouseTypes FindHouseType(string? houseTypeId);
    }
}
