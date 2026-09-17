using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class AddOnsService
    {
        private readonly IAddOnsRepository _repository;
        public AddOnsService(IAddOnsRepository repository)
        {
            _repository = repository;
        }
        public IList<AddOns> ViewAllAddOns()
        {
            return _repository.GetAddOns();
        }
        public AddOns FindAddOn(string addOnsId)
        {
            return _repository.AddOnsByID(addOnsId);
        }
    }
}
