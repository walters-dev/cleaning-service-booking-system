using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository repository)//prevents the service from running without its dependency
        {
            _adminRepository = repository;
        }
        public void RegisterAdmin(Admins admin)
        {
            _adminRepository.Add(admin);
        }
        public Admins FindAdminPassword(string? userName)
        {
            return _adminRepository.GetAdminPasswordByUsername(userName);
        }
        public string FindAdminCount()
        {
            return _adminRepository.AdminRowCount();
        }
    }
}
