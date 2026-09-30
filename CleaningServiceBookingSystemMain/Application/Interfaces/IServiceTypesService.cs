using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IServiceTypesService
    {
        IList<ServiceTypes> ViewAllServiceTypes();//returns all service types
        ServiceTypes FindServiceType(string? serviceTypeId);//finds 1 service type
    }
}
