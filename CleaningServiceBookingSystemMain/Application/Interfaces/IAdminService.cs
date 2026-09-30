using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IAdminService
    {
        void RegisterAdmin(Admins admin);
        Admins FindAdminPassword(string? userName);
        string FindAdminCount();
    }
}
