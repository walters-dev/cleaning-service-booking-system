using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class AddOnsService : IAddOnsService
    {
        private readonly IAddOnsRepository _repository;
        
        public AddOnsService(IAddOnsRepository repository)//prevents the service from running without its dependency
        {
            _repository = repository;
        }
        public IList<AddOns> ViewAllAddOns()
        {
            return _repository.GetAddOns();
        }
    }
}
