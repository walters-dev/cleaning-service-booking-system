using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IAdminService
    {
        void RegisterAdmin(Admins admin);//register admin in system
        Admins FindAdminPassword(string? userName);//finds 1 admin via username
        string FindAdminCount();//finds count of admins in system
    }
}
