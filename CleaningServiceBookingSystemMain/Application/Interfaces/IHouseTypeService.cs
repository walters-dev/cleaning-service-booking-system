using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IHouseTypeService
    {
        IList<HouseTypes> ViewAllHouseTypes();//returns all house types
        HouseTypes FindHouseType(string? houseTypeId);//finds 1 house type
    }
}
