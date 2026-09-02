using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IServiceTypesRepository
    {
        IList<ServiceTypes> GetServiceTypes();
        ServiceTypes GetServiceTypesById(int? Id);
    }
}
