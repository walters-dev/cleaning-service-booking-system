using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IServiceTypesService
    {
        IList<ServiceTypes> ViewAllServiceTypes();
        ServiceTypes FindServiceType(string? serviceTypeId);
    }
}
