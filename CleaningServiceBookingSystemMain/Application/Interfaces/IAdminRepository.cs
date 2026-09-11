using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application.Interfaces
{
    public interface IAdminRepository
    {
        void Add(Admins admins);        //Adds an admin to storage
        public string GetAdminPasswordByUsername(string userName); //Gets an admin from stoarge where Username matches
    }
}
