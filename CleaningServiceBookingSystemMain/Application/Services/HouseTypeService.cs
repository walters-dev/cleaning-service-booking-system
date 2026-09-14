using CleaningServiceBookingSystemMain.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class HouseTypeService
    {
        private readonly IHouseTypesRepository _houseTypesRepository;
        public HouseTypeService(IHouseTypesRepository repository)
        {
            _houseTypesRepository = repository;
        }
        public IList<HouseTypes> ViewAllHouseTypes()
        {
            return _houseTypesRepository.GetHouseTypes();
        }
        public HouseTypes FindHouseType(string? houseTypeId)
        {
            return _houseTypesRepository.GetHouseTypesById(houseTypeId);
        }
    }
}
