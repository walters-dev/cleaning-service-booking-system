using CleaningServiceBookingSystemMain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleaningServiceBookingSystemMain.Application
{
    public interface IAdminRepository
    {
        IList<Admins> GetAdmins();      //gets list of all Admins from storage
        Admins GetAdminsById(int? Id);  //gets a specific Admin from storage
        void Add(Admins admins);        //Adds an admin to storage
        void Update(Admins admins);     //edits an already existing admin record to storage
        void Delete(Admins admins);     //deletes an admin from storage
    }
}
