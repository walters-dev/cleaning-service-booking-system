using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IServiceTypesRepository
    {
        IList<ServiceTypes> GetServiceTypes();      //gets list of all Service Types from storage
        ServiceTypes GetServiceTypesById(int? Id);  //gets a specific Service Type from storage
    }
}
