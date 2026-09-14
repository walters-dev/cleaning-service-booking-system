using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class ServiceTypesService
    {
        private readonly IServiceTypesRepository _serviceTypesRepository;
        public ServiceTypesService(IServiceTypesRepository repository)
        {
            _serviceTypesRepository = repository;
        }
        public IList<ServiceTypes> ViewAllServiceTypes()
        {
            return _serviceTypesRepository.GetServiceTypes();
        }
        public ServiceTypes FindServiceType(string? serviceTypeId)
        {
            return _serviceTypesRepository.GetServiceTypesById(serviceTypeId);
        }
    }
}
