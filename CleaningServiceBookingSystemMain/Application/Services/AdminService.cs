using System;
using System.Collections.Generic;
using System.Text;
using CleaningServiceBookingSystemMain.Application.Interfaces;
using CleaningServiceBookingSystemMain.Domain.Models;

namespace CleaningServiceBookingSystemMain.Application.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository repository)
        {
            _adminRepository = repository;
        }
        public void RegisterAdmin(Admins admin)
        {
            _adminRepository.Add(admin);
        }
        public string FindAdminPassword(string? userName)
        {
            return _adminRepository.GetAdminPasswordByUsername(userName);
        }
        public string FindAdminCount()
        {
            return _adminRepository.AdminRowCount();
        }
    }
}
